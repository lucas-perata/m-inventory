using System;
using Microsoft.AspNetCore.Identity;

namespace MainService.Entities;

public class UserItem
{
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public Guid ItemId { get; set; }
    public Item Item { get; set; }
    public int Quantity { get; set; }
    public int Threshold { get; set; } = 1; 
    public DateTime AddedAt { get; set; } = DateTime.UtcNow;
    public DateTime LastUpdated {get; set;} = DateTime.UtcNow;
}
