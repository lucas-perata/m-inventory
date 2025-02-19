using System;
using System.ComponentModel.DataAnnotations.Schema;
using MainService.Entities.Enums;

namespace MainService.Entities;

[Table("Projects")]
public class Project
{
    public Guid Id { get; set; }               
    public string MakerId { get; set; }
    public string Maker {get; set;}
    public string Name { get; set; }            
    public string Description { get; set; }            
    public DateTime CreatedAt {get; set;} = DateTime.UtcNow;
    public DateTime? StartDate { get; set; } = DateTime.UtcNow;
    public DateTime? EndDate { get; set; }             
    public ProjectStatus Status { get; set; }          
    public string Notes { get; set; }                  
    public bool IsPrivate { get; set; } = true; 
    // NAV 
    public ICollection<ProjectItem> ProjectItems { get; set; } = new List<ProjectItem>(); 
}
