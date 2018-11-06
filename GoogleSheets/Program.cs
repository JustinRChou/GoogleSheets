using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

using Data = Google.Apis.Sheets.v4.Data;

namespace GoogleSheets
{
    class Program
    {
        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/sheets.googleapis.com-dotnet-quickstart.json
        static string[] Scopes = {SheetsService.Scope.Spreadsheets };
        static string ApplicationName = "TestProgram";

        static void Main(string[] args)
        {
            UserCredential credential;

            using (var stream =
                new FileStream("credentials/credentials4.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Google Sheets API service.
            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define request parameters.
            String spreadsheetId = "1NcH_nkQ6NvobuC6k0e1U0iJFFLk8_Vl5ZxU-t0_Z0Yk";
            String range = "Sheet1!A1:C2";
            SpreadsheetsResource.ValuesResource.GetRequest request =
                    service.Spreadsheets.Values.Get(spreadsheetId, range);

            // Prints the names and majors of students in a sample spreadsheet:
            // https://docs.google.com/spreadsheets/d/1BxiMVs0XRA5nFMdKvBdBZjgmUUqptlbs74OgvE2upms/edit
            //ValueRange response = request.Execute();
            //IList<IList<Object>> values = response.Values;
            //if (values != null && values.Count > 0)
            //{
            //    Console.WriteLine("Name, Major");
            //    foreach (var row in values)
            //    {
            //        // Print columns A and E, which correspond to indices 0 and 4.
            //        Console.WriteLine("{0}, {1}", row[0], row[4]);
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("No data found.");
            //}

            SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum valueInputOption 
                = (SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum)1;  // TODO: Update placeholder value.
            Data.ValueRange requestBody = new Data.ValueRange();
            List<IList<object>> abc = new List<IList<object>>();
            IList<object> tmp = new List<object>() { "1","abc","222" };
            IList<object> tmp2 = new List<object>() { "21", "ab2c", "2322" };
            abc.Add(tmp);
            abc.Add(tmp2);
            requestBody.Values = abc;
            //requestBody.Values = abc as IList<IList<object>>;
            SpreadsheetsResource.ValuesResource.UpdateRequest request2 
                = service.Spreadsheets.Values.Update(requestBody, spreadsheetId, range);
            request2.ValueInputOption = valueInputOption;

            // To execute asynchronously in an async method, replace `request.Execute()` as shown:
            Data.UpdateValuesResponse response3 = request2.Execute();
            // Data.UpdateValuesResponse response = await request.ExecuteAsync();

            Console.Read();
        }
    }
}
