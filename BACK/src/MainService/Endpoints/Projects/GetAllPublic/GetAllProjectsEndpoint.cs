using System;
using System.Security.Claims;
using FastEndpoints;
using MainService.Data;
using MainService.Dtos;
using MainService.Entities;
using Microsoft.EntityFrameworkCore;

namespace MainService.Endpoints.Projects.GetAll;

public class GetAllProjectsEndpoint : Endpoint<RequestDto, ResponseDto>
{
    public DataContext _context { get; set; }

    public override void Configure()
    {
        Get("/api/projects/get-all");
        AllowAnonymous();
    }

    public override async Task HandleAsync(RequestDto req, CancellationToken ct)
    {
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        int skip = (req.Page - 1) * req.PageSize;

        var p = await _context.Projects.Where(x => x.IsPrivate == false).ToListAsync();

        int totalItems = p.Count();

        List<Project> projects = await _context.Projects.Include(x => x.ProjectItems)
        .ThenInclude(x => x.Item)
        .Where(x => x.IsPrivate == false)
        .OrderByDescending(y => y.Name)
        .Skip(skip)
        .Take(req.PageSize)
        .ToListAsync();

        if (totalItems < 1)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var response = new ResponseDto
        {
            TotalItems = totalItems,
            Page = req.Page,
            PageSize = req.PageSize,
            TotalPages = (int)Math.Ceiling(totalItems / (double)req.PageSize),
            Projects = projects.Select(project => new ProjectDto
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
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
            }).ToList()
        };

        await SendAsync(response, cancellation: ct);
    }
}
