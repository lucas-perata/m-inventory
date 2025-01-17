using System;
using BACK.Data;
using BACK.Entities;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BACK.Endpoints.Items.Post;

public class PostItemsEndpoint : Endpoint<RequestDto, ResponseDto>
{
    public DataContext _context {get;set;}

    public override void Configure()
    {
        Post("/api/items");
        AllowAnonymous();
    }

    public override async Task HandleAsync(RequestDto req, CancellationToken ct)
    {
        Item item = new Item 
        {
            Name = req.Name,
            Description = req.Description,
            Category = req.Category,
            Quantity = req.Quantity,
            Unit = req.Unit, 
            Location = req.Location,
            MinThreshold = req.MinThreshold,
            LastUsed = req.LastUsed,
            Barcode = req.Barcode,
            ImageUrl = req.ImageUrl
        };

        var response = _context.Items.Add(item); 

        await _context.SaveChangesAsync(); 


        await SendAsync(new ()
        {
            Id = item.Id,
            Name = req.Name,
            Description = req.Description,
            Category = req.Category,
            Quantity = req.Quantity,
            Unit = req.Unit, 
            Location = req.Location,
            MinThreshold = req.MinThreshold,
            LastUsed = req.LastUsed,
            Barcode = req.Barcode,
            ImageUrl = req.ImageUrl
        });
    }
}
