namespace Agriculiture.BLL.Dtos;

public class AfforestationSearchRequest
{
    public DateOnly? FromDate { get; set; }
    public DateOnly? ToDate { get; set; }
    public int? SelectedUserId { get; set; }
    public int? SelectedLocationId { get; set; }
    public int? SelectedTreeId { get; set; }
}
