// See https://aka.ms/new-console-template for more information
using gigabyte_homework_2;
using Spectre.Console;
using System.Data;

string connStr = "Server=localhost;Database=DemoDb;User Id=sa;Password=50100300Cc;TrustServerCertificate=true;";
var checker = new PermissionChecker(connStr);

while (true)
{
    var name = AnsiConsole.Ask<string>("Enter UserName [green]name[/] (or 'exit' to quit):");
    if (name.ToLower() == "exit") break;

    var dataTable = checker.Query(name);

    // 建立 Spectre.Console 的 Table
    var table = new Table();

    // 加入欄位標題
    foreach (DataColumn column in dataTable.Columns)
    {
        table.AddColumn(column.ColumnName);
    }

    // 加入每一列的資料
    foreach (DataRow row in dataTable.Rows)
    {
        var rowValues = new List<string>();
        foreach (var item in row.ItemArray)
        {
            rowValues.Add(item?.ToString() ?? string.Empty);
        }

        table.AddRow(rowValues.ToArray());
    }

    // 顯示表格
    AnsiConsole.Write(table);
}

AnsiConsole.MarkupLine("[bold green]Goodbye![/]");