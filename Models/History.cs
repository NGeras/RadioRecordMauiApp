namespace RadioRecord.Models;

public class History
{
    public int id { get; set; }
    public string artist { get; set; }
    public string song { get; set; }
    public string image100 { get; set; }
    public string image200 { get; set; }
    public string image600 { get; set; }
    public string listenUrl { get; set; }
    public string itunesUrl { get; set; }
    public string itunesId { get; set; }
    public bool noFav { get; set; }
    public bool noShow { get; set; }
    public string shareUrl { get; set; }
    public int time { get; set; }
    public string time_formatted { get; set; }
}