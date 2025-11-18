using System;
using System.Threading;
using System.Threading.Tasks;

namespace Plugin.Core.Colorful
{
    public class TaskQueue
    {
        private readonly SemaphoreSlim semaphoreSlim_0;

        public TaskQueue()
        {
            semaphoreSlim_0 = new SemaphoreSlim(1);
        }

        public async Task<T> Enqueue<T>(Func<Task<T>> taskGenerator)
        {
            await semaphoreSlim_0.WaitAsync();
            try
            {
                return await taskGenerator();
            }
            finally
            {
                semaphoreSlim_0.Release();
            }
        }

        public async Task Enqueue(Func<Task> taskGenerator)
        {
            await semaphoreSlim_0.WaitAsync();
            try
            {
                await taskGenerator();
            }
            finally
            {
                semaphoreSlim_0.Release();
            }
        }
    }
}
