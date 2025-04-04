# ExcelToSqlImporter

## Description
ExcelToSqlImporter is a .NET 8 application that allows users to import data from Excel files into SQL Server databases. It provides a user-friendly interface to select Excel files, specify SQL Server connection details, and import data seamlessly.

## Features
- Select and read Excel files (.xls, .xlsx, .xlsm).
- Display Excel data in a DataGridView for preview.
- Specify SQL Server connection string and destination table name.
- Automatically create SQL tables based on Excel data structure.
- Perform bulk data import into SQL Server.
- Save and load the last used SQL Server connection string.
- Check SQL Server connection before importing data.

## Usage
1. Launch the application.
2. Click the "Browse" button to select an Excel file.
3. Enter the SQL Server connection string.
4. Enter the destination table name.
5. Click the "Import" button to import data into the specified SQL table.
6. Optionally, click the "Check Connection" button to verify the SQL Server connection.

## Dependencies
- .NET 8
- ExcelDataReader
- Microsoft.Data.SqlClient

## Prerequisites
- Ensure you have .NET 8 installed.
- Ensure you have the necessary permissions to access the SQL Server and perform data import operations.
