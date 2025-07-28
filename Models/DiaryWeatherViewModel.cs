namespace MyWebApp.Models;

public class DiaryWeatherViewModel
{
    public List<DiaryEntry> Diaries { get; set; } = new List<DiaryEntry>();
    public WeatherInfo? Weather { get; set; }
}