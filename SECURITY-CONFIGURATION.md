# Security configuration

The API requires a SQL Server connection string and a unique JWT signing key. Supply them through a secret store in production. For local development, set them using .NET User Secrets (replace the placeholders privately; do not commit actual values):

```sh
cd AgriBackend/Agriculture.API
dotnet user-secrets set 'ConnectionStrings:cs' '<SQL_SERVER_CONNECTION_STRING>'
dotnet user-secrets set 'Jwt:Key' '<PRIVATE_RANDOM_KEY_AT_LEAST_32_UTF8_BYTES>'
```

Alternatively use environment variables `ConnectionStrings__cs` and `Jwt__Key`. The existing issuer and audience settings remain in appsettings.json. EF design-time commands use `ConnectionStrings__cs` from the environment. Never put credentials in a shell history or committed file; use an approved secret management process.

The frontend uses `BackendApi:BaseUrl` (environment variable `BackendApi__BaseUrl`) to select the API; the default is HTTPS. Configure an HTTPS endpoint with a valid certificate. Login and authenticated requests use the same named client.

User administration and registration now require an existing administrator token. Provision an initial administrator using a controlled out-of-band process; no anonymous administrator bootstrap endpoint is provided. Publish profiles are ignored and remain local; configure deployment credentials securely outside Git.
