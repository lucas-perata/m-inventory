using System;
using System.ComponentModel.DataAnnotations.Schema;
using BACK.Entities.Enums;

namespace BACK.Entities;

[Table("Projects")]
public class Project
{
    public Guid Id { get; set; }               
    public string ProjectName { get; set; }            
    public string Description { get; set; }            
    public DateTime StartDate { get; set; } = DateTime.UtcNow;
    public DateTime? EndDate { get; set; }             
    public ProjectStatus Status { get; set; }          
    public string Notes { get; set; }                  

    // NAV 
    public ICollection<ProjectItem> ProjectItems { get; set; } = new List<ProjectItem>(); 
}
