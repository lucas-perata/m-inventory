using System;
using System.Security.Claims;
using FastEndpoints;
using MainService.Data;
using MainService.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace MainService.Endpoints.Inventory.Delete;

public class DeleteItemInventoryEndpoint : Endpoint<RequestDto, ResponseDto>
{
    public DataContext _context { get; set; }

    [Authorize]
    public override void Configure()
    {
        Delete("/api/inventory/delete/{id}");
    }

    public override async Task HandleAsync(RequestDto req, CancellationToken ct)
    {
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        UserItem userItem = await _context.UserItems
                .Where(x => x.UserId == userId && x.ItemId == req.Id)
                .FirstOrDefaultAsync();
        
        if (userItem is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        _context.Remove(userItem);
        bool response = await _context.SaveChangesAsync() > 0;

        if(!response)
        {
            await SendErrorsAsync();
            return;
       }

       await SendAsync(new()
       {
        Message = "Item removed from inventory: " + req.Id,
       });
    }
}
