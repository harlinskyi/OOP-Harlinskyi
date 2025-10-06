namespace Lab5v7.Models;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;

    public override string ToString() => $"{Title} ({Author})";
}