using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Util.Store;
using Pcbc.DiaperPantry.SheetIngestion.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pcbc.DiaperPantry.SheetIngestion.Google
{
    public interface IGoogleSheetService
    {
        bool Authenticate();
        string GetSheet();
    }

    public class GoogleSheetService : IGoogleSheetService
    {
        private IDynamoToChildForm _dynamoMapper;

        public GoogleSheetService(IDynamoToChildForm dynamoMapper)
        {
            _dynamoMapper = dynamoMapper;
        }

        SheetsService _sheetService { get; set; } = null;

        public bool Authenticate()
        {
            //validate to see if the calendar has already been authenticated
            if (_sheetService != null)
                return true;

            //Log into google with its oauth2 client, add the claim to the permissions as well
            UserCredential credential;
            using (var stream = new FileStream("client_secrets.json", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.FromStream(stream).Secrets,
                        new[] { "https://www.googleapis.com/auth/spreadsheets" },
                        "user",
                        CancellationToken.None,
                        new FileDataStore("Sheets.MySheet")
                    ).Result;
            }

            //Initialize the Calendar service with the auth creds we just received.
            _sheetService = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Pcbc Diaper Pantry",
            });

            return true;
        }

        public string GetSheet()
        {
            var documentName = "New Child Form";
            var sheetName = "Results";
            var vals = _sheetService.Spreadsheets.Values;
            var vals2 = GetDataFromSheet();
            Console.Write("Hello world");
            return "";
        }

        public List<ExpandoObject> GetDataFromSheet()
        {
            var rangeColumnStart = 0;
            var rangeColumnEnd = 45;
            var rangeRowStart = 1;
            var rangeRowEnd = 5;
            var sheetName = "Results";
            var spreadSheetId = "1DNfRhbCEhgwSKB1U4DlcdE6f4pKGSYSaX0sDdgkafa8";
            var firstRowIsHeaders = true;
            var range = $"{sheetName}!{GetColumnName(rangeColumnStart)}{rangeRowStart}:{GetColumnName(rangeColumnEnd)}{rangeRowEnd}";

            SpreadsheetsResource.ValuesResource.GetRequest request =
                _sheetService.Spreadsheets.Values.Get(spreadSheetId, range);

            var numberOfColumns = rangeColumnEnd - rangeColumnStart;
            var columnNames = new List<string>();
            var returnValues = new List<ExpandoObject>();

            if (!firstRowIsHeaders)
            {
                for (var i = 0; i <= numberOfColumns; i++)
                {
                    columnNames.Add($"Column{i}");
                }
            }

            var response = request.Execute();

            int rowCounter = 0;
            IList<IList<Object>> values = response.Values;
            if (values != null && values.Count > 0)
            {
                foreach (var row in values)
                {
                    if (firstRowIsHeaders && rowCounter == 0)
                    {
                        for (var i = 0; i <= numberOfColumns; i++)
                        {
                            columnNames.Add(row[i].ToString() ?? string.Empty);
                        }
                        rowCounter++;
                        continue;
                    }

                    var expando = new ExpandoObject();
                    var expandoDict = expando as IDictionary<String, object>;
                    var columnCounter = 3;
                    expandoDict.Add("Id", row[0].ToString());
                    expandoDict.Add("CreatedDateTime", row[1].ToString());
                    expandoDict.Add("Link", row[2].ToString());
                    var toIngest = columnNames.Skip(columnCounter).ToList();
                    foreach (var columnName in toIngest)
                    {
                        if (columnCounter >= row.Count())
                            continue;
                        expandoDict.Add(columnName, row[columnCounter].ToString());
                        columnCounter++;
                    }
                    returnValues.Add(expando);
                    rowCounter++;
                }
            }

            var ret = _dynamoMapper.Map(returnValues[1]);

            return returnValues;
        }

        /// <summary>
        ///     Gets a column name
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private string GetColumnName(int index)
        {
            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var value = "";

            if (index >= letters.Length)
                value += letters[index / letters.Length - 1];

            value += letters[index % letters.Length];
            return value;
        }
    }
}
