namespace RadioRecord.Models;

public class Result
{
    public List<ResultItem> tags { get; set; }
    public List<ResultItem> track { get; set; }
    public List<ResultItem> genre { get; set; }
    public List<Station> stations { get; set; }
    public List<History> history { get; set; }

}