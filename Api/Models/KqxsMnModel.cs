namespace Api.Models;

public class KqxsMnModel
{
    public string NgayQuay { get; set; }
    public List<KqxsModel> Datas { get; set; } = new List<KqxsModel>();
}