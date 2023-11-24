using System.Text.Json.Serialization;

namespace RadioRecord.Models;

public class ResultItem
{
    public int id { get; set; }
    public Track track { get; set; }
    [JsonIgnore] public Station Station { get; set; }
}