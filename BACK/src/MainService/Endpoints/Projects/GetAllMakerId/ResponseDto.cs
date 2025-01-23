using System;
using MainService.Dtos;

namespace MainService.Endpoints.Projects.GetAllMakerId;

public class ResponseDto
{
    public int TotalItems { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public List<ProjectDto> Projects { get; set; }
}
