using Newtonsoft.Json;

namespace RadioRecord.Models;

public class Station
{
    public int id { get; set; }
    public string prefix { get; set; }
    public string title { get; set; }
    public string tooltip { get; set; }
    public int sort { get; set; }
    public object bg_color { get; set; }
    public string bg_image { get; set; }
    public string bg_image_mobile { get; set; }
    public string svg_outline { get; set; }
    public string svg_fill { get; set; }
    public string pdf_outline { get; set; }
    public string pdf_fill { get; set; }
    public string short_title { get; set; }
    public string icon_gray { get; set; }
    public string icon_fill_colored { get; set; }
    public string icon_fill_white { get; set; }
    public object new_date { get; set; }
    public string stream_64 { get; set; }
    public string stream_128 { get; set; }
    public string stream_320 { get; set; }
    public string stream_hls { get; set; }
    public List<Genre> genre { get; set; }
    public string detail_page_url { get; set; }
    public string shareUrl { get; set; }
    public object mark { get; set; }
    public string updated { get; set; }
}