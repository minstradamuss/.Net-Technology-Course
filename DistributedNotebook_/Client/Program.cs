using System;
using System.Net.Http;
using System.Net.Http.Json;

var client = new HttpClient();
client.DefaultRequestHeaders.Add("clientToken", "client-secret");

Console.WriteLine("Adding entry...");
await client.PostAsJsonAsync("http://localhost:5005/notebook/add", new EntryModel { Name = "Маша", Phone = "75775775775" });

Console.WriteLine("Searching entry...");
var res = await client.GetAsync("http://localhost:5005/notebook/search?query=Маша");
Console.WriteLine(await res.Content.ReadAsStringAsync());

public class EntryModel
{
    public string Name { get; set; }
    public string Phone { get; set; }
}
