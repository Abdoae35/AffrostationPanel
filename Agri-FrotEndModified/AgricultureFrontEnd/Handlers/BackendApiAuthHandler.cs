using System.Net.Http.Headers;

namespace AgricultureFrontEnd.Handlers;

/// <summary>
/// DelegatingHandler that reads the JWT token from the user's session
/// and attaches it as a Bearer token to all outgoing HTTP requests to the backend API.
/// </summary>
public class BackendApiAuthHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public BackendApiAuthHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var context = _httpContextAccessor.HttpContext;
        var token = context?.Session.GetString("JWToken");

        if (!string.IsNullOrEmpty(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
