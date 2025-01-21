using System;
using MainService.Entities;

namespace MainService.Endpoints.Inventory.GetAll;

public class RequestDto
{
    public int TotalItems { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public List<UserItem> UserItems { get; set; }
}
