using System;
using BACK.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace BACK.Endpoints.Items.GetById;

public class GetItemByIdEndpoint : Endpoint<RequestDto, ResponseDto>
{
    public DataContext _context { get; set; }

    public override void Configure()
    {
        Get("/api/items/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(RequestDto req, CancellationToken ct)
    {
        var item = await _context.Items.FirstOrDefaultAsync(x => x.Id == req.Id);

        if (item == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendAsync(new()
        {
            Id = item.Id,
            Name = item.Name,
            Description = item.Description,
            Category = item.Category,
            Quantity = item.Quantity,
            Unit = item.Unit,
            Location = item.Location,
            MinThreshold = item.MinThreshold,
            LastUsed = item.LastUsed,
            Barcode = item.Barcode,
            ImageUrl = item.ImageUrl
        });

    }
}
