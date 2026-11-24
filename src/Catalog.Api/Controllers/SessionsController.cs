using Microsoft.AspNetCore.Mvc;
using Catalog.Api.Models;

namespace Catalog.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SessionsController : ControllerBase
{
    private static readonly Session[] Sessions =
    [
        new(1, "Фотопрогулка по ночному городу"),
        new(2, "Джаз на крыше"),
        new(3, "Кино под звёздами")
    ];

    [HttpGet]
    public ActionResult<Session[]> GetAll()
    {
        return Ok(Sessions);
    }

    [HttpGet("{id:int}")]
    public ActionResult<Session> GetByid(int id)
    {
        var session = Sessions.FirstOrDefault(s => s.Id == id);

        return session is null ? NotFound() : Ok(session);
    }
}
