using System;
using FastEndpoints;
using MainService.Dtos;
using MainService.Entities;
using MainService.Entities.Enums;

namespace MainService.Endpoints.Projects.Update;

public class RequestDto
{
    public Guid Id { get; set; }               
    public string Name { get; set; }            
    public string Description { get; set; }            
    public DateTime? EndDate { get; set; }             
    public ProjectStatus Status { get; set; }          
    public string Notes { get; set; }                  
    public bool IsPrivate { get; set; }
}
