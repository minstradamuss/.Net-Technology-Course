using System.Collections.Generic;

public class NotebookService
{
    private readonly List<(string Name, string Phone)> _entries = new();
    public List<(string Name, string Phone)> Entries => _entries;
}
