namespace RadioRecord.Models;

public class TrackInfo
{
    public string artist { get; set; }
    public string song { get; set; }
    public string image100 { get; set; }
    public string image200 { get; set; }
    public string image600 { get; set; }
    public string listenUrl { get; set; }
    public string itunesUrl { get; set; }
    public int itunesId { get; set; }
    public bool noFav { get; set; }
    public bool noShow { get; set; }
    public string shareUrl { get; set; }
}