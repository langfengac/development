using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MediaTRSample.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public ActionResult Get()
        {
            RegisterUser registerUser=new RegisterUser() {  EmailAddress="1", LastName="2", FirstName="3"};
            Task registered = _mediator.Send(registerUser);
            return View();
        }
    }
    public class RegisterUserHandler : IRequestHandler<RegisterUser, bool>
    {
        public bool Handle(RegisterUser message)
        {
            // save to database
            return true;
        }

        public Task<bool> Handle(RegisterUser request, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
            {
                return false;
            });
        }
    }
}