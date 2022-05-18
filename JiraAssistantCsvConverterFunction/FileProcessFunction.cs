using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using JiraAssistantCsvConverterBase;
using Csv;
using System.Text;

namespace JiraAssistantCsvConverterFunction
{
    public static class FileProcessFunction
    {
        [FunctionName("FileProcessFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            try
            {
                var formdata = await req.ReadFormAsync();
                var file = req.Form.Files["file"];
                StreamReader reader = new StreamReader(file.OpenReadStream());
                string csvFile = reader.ReadToEnd();
                var text = CsvReader.ReadFromText(csvFile);
                var csv = CsvWriter.WriteToText(
                            CsvConverter.GetColumnHeaders(),
                            CsvConverter.ConvertRows(text));
                return new FileContentResult(Encoding.ASCII.GetBytes(csv), "text/csv");
            }
            catch(Exception ex)
            {
                return new BadRequestObjectResult(ex);
            }
        }
    }
}
