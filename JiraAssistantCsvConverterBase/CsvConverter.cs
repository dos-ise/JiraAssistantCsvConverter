using System;
using System.Collections.Generic;
using System.Globalization;
using Csv;

namespace JiraAssistantCsvConverterBase
{
    public static class CsvConverter
    {
        public static string[] GetColumnHeaders()
        {
           return new[] { "Ticket No", "Start Date", "Timespent", "Comment" };
        }

        public static IEnumerable<string[]> ConvertRows(IEnumerable<ICsvLine> lines)
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

                if(DateTime.TryParse(day, CultureInfo.GetCultureInfo("de-DE"), DateTimeStyles.None, out var dayDate))
                {
                    var timeSpent = dayDate.Add(TimeSpan.Parse(startTime)).ToString("o", CultureInfo.GetCultureInfo("en-US"));
                    yield return new[] { task, timeSpent, duration, description };
                }
                else
                {
                    yield break;
                }
            }
        }
    }
}
