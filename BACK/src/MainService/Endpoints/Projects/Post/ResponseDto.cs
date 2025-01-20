using System;
using MainService.Dtos;
using MainService.Entities;

namespace MainService.Endpoints.Projects.Post;

public class ResponseDto
{
    public ProjectDto Project { get; set; }
}
