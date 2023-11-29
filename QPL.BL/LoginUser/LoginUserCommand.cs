using MediatR;
using Microsoft.IdentityModel.Tokens;
using QPL.BL.Token;

namespace QPL.BL.LoginUser
{
    public class LoginUserCommand : IRequest<LoginUserCommandResponse>
    {

    }

    public class LoginUserCommandResponse : BaseHandlerResponse
    {

    }

    public class LoginUserSuccesCommandResponse : LoginUserCommandResponse
    {
        public DTOs.Token Token { get; set; }
    }
    public class LoginUserErrorCommandResponse : LoginUserCommandResponse
    {
        public string Message { get; set; }
    }

    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserCommandResponse>
    {
        readonly ITokenHandler _tokenHandler;
        public LoginUserCommandHandler(ITokenHandler tokenHandler)
        {
            _tokenHandler = tokenHandler;
        }
        public async Task<LoginUserCommandResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            if (true)//Authentication başarılı ise
            {
                DTOs.Token token = _tokenHandler.CreateAccessToken(5);
                return new LoginUserSuccesCommandResponse()
                {
                    Token = token
                };
            }
            return new LoginUserErrorCommandResponse()
            {
                Message = "Kullanıcı adı şifre hatalı"
            };
        }
    }
}
