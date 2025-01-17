using System;
using BACK.Entities.Enums;

namespace BACK.Endpoints.Items;

public class ResponseDto
{
    public Guid Id {get; set;}
    public string Name { get; set; }
    public string Description { get; set; }
    public ItemCategory Category { get; set; }
    public int Quantity { get; set; }
    public MeasurementUnit Unit { get; set; }
    public DateTime DateAcquired { get; set; } 
    public string Location { get; set; }
    public int MinThreshold { get; set; }
    public DateTime LastUsed { get; set; }
    public string Barcode { get; set; }
    public string ImageUrl { get; set; }
}
