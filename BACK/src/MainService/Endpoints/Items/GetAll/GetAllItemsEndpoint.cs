using System;
using MainService.Data;
using MainService.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace MainService.Endpoints.Items.GetAll;


public class GetAllItemsEndpoint : Endpoint<RequestDto, ResponseDto>
{
    public DataContext _context {get; set;}
    public override void Configure()
    {
        Get("/api/items");
        AllowAnonymous();
    }

    public override async Task HandleAsync(RequestDto req, CancellationToken ct)
    {
        var skip = (req.Page - 1) * req.PageSize;

        var t = await _context.Items.ToListAsync();

        int totalItems = t.Count();

        var items = await _context.Items
            .OrderByDescending(x => x.Name)
            .Skip(skip)
            .Take(req.PageSize)
            .ToListAsync();

        if (items is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var response = new ResponseDto
        {
            TotalItems = totalItems,
            Page = req.Page,
            PageSize = req.PageSize,
            TotalPages =totalItems / req.PageSize,
            Items = items.Select(item => new Item
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Category = item.Category,
                Unit = item.Unit,
                ImageUrl = item.ImageUrl,
                SKU = item.SKU,
            }).ToList()
        };

        await SendAsync(response, cancellation: ct);
    }
}

