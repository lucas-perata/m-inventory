using System;
using System.Security.Claims;
using FastEndpoints;
using MainService.Data;
using MainService.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace MainService.Endpoints.Projects.Update;

public class UpdateProjectEndpoint : Endpoint<RequestDto, ResponseDto>
{
    public DataContext _context { get; set; }

    [Authorize]
    public override void Configure()
    {
        Put("api/projects/update/{id}");
    }

    public override async Task HandleAsync(RequestDto req, CancellationToken ct)
    {
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        Project project = await _context.Projects.Where(x => x.Id == req.Id).FirstOrDefaultAsync();

        if(project is null)
        {
            await SendNotFoundAsync(ct);
            return; 
        }

        if(project.MakerId != userId)
        {
            await SendUnauthorizedAsync(ct);
            return;
        }

        project.Name = req.Name ?? project.Name; 
        project.Status = project.Status == req.Status ? project.Status : req.Status; 
        project.IsPrivate = project.IsPrivate == req.IsPrivate ? project.IsPrivate : req.IsPrivate;
        project.EndDate = project.EndDate ?? req.EndDate;
        project.Notes = req.Notes ?? project.Notes;
        project.Description = req.Description ?? project.Description;

        _context.Projects.Update(project);

        await SendAsync(new()
        {
            Id = project.Id,
            Name = project.Name, 
            Maker = project.Maker, 
            MakerId = project.MakerId, 
            Status = project.Status ,
            IsPrivate = project.IsPrivate,  
            EndDate = project.EndDate,  
            Description = project.Description,
            Notes = project.Notes
        });
    }
}
