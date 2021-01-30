using Polly;
using Polly.Wrap;

namespace Carlton.Base.Infrastructure.Resiliance
{
    public interface IResiliancePolicyHandler<T>
    {
        AsyncPolicyWrap<T> CreatePolicyWrap();
        void HandleResult(PolicyResult<T> result);
    }
}

