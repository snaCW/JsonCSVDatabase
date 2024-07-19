using System.Text;

namespace JsonCSVDatabase;

public class CSVDatabase
{
    private List<Dictionary<string, string>> CSV = new List<Dictionary<string, string>>();
    private List<string> Headers = new List<string>();
    public string Delimeter { get; set; } = ",";
    public int ColumnCount { get; private set; } = 0;
    public int RowCount { get; private set; } = 0;
    public CSVDatabase() {}
    public CSVDatabase(string relativePath)
    {
        SetPath(relativePath);
    }
    public CSVDatabase SetPath(string relativePath)
    {
        StreamReader streamReader = new StreamReader(relativePath);
        string? line = streamReader.ReadLine();
        if (line == null)
        {
            throw new ArgumentException("File is empty.");
        }
        Headers = line.Split(Delimeter).ToList();
        ColumnCount = Headers.Count;
        
        line = streamReader.ReadLine();
        while (line != null)
        {
            AddRow(line);
        }
        streamReader.Close();

        return this;
    }
    public CSVDatabase SetDelimeter(string delimeter)
    {
        Delimeter = delimeter;
        return this;
    }
    public static CSVDatabase SetNewDatabaseHeader(string[] header, string delimeter = ",")
    {
        return SetNewDatabaseHeader(header.ToList(), delimeter);
    }
    public static CSVDatabase SetNewDatabaseHeader(List<string> header, string delimeter = ",")
    {
        CSVDatabase database = new CSVDatabase
        {
            Headers = header,
            ColumnCount = header.Count,
            Delimeter = delimeter
        };

        return database;
    }
    public static CSVDatabase SetNewDatabaseHeader(string header, string delimeter = ",")
    {
        return SetNewDatabaseHeader(header.Split(delimeter), delimeter);
    }
    public void ChangeHeaderTo(string header)
    {
        ChangeHeaderTo(header.Split(','));
    }
    public void ChangeHeaderTo(string[] header)
    {
        ChangeHeaderTo(header.ToList());
    }
    public void ChangeHeaderTo(List<string> header)
    {
        ArgumentOutOfRangeException.ThrowIfNotEqual(header.Count, ColumnCount, "Column Count");
        
        List<Dictionary<string, string>> newCSV = new List<Dictionary<string, string>>();
        foreach (Dictionary<string, string> row in CSV)
        {
            int index = 0;
            Dictionary<string, string> newRow = new Dictionary<string, string>();
            foreach (string value in row.Values)
            {
                newRow.Add(header[index], value);
                index++;
            }
            newCSV.Add(newRow);
        }
        CSV = newCSV;
        Headers = header;
    }
    public void Clear()
    {
        CSV.Clear();
        Headers.Clear();
        ColumnCount = 0;
        RowCount = 0;
        Delimeter = ",";
    }
    public CSVDatabase AddRow(string[] row)
    {
        ArgumentOutOfRangeException.ThrowIfNotEqual(row.Length, ColumnCount, "Column Count");

        Dictionary<string, string> lineTokens = new Dictionary<string, string>();
        for (int i = 0; i < ColumnCount; i++)
        {
            lineTokens.Add(Headers[i], row[i]);
        }
        CSV.Add(lineTokens);
        RowCount++;
        
        return this;
    }
    public CSVDatabase AddRow(List<string> row)
    {
        return AddRow(row.ToArray());
    }
    public CSVDatabase AddRow(string row)
    {
        return AddRow(row.Split(Delimeter));
    }
    public bool TryAddRow(string[] row)
    {
        if (row.Length != ColumnCount)
        {
            return false;
        }

        Dictionary<string, string> lineTokens = new Dictionary<string, string>();
        for (int i = 0; i < ColumnCount; i++)
        {
            lineTokens.Add(Headers[i], row[i]);
        }
        CSV.Add(lineTokens);
        RowCount++;
        
        return true;
    }
    public bool TryAddRow(List<string> row)
    {
        return TryAddRow(row.ToArray());
    }
    public bool TryAddRow(string row)
    {
        return TryAddRow(row.Split(Delimeter));
    }
    public CSVDatabase AddColumn(string key, string[] column)
    {
        ArgumentOutOfRangeException.ThrowIfNotEqual(column.Length, RowCount, "Row Count");

        for (int i = 0; i < RowCount; i++)
        {
            CSV[i].Add(key, column[i]);
        }
        ColumnCount++;

        return this;
    }
    public CSVDatabase AddColumn(string key, List<string> column)
    {
        return AddColumn(key, column.ToArray());
    }
    public CSVDatabase AddColumn(string key, string column)
    {
        return AddColumn(key, column.Split(Delimeter));
    }
    public bool TryAddColumn(string key, string[] column)
    {
        if (column.Length != RowCount)
        {
            return false;
        }

        for (int i = 0; i < RowCount; i++)
        {
            CSV[i].Add(key, column[i]);
        }
        ColumnCount++;

        return true;
    }
    public bool TryAddColumn(string key, List<string> column)
    {
        return TryAddColumn(key, column.ToArray());
    }
    public bool TryAddColumn(string key, string column)
    {
        return TryAddColumn(key, column.Split(Delimeter));
    }
    public bool RemoveAllRows(string key, string value)
    {
        bool removed = false;
        foreach (Dictionary<string, string> row in CSV)
        {
            if (row.TryGetValue(key, out string? _value) && _value == value)
            {
                CSV.Remove(row);
                removed = true;
                RowCount--;
            }
        }
        return removed;
    }
    public void RemoveRowAt(int index)
    {
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, RowCount);
        CSV.RemoveAt(index);
        RowCount--;
    }
    public bool RemoveColumn(string key)
    {
        if (!Headers.Contains(key))
        {
            return false;
        }
        foreach (Dictionary<string, string> row in CSV)
        {
            row.Remove(key);
        }
        ColumnCount--;
        return true;
    }
    public void RemoveColumnAt(int index)
    {
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, ColumnCount);
        
        RemoveColumn(Headers[index]);
    }
    public Dictionary<string, string> GetRowAt(int index)
    {
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, RowCount);
        return CSV[index];
    }
    public List<string>? GetColumn(string key)
    {
        bool containsKey = Headers.Contains(key);
        if (!containsKey)
        {
            return null;
        }
        
        List<string> values = new List<string>();
        foreach (Dictionary<string, string> row in CSV)
        {
            values.Add(row[key]);
        }
        return values;
    }
    public void SetDataAt(int row, string key, string newData)
    {
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(row, RowCount);
        bool containsKey = Headers.Contains(key);
        if (!containsKey)
        {
            throw new ArgumentException("No such key found in header.");
        }
        
        CSV[row][key] = newData;
    }
    public void SetDataAt(int row, int column, string newData)
    {
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(column, ColumnCount);
        
        SetDataAt(row, Headers[column], newData);
    }
    public Dictionary<string, string>? FindRow(string value)
    {
        foreach (Dictionary<string, string> row in CSV)
        {
            if (row.ContainsValue(value))
            {
                return row;
            }
        }
        return null;
    }
    public void WriteToJson(string relativePath)
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("[");
        
        int rowIndex = 0;
        foreach (Dictionary<string, string> row in CSV)
        {
            stringBuilder.AppendLine("\t{");
            
            int columnIndex = 0;
            foreach (KeyValuePair<string, string> pair in row)
            {
                stringBuilder.Append($"\t\t\"{pair.Key}\": \"{pair.Value}\"");
                if (columnIndex < ColumnCount - 1)
                {
                    stringBuilder.Append(',');
                }
                columnIndex++;
                stringBuilder.AppendLine();
            }

            stringBuilder.Append("\t}");
            if (rowIndex < RowCount - 1)
            {
                stringBuilder.Append(',');
            }
            rowIndex++;
            stringBuilder.AppendLine();
        }
        
        stringBuilder.AppendLine("]");

        StreamWriter streamWriter = new StreamWriter(relativePath);
        streamWriter.Write(stringBuilder);
        streamWriter.Close();
    }
    public void WriteToCSV(string relativePath)
    {
        StringBuilder stringBuilder = new StringBuilder();
        
        for (int i = 0; i < ColumnCount; i++)
        {
            stringBuilder.Append(Headers[i]);
            if (i < ColumnCount - 1)
            {
                stringBuilder.Append(',');
            }
            stringBuilder.AppendLine();
        }

        foreach (Dictionary<string, string> row in CSV)
        {
            int columnIndex = 0;
            foreach (string value in row.Values)
            {
                stringBuilder.Append(value);
                if (columnIndex < ColumnCount - 1)
                {
                    stringBuilder.Append(',');
                }
                columnIndex++;
                stringBuilder.AppendLine();
            }
            stringBuilder.AppendLine();
        }

        StreamWriter streamWriter = new StreamWriter(relativePath);
        streamWriter.Write(stringBuilder);
        streamWriter.Close();
    }
}