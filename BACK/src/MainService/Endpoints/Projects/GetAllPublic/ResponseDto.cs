using System;
using MainService.Dtos;
using MainService.Entities;
using MainService.Entities.Enums;

namespace MainService.Endpoints.Projects.GetAll;

public class ResponseDto
{
    public int TotalItems { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public List<ProjectDto> Projects { get; set; }
}
