using System;
using MainService.Endpoints.Projects.GetAll;
using MainService.Entities;

namespace MainService.Services.UserItem;

public interface IUserItemService
{
    public Task<(List<ProjectItemAnalysis> ownedItems, List<ProjectItem> unownedItems, List<ProjectItemAnalysis> insufficientItems)> AnalyzeProjectItemsAsync(List<ProjectItem> projectItems, string userId);
}
