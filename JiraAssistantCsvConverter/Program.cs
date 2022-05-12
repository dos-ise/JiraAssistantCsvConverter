using System.Globalization;
using Csv;

namespace JiraAssistantCsvConverter // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var csvFile = args.First();
            //var columnNames = new[] { "Ticket No", "Issue Type", "Summary", "Log", "Date", "Timespent", "Comment", "Assignee", "Status" };
            var columnNames = new[] { "Ticket No", "Start Date", "Timespent", "Comment" };


            var csv = CsvWriter.WriteToText(columnNames, getRows(CsvReader.ReadFromText(File.ReadAllText(csvFile))), ',');
            var target = Path.ChangeExtension(csvFile, "_converted.csv");
            File.WriteAllText(target, csv);
        }

        private static IEnumerable<string[]> getRows(IEnumerable<ICsvLine> lines)
        {
            foreach (var line in lines)
            {
                // Header is handled, each line will contain the actual row data
                var day = line[0];
                var startTime = line[1];
                var endTime = line[2];
                var description = line[3];
                var duration = line[4];
                var task = line[5];
                if (string.IsNullOrEmpty(task))
                {
                    yield break;
                }

                var timeSpent = DateTime.Parse(day + " " + startTime).ToString("o", CultureInfo.GetCultureInfo("en-US"));
                yield return new[] { task, timeSpent, duration, description };
            }
        }
    }
}