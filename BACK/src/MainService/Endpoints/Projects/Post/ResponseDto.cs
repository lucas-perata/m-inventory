using System;
using MainService.Dtos;
using MainService.Dtos.Item;
using MainService.Entities;

namespace MainService.Endpoints.Projects.Post;

public class ResponseDto
{
    public ProjectDto Project { get; set; }
    public List<OwnedItemDto> OwnedItems { get; set; } 
    public List<UnownedItemDto> UnownedItems { get; set; } 
    public List<InsufficientItemsDto> InsufficientItems {get; set;}
}
