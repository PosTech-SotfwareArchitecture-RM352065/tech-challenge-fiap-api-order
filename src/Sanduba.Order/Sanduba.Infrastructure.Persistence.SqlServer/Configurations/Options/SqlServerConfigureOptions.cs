using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Sanduba.Infrastructure.Persistence.SqlServer.Configurations.Options
{
    public class SqlServerConfigureOptions(IConfiguration configuration) : IConfigureOptions<SqlServerOptions>
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly string _connectionStringKey = "ConnectionStrings:OrderDatabase:Value";

        public void Configure(SqlServerOptions options)
        {
            options.ConnectionString = _configuration.GetValue<string>(_connectionStringKey) ?? string.Empty;
        }
    }
}
