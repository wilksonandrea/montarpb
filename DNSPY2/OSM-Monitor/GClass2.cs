using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Plugin.Core.Utility;

// Token: 0x0200000D RID: 13
public class GClass2
{
	// Token: 0x0600004F RID: 79 RVA: 0x00003D74 File Offset: 0x00001F74
	public GClass2(string string_2)
	{
		this.string_0 = string_2;
		ProcessStartInfo processStartInfo = new ProcessStartInfo(string_2)
		{
			RedirectStandardError = true,
			StandardErrorEncoding = Encoding.UTF8,
			RedirectStandardInput = true,
			RedirectStandardOutput = true
		};
		this.process_0.EnableRaisingEvents = true;
		processStartInfo.CreateNoWindow = true;
		processStartInfo.UseShellExecute = false;
		processStartInfo.StandardOutputEncoding = Encoding.UTF8;
		this.process_0.Exited += this.process_0_Exited;
	}

	// Token: 0x14000001 RID: 1
	// (add) Token: 0x06000050 RID: 80 RVA: 0x00003E08 File Offset: 0x00002008
	// (remove) Token: 0x06000051 RID: 81 RVA: 0x00003E40 File Offset: 0x00002040
	public event EventHandler<string> Event_0
	{
		[CompilerGenerated]
		add
		{
			EventHandler<string> eventHandler = this.eventHandler_0;
			EventHandler<string> eventHandler2;
			do
			{
				eventHandler2 = eventHandler;
				EventHandler<string> eventHandler3 = (EventHandler<string>)Delegate.Combine(eventHandler2, value);
				eventHandler = Interlocked.CompareExchange<EventHandler<string>>(ref this.eventHandler_0, eventHandler3, eventHandler2);
			}
			while (eventHandler != eventHandler2);
		}
		[CompilerGenerated]
		remove
		{
			EventHandler<string> eventHandler = this.eventHandler_0;
			EventHandler<string> eventHandler2;
			do
			{
				eventHandler2 = eventHandler;
				EventHandler<string> eventHandler3 = (EventHandler<string>)Delegate.Remove(eventHandler2, value);
				eventHandler = Interlocked.CompareExchange<EventHandler<string>>(ref this.eventHandler_0, eventHandler3, eventHandler2);
			}
			while (eventHandler != eventHandler2);
		}
	}

	// Token: 0x14000002 RID: 2
	// (add) Token: 0x06000052 RID: 82 RVA: 0x00003E78 File Offset: 0x00002078
	// (remove) Token: 0x06000053 RID: 83 RVA: 0x00003EB0 File Offset: 0x000020B0
	public event EventHandler Event_1
	{
		[CompilerGenerated]
		add
		{
			EventHandler eventHandler = this.eventHandler_1;
			EventHandler eventHandler2;
			do
			{
				eventHandler2 = eventHandler;
				EventHandler eventHandler3 = (EventHandler)Delegate.Combine(eventHandler2, value);
				eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.eventHandler_1, eventHandler3, eventHandler2);
			}
			while (eventHandler != eventHandler2);
		}
		[CompilerGenerated]
		remove
		{
			EventHandler eventHandler = this.eventHandler_1;
			EventHandler eventHandler2;
			do
			{
				eventHandler2 = eventHandler;
				EventHandler eventHandler3 = (EventHandler)Delegate.Remove(eventHandler2, value);
				eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.eventHandler_1, eventHandler3, eventHandler2);
			}
			while (eventHandler != eventHandler2);
		}
	}

	// Token: 0x14000003 RID: 3
	// (add) Token: 0x06000054 RID: 84 RVA: 0x00003EE8 File Offset: 0x000020E8
	// (remove) Token: 0x06000055 RID: 85 RVA: 0x00003F20 File Offset: 0x00002120
	public event EventHandler<string> Event_2
	{
		[CompilerGenerated]
		add
		{
			EventHandler<string> eventHandler = this.eventHandler_2;
			EventHandler<string> eventHandler2;
			do
			{
				eventHandler2 = eventHandler;
				EventHandler<string> eventHandler3 = (EventHandler<string>)Delegate.Combine(eventHandler2, value);
				eventHandler = Interlocked.CompareExchange<EventHandler<string>>(ref this.eventHandler_2, eventHandler3, eventHandler2);
			}
			while (eventHandler != eventHandler2);
		}
		[CompilerGenerated]
		remove
		{
			EventHandler<string> eventHandler = this.eventHandler_2;
			EventHandler<string> eventHandler2;
			do
			{
				eventHandler2 = eventHandler;
				EventHandler<string> eventHandler3 = (EventHandler<string>)Delegate.Remove(eventHandler2, value);
				eventHandler = Interlocked.CompareExchange<EventHandler<string>>(ref this.eventHandler_2, eventHandler3, eventHandler2);
			}
			while (eventHandler != eventHandler2);
		}
	}

	// Token: 0x17000015 RID: 21
	// (get) Token: 0x06000056 RID: 86 RVA: 0x000022FD File Offset: 0x000004FD
	public int Int32_0
	{
		get
		{
			return this.process_0.ExitCode;
		}
	}

	// Token: 0x17000016 RID: 22
	// (get) Token: 0x06000057 RID: 87 RVA: 0x0000230A File Offset: 0x0000050A
	// (set) Token: 0x06000058 RID: 88 RVA: 0x00002312 File Offset: 0x00000512
	public bool Boolean_0
	{
		[CompilerGenerated]
		get
		{
			return this.bool_0;
		}
		[CompilerGenerated]
		private set
		{
			this.bool_0 = value;
		}
	}

	// Token: 0x06000059 RID: 89 RVA: 0x00003F58 File Offset: 0x00002158
	public void method_0(params string[] string_2)
	{
		if (this.Boolean_0)
		{
			throw new InvalidOperationException("Process is still Running. Please wait for the process to complete.");
		}
		string text = string.Join(" ", string_2);
		this.process_0.StartInfo.Arguments = text;
		this.synchronizationContext_0 = SynchronizationContext.Current;
		this.process_0.Start();
		this.Boolean_0 = true;
		new Task(new Action(this.method_3)).Start();
		new Task(new Action(this.method_5)).Start();
		new Task(new Action(this.method_4)).Start();
	}

	// Token: 0x0600005A RID: 90 RVA: 0x00003FF8 File Offset: 0x000021F8
	public void method_1(string string_2)
	{
		if (string_2 == null)
		{
			return;
		}
		object obj = this.object_0;
		lock (obj)
		{
			this.string_1 = string_2;
		}
	}

	// Token: 0x0600005B RID: 91 RVA: 0x0000231B File Offset: 0x0000051B
	public void method_2(string string_2)
	{
		this.method_1(string_2 + Environment.NewLine);
	}

	// Token: 0x0600005C RID: 92 RVA: 0x00004040 File Offset: 0x00002240
	protected virtual void vmethod_0(string string_2)
	{
		GClass2.Class5 @class = new GClass2.Class5();
		@class.gclass2_0 = this;
		@class.string_0 = string_2;
		@class.eventHandler_0 = this.eventHandler_0;
		if (@class.eventHandler_0 != null)
		{
			if (this.synchronizationContext_0 != null)
			{
				this.synchronizationContext_0.Post(new SendOrPostCallback(@class.method_0), null);
				return;
			}
			@class.eventHandler_0(this, @class.string_0);
		}
	}

	// Token: 0x0600005D RID: 93 RVA: 0x000040A8 File Offset: 0x000022A8
	protected virtual void vmethod_1()
	{
		EventHandler eventHandler = this.eventHandler_1;
		if (eventHandler != null)
		{
			eventHandler(this, EventArgs.Empty);
		}
	}

	// Token: 0x0600005E RID: 94 RVA: 0x000040CC File Offset: 0x000022CC
	protected virtual void vmethod_2(string string_2)
	{
		GClass2.Class6 @class = new GClass2.Class6();
		@class.gclass2_0 = this;
		@class.string_0 = string_2;
		@class.eventHandler_0 = this.eventHandler_2;
		if (@class.eventHandler_0 != null)
		{
			if (this.synchronizationContext_0 != null)
			{
				this.synchronizationContext_0.Post(new SendOrPostCallback(@class.method_0), null);
				return;
			}
			@class.eventHandler_0(this, @class.string_0);
		}
	}

	// Token: 0x0600005F RID: 95 RVA: 0x0000232E File Offset: 0x0000052E
	private void process_0_Exited(object sender, EventArgs e)
	{
		this.vmethod_1();
	}

	// Token: 0x06000060 RID: 96 RVA: 0x00004134 File Offset: 0x00002334
	private async void method_3()
	{
		StringBuilder stringBuilder = new StringBuilder();
		char[] array = new char[1024];
		while (!this.process_0.HasExited)
		{
			stringBuilder.Clear();
			int num = await this.process_0.StandardOutput.ReadAsync(array, 0, array.Length);
			int num2 = num;
			stringBuilder.Append(array.SubArray(0, num2));
			this.vmethod_2(stringBuilder.ToString());
			Thread.Sleep(1);
		}
		this.Boolean_0 = false;
	}

	// Token: 0x06000061 RID: 97 RVA: 0x0000416C File Offset: 0x0000236C
	private async void method_4()
	{
		StringBuilder stringBuilder = new StringBuilder();
		do
		{
			stringBuilder.Clear();
			char[] array = new char[1024];
			int num = await this.process_0.StandardError.ReadAsync(array, 0, array.Length);
			int num2 = num;
			stringBuilder.Append(array.SubArray(0, num2));
			this.vmethod_0(stringBuilder.ToString());
			Thread.Sleep(1);
			array = null;
		}
		while (!this.process_0.HasExited);
	}

	// Token: 0x06000062 RID: 98 RVA: 0x000041A4 File Offset: 0x000023A4
	private async void method_5()
	{
		while (!this.process_0.HasExited)
		{
			Thread.Sleep(1);
			if (this.string_1 != null)
			{
				await this.process_0.StandardInput.WriteLineAsync(this.string_1);
				await this.process_0.StandardInput.FlushAsync();
				object obj = this.object_0;
				lock (obj)
				{
					this.string_1 = null;
				}
			}
		}
	}

	// Token: 0x04000024 RID: 36
	private readonly string string_0;

	// Token: 0x04000025 RID: 37
	private readonly Process process_0 = new Process();

	// Token: 0x04000026 RID: 38
	private readonly object object_0 = new object();

	// Token: 0x04000027 RID: 39
	private SynchronizationContext synchronizationContext_0;

	// Token: 0x04000028 RID: 40
	private string string_1;

	// Token: 0x04000029 RID: 41
	[CompilerGenerated]
	private EventHandler<string> eventHandler_0;

	// Token: 0x0400002A RID: 42
	[CompilerGenerated]
	private EventHandler eventHandler_1;

	// Token: 0x0400002B RID: 43
	[CompilerGenerated]
	private EventHandler<string> eventHandler_2;

	// Token: 0x0400002C RID: 44
	[CompilerGenerated]
	private bool bool_0;

	// Token: 0x0200000E RID: 14
	[CompilerGenerated]
	private sealed class Class5
	{
		// Token: 0x06000063 RID: 99 RVA: 0x00002133 File Offset: 0x00000333
		public Class5()
		{
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002336 File Offset: 0x00000536
		internal void method_0(object object_0)
		{
			this.eventHandler_0(this.gclass2_0, this.string_0);
		}

		// Token: 0x0400002D RID: 45
		public EventHandler<string> eventHandler_0;

		// Token: 0x0400002E RID: 46
		public GClass2 gclass2_0;

		// Token: 0x0400002F RID: 47
		public string string_0;
	}

	// Token: 0x0200000F RID: 15
	[CompilerGenerated]
	private sealed class Class6
	{
		// Token: 0x06000065 RID: 101 RVA: 0x00002133 File Offset: 0x00000333
		public Class6()
		{
		}

		// Token: 0x06000066 RID: 102 RVA: 0x0000234F File Offset: 0x0000054F
		internal void method_0(object object_0)
		{
			this.eventHandler_0(this.gclass2_0, this.string_0);
		}

		// Token: 0x04000030 RID: 48
		public EventHandler<string> eventHandler_0;

		// Token: 0x04000031 RID: 49
		public GClass2 gclass2_0;

		// Token: 0x04000032 RID: 50
		public string string_0;
	}

	// Token: 0x02000010 RID: 16
	[CompilerGenerated]
	[StructLayout(LayoutKind.Auto)]
	private struct Struct1 : IAsyncStateMachine
	{
		// Token: 0x06000067 RID: 103 RVA: 0x000041DC File Offset: 0x000023DC
		void IAsyncStateMachine.MoveNext()
		{
			int num2;
			int num = num2;
			GClass2 gclass = this;
			try
			{
				if (num != 0)
				{
					stringBuilder = new StringBuilder();
					array = new char[1024];
					goto IL_E7;
				}
				TaskAwaiter<int> taskAwaiter2;
				TaskAwaiter<int> taskAwaiter = taskAwaiter2;
				taskAwaiter2 = default(TaskAwaiter<int>);
				num2 = -1;
				IL_AD:
				int result = taskAwaiter.GetResult();
				int num3 = result;
				stringBuilder.Append(array.SubArray(0, num3));
				gclass.vmethod_2(stringBuilder.ToString());
				Thread.Sleep(1);
				IL_E7:
				if (gclass.process_0.HasExited)
				{
					gclass.Boolean_0 = false;
				}
				else
				{
					stringBuilder.Clear();
					taskAwaiter = gclass.process_0.StandardOutput.ReadAsync(array, 0, array.Length).GetAwaiter();
					if (!taskAwaiter.IsCompleted)
					{
						num2 = 0;
						taskAwaiter2 = taskAwaiter;
						this.asyncVoidMethodBuilder_0.AwaitUnsafeOnCompleted<TaskAwaiter<int>, GClass2.Struct1>(ref taskAwaiter, ref this);
						return;
					}
					goto IL_AD;
				}
			}
			catch (Exception ex)
			{
				num2 = -2;
				stringBuilder = null;
				array = null;
				this.asyncVoidMethodBuilder_0.SetException(ex);
				return;
			}
			num2 = -2;
			stringBuilder = null;
			array = null;
			this.asyncVoidMethodBuilder_0.SetResult();
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002368 File Offset: 0x00000568
		[DebuggerHidden]
		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
		{
			this.asyncVoidMethodBuilder_0.SetStateMachine(stateMachine);
		}

		// Token: 0x04000033 RID: 51
		public int int_0;

		// Token: 0x04000034 RID: 52
		public AsyncVoidMethodBuilder asyncVoidMethodBuilder_0;

		// Token: 0x04000035 RID: 53
		public GClass2 gclass2_0;

		// Token: 0x04000036 RID: 54
		private StringBuilder stringBuilder_0;

		// Token: 0x04000037 RID: 55
		private char[] char_0;

		// Token: 0x04000038 RID: 56
		private TaskAwaiter<int> taskAwaiter_0;
	}

	// Token: 0x02000011 RID: 17
	[CompilerGenerated]
	[StructLayout(LayoutKind.Auto)]
	private struct Struct2 : IAsyncStateMachine
	{
		// Token: 0x06000069 RID: 105 RVA: 0x00004344 File Offset: 0x00002544
		void IAsyncStateMachine.MoveNext()
		{
			int num2;
			int num = num2;
			GClass2 gclass = this;
			try
			{
				TaskAwaiter<int> taskAwaiter;
				if (num == 0)
				{
					TaskAwaiter<int> taskAwaiter2;
					taskAwaiter = taskAwaiter2;
					taskAwaiter2 = default(TaskAwaiter<int>);
					num2 = -1;
					goto IL_A8;
				}
				stringBuilder = new StringBuilder();
				IL_1C:
				stringBuilder.Clear();
				array = new char[1024];
				taskAwaiter = gclass.process_0.StandardError.ReadAsync(array, 0, array.Length).GetAwaiter();
				if (!taskAwaiter.IsCompleted)
				{
					num2 = 0;
					TaskAwaiter<int> taskAwaiter2 = taskAwaiter;
					this.asyncVoidMethodBuilder_0.AwaitUnsafeOnCompleted<TaskAwaiter<int>, GClass2.Struct2>(ref taskAwaiter, ref this);
					return;
				}
				IL_A8:
				int result = taskAwaiter.GetResult();
				int num3 = result;
				stringBuilder.Append(array.SubArray(0, num3));
				gclass.vmethod_0(stringBuilder.ToString());
				Thread.Sleep(1);
				array = null;
				if (!gclass.process_0.HasExited)
				{
					goto IL_1C;
				}
			}
			catch (Exception ex)
			{
				num2 = -2;
				stringBuilder = null;
				this.asyncVoidMethodBuilder_0.SetException(ex);
				return;
			}
			num2 = -2;
			stringBuilder = null;
			this.asyncVoidMethodBuilder_0.SetResult();
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002376 File Offset: 0x00000576
		[DebuggerHidden]
		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
		{
			this.asyncVoidMethodBuilder_0.SetStateMachine(stateMachine);
		}

		// Token: 0x04000039 RID: 57
		public int int_0;

		// Token: 0x0400003A RID: 58
		public AsyncVoidMethodBuilder asyncVoidMethodBuilder_0;

		// Token: 0x0400003B RID: 59
		public GClass2 gclass2_0;

		// Token: 0x0400003C RID: 60
		private StringBuilder stringBuilder_0;

		// Token: 0x0400003D RID: 61
		private char[] char_0;

		// Token: 0x0400003E RID: 62
		private TaskAwaiter<int> taskAwaiter_0;
	}

	// Token: 0x02000012 RID: 18
	[CompilerGenerated]
	[StructLayout(LayoutKind.Auto)]
	private struct Struct3 : IAsyncStateMachine
	{
		// Token: 0x0600006B RID: 107 RVA: 0x00004498 File Offset: 0x00002698
		void IAsyncStateMachine.MoveNext()
		{
			int num2;
			int num = num2;
			GClass2 gclass = this;
			try
			{
				TaskAwaiter taskAwaiter2;
				TaskAwaiter taskAwaiter;
				if (num == 0)
				{
					taskAwaiter = taskAwaiter2;
					taskAwaiter2 = default(TaskAwaiter);
					num = (num2 = -1);
					goto IL_92;
				}
				TaskAwaiter taskAwaiter3;
				if (num == 1)
				{
					taskAwaiter3 = taskAwaiter2;
					taskAwaiter2 = default(TaskAwaiter);
					num = (num2 = -1);
					goto IL_F7;
				}
				IL_12B:
				while (!gclass.process_0.HasExited)
				{
					Thread.Sleep(1);
					if (gclass.string_1 != null)
					{
						taskAwaiter = gclass.process_0.StandardInput.WriteLineAsync(gclass.string_1).GetAwaiter();
						if (!taskAwaiter.IsCompleted)
						{
							num = (num2 = 0);
							taskAwaiter2 = taskAwaiter;
							this.asyncVoidMethodBuilder_0.AwaitUnsafeOnCompleted<TaskAwaiter, GClass2.Struct3>(ref taskAwaiter, ref this);
							return;
						}
						goto IL_92;
					}
				}
				goto IL_156;
				IL_92:
				taskAwaiter.GetResult();
				taskAwaiter3 = gclass.process_0.StandardInput.FlushAsync().GetAwaiter();
				if (!taskAwaiter3.IsCompleted)
				{
					num = (num2 = 1);
					taskAwaiter2 = taskAwaiter3;
					this.asyncVoidMethodBuilder_0.AwaitUnsafeOnCompleted<TaskAwaiter, GClass2.Struct3>(ref taskAwaiter3, ref this);
					return;
				}
				IL_F7:
				taskAwaiter3.GetResult();
				object object_ = gclass.object_0;
				bool flag = false;
				try
				{
					Monitor.Enter(object_, ref flag);
					gclass.string_1 = null;
				}
				finally
				{
					if (num < 0 && flag)
					{
						Monitor.Exit(object_);
					}
				}
				goto IL_12B;
			}
			catch (Exception ex)
			{
				num2 = -2;
				this.asyncVoidMethodBuilder_0.SetException(ex);
				return;
			}
			IL_156:
			num2 = -2;
			this.asyncVoidMethodBuilder_0.SetResult();
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002384 File Offset: 0x00000584
		[DebuggerHidden]
		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
		{
			this.asyncVoidMethodBuilder_0.SetStateMachine(stateMachine);
		}

		// Token: 0x0400003F RID: 63
		public int int_0;

		// Token: 0x04000040 RID: 64
		public AsyncVoidMethodBuilder asyncVoidMethodBuilder_0;

		// Token: 0x04000041 RID: 65
		public GClass2 gclass2_0;

		// Token: 0x04000042 RID: 66
		private TaskAwaiter taskAwaiter_0;
	}
}
