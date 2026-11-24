using System.ComponentModel.DataAnnotations;

namespace Catalog.Api.Contracts.Sessions;

public record CreateSessionRequest([Required] string Title);
