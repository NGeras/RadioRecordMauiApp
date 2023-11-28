using System.Text.Json.Serialization;
using RadioRecord.MarkupExtensions;

namespace RadioRecord.Models;

public class Track
{
    public int id { get; set; }
    public string artist { get; set; }
    public string song { get; set; }
    public string image100 { get; set; }
    public string image200 { get; set; }
    public string image600 { get; set; }
    public string listenUrl { get; set; }
    public string itunesUrl { get; set; }
    [JsonConverter(typeof(NullableIntConverter))]
    public int? itunesId { get; set; }
    public bool noFav { get; set; }
    public bool noShow { get; set; }
    public string shareUrl { get; set; }
}