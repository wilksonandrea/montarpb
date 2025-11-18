namespace Plugin.Core.Colorful
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;

    public class TaskQueue
    {
        private readonly SemaphoreSlim semaphoreSlim_0 = new SemaphoreSlim(1);

        [AsyncStateMachine(typeof(Struct5))]
        public Task Enqueue(Func<Task> taskGenerator)
        {
            Struct5 struct2;
            struct2.asyncTaskMethodBuilder_0 = AsyncTaskMethodBuilder.Create();
            struct2.taskQueue_0 = this;
            struct2.func_0 = taskGenerator;
            struct2.int_0 = -1;
            struct2.asyncTaskMethodBuilder_0.Start<Struct5>(ref struct2);
            return struct2.asyncTaskMethodBuilder_0.Task;
        }

        [AsyncStateMachine(typeof(Struct4))]
        public Task<T> Enqueue<T>(Func<Task<T>> taskGenerator)
        {
            Struct4<T> struct2;
            struct2.asyncTaskMethodBuilder_0 = AsyncTaskMethodBuilder<T>.Create();
            struct2.taskQueue_0 = this;
            struct2.func_0 = taskGenerator;
            struct2.int_0 = -1;
            struct2.asyncTaskMethodBuilder_0.Start<Struct4<T>>(ref struct2);
            return struct2.asyncTaskMethodBuilder_0.Task;
        }

        [CompilerGenerated]
        private struct Struct4<T> : IAsyncStateMachine
        {
            public int int_0;
            public AsyncTaskMethodBuilder<T> asyncTaskMethodBuilder_0;
            public TaskQueue taskQueue_0;
            public Func<Task<T>> func_0;
            private TaskAwaiter taskAwaiter_0;
            private TaskAwaiter<T> taskAwaiter_1;

            private void MoveNext()
            {
                int num = this.int_0;
                TaskQueue queue = this.taskQueue_0;
                try
                {
                    TaskAwaiter awaiter;
                    if (num == 0)
                    {
                        awaiter = this.taskAwaiter_0;
                        this.taskAwaiter_0 = new TaskAwaiter();
                        num = -1;
                        this.int_0 = -1;
                    }
                    else
                    {
                        if (num == 1)
                        {
                            goto TR_000D;
                        }
                        else
                        {
                            awaiter = queue.semaphoreSlim_0.WaitAsync().GetAwaiter();
                            if (awaiter.IsCompleted)
                            {
                                goto TR_000E;
                            }
                            else
                            {
                                num = 0;
                                this.int_0 = 0;
                                this.taskAwaiter_0 = awaiter;
                                this.asyncTaskMethodBuilder_0.AwaitUnsafeOnCompleted<TaskAwaiter, TaskQueue.Struct4<T>>(ref awaiter, ref (TaskQueue.Struct4<T>) ref this);
                            }
                        }
                        return;
                    }
                    goto TR_000E;
                TR_000D:
                    try
                    {
                        T local;
                        TaskAwaiter<T> awaiter2;
                        if (num == 1)
                        {
                            awaiter2 = this.taskAwaiter_1;
                            this.taskAwaiter_1 = new TaskAwaiter<T>();
                            num = -1;
                            this.int_0 = -1;
                            goto TR_0008;
                        }
                        else
                        {
                            awaiter2 = this.func_0().GetAwaiter();
                            if (awaiter2.IsCompleted)
                            {
                                goto TR_0008;
                            }
                            else
                            {
                                num = 1;
                                this.int_0 = 1;
                                this.taskAwaiter_1 = awaiter2;
                                this.asyncTaskMethodBuilder_0.AwaitUnsafeOnCompleted<TaskAwaiter<T>, TaskQueue.Struct4<T>>(ref awaiter2, ref (TaskQueue.Struct4<T>) ref this);
                            }
                        }
                        return;
                    TR_0008:
                        local = awaiter2.GetResult();
                        this.int_0 = -2;
                        this.asyncTaskMethodBuilder_0.SetResult(local);
                        return;
                    }
                    finally
                    {
                        if (num < 0)
                        {
                            queue.semaphoreSlim_0.Release();
                        }
                    }
                    return;
                TR_000E:
                    awaiter.GetResult();
                    goto TR_000D;
                }
                catch (Exception exception)
                {
                    this.int_0 = -2;
                    this.asyncTaskMethodBuilder_0.SetException(exception);
                }
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.asyncTaskMethodBuilder_0.SetStateMachine(stateMachine);
            }
        }

        [CompilerGenerated]
        private struct Struct5 : IAsyncStateMachine
        {
            public int int_0;
            public AsyncTaskMethodBuilder asyncTaskMethodBuilder_0;
            public TaskQueue taskQueue_0;
            public Func<Task> func_0;
            private TaskAwaiter taskAwaiter_0;

            private void MoveNext()
            {
                int num = this.int_0;
                TaskQueue queue = this.taskQueue_0;
                try
                {
                    TaskAwaiter awaiter;
                    if (num == 0)
                    {
                        awaiter = this.taskAwaiter_0;
                        this.taskAwaiter_0 = new TaskAwaiter();
                        num = -1;
                        this.int_0 = -1;
                    }
                    else
                    {
                        if (num == 1)
                        {
                            goto TR_000D;
                        }
                        else
                        {
                            awaiter = queue.semaphoreSlim_0.WaitAsync().GetAwaiter();
                            if (awaiter.IsCompleted)
                            {
                                goto TR_000E;
                            }
                            else
                            {
                                num = 0;
                                this.int_0 = 0;
                                this.taskAwaiter_0 = awaiter;
                                this.asyncTaskMethodBuilder_0.AwaitUnsafeOnCompleted<TaskAwaiter, TaskQueue.Struct5>(ref awaiter, ref this);
                            }
                        }
                        return;
                    }
                    goto TR_000E;
                TR_000D:
                    try
                    {
                        if (num == 1)
                        {
                            awaiter = this.taskAwaiter_0;
                            this.taskAwaiter_0 = new TaskAwaiter();
                            num = -1;
                            this.int_0 = -1;
                            goto TR_0008;
                        }
                        else
                        {
                            awaiter = this.func_0().GetAwaiter();
                            if (awaiter.IsCompleted)
                            {
                                goto TR_0008;
                            }
                            else
                            {
                                num = 1;
                                this.int_0 = 1;
                                this.taskAwaiter_0 = awaiter;
                                this.asyncTaskMethodBuilder_0.AwaitUnsafeOnCompleted<TaskAwaiter, TaskQueue.Struct5>(ref awaiter, ref this);
                            }
                        }
                        return;
                    TR_0008:
                        awaiter.GetResult();
                        this.int_0 = -2;
                        this.asyncTaskMethodBuilder_0.SetResult();
                        return;
                    }
                    finally
                    {
                        if (num < 0)
                        {
                            queue.semaphoreSlim_0.Release();
                        }
                    }
                    return;
                TR_000E:
                    awaiter.GetResult();
                    goto TR_000D;
                }
                catch (Exception exception)
                {
                    this.int_0 = -2;
                    this.asyncTaskMethodBuilder_0.SetException(exception);
                }
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.asyncTaskMethodBuilder_0.SetStateMachine(stateMachine);
            }
        }
    }
}

