using System;

namespace MainService.Endpoints.Projects.GetAll;

public class ProjectItemDto
{
    public Guid ItemId { get; set; }
    public string ItemName { get; set; }
    public int Quantity { get; set; }
}
