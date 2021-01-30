using System.Data;

namespace Carlton.Base.Infrastructure.Data.Connections
{
    public interface IDbConnectionFactory
    {
        IDbConnection Create();
    }
}
