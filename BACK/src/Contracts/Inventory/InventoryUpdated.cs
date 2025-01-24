using System;

namespace Contracts;

public class InventoryUpdated
{
    public Guid ItemId { get; set; }
    public string ItemName { get; set; }
    public int Quantity { get; set; }
    public int Threshold { get; set; }
    public DateTime LastUpdated { get; set; }
    public DateTime AddedAt { get; set; }
}
