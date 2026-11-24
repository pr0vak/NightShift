using System.ComponentModel.DataAnnotations;

namespace Catalog.Api.Contracts.Sessions;

public record CreateOrUpdateSessionRequest([Required] string Title);
