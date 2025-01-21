using System;
using System.Security.Claims;
using FastEndpoints;
using MainService.Data;
using MainService.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace MainService.Endpoints.Inventory.Update;

public class UpdateItemInventoryEndpoint : Endpoint<RequestDto, ResponseDto>
{
    public DataContext _context { get; set; }

    [Authorize]
    public override void Configure()
    {
        Put("/api/inventory/update/{id}");
    }

    public override async Task HandleAsync(RequestDto req, CancellationToken ct)
    {
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        UserItem userItem = await _context.UserItems.Where(x => x.UserId == userId && x.Id == req.Id).FirstOrDefaultAsync();

        if(userItem is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        userItem.Quantity = userItem.Quantity != req.Quantity ? req.Quantity : userItem.Quantity;
        userItem.Treshold = userItem.Treshold != req.Treshold ? req.Treshold : userItem.Treshold;
        userItem.LastUpdated = DateTime.UtcNow;

        _context.Update(userItem);
        await _context.SaveChangesAsync();

        userItem = await _context.UserItems.Include(x => x.Item).Where(x => x.Id == req.Id).FirstOrDefaultAsync();

        await SendAsync(new()
        {
            ItemId = userItem.ItemId,
            ItemName = userItem.Item.Name, 
            Quantity = userItem.Quantity, 
            Treshold = userItem.Treshold,
            LastUpdated = userItem.LastUpdated,
            AddedAt = userItem.AddedAt
        });
    }
}

