using System;

namespace MainService.Endpoints.Inventory.Update;

public class RequestDto
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }
    public int Threshold { get; set; }
}
