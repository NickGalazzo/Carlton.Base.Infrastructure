namespace Carlton.Base.Infrastructure.Security
{
    public interface IAuthorizer
    {
        bool IsAuthorized(object instance);
    }
}
