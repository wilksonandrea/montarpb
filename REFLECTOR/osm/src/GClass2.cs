using Plugin.Core.Utility;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public class GClass2
{
    private readonly string string_0;
    private readonly Process process_0 = new Process();
    private readonly object object_0 = new object();
    private SynchronizationContext synchronizationContext_0;
    private string string_1;

    public event EventHandler<string> Event_0;

    public event EventHandler Event_1;

    public event EventHandler<string> Event_2;

    public GClass2(string string_2)
    {
        this.string_0 = string_2;
        ProcessStartInfo info1 = new ProcessStartInfo(string_2);
        info1.RedirectStandardError = true;
        info1.StandardErrorEncoding = Encoding.UTF8;
        info1.RedirectStandardInput = true;
        info1.RedirectStandardOutput = true;
        ProcessStartInfo info = info1;
        this.process_0.EnableRaisingEvents = true;
        info.CreateNoWindow = true;
        info.UseShellExecute = false;
        info.StandardOutputEncoding = Encoding.UTF8;
        this.process_0.Exited += new EventHandler(this.process_0_Exited);
    }

    public void method_0(params string[] string_2)
    {
        if (this.Boolean_0)
        {
            throw new InvalidOperationException("Process is still Running. Please wait for the process to complete.");
        }
        string str = string.Join(" ", string_2);
        this.process_0.StartInfo.Arguments = str;
        this.synchronizationContext_0 = SynchronizationContext.Current;
        this.process_0.Start();
        this.Boolean_0 = true;
        new Task(new Action(this.method_3)).Start();
        new Task(new Action(this.method_5)).Start();
        new Task(new Action(this.method_4)).Start();
    }

    public void method_1(string string_2)
    {
        if (string_2 != null)
        {
            object obj2 = this.object_0;
            lock (obj2)
            {
                this.string_1 = string_2;
            }
        }
    }

    public void method_2(string string_2)
    {
        this.method_1(string_2 + Environment.NewLine);
    }

    [AsyncStateMachine(typeof(Struct1))]
    private void method_3()
    {
        Struct1 struct2;
        struct2.asyncVoidMethodBuilder_0 = AsyncVoidMethodBuilder.Create();
        struct2.gclass2_0 = this;
        struct2.int_0 = -1;
        struct2.asyncVoidMethodBuilder_0.Start<Struct1>(ref struct2);
    }

    [AsyncStateMachine(typeof(Struct2))]
    private void method_4()
    {
        Struct2 struct2;
        struct2.asyncVoidMethodBuilder_0 = AsyncVoidMethodBuilder.Create();
        struct2.gclass2_0 = this;
        struct2.int_0 = -1;
        struct2.asyncVoidMethodBuilder_0.Start<Struct2>(ref struct2);
    }

    [AsyncStateMachine(typeof(Struct3))]
    private void method_5()
    {
        Struct3 struct2;
        struct2.asyncVoidMethodBuilder_0 = AsyncVoidMethodBuilder.Create();
        struct2.gclass2_0 = this;
        struct2.int_0 = -1;
        struct2.asyncVoidMethodBuilder_0.Start<Struct3>(ref struct2);
    }

    private void process_0_Exited(object sender, EventArgs e)
    {
        this.vmethod_1();
    }

    protected virtual void vmethod_0(string string_2)
    {
        Class5 class2 = new Class5 {
            gclass2_0 = this,
            string_0 = string_2,
            eventHandler_0 = this.eventHandler_0
        };
        if (class2.eventHandler_0 != null)
        {
            if (this.synchronizationContext_0 != null)
            {
                this.synchronizationContext_0.Post(new SendOrPostCallback(class2.method_0), null);
            }
            else
            {
                class2.eventHandler_0(this, class2.string_0);
            }
        }
    }

    protected virtual void vmethod_1()
    {
        EventHandler handler = this.eventHandler_1;
        if (handler != null)
        {
            handler(this, EventArgs.Empty);
        }
    }

    protected virtual void vmethod_2(string string_2)
    {
        Class6 class2 = new Class6 {
            gclass2_0 = this,
            string_0 = string_2,
            eventHandler_0 = this.eventHandler_2
        };
        if (class2.eventHandler_0 != null)
        {
            if (this.synchronizationContext_0 != null)
            {
                this.synchronizationContext_0.Post(new SendOrPostCallback(class2.method_0), null);
            }
            else
            {
                class2.eventHandler_0(this, class2.string_0);
            }
        }
    }

    public int Int32_0 =>
        this.process_0.ExitCode;

    public bool Boolean_0 { get; private set; }

    [CompilerGenerated]
    private sealed class Class5
    {
        public EventHandler<string> eventHandler_0;
        public GClass2 gclass2_0;
        public string string_0;

        internal void method_0(object object_0)
        {
            this.eventHandler_0(this.gclass2_0, this.string_0);
        }
    }

    [CompilerGenerated]
    private sealed class Class6
    {
        public EventHandler<string> eventHandler_0;
        public GClass2 gclass2_0;
        public string string_0;

        internal void method_0(object object_0)
        {
            this.eventHandler_0(this.gclass2_0, this.string_0);
        }
    }

    [CompilerGenerated]
    private struct Struct1 : IAsyncStateMachine
    {
        public int int_0;
        public AsyncVoidMethodBuilder asyncVoidMethodBuilder_0;
        public GClass2 gclass2_0;
        private StringBuilder stringBuilder_0;
        private char[] char_0;
        private TaskAwaiter<int> taskAwaiter_0;

        private void MoveNext()
        {
            int num = this.int_0;
            GClass2 class2 = this.gclass2_0;
            try
            {
                int num2;
                TaskAwaiter<int> awaiter;
                if (num == 0)
                {
                    awaiter = this.taskAwaiter_0;
                    this.taskAwaiter_0 = new TaskAwaiter<int>();
                    this.int_0 = num = -1;
                }
                else
                {
                    this.stringBuilder_0 = new StringBuilder();
                    this.char_0 = new char[0x400];
                    goto TR_0009;
                }
            TR_0005:
                num2 = awaiter.GetResult();
                this.stringBuilder_0.Append(this.char_0.SubArray(0, num2));
                class2.vmethod_2(this.stringBuilder_0.ToString());
                Thread.Sleep(1);
            TR_0009:
                while (true)
                {
                    if (!class2.process_0.HasExited)
                    {
                        this.stringBuilder_0.Clear();
                        awaiter = class2.process_0.StandardOutput.ReadAsync(this.char_0, 0, this.char_0.Length).GetAwaiter();
                        if (awaiter.IsCompleted)
                        {
                            goto TR_0005;
                        }
                        else
                        {
                            this.int_0 = num = 0;
                            this.taskAwaiter_0 = awaiter;
                            this.asyncVoidMethodBuilder_0.AwaitUnsafeOnCompleted<TaskAwaiter<int>, GClass2.Struct1>(ref awaiter, ref this);
                        }
                        return;
                    }
                    else
                    {
                        class2.Boolean_0 = false;
                    }
                    break;
                }
                this.int_0 = -2;
                this.stringBuilder_0 = null;
                this.char_0 = null;
                this.asyncVoidMethodBuilder_0.SetResult();
            }
            catch (Exception exception)
            {
                this.int_0 = -2;
                this.stringBuilder_0 = null;
                this.char_0 = null;
                this.asyncVoidMethodBuilder_0.SetException(exception);
            }
        }

        [DebuggerHidden]
        private void SetStateMachine(IAsyncStateMachine stateMachine)
        {
            this.asyncVoidMethodBuilder_0.SetStateMachine(stateMachine);
        }
    }

    [CompilerGenerated]
    private struct Struct2 : IAsyncStateMachine
    {
        public int int_0;
        public AsyncVoidMethodBuilder asyncVoidMethodBuilder_0;
        public GClass2 gclass2_0;
        private StringBuilder stringBuilder_0;
        private char[] char_0;
        private TaskAwaiter<int> taskAwaiter_0;

        private void MoveNext()
        {
            int num = this.int_0;
            GClass2 class2 = this.gclass2_0;
            try
            {
                int num3;
                TaskAwaiter<int> awaiter;
                if (num == 0)
                {
                    awaiter = this.taskAwaiter_0;
                    this.taskAwaiter_0 = new TaskAwaiter<int>();
                    this.int_0 = num = -1;
                }
                else
                {
                    this.stringBuilder_0 = new StringBuilder();
                    goto TR_0008;
                }
            TR_0004:
                num3 = awaiter.GetResult();
                int length = num3;
                this.stringBuilder_0.Append(this.char_0.SubArray(0, length));
                class2.vmethod_0(this.stringBuilder_0.ToString());
                Thread.Sleep(1);
                this.char_0 = null;
                if (class2.process_0.HasExited)
                {
                    this.int_0 = -2;
                    this.stringBuilder_0 = null;
                    this.asyncVoidMethodBuilder_0.SetResult();
                    return;
                }
            TR_0008:
                while (true)
                {
                    this.stringBuilder_0.Clear();
                    this.char_0 = new char[0x400];
                    awaiter = class2.process_0.StandardError.ReadAsync(this.char_0, 0, this.char_0.Length).GetAwaiter();
                    if (awaiter.IsCompleted)
                    {
                        goto TR_0004;
                    }
                    else
                    {
                        this.int_0 = num = 0;
                        this.taskAwaiter_0 = awaiter;
                        this.asyncVoidMethodBuilder_0.AwaitUnsafeOnCompleted<TaskAwaiter<int>, GClass2.Struct2>(ref awaiter, ref this);
                    }
                    break;
                }
            }
            catch (Exception exception)
            {
                this.int_0 = -2;
                this.stringBuilder_0 = null;
                this.asyncVoidMethodBuilder_0.SetException(exception);
            }
        }

        [DebuggerHidden]
        private void SetStateMachine(IAsyncStateMachine stateMachine)
        {
            this.asyncVoidMethodBuilder_0.SetStateMachine(stateMachine);
        }
    }

    [CompilerGenerated]
    private struct Struct3 : IAsyncStateMachine
    {
        public int int_0;
        public AsyncVoidMethodBuilder asyncVoidMethodBuilder_0;
        public GClass2 gclass2_0;
        private TaskAwaiter taskAwaiter_0;

        private void MoveNext()
        {
            int num = this.int_0;
            GClass2 class2 = this.gclass2_0;
            try
            {
                TaskAwaiter awaiter;
                TaskAwaiter awaiter2;
                if (num == 0)
                {
                    awaiter = this.taskAwaiter_0;
                    this.taskAwaiter_0 = new TaskAwaiter();
                    this.int_0 = num = -1;
                }
                else
                {
                    if (num == 1)
                    {
                        awaiter2 = this.taskAwaiter_0;
                        this.taskAwaiter_0 = new TaskAwaiter();
                        this.int_0 = num = -1;
                    }
                    else
                    {
                        goto TR_0012;
                    }
                    goto TR_000A;
                }
                goto TR_000B;
            TR_000A:
                awaiter2.GetResult();
                object obj2 = class2.object_0;
                bool lockTaken = false;
                try
                {
                    Monitor.Enter(obj2, ref lockTaken);
                    class2.string_1 = null;
                }
                finally
                {
                    if ((num < 0) && lockTaken)
                    {
                        Monitor.Exit(obj2);
                    }
                }
                goto TR_0012;
            TR_000B:
                awaiter.GetResult();
                awaiter2 = class2.process_0.StandardInput.FlushAsync().GetAwaiter();
                if (awaiter2.IsCompleted)
                {
                    goto TR_000A;
                }
                else
                {
                    this.int_0 = num = 1;
                    this.taskAwaiter_0 = awaiter2;
                    this.asyncVoidMethodBuilder_0.AwaitUnsafeOnCompleted<TaskAwaiter, GClass2.Struct3>(ref awaiter2, ref this);
                }
                return;
            TR_0012:
                while (true)
                {
                    if (!class2.process_0.HasExited)
                    {
                        Thread.Sleep(1);
                        if (class2.string_1 == null)
                        {
                            continue;
                        }
                        awaiter = class2.process_0.StandardInput.WriteLineAsync(class2.string_1).GetAwaiter();
                        if (awaiter.IsCompleted)
                        {
                            goto TR_000B;
                        }
                        else
                        {
                            this.int_0 = num = 0;
                            this.taskAwaiter_0 = awaiter;
                            this.asyncVoidMethodBuilder_0.AwaitUnsafeOnCompleted<TaskAwaiter, GClass2.Struct3>(ref awaiter, ref this);
                        }
                    }
                    else
                    {
                        this.int_0 = -2;
                        this.asyncVoidMethodBuilder_0.SetResult();
                        return;
                    }
                    break;
                }
            }
            catch (Exception exception)
            {
                this.int_0 = -2;
                this.asyncVoidMethodBuilder_0.SetException(exception);
            }
        }

        [DebuggerHidden]
        private void SetStateMachine(IAsyncStateMachine stateMachine)
        {
            this.asyncVoidMethodBuilder_0.SetStateMachine(stateMachine);
        }
    }
}

