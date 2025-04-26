using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

public class NotebookTests
{
    private readonly HttpClient _client;

    public NotebookTests()
    {
        _client = new HttpClient();
        _client.DefaultRequestHeaders.Add("clientToken", "client-secret");
    }

    public class EntryModel
    {
        public string Name { get; set; }
        public string Phone { get; set; }
    }

    [Fact]
    public async Task AddEntry_ShouldSucceed()
    {
        var response = await _client.PostAsJsonAsync("http://localhost:5005/notebook/add", new EntryModel { Name = "Иван", Phone = "89001112233" });
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task SearchExistingEntry_ShouldReturnEntry()
    {
        await _client.PostAsJsonAsync("http://localhost:5005/notebook/add", new EntryModel { Name = "Петр", Phone = "89009998877" });

        var response = await _client.GetAsync("http://localhost:5005/notebook/search?query=Петр");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        Assert.Contains("Петр", content);
    }

    [Fact]
    public async Task SearchNonExistingEntry_ShouldReturnNotFound()
    {
        var response = await _client.GetAsync("http://localhost:5005/notebook/search?query=НесуществующийЧувак");
        Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task WrongClientToken_ShouldReturnUnauthorized()
    {
        var wrongClient = new HttpClient();
        wrongClient.DefaultRequestHeaders.Add("clientToken", "wrong-token");

        var response = await wrongClient.PostAsJsonAsync("http://localhost:5005/notebook/add", new EntryModel { Name = "Михаил", Phone = "89001234567" });

        Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task RingForwarding_ShouldWork()
    {
        await _client.PostAsJsonAsync("http://localhost:5005/notebook/add", new EntryModel { Name = "Сергей", Phone = "89006543210" });

        var anotherClient = new HttpClient();
        anotherClient.DefaultRequestHeaders.Add("clientToken", "client-secret");

        var response = await anotherClient.GetAsync("http://localhost:5001/notebook/search?query=Сергей"); // 5001 — порт Service1
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        Assert.Contains("Сергей", content);
    }
}
