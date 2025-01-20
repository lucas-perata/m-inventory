using System;
using MainService.Dtos;
using MainService.Endpoints.Projects.GetAll;
using MainService.Entities;
using MainService.Entities.Enums;

namespace MainService.Endpoints.Projects.Post;

public class RequestDto
{
    public string Name { get; set; }            
    public string Description { get; set; }            
    public DateTime? StartDate { get; set; } = DateTime.UtcNow;
    public DateTime? EndDate { get; set; }             
    public ProjectStatus Status { get; set; }          
    public string Notes { get; set; }                  
    public bool IsPrivate { get; set; } = true; 
    // NAV 
    public ICollection<ProjectItem> ProjectItems { get; set; } = new List<ProjectItem>(); 
}
