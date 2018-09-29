using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaTRSample
{
    public class RegisterUser : IRequest<bool>
    {
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
