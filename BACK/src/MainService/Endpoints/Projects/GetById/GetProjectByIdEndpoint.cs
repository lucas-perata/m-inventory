using System;
using System.Security.Claims;
using FastEndpoints;
using MainService.Data;
using MainService.Dtos;
using MainService.Endpoints.Projects.GetAll;
using MainService.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace MainService.Endpoints.Projects.GetById;

public class GetProjectById : Endpoint<RequestDto, ResponseDto>
{
    public DataContext _context { get; set; }

    [Authorize]
    public override void Configure()
    {
        Get("/api/projects/{id}");
    }

    public override async Task HandleAsync(RequestDto req, CancellationToken ct)
    {
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        Project project = await _context.Projects.Include(x => x.ProjectItems)
                    .ThenInclude(x => x.Item)
                    .Where(x => x.Id == req.Id)
                    .FirstOrDefaultAsync();

        if (project is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        if (project.IsPrivate && userId != project.MakerId)
        {
            await SendUnauthorizedAsync(ct);
            return;
        }

        await SendAsync(new()
        {
        ResponseProject = new ProjectDto
          {
            Name = project.Name,
            MakerId = project.MakerId,
            Maker = project.Maker,
            Description = project.Description,
            CreatedAt = project.CreatedAt,
            StartDate = project.StartDate,
            EndDate = project.EndDate,
            Status = project.Status,
            Notes = project.Notes,
            IsPrivate = project.IsPrivate,
            ProjectItems = project.ProjectItems.Select(item => new ProjectItemDto
                {
                    ItemId = item.ItemId,
                    Quantity = item.Quantity,
                    ItemName = item.Item.Name
                }).ToList()
          }
        });
    }
}
