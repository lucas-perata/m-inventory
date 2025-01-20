using System;
using MainService.Data;
using MainService.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace MainService.Endpoints.Items.Delete;

public class DeleteItemEndpoint : Endpoint<RequestDto, ResponseDto>
{
    public DataContext _context {get; set;}

    [Authorize]
    public override void Configure()
    {
        Delete("api/items/{id}");
    }

    public override async Task HandleAsync(RequestDto req, CancellationToken ct)
    {
        Item item = await _context.Items.Where(x => x.Id == req.Id).FirstOrDefaultAsync();

        if(item is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        
        _context.Remove(item);

        if (await _context.SaveChangesAsync() > 0 )
        {
            await SendAsync(new()
            {
                Message = "Item deleted: " + req.Id
            });
        }
    }
}
