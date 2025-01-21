using System;
using System.Security.Claims;
using FastEndpoints;
using MainService.Data;
using MainService.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace MainService.Endpoints.Inventory.Post;

public class AddItemToInventoryEndpoint : Endpoint<RequestDto, ResponseDto>
{
    public DataContext _context {get; set;}

    [Authorize]
    public override void Configure()
    {
        Post("/api/inventory/items/add/{id}");
    }

    public override async Task HandleAsync(RequestDto req, CancellationToken ct)
    {
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        Item item = await _context.Items.FirstOrDefaultAsync(x => x.Id == req.Id);

        if (item is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var userItem = new UserItem
        {
            UserId = userId,
            ItemId = item.Id,
            Quantity = req.Quantity,
            Treshold = req.Treshold > item.DefaultThreshold ? req.Treshold : item.DefaultThreshold
        };

        _context.UserItems.Add(userItem);

        await _context.SaveChangesAsync(); 

        userItem = await _context.UserItems.Include(x => x.Item).Where(x => x.ItemId == req.Id).FirstOrDefaultAsync();

        await SendAsync(new()
        {
            ItemId = userItem.ItemId,
            ItemName = userItem.Item.Name,
            Quantity = userItem.Quantity,
            Treshold = userItem.Treshold,
            AddedAt = userItem.AddedAt
        });
    }

}
