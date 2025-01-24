using System;

namespace Contracts;

public class ProjectCreated
{
    public ProjectDto Project { get; set; }
}

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
}

public enum ProjectStatus
{
    Planificado,
    EnProgreso,
    Completado,
    Cancelado
}

