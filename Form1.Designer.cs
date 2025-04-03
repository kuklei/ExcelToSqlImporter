namespace ExcelToSqlImporter // Make sure this namespace matches yours
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            txtConnectionString = new TextBox();
            label2 = new Label();
            txtTableName = new TextBox();
            btnBrowse = new Button();
            txtFilePath = new TextBox();
            btnImport = new Button();
            dataGridViewResults = new DataGridView();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            label3 = new Label();
            btnCheckConnection = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewResults).BeginInit();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 17);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(165, 15);
            label1.TabIndex = 0;
            label1.Text = "SQL Server Connection String:";
            // 
            // txtConnectionString
            // 
            txtConnectionString.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtConnectionString.Location = new Point(183, 14);
            txtConnectionString.Margin = new Padding(4, 3, 4, 3);
            txtConnectionString.Name = "txtConnectionString";
            txtConnectionString.Size = new Size(631, 23);
            txtConnectionString.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(14, 47);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(135, 15);
            label2.TabIndex = 2;
            label2.Text = "Destination Table Name:";
            // 
            // txtTableName
            // 
            txtTableName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtTableName.Location = new Point(183, 44);
            txtTableName.Margin = new Padding(4, 3, 4, 3);
            txtTableName.Name = "txtTableName";
            txtTableName.Size = new Size(631, 23);
            txtTableName.TabIndex = 3;
            // 
            // btnBrowse
            // 
            btnBrowse.Location = new Point(18, 74);
            btnBrowse.Margin = new Padding(4, 3, 4, 3);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(159, 27);
            btnBrowse.TabIndex = 4;
            btnBrowse.Text = "Browse Excel File...";
            btnBrowse.UseVisualStyleBackColor = true;
            btnBrowse.Click += btnBrowse_Click;
            // 
            // txtFilePath
            // 
            txtFilePath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtFilePath.Location = new Point(183, 76);
            txtFilePath.Margin = new Padding(4, 3, 4, 3);
            txtFilePath.Name = "txtFilePath";
            txtFilePath.ReadOnly = true;
            txtFilePath.Size = new Size(717, 23);
            txtFilePath.TabIndex = 5;
            // 
            // btnImport
            // 
            btnImport.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnImport.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnImport.Location = new Point(766, 106);
            btnImport.Margin = new Padding(4, 3, 4, 3);
            btnImport.Name = "btnImport";
            btnImport.Size = new Size(134, 33);
            btnImport.TabIndex = 6;
            btnImport.Text = "Import to SQL";
            btnImport.UseVisualStyleBackColor = true;
            btnImport.Click += btnImport_Click;
            // 
            // dataGridViewResults
            // 
            dataGridViewResults.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewResults.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewResults.Location = new Point(18, 147);
            dataGridViewResults.Margin = new Padding(4, 3, 4, 3);
            dataGridViewResults.Name = "dataGridViewResults";
            dataGridViewResults.ReadOnly = true;
            dataGridViewResults.Size = new Size(883, 345);
            dataGridViewResults.TabIndex = 7;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new Point(0, 510);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new Padding(1, 0, 16, 0);
            statusStrip1.Size = new Size(915, 22);
            statusStrip1.TabIndex = 8;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(39, 17);
            toolStripStatusLabel1.Text = "Ready";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(14, 128);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(86, 15);
            label3.TabIndex = 9;
            label3.Text = "Imported Data:";
            // 
            // btnCheckConnection
            // 
            btnCheckConnection.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCheckConnection.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCheckConnection.Location = new Point(822, 14);
            btnCheckConnection.Margin = new Padding(4, 3, 4, 3);
            btnCheckConnection.Name = "btnCheckConnection";
            btnCheckConnection.Size = new Size(93, 24);
            btnCheckConnection.TabIndex = 10;
            btnCheckConnection.Text = "Check...";
            btnCheckConnection.UseVisualStyleBackColor = true;
            btnCheckConnection.Click += btnCheckConnection_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(915, 532);
            Controls.Add(btnCheckConnection);
            Controls.Add(label3);
            Controls.Add(statusStrip1);
            Controls.Add(dataGridViewResults);
            Controls.Add(btnImport);
            Controls.Add(txtFilePath);
            Controls.Add(btnBrowse);
            Controls.Add(txtTableName);
            Controls.Add(label2);
            Controls.Add(txtConnectionString);
            Controls.Add(label1);
            Margin = new Padding(4, 3, 4, 3);
            MinimumSize = new Size(522, 398);
            Name = "Form1";
            Text = "Excel to SQL Importer";
            ((System.ComponentModel.ISupportInitialize)dataGridViewResults).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtConnectionString;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTableName;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.DataGridView dataGridViewResults;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Label label3;
        private Button btnCheckConnection;
    }
}