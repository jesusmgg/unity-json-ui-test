# Unity JSON UI test
Reads a specified file from the streaming assets folder and renders a table using the data. By default this file name is "JsonChallenge.json".

Any changes made to the JSON file are hot-reloaded into the table.

The JSON file must have this structure:
```json
{
  "Title": "Team Members",
  "ColumnHeaders": [
    "ID",
    "Name"
  ],
  "Data": [
    {
      "ID": "001",
      "Name": "John Doe"
    },
    {
      "ID": "023",
      "Name": "Claire Dawn"
    },
    {
      "ID": "012",
      "Name": "Paul Beef"
    },
    {
      "ID": "056",
      "Name": "Sally Sue"
    }
  ]
}
```

An arbitrary number of column headers and data rows are supported.

### Project approach
The JSON file is parsed and deserialized with [Json.NET](https://github.com/jilleJr/Newtonsoft.Json-for-Unity).

A .NET [FileSystemWatcher](https://docs.microsoft.com/en-us/dotnet/api/system.io.filesystemwatcher?view=net-5.0) is used to keep track of live file changes. The changes invoke a UnityEvent which the UI components listens to and update accordingly.

The UI is made with the default Unity UI components (GameObject based). The table has a template row and each row has a template cell text. With each data update these templates are taken and used to instantiate the data rows. 