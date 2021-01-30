using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Carlton.Base.Infrastructure.PipelineBehaviors
{
    public class ValidationPipelineBehavior<TRequest, TResponse> : BasePipelineBehavior<TRequest, TResponse>
    {
        private readonly AbstractValidator<TRequest> _validator;

        public ValidationPipelineBehavior(ILogger logger, AbstractValidator<TRequest> validator) : base(logger)
        {
            _validator = validator;
        }

        public async override Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            Logger.LogDebug($"{RequestType} is about to be validated");

            var result = _validator.Validate(request);

            if(!result.IsValid)
            {
                Logger.LogInformation($"{RequestType} failed validation");
                throw new ValidationException(result.Errors);
            }

            Logger.LogDebug($"{RequestType} passed validation");

            return await next().ConfigureAwait(false);
        }
    }
}
