namespace Carlton.Base.Infrastructure.Data.Connections;

public interface IDbConnectionFactory
{
    IDbConnection Create();
};

