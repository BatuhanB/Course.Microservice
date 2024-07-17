using Course.Web.Models;
using Course.Web.Services.Interfaces;
using IdentityModel.AspNetCore.AccessTokenManagement;
using IdentityModel.Client;
using Microsoft.Extensions.Options;
using System.Net.Http;

namespace Course.Web.Services;

public class ClientCredentialTokenService : IClientCredentialTokenService
{
    private readonly ServiceApiSettings _serviceApiSettings;
    private readonly ClientSettings _clientSettings;
    private readonly IClientAccessTokenCache _clientAccessTokenCache;
    private readonly HttpClient _httpClient;

    public ClientCredentialTokenService(IOptions<ServiceApiSettings> serviceApiSettings,
        IOptions<ClientSettings> clientSettings,
        IClientAccessTokenCache clientAccessTokenCache,
        HttpClient httpClient)
    {
        _serviceApiSettings = serviceApiSettings.Value;
        _clientSettings = clientSettings.Value;
        _clientAccessTokenCache = clientAccessTokenCache;
        _httpClient = httpClient;
    }

    public async Task<string> GetToken()
    {
        var discovery = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
        {
            Address = _serviceApiSettings.IdentityUrl,
            Policy = new DiscoveryPolicy { RequireHttps = false }
        });

        if (discovery.IsError)
        {
            throw discovery.Exception;
        }

        var paramss = new ClientAccessTokenParameters();
        var currentToken = await _clientAccessTokenCache.GetAsync("WebClientToken", paramss);

        if (currentToken != null)
        {
            return currentToken.AccessToken;
        }

        var tokenRequest = new ClientCredentialsTokenRequest
        {
            ClientId = _clientSettings.WebClient.ClientId,
            ClientSecret = _clientSettings.WebClient.ClientSecret,
            Address = discovery.TokenEndpoint
        };

        var newToken = await _httpClient.RequestClientCredentialsTokenAsync(tokenRequest);

        if (newToken.IsError)
        {
            throw newToken.Exception;
        }

        await _clientAccessTokenCache.SetAsync("WebClientToken", newToken.AccessToken, newToken.ExpiresIn, paramss);

        return newToken.AccessToken;
    }
}
