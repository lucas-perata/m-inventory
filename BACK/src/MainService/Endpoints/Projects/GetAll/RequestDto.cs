using System;

namespace MainService.Endpoints.Projects.GetAll;

public class RequestDto
{
    public int Page { get; set; }
    public int PageSize { get; set; }
}
