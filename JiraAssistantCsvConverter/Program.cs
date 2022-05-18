using System.Globalization;
using Csv;
using JiraAssistantCsvConverterBase;

namespace JiraAssistantCsvConverter // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var csvFile = args.First();
            var csv = CsvWriter.WriteToText(
                CsvConverter.GetColumnHeaders(), 
                CsvConverter.ConvertRows(CsvReader.ReadFromText(File.ReadAllText(csvFile))), ',');
            var target = Path.ChangeExtension(csvFile, "_converted.csv");
            File.WriteAllText(target, csv);
        }
    }
}