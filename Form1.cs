using ExcelDataReader;
using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Text;
// Add this using statement for settings:

namespace ExcelToSqlImporter // Ensure this matches your project's namespace
{
    public partial class Form1 : Form
    {
        private string selectedFilePath = string.Empty;
        private DataTable excelData = null;

        public Form1()
        {
            InitializeComponent();
            // Wire up Load and FormClosing events AFTER InitializeComponent()
            this.Load += new System.EventHandler(this.Form1_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);

            toolStripStatusLabel1.Text = "Ready";
        }

        // --- Event Handler for Form Load ---
        private void Form1_Load(object sender, EventArgs e)
        {
            // Load the last saved connection string
            txtConnectionString.Text = ConfigurationManager.AppSettings["LastConnectionString"] ?? "Server=sad31;Database=seas;Encrypt=False;User ID=savanah;Password=alterlogin;Connect Timeout=2;";

            toolStripStatusLabel1.Text = "Ready"; // Reset status on load
        }

        // --- Event Handler for Form Closing ---
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Save the current connection string before closing
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings.Remove("LastConnectionString");
            config.AppSettings.Settings.Add("LastConnectionString", txtConnectionString.Text);
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings"); // IMPORTANT: Persist the changes

        }
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
                openFileDialog.Title = "Select an Excel File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedFilePath = openFileDialog.FileName;
                    txtFilePath.Text = selectedFilePath;
                    toolStripStatusLabel1.Text = $"Selected: {Path.GetFileName(selectedFilePath)}";

                    excelData = ReadExcelFile(selectedFilePath);
                    if (excelData == null || excelData.Rows.Count == 0)
                    {
                        MessageBox.Show("The selected Excel file is empty or could not be read properly (ensure first sheet has data with headers).", "Empty/Invalid File", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        toolStripStatusLabel1.Text = "Ready";
                        this.Cursor = Cursors.Default;
                        return;
                    }
                    toolStripStatusLabel1.Text = $"Read {excelData.Rows.Count} data rows from Excel.";
                    dataGridViewResults.DataSource = null;
                    dataGridViewResults.DataSource = excelData;
                }
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            // --- Basic Validation ---
            string connectionString = txtConnectionString.Text.Trim();
            string tableName = txtTableName.Text.Trim();

            if (string.IsNullOrEmpty(connectionString))
            {
                MessageBox.Show("Please enter a SQL Server connection string.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConnectionString.Focus();
                return;
            }

            if (string.IsNullOrEmpty(tableName))
            {
                MessageBox.Show("Please enter a destination table name.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTableName.Focus();
                return;
            }
            tableName = tableName.Replace("]", "]]");

            if (string.IsNullOrEmpty(selectedFilePath) || !File.Exists(selectedFilePath))
            {
                MessageBox.Show("Please select a valid Excel file first.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnBrowse.Focus();
                return;
            }

            toolStripStatusLabel1.Text = "Reading Excel file...";
            this.Cursor = Cursors.WaitCursor; // Show wait cursor
            Application.DoEvents();

            DataTable excelData = null;
            try
            {
                excelData = ReadExcelFile(selectedFilePath);
                if (excelData == null || excelData.Rows.Count == 0)
                {
                    MessageBox.Show("The selected Excel file is empty or could not be read properly (ensure first sheet has data with headers).", "Empty/Invalid File", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    toolStripStatusLabel1.Text = "Ready";
                    this.Cursor = Cursors.Default;
                    return;
                }
                toolStripStatusLabel1.Text = $"Read {excelData.Rows.Count} data rows from Excel.";
                dataGridViewResults.DataSource = null;
                dataGridViewResults.DataSource = excelData;
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reading Excel file: {ex.Message}", "Excel Read Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                toolStripStatusLabel1.Text = "Error reading Excel.";
                this.Cursor = Cursors.Default;
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    toolStripStatusLabel1.Text = "Connected to SQL Server. Preparing table...";
                    Application.DoEvents();

                    DropTableIfExists(connection, tableName);
                    CreateTableFromDataTable(connection, tableName, excelData);
                    toolStripStatusLabel1.Text = $"Table '{tableName}' created. Starting bulk import...";
                    Application.DoEvents();

                    BulkInsertData(connection, tableName, excelData);

                    // --- Success ---
                    int importedRowCount = excelData.Rows.Count;
                    toolStripStatusLabel1.Text = $"Successfully imported {importedRowCount} rows into table '{tableName}'.";

                    // Optional: Save connection string only on success
                    // Settings.Default.LastConnectionString = connectionString;
                    // Settings.Default.Save();

                    MessageBox.Show($"Successfully imported {importedRowCount} rows into table '{tableName}'.", "Import Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    dataGridViewResults.DataSource = null;
                    dataGridViewResults.DataSource = excelData;
                    // Auto-size columns for better visibility
                    dataGridViewResults.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"SQL Error during import: {sqlEx.Message}\n\nCheck connection string, permissions, and table name.", "SQL Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                toolStripStatusLabel1.Text = "SQL Error during import.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Import Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                toolStripStatusLabel1.Text = "Import failed.";
            }
            finally
            {
                this.Cursor = Cursors.Default; // Reset cursor
            }
        }

        private DataTable ReadExcelFile(string filePath)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        { UseHeaderRow = true }
                    });
                    if (result.Tables.Count > 0) return result.Tables[0];
                }
            }
            return null;
        }

        private void DropTableIfExists(SqlConnection connection, string tableName)
        {
            string dropSql = $"IF OBJECT_ID(N'[{tableName}]', 'U') IS NOT NULL DROP TABLE [{tableName}];";
            using (SqlCommand command = new SqlCommand(dropSql, connection)) { command.ExecuteNonQuery(); }
            toolStripStatusLabel1.Text = $"Checked/dropped existing table '{tableName}'."; Application.DoEvents();
        }

        private void CreateTableFromDataTable(SqlConnection connection, string tableName, DataTable table)
        {
            StringBuilder createSql = new StringBuilder();
            createSql.AppendFormat("CREATE TABLE [{0}] (", tableName);
            foreach (DataColumn column in table.Columns)
            {
                string columnName = column.ColumnName.Replace("]", "]]");
                createSql.AppendFormat("[{0}] NVARCHAR(MAX) NULL, ", columnName);
            }
            if (table.Columns.Count > 0) { createSql.Length -= 2; }
            createSql.Append(");");
            using (SqlCommand command = new SqlCommand(createSql.ToString(), connection)) { command.ExecuteNonQuery(); }
        }

        private void BulkInsertData(SqlConnection connection, string tableName, DataTable table)
        {
            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
            {
                bulkCopy.DestinationTableName = $"[{tableName}]";
                try
                {
                    bulkCopy.WriteToServer(table);
                }
                catch (Exception ex)
                {
                    throw new Exception($"SqlBulkCopy failed: {ex.Message}. Ensure column names in Excel match and data types are compatible (or use NVARCHAR(MAX)).", ex);
                }
            }
        }

        private void btnCheckConnection_Click(object sender, EventArgs e)
        {
            //implement connection check logic
            string connectionString = txtConnectionString.Text.Trim();
            if (string.IsNullOrEmpty(connectionString))
            {
                MessageBox.Show("Please enter a SQL Server connection string.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConnectionString.Focus();
                return;
            }

            // Ensure the connection string includes Encrypt=False and TrustServerCertificate=True
            if (!connectionString.Contains("Encrypt="))
            {
                connectionString += ";Encrypt=False";
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    MessageBox.Show("Connection successful!", "Connection Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    toolStripStatusLabel1.Text = "Connection successful!";
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"Connection failed: {sqlEx.Message}", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                toolStripStatusLabel1.Text = "Connection failed.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                toolStripStatusLabel1.Text = "Error during connection check.";
            }
        }
    }
}