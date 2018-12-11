using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaTRSample.Request
{
    public class SendUpdateCommand:MediatR.IRequest<string>
    {
        public string Message { get; set; }

        public SendUpdateCommand(string message){
            Message = message;
            }
    }
}
