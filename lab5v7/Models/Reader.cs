namespace Lab5v7.Models;

public class Reader
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public override string ToString() => Name;
}