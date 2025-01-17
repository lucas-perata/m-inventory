using System;

namespace BACK.Entities;

public class ProjectItem
{
    public Guid ProjectItemId { get; set; }    
    public Guid ProjectId { get; set; }        
    public Project Project { get; set; }       
    public Guid ItemId { get; set; }           
    public Item Item { get; set; }             
    public int Quantity { get; set; }          
}
