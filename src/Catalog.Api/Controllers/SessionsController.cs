using Microsoft.AspNetCore.Mvc;
using Catalog.Api.Models;
using Catalog.Api.Contracts.Sessions;

namespace Catalog.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SessionsController : ControllerBase
{
    private static readonly List<Session> Sessions =
    [
        new(1, "Фотопрогулка по ночному городу"),
        new(2, "Джаз на крыше"),
        new(3, "Кино под звёздами")
    ];

    private static readonly object Sync = new();
    private static int lastId = Sessions.Max(s => s.Id);

    [HttpGet]
    public ActionResult<Session[]> GetAll()
    {
        lock (Sync)
        {
            return Ok(Sessions.ToArray());
        }
    }

    [HttpGet("{id:int}")]
    public ActionResult<Session> GetByid(int id)
    {
        lock (Sync)
        {
            var session = Sessions.FirstOrDefault(s => s.Id == id);
            return session is null ? NotFound() : Ok(session);
        }
    }

    [HttpPost]
    public ActionResult<Session> Create(CreateOrUpdateSessionRequest request)
    {
        Session session;
        lock (Sync)
        {
            session = new Session(++lastId, request.Title.Trim());
            Sessions.Add(session);
        }

        return CreatedAtAction(nameof(GetByid), new { id = session.Id }, session);
    }

    [HttpPut("{id:int}")]
    public IActionResult Update(int id, CreateOrUpdateSessionRequest request)
    {
        lock (Sync)
        {
            var index = Sessions.FindIndex(session => session.Id == id);
            if (index == -1)
            {
                return NotFound();
            }

            Sessions[index] = Sessions[index] with { Title = request.Title.Trim() };
        }

        return NoContent();
    }
}
