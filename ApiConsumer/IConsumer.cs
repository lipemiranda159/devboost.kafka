using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApiConsumer
{
    public interface IConsumer
    {

        Task ExecuteAsync(CancellationToken stopingToken,string topic);
        List<string> GetMessages();
    }
}
