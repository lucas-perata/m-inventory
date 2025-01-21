using System;
using System.Security.Claims;
using FastEndpoints;
using MainService.Data;
using MainService.Dtos.Item;
using MainService.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace MainService.Endpoints.Inventory.GetAll;

public class GetInventoryEndpoint : Endpoint<RequestDto, ResponseDto>
{
    public DataContext _context { get; set; }
    [Authorize]
    public override void Configure()
    {
        Get("/api/inventory/get-all");
    }

    public override async Task HandleAsync(RequestDto req, CancellationToken ct)
    {
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        var skip = (req.Page - 1) * req.PageSize;

        var t = await _context.UserItems.ToListAsync();

        int totalItems = t.Count();

        var userItems = await _context.UserItems
            .Include(x => x.Item)
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.AddedAt)
            .Skip(skip)
            .Take(req.PageSize)
            .ToListAsync();

        if (userItems is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var response = new ResponseDto
        {
            TotalItems = totalItems,
            Page = req.Page,
            PageSize = req.Page,
            TotalPages = totalItems > 0 ? totalItems / req.PageSize : 0,
            Items = userItems.Select(userItem => new ItemInventoryDto
            {
                Id = userItem.ItemId,
                Name = userItem.Item.Name,
                Description = userItem.Item.Description,
                Category = userItem.Item.Category,
                Unit = userItem.Item.Unit,
                ImageUrl = userItem.Item.ImageUrl,
                SKU = userItem.Item.SKU,
                DefaultThreshold = userItem.Threshold,
                Quantity = userItem.Quantity
            }).ToList()
        };

        await SendAsync(response, cancellation: ct);
    }
}
