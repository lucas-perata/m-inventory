using System;
using MainService.Endpoints.Projects.GetAll;
using MainService.Entities.Enums;

namespace MainService.Dtos;

public class ProjectDto
{
    public Guid Id { get; set; }               
    public string Name { get; set; }            
    public string MakerId { get; set; }
    public string Maker {get; set;}
    public string Description { get; set; }            
    public DateTime CreatedAt { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }             
    public ProjectStatus Status { get; set; }          
    public string Notes { get; set; }                  
    public bool IsPrivate { get; set; } = true; 
    // NAV 
    public ICollection<ProjectItemDto> ProjectItems { get; set; } = new List<ProjectItemDto>(); 
}
