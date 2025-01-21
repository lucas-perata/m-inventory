using System;
using MainService.Data;
using MainService.Endpoints.Projects.GetAll;
using MainService.Entities;
using Microsoft.EntityFrameworkCore;

namespace MainService.Services.UserItem;

public class UserItemService : IUserItemService
{
    private readonly DataContext _context;

    public UserItemService(DataContext context)
    {
        _context = context;
    }
    public async Task<(List<ProjectItemAnalysis> ownedItems, List<ProjectItem> unownedItems, List<ProjectItemAnalysis> insufficientItems)> AnalyzeProjectItemsAsync(List<ProjectItem> projectItems, string userId)
    {
        var userItems = await _context.UserItems
                .Include(x => x.Item)
                .Where(ui => ui.UserId == userId)
                .ToListAsync();

        var userItemsDict = userItems.ToDictionary(x => x.ItemId, y => y.Quantity);


        var ownedItems = projectItems
            .Where(pi => userItemsDict.ContainsKey(pi.ItemId))
            .Select(pi => new ProjectItemAnalysis 
            { 
                Item = pi,
                OwnedQuantity = userItemsDict[pi.ItemId],
                RequiredQuantity = pi.Quantity
            })
            .ToList();

        var unownedItems = projectItems
            .Where(pi => !userItemsDict.ContainsKey(pi.ItemId))
            .ToList();

        var insufficientItems = projectItems
            .Where(pi => userItemsDict.TryGetValue(pi.ItemId, out int quantityOwned) && quantityOwned < pi.Quantity)
            .Select(pi => new ProjectItemAnalysis
            {
                Item = pi,
                OwnedQuantity = userItemsDict[pi.ItemId],
                RequiredQuantity = pi.Quantity
            })
            .ToList();

        return (ownedItems, unownedItems, insufficientItems);
    }
}

public record ProjectItemAnalysis
{
    public ProjectItem Item { get; init; }
    public int OwnedQuantity { get; init; }
    public int RequiredQuantity { get; init; }
}