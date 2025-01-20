using System;
using MainService.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace api.Endpoints.Items.Update;

public class UpdateItemEndpoint : Endpoint<RequestDto, ResponseDto>
{
    public DataContext _context { get; set; }

    [Authorize]
    public override void Configure()
    {
        Put("api/items/{id}");
    }

    public override async Task HandleAsync(RequestDto req, CancellationToken ct)
    {
        var item = await _context.Items.FirstOrDefaultAsync(x => x.Id == req.Id);

        if (item is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }


        item.Name = req.Name ?? item.Name;
        item.Description = req.Description ?? item.Description;
        item.Category = req.Category.Equals(item.Category) ? item.Category : req.Category;
        item.Unit = req.Unit.Equals(item.Unit) ? item.Unit : req.Unit;
        item.ImageUrl = req.ImageUrl ?? item.ImageUrl;
        item.SKU = req.SKU ?? item.SKU;

        var response = _context.Items.Update(item);

        await _context.SaveChangesAsync();

        await SendAsync(new()
        {
            Id = item.Id,
            Name = item.Name,
            Description = item.Description,
            Category = item.Category,
            Unit = item.Unit,
            ImageUrl = item.ImageUrl,
            SKU = item.SKU,
            DefaultThreshold = item.DefaultThreshold,
        });
    }
}
