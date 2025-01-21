using System;
using MainService.Entities;

namespace MainService.Endpoints.Inventory.Post;

public class ResponseDto
{
    public Guid ItemId { get; set; }
    public string ItemName { get; set; }
    public int Quantity { get; set; }
    public int Treshold { get; set; } = 1; 
    public DateTime AddedAt { get; set; } = DateTime.UtcNow;
}
