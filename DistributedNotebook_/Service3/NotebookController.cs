using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

[ApiController]
[Route("[controller]")]
public class NotebookController : ControllerBase
{
    private readonly NotebookService _notebook;
    private readonly RingManager _ring;
    private readonly HttpClient _http;

    public NotebookController(NotebookService notebook, RingManager ring)
    {
        _notebook = notebook;
        _ring = ring;
        _http = new HttpClient();
    }

    [HttpPost("add")]
    public IActionResult AddEntry([FromHeader] string clientToken, [FromBody] EntryModel model)
    {
        if (!ValidateClient(clientToken))
            return Unauthorized();

        _notebook.Entries.Add((model.Name, model.Phone));
        return Ok("Added");
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromHeader] string clientToken, [FromQuery] string query, [FromQuery] int hop = 0)
    {
        if (!ValidateClient(clientToken))
            return Unauthorized();

        var entry = _notebook.Entries.FirstOrDefault(x => x.Name == query || x.Phone == query);
        if (entry != default)
            return Ok(entry);

        if (hop >= 5)
            return NotFound("Not found");

        var nextUrl = _ring.RightNeighborUrl + "/notebook/search?query=" + query + "&hop=" + (hop + 1);
        _http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _ring.RightToken);
        var response = await _http.GetAsync(nextUrl);
        var result = await response.Content.ReadAsStringAsync();
        return StatusCode((int)response.StatusCode, result);
    }

    private bool ValidateClient(string token)
    {
        return token == "client-secret" || token == _ring.LeftToken || token == _ring.RightToken;
    }
}
