namespace TrucTiepKetQua.net.Shared.Models;

public class CountryModel
{
    public string Country { get; set; }
    public string Season { get; set; }
    public List<MatchModel> MatchLives { get; set; }
}