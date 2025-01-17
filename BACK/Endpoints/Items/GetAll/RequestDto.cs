using System;

namespace BACK.Endpoints.Items.GetAll;

public class RequestDto
{
    public int Page { get; set; }
    public int PageSize { get; set; }
}
