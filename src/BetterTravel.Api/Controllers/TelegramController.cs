using AutoMapper;
using MediatR;

namespace BetterTravel.Api.Controllers
{
    public class TelegramController : ApiController
    {
        public TelegramController(IMapper mapper, IMediator mediator) 
            : base(mapper, mediator)
        {
        }
    }
}