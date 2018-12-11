using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediaTRSample.Command
{
    public class UpdateCommandHandler : MediatR.INotificationHandler<UpdateCommand>
    {
        public UpdateCommandHandler() {

        }
        public  Task Handle(UpdateCommand notification, CancellationToken cancellationToken)
        {
            return Task.Run(() => { Console.WriteLine("我是UpdateCommandHandler:" + notification.Message); });
        }
    }
}
