#r "Newtonsoft.Json"

using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
{
    log.Info("C# HTTP trigger function processed a request.");

    // Get request body (CSV content)
    string data = await req.Content.ReadAsStringAsync();

    //Create a new JOBject to store the results
    JObject resultSet = JObject.FromObject(new { rows = new JArray() });

    //Split the CSV content into lines
    string[] csvLines = data.Split(new char [] {'\n', '\r'});
    var headers = csvLines[0].Split(',').ToList<string>();

    //For each line, create a JSON Object with property names from the first line of the CSV
    foreach (var line in csvLines.Skip(1))
    {
        if(line == null || line == "")
            continue;
        var lineObject = new JObject();
        var lineAttr = line.Split(',');
        for (int x = 0; x < headers.Count; x++)
        {
            lineObject[headers[x]] = lineAttr[x];
        }
        ((JArray)resultSet["rows"]).Add(lineObject);
    }

    //Return the new JSON
    return req.CreateResponse(HttpStatusCode.OK, resultSet);

}