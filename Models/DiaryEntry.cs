namespace MyWebApp.Models;

public class DiaryEntry
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Content { get; set; } = string.Empty;
}