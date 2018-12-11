using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediaTRSample.Command
{
    public class Update随便写 : MediatR.INotificationHandler<UpdateCommand>
    {
        public Update随便写() {

        }
        public Task Handle(UpdateCommand notification, CancellationToken cancellationToken)
        {
            return Task.Run(() => { Console.WriteLine("Update随便写:" + notification.Message); });
        }
    }
}
