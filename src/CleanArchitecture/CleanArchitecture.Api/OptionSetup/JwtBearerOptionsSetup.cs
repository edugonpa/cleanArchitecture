using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

namespace CleanArchitecture.Api.OptionsSetup;

public class JwtBearerOptionsSetup : IConfigureNamedOptions<JwtBearerOptions>
{
    public void Configure(string? name, JwtBearerOptions options)
    {
        throw new NotImplementedException();
    }

    public void Configure(JwtBearerOptions options)
    {
        throw new NotImplementedException();
    }
}