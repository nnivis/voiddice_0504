using System.Threading;
using System.Threading.Tasks;

namespace CodeBase.Services.Turn
{
    public interface IFightController
    {
        Task RequestTurnAsync(CancellationToken ct);
    }
}
