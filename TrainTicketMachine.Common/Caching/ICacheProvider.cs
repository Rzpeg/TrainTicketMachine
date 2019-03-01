using System;
using System.Threading.Tasks;

namespace TrainTicketMachine.Common.Caching
{
    public interface ICacheProvider
    {
        Task<TResult> GetOrSet<TResult>(string cacheKey, Func<Task<TResult>> cacheSetter);
    }
}