using Plugin.Core.Utility;
using System;
using System.Diagnostics;
using System.IO;
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

	public bool Boolean_0
	{
		get;
		private set;
	}

	public int Int32_0
	{
		get
		{
			return this.process_0.ExitCode;
		}
	}

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
		(new Task(new Action(this.method_3))).Start();
		(new Task(new Action(this.method_5))).Start();
		(new Task(new Action(this.method_4))).Start();
	}

	public void method_1(string string_2)
	{
		if (string_2 == null)
		{
			return;
		}
		lock (this.object_0)
		{
			this.string_1 = string_2;
		}
	}

	public void method_2(string string_2)
	{
		this.method_1(string.Concat(string_2, Environment.NewLine));
	}

	private async void method_3()
	{
		StringBuilder stringBuilder = new StringBuilder();
		char[] chrArray = new char[1024];
		while (!this.process_0.HasExited)
		{
			stringBuilder.Clear();
			int ınt32 = await this.process_0.StandardOutput.ReadAsync(chrArray, 0, (int)chrArray.Length);
			stringBuilder.Append(chrArray.SubArray(0, ınt32));
			this.vmethod_2(stringBuilder.ToString());
			Thread.Sleep(1);
		}
		this.Boolean_0 = false;
		stringBuilder = null;
		chrArray = null;
	}

	private async void method_4()
	{
		StringBuilder stringBuilder = new StringBuilder();
		do
		{
			stringBuilder.Clear();
			char[] chrArray = new char[1024];
			int ınt32 = await this.process_0.StandardError.ReadAsync(chrArray, 0, (int)chrArray.Length);
			stringBuilder.Append(chrArray.SubArray(0, ınt32));
			this.vmethod_0(stringBuilder.ToString());
			Thread.Sleep(1);
			chrArray = null;
		}
		while (!this.process_0.HasExited);
		stringBuilder = null;
	}

	private async void method_5()
	{
		while (!this.process_0.HasExited)
		{
			Thread.Sleep(1);
			if (this.string_1 == null)
			{
				continue;
			}
			await this.process_0.StandardInput.WriteLineAsync(this.string_1);
			await this.process_0.StandardInput.FlushAsync();
			lock (this.object_0)
			{
				this.string_1 = null;
			}
		}
	}

	private void process_0_Exited(object sender, EventArgs e)
	{
		this.vmethod_1();
	}

	protected virtual void vmethod_0(string string_2)
	{
		EventHandler<string> eventHandler0 = this.eventHandler_0;
		if (eventHandler0 != null)
		{
			if (this.synchronizationContext_0 != null)
			{
				this.synchronizationContext_0.Post((object object_0) => eventHandler0(this, string_2), null);
				return;
			}
			eventHandler0(this, string_2);
		}
	}

	protected virtual void vmethod_1()
	{
		EventHandler eventHandler1 = this.eventHandler_1;
		if (eventHandler1 != null)
		{
			eventHandler1(this, EventArgs.Empty);
		}
	}

	protected virtual void vmethod_2(string string_2)
	{
		EventHandler<string> eventHandler2 = this.eventHandler_2;
		if (eventHandler2 != null)
		{
			if (this.synchronizationContext_0 != null)
			{
				this.synchronizationContext_0.Post((object object_0) => eventHandler2(this, string_2), null);
				return;
			}
			eventHandler2(this, string_2);
		}
	}

	public event EventHandler<string> Event_0
	{
		add
		{
			EventHandler<string> eventHandler;
			EventHandler<string> eventHandler0 = this.eventHandler_0;
			do
			{
				eventHandler = eventHandler0;
				EventHandler<string> eventHandler1 = (EventHandler<string>)Delegate.Combine(eventHandler, value);
				eventHandler0 = Interlocked.CompareExchange<EventHandler<string>>(ref this.eventHandler_0, eventHandler1, eventHandler);
			}
			while ((object)eventHandler0 != (object)eventHandler);
		}
		remove
		{
			EventHandler<string> eventHandler;
			EventHandler<string> eventHandler0 = this.eventHandler_0;
			do
			{
				eventHandler = eventHandler0;
				EventHandler<string> eventHandler1 = (EventHandler<string>)Delegate.Remove(eventHandler, value);
				eventHandler0 = Interlocked.CompareExchange<EventHandler<string>>(ref this.eventHandler_0, eventHandler1, eventHandler);
			}
			while ((object)eventHandler0 != (object)eventHandler);
		}
	}

	public event EventHandler Event_1
	{
		add
		{
			EventHandler eventHandler;
			EventHandler eventHandler1 = this.eventHandler_1;
			do
			{
				eventHandler = eventHandler1;
				EventHandler eventHandler2 = (EventHandler)Delegate.Combine(eventHandler, value);
				eventHandler1 = Interlocked.CompareExchange<EventHandler>(ref this.eventHandler_1, eventHandler2, eventHandler);
			}
			while ((object)eventHandler1 != (object)eventHandler);
		}
		remove
		{
			EventHandler eventHandler;
			EventHandler eventHandler1 = this.eventHandler_1;
			do
			{
				eventHandler = eventHandler1;
				EventHandler eventHandler2 = (EventHandler)Delegate.Remove(eventHandler, value);
				eventHandler1 = Interlocked.CompareExchange<EventHandler>(ref this.eventHandler_1, eventHandler2, eventHandler);
			}
			while ((object)eventHandler1 != (object)eventHandler);
		}
	}

	public event EventHandler<string> Event_2
	{
		add
		{
			EventHandler<string> eventHandler;
			EventHandler<string> eventHandler2 = this.eventHandler_2;
			do
			{
				eventHandler = eventHandler2;
				EventHandler<string> eventHandler1 = (EventHandler<string>)Delegate.Combine(eventHandler, value);
				eventHandler2 = Interlocked.CompareExchange<EventHandler<string>>(ref this.eventHandler_2, eventHandler1, eventHandler);
			}
			while ((object)eventHandler2 != (object)eventHandler);
		}
		remove
		{
			EventHandler<string> eventHandler;
			EventHandler<string> eventHandler2 = this.eventHandler_2;
			do
			{
				eventHandler = eventHandler2;
				EventHandler<string> eventHandler1 = (EventHandler<string>)Delegate.Remove(eventHandler, value);
				eventHandler2 = Interlocked.CompareExchange<EventHandler<string>>(ref this.eventHandler_2, eventHandler1, eventHandler);
			}
			while ((object)eventHandler2 != (object)eventHandler);
		}
	}
}