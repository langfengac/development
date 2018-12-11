using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace MediaTRSample.Request
{
    public class SendUpdateCommandHandler : IRequestHandler<SendUpdateCommand, string>
    {
        public Task<string> Handle(SendUpdateCommand request, CancellationToken cancellationToken)
        { 
            return Task.Run(() => { Console.WriteLine("我是UpdateCommandHandler:" + request.Message); return ""; });
        }
    }
}
