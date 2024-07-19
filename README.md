# JsonCSVDatabase
A namespace containing classes to convert CSV to JSON or Excel and vice versa.

Class `CSVDatabase` gets data to create an obj with several methods.

The below is the documentary.

## `public string Delimeter { get; set; } = ","`
You can change the default value for the delimeter easily.

## `public int ColumnCount { get; private set; }`
Get the amount of columns.

## `public int RowCount { get; private set; }`
Get the amount of rows.

## `public CSVDatabase(string relativePath)`
Constructor with path to an existing CSV file.

## `public CSVDatabase SetPath(string relativePath)`
Reads from the specified file and returns `this`.

## `public CSVDatabase SetDelimeter(string delimeter)`
Sets the specified delimeter and returns `this`.

## `public static CSVDatabase SetNewDatabaseHeader(string header, string delimeter = ",")`
Uses the specified delimeter to split the header and assigns the value to `Headers` private data member.

Overloads:
- `public static CSVDatabase SetNewDatabaseHeader(string[] header, string delimeter = ",")`
- `public static CSVDatabase SetNewDatabaseHeader(List<string> header, string delimeter = ",")`

## `public void ChangeHeaderTo(string header)`
Changes the current header to the new one.

Exceptions:
- `ArgumentOutOfRangeException` if the new header elements count doesn't match the current header elements count.

Overloads:
- `public void ChangeHeaderTo(string[] header)`
- `public void ChangeHeaderTo(List<string> header)`

## `public void Clear()`
Clears the entire object to the initial state. Please note that the `Delimeter` value will return back to `","`.

## `public CSVDatabase AddRow(string row)`
Adds a new row to the CSV database object.

Exceptions:
- `ArgumentOutOfRangeException` if the new row elements count doesn't match the header elements count.

Overloads:
- `public void AddRow(string[] row)`
- `public void AddRow(List<string> row)`

## `public bool TryAddRow(string row)`
Returns `true` if the add process was successful, or else returns `false`.

Overloads:
- `public bool TryAddRow(string[] row)`
- `public bool TryAddRow(List<string> row)`

## `public CSVDatabase AddColumn(string key, string column)`
Adds a new column to the existing database. `key` will be the new element in headers, and `column` with be splitted to be values of this new header.

Exceptions:
- `ArgumentOutOfRangeException` if the new column elements count doesn't match the current rows count.

Overloads:
- `public CSVDatabase AddColumn(string key, string[] column)`
- `public CSVDatabase AddColumn(string key, List<string> column)`

## `public bool TryAddColumn(string key, string column)`
Returns `true` if the add process was successful, or else returns `false`.

Overloads:
- `public bool TryAddColumn(string key, string[] column)`
- `public bool TryAddColumn(string key, List<string> column)`

## `public bool RemoveAllRows(string key, string value)`
Removes all the rows which contain `value` at their `key` (column header). If at least one match was found and removed, returns `true`, or else `false`.

## `public void RemoveRowAt(int index)`
Removes the row at the `index` index. Indexing starts with 0.

Exceptions:
- `ArgumentOutOfRangeException` if the argument targets an index out of the possible range.

## `public bool RemoveColumn(string key)`
Removes the entire column with the specified `key` at header. If any match was found and removed, returns `true`, or else `false`.

## `public Dictionary<string, string> GetRowAt(int index)`
Returns the entire row at `index`.

Exceptions:
- `ArgumentOutOfRangeException` if the argument targets an index out of the possible range.

## `public List<string>? GetColumn(string key)`
Returns the entire column with `key` as its header. Returns `null` if such `key` was not found in headers.

## `public void SetDataAt(int row, string key, string newData)`
Sets `newData` to the value at the data in the `row` index with the `key` header.

Exceptions:
- `ArgumentOutOfRangeException` if the argument `row` targets an index out of the possible range.

Overloads:
- `public void SetDataAt(int row, int column, string newData)`
    - Can throw `ArgumentOutOfRangeException` if the argument `column` targets a header not present in the database.

## `public Dictionary<string, string>? FindRow(string value)`
Tries to find the first row containing `value`. Returns `null` if there is no match.

## `public void WriteToJson(string relativePath)`
Writes the current database as a JSON file in the `relativePath`. The structur of the resulted file will be like below:

```JSON
[
    {
        "key1": "value1-1",
        "key2": "value1-2"
    },
    {
        "key1": "value2-1",
        "key2": "value2-2"
    }
]
```

## `public void WriteToCSV(string relativePath)`
Writes the current database as a CSV file in the `relativePath`. The structur of the resulted file will be like below:

```CSV
key1,key2
value1-1,value1-2
value2-1,value2-2
```