using System;
using System.Security.Claims;
using FastEndpoints;
using MainService.Data;
using MainService.Dtos;
using MainService.Endpoints.Projects.GetAll;
using MainService.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace MainService.Endpoints.Projects.Post;

public class PostProjectEndpoint : Endpoint<RequestDto, ResponseDto>
{
    public DataContext _context { get; set; }

    [Authorize]
    public override void Configure()
    {
        Post("/api/projects/create");
    }

    public override async Task HandleAsync(RequestDto req, CancellationToken ct)
    {
        Project project = new Project
        {
            Name = req.Name,
            Maker = User.Identity.Name,
            MakerId = User.FindFirstValue(ClaimTypes.NameIdentifier),
            Description = req.Description,
            StartDate = req.StartDate,
            EndDate = req.EndDate,
            Status = req.Status,
            Notes = req.Notes,
            IsPrivate = req.IsPrivate,
        };

        _context.Projects.Add(project);
        await _context.SaveChangesAsync();

        Project newProject = await _context.Projects
                .OrderByDescending(x => x.CreatedAt)
                .Where(x => x.MakerId == User.FindFirstValue(ClaimTypes.NameIdentifier))
                .FirstOrDefaultAsync();

        var projectItems = req.ProjectItems.Select(pi => new ProjectItem
        {
            ProjectId = newProject.Id,
            ItemId = pi.ItemId,
            Quantity = pi.Quantity
        }).ToList();

        _context.ProjectItems.AddRange(projectItems);
        await _context.SaveChangesAsync();

        newProject = await _context.Projects
            .Include(x => x.ProjectItems)
            .ThenInclude(x => x.Item)
            .FirstOrDefaultAsync(x => x.Id == newProject.Id);

        var response = new ResponseDto
        {
            Project = new ProjectDto
            {
                Id = newProject.Id,
                Name = req.Name,
                Maker = newProject.Maker,
                MakerId = newProject.MakerId,
                Description = req.Description,
                StartDate = req.StartDate,
                EndDate = req.EndDate,
                CreatedAt = newProject.CreatedAt,
                Status = req.Status,
                Notes = req.Notes,
                IsPrivate = req.IsPrivate,
                ProjectItems = newProject.ProjectItems.Select(pi => new ProjectItemDto
                {
                    ItemId = pi.ItemId,
                    ItemName = pi.Item?.Name,
                    Quantity = pi.Quantity
                }).ToList()
            }
        };
        await SendAsync(response, cancellation: ct);
    }
}
