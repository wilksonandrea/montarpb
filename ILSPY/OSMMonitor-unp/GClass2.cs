using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Plugin.Core.Utility;

public class GClass2
{
	[CompilerGenerated]
	private sealed class Class5
	{
		public EventHandler<string> eventHandler_0;

		public GClass2 gclass2_0;

		public string string_0;

		internal void method_0(object object_0)
		{
			eventHandler_0(gclass2_0, string_0);
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
			eventHandler_0(gclass2_0, string_0);
		}
	}

	private readonly string string_0;

	private readonly Process process_0 = new Process();

	private readonly object object_0 = new object();

	private SynchronizationContext synchronizationContext_0;

	private string string_1;

	[CompilerGenerated]
	private EventHandler<string> eventHandler_0;

	[CompilerGenerated]
	private EventHandler eventHandler_1;

	[CompilerGenerated]
	private EventHandler<string> eventHandler_2;

	[CompilerGenerated]
	private bool bool_0;

	public int Int32_0 => process_0.ExitCode;

	public bool Boolean_0
	{
		[CompilerGenerated]
		get
		{
			return bool_0;
		}
		[CompilerGenerated]
		private set
		{
			bool_0 = value;
		}
	}

	public event EventHandler<string> Event_0
	{
		[CompilerGenerated]
		add
		{
			EventHandler<string> eventHandler = eventHandler_0;
			EventHandler<string> eventHandler2;
			do
			{
				eventHandler2 = eventHandler;
				EventHandler<string> value2 = (EventHandler<string>)Delegate.Combine(eventHandler2, value);
				eventHandler = Interlocked.CompareExchange(ref eventHandler_0, value2, eventHandler2);
			}
			while ((object)eventHandler != eventHandler2);
		}
		[CompilerGenerated]
		remove
		{
			EventHandler<string> eventHandler = eventHandler_0;
			EventHandler<string> eventHandler2;
			do
			{
				eventHandler2 = eventHandler;
				EventHandler<string> value2 = (EventHandler<string>)Delegate.Remove(eventHandler2, value);
				eventHandler = Interlocked.CompareExchange(ref eventHandler_0, value2, eventHandler2);
			}
			while ((object)eventHandler != eventHandler2);
		}
	}

	public event EventHandler Event_1
	{
		[CompilerGenerated]
		add
		{
			EventHandler eventHandler = eventHandler_1;
			EventHandler eventHandler2;
			do
			{
				eventHandler2 = eventHandler;
				EventHandler value2 = (EventHandler)Delegate.Combine(eventHandler2, value);
				eventHandler = Interlocked.CompareExchange(ref eventHandler_1, value2, eventHandler2);
			}
			while ((object)eventHandler != eventHandler2);
		}
		[CompilerGenerated]
		remove
		{
			EventHandler eventHandler = eventHandler_1;
			EventHandler eventHandler2;
			do
			{
				eventHandler2 = eventHandler;
				EventHandler value2 = (EventHandler)Delegate.Remove(eventHandler2, value);
				eventHandler = Interlocked.CompareExchange(ref eventHandler_1, value2, eventHandler2);
			}
			while ((object)eventHandler != eventHandler2);
		}
	}

	public event EventHandler<string> Event_2
	{
		[CompilerGenerated]
		add
		{
			EventHandler<string> eventHandler = eventHandler_2;
			EventHandler<string> eventHandler2;
			do
			{
				eventHandler2 = eventHandler;
				EventHandler<string> value2 = (EventHandler<string>)Delegate.Combine(eventHandler2, value);
				eventHandler = Interlocked.CompareExchange(ref eventHandler_2, value2, eventHandler2);
			}
			while ((object)eventHandler != eventHandler2);
		}
		[CompilerGenerated]
		remove
		{
			EventHandler<string> eventHandler = eventHandler_2;
			EventHandler<string> eventHandler2;
			do
			{
				eventHandler2 = eventHandler;
				EventHandler<string> value2 = (EventHandler<string>)Delegate.Remove(eventHandler2, value);
				eventHandler = Interlocked.CompareExchange(ref eventHandler_2, value2, eventHandler2);
			}
			while ((object)eventHandler != eventHandler2);
		}
	}

	public GClass2(string string_2)
	{
		string_0 = string_2;
		ProcessStartInfo processStartInfo = new ProcessStartInfo(string_2)
		{
			RedirectStandardError = true,
			StandardErrorEncoding = Encoding.UTF8,
			RedirectStandardInput = true,
			RedirectStandardOutput = true
		};
		process_0.EnableRaisingEvents = true;
		processStartInfo.CreateNoWindow = true;
		processStartInfo.UseShellExecute = false;
		processStartInfo.StandardOutputEncoding = Encoding.UTF8;
		process_0.Exited += process_0_Exited;
	}

	public void method_0(params string[] string_2)
	{
		if (Boolean_0)
		{
			throw new InvalidOperationException("Process is still Running. Please wait for the process to complete.");
		}
		string arguments = string.Join(" ", string_2);
		process_0.StartInfo.Arguments = arguments;
		synchronizationContext_0 = SynchronizationContext.Current;
		process_0.Start();
		Boolean_0 = true;
		new Task(method_3).Start();
		new Task(method_5).Start();
		new Task(method_4).Start();
	}

	public void method_1(string string_2)
	{
		if (string_2 == null)
		{
			return;
		}
		lock (object_0)
		{
			string_1 = string_2;
		}
	}

	public void method_2(string string_2)
	{
		method_1(string_2 + Environment.NewLine);
	}

	protected virtual void vmethod_0(string string_2)
	{
		EventHandler<string> eventHandler_0 = this.eventHandler_0;
		if (eventHandler_0 == null)
		{
			return;
		}
		if (synchronizationContext_0 != null)
		{
			synchronizationContext_0.Post(delegate
			{
				eventHandler_0(this, string_2);
			}, null);
		}
		else
		{
			eventHandler_0(this, string_2);
		}
	}

	protected virtual void vmethod_1()
	{
		eventHandler_1?.Invoke(this, EventArgs.Empty);
	}

	protected virtual void vmethod_2(string string_2)
	{
		EventHandler<string> eventHandler_0 = eventHandler_2;
		if (eventHandler_0 == null)
		{
			return;
		}
		if (synchronizationContext_0 != null)
		{
			synchronizationContext_0.Post(delegate
			{
				eventHandler_0(this, string_2);
			}, null);
		}
		else
		{
			eventHandler_0(this, string_2);
		}
	}

	private void process_0_Exited(object sender, EventArgs e)
	{
		vmethod_1();
	}

	private async void method_3()
	{
		StringBuilder stringBuilder = new StringBuilder();
		char[] array = new char[1024];
		while (!process_0.HasExited)
		{
			stringBuilder.Clear();
			stringBuilder.Append(array.SubArray(0, await process_0.StandardOutput.ReadAsync(array, 0, array.Length)));
			vmethod_2(stringBuilder.ToString());
			Thread.Sleep(1);
		}
		Boolean_0 = false;
	}

	private async void method_4()
	{
		StringBuilder stringBuilder = new StringBuilder();
		do
		{
			stringBuilder.Clear();
			char[] array = new char[1024];
			stringBuilder.Append(array.SubArray(0, await process_0.StandardError.ReadAsync(array, 0, array.Length)));
			vmethod_0(stringBuilder.ToString());
			Thread.Sleep(1);
		}
		while (!process_0.HasExited);
	}

	private async void method_5()
	{
		while (!process_0.HasExited)
		{
			Thread.Sleep(1);
			if (string_1 != null)
			{
				await process_0.StandardInput.WriteLineAsync(string_1);
				await process_0.StandardInput.FlushAsync();
				lock (object_0)
				{
					string_1 = null;
				}
			}
		}
	}
}
