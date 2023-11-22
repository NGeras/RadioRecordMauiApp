using Microsoft.Maui.Controls;

namespace RadioRecord.Models;

public class Result
{
    public List<Tag> tags { get; set; }
    public List<Genre> genre { get; set; }
    public List<Station> stations { get; set; }
}