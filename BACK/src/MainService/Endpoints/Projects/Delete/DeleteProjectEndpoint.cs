using System;
using System.Security.Claims;
using FastEndpoints;
using MainService.Data;
using MainService.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace MainService.Endpoints.Projects.Delete;

public class DeleteProjectEndpoint : Endpoint<RequestDto, ResponseDto>
{
    public DataContext _context { get; set; }

    [Authorize]
    public override void Configure()
    {
        Delete("/api/projects/delete/{id}");
    }

    public override async Task HandleAsync(RequestDto req, CancellationToken ct)
    {
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var project = await _context.Projects.Where(x => x.Id == req.Id && x.MakerId == userId).FirstOrDefaultAsync();

        if (project is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        if (project.MakerId != userId)
        {
            await SendUnauthorizedAsync(ct);
            return;
        }

        _context.Remove(project);

        if (await _context.SaveChangesAsync() > 0)
        {
            await SendAsync(new()
            {
                Message = "Project deleted: " + req.Id
            });
        }
    }
}
