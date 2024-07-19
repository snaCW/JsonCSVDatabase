# JsonCSVDatabase
A namespace containing classes to convert CSV to JSON or Excel and vice versa.

Class `CSVDatabase` gets data to create an obj with several methods.
## public string Delimeter
You can change the default value for the delimeter (`","`) easily.
## public int ColumnCount { get; private set; }
Get the amount of columns.
## public int RowCount { get; private set; }
Get the amount of rows.
## public CSVDatabase(string relativePath)
Constructor with path to an existing CSV file.
## public CSVDatabase SetPath(string relativePath)
Reads from the specified file and returns `this`.
## public CSVDatabase SetDelimeter(string delimeter)
Sets the specified delimeter and returns `this`.
## `public static CSVDatabase SetNewDatabaseHeader(string header, string delimeter = ",")`
Uses the specified delimeter to split the header and assigns the value to `Headers` private data member.
Overloads:
- public static CSVDatabase SetNewDatabaseHeader(string[] header, string delimeter = ",")
- public static CSVDatabase SetNewDatabaseHeader(List<string> header, string delimeter = ",")