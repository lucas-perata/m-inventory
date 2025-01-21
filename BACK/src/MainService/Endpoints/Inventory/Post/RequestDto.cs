using System;

namespace MainService.Endpoints.Inventory.Post;

public class RequestDto
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }
    public int Treshold {get; set;}
}
