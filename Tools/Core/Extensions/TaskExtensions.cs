using System;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class TaskExtensions
    {
        public static async Task WithCancellation(this Task task, CancellationToken token)
        {
            var tcs = new TaskCompletionSource<bool>();
            Action action = () =>
            {
                tcs.SetCanceled();
                task.Dispose();
            };
            
            using (token.Register(action))
            {
                task.ContinueWith(t =>
                {
                    if (t.IsCanceled)
                        tcs.SetCanceled();
                    if (t.IsFaulted)
                        tcs.SetException(t.Exception);
                    if (t.IsCompleted)
                        tcs.SetResult(true);
                });

                await tcs.Task;
            }
        }
        
        public static async Task<T> WithCancellation<T>(this Task<T> task, CancellationToken token)
        {
            var tcs = new TaskCompletionSource<T>();
            Action action = () =>
            {
                tcs.SetCanceled();
                task.Dispose();
            };
            
            using (token.Register(action))
            {
                task.ContinueWith(t =>
                {
                    if (t.IsCanceled)
                        tcs.SetCanceled();
                    if (t.IsFaulted)
                        tcs.SetException(t.Exception);
                    if (t.IsCompleted)
                        tcs.SetResult(t.Result);
                });

                await tcs.Task;
            }

            return tcs.Task.Result;
        }
    }
}