using System;
using System.IO;
using System.Text;

namespace USATU_OOP_LW_7;

public static class StorageTools
{
    private const string FileName = "GraphicObjectsStorage.txt";

    public static bool IsFileExists()
    {
        return File.Exists(FileName);
    }

    public static StringReader GetFormattedDataFromStorage()
    {
        var readText = File.ReadAllText(FileName);
        readText = readText.Replace("\t", "");
        while (readText.Contains(Environment.NewLine + Environment.NewLine))
        {
            readText = readText.Replace(Environment.NewLine + Environment.NewLine, Environment.NewLine);
        }

        var stringReader = new StringReader(readText);
        var formattedText = new StringBuilder();
        while (stringReader.ReadLine() is { } line)
        {
            line = line.Substring(line.LastIndexOf(':') + 2);
            formattedText.AppendLine(line);
        }

        return new StringReader(formattedText.ToString());
    }

    public static void WriteDataToStorage(string data)
    {
        File.WriteAllText(FileName, data);
    }
}