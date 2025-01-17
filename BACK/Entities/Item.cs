using System;
using System.ComponentModel.DataAnnotations.Schema;
using BACK.Entities.Enums;

namespace BACK.Entities;

[Table("Items")]
public class Item
{
    public Guid Id { get; set; }             
    public string Name { get; set; }              
    public string Description { get; set; }       
    public ItemCategory Category { get; set; }          
    public int Quantity { get; set; }             
    public MeasurementUnit Unit { get; set; }              
    public string Location { get; set; }          
    public int MinThreshold { get; set; }         
    public DateTime DateAcquired { get; set; } = DateTime.UtcNow; 
    public DateTime LastUsed { get; set; }       
    public string Barcode { get; set; }           
    public string ImageUrl { get; set; }          

    // NAV 
    public ICollection<ProjectItem> ProjectItems { get; set; } = new List<ProjectItem>(); 

}
