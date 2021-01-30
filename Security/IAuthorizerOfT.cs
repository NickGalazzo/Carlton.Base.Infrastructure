namespace Carlton.Base.Infrastructure.Security
{
    public interface IAuthorizer<in T> : IAuthorizer
    {
        bool IsAuthorized(T instance);
    }
}
