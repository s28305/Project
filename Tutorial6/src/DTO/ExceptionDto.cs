using System.Net;

namespace Tutorial6.DTO;

public record ExceptionDto(HttpStatusCode StatusCode, string Description);