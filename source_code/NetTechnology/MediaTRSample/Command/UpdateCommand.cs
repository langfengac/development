using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaTRSample.Command
{
    public class UpdateCommand:MediatR.INotification
    {

        public string Message { get; set; }

        public UpdateCommand(string message)
        {
            this.Message = message;
        }
    }
}
