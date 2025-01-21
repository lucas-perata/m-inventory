using System;

namespace MainService.Endpoints.Inventory.Update;

public class ResponseDto
{
    public Guid ItemId { get; set; }
    public string ItemName { get; set; }
    public int Quantity { get; set; }
    public int Treshold { get; set; } 
    public DateTime LastUpdated { get; set; }
    public DateTime AddedAt { get; set; }
}
