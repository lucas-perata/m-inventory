using System;

namespace Contracts.Inventory;

public class InventoryCreated
{
    public Guid ItemId { get; set; }
    public string ItemName { get; set; }
    public int Quantity { get; set; }
    public int Treshold { get; set; }
    public DateTime AddedAt { get; set; }
}
