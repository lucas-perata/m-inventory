using System;
using BACK.Data;
using BACK.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace BACK.Endpoints.Items.Delete;

public class DeleteItemEndpoint : Endpoint<RequestDto, ResponseDto>
{
    public DataContext _context {get; set;}

    public override void Configure()
    {
        Delete("api/items/{id}");
        AllowAnonymous();
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
