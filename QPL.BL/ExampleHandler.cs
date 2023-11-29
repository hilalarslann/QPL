using MediatR;

namespace QPL.BL
{
    public class ExampleCommand : IRequest<ExampleResponse>
    {
        public ExampleCommand()
        {
        }
    }

    public class ExampleResponse : BaseHandlerResponse
    {

    }

    public class NotifyLatePlannedHandler : IRequestHandler<ExampleCommand, ExampleResponse>
    {
        public NotifyLatePlannedHandler()
        {

        }

        public async Task<ExampleResponse> Handle(ExampleCommand request, CancellationToken cancellationToken)
        {
            var response = new ExampleResponse();

            try
            {

            }
            catch
            {
                throw;
            }

            return response;
        }
    }
}