using System;
using BACK.Data;
using BACK.Entities;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.OpenApi.Extensions;

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
            Unit = req.Unit, 
            ImageUrl = req.ImageUrl,
            SKU = req.SKU
        };

        var response = _context.Items.Add(item); 

        await _context.SaveChangesAsync(); 


        await SendAsync(new ()
        {
            Id = item.Id,
            Name = req.Name,
            Description = req.Description,
            Category = req.Category,
            Unit = req.Unit, 
            ImageUrl = req.ImageUrl,
            SKU = req.SKU,
            DefaultThreshold = item.DefaultThreshold,
        });
    }
}
