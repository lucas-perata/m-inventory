using System;
using MainService.Data;
using MainService.Entities;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.OpenApi.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace MainService.Endpoints.Items.Post;

public class PostItemsEndpoint : Endpoint<RequestDto, ResponseDto>
{
    public DataContext _context {get;set;}

    [Authorize]
    public override void Configure()
    {
        Post("/api/items");
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
