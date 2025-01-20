using System;
using MainService.Dtos;
using MainService.Entities.Enums;

namespace MainService.Endpoints.Projects.Update;

public class ResponseDto
{
    public Guid Id { get; set; }
    public string MakerId { get; set; }
    public string Maker { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? StartDate { get; set; } = DateTime.UtcNow;
    public DateTime? EndDate { get; set; }
    public ProjectStatus Status { get; set; }
    public string Notes { get; set; }
    public bool IsPrivate { get; set; } = true;
}
