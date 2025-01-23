using System;

namespace MainService.Endpoints.Projects.GetAll;

public class RequestDto
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 4;
}
