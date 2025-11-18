// Decompiled with JetBrains decompiler
// Type: GClass2
// Assembly: OSM-Monitor, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 7AB73F6A-01A7-48C3-AEBF-9703F61790CD
// Assembly location: C:\Users\Administrator\Desktop\unpack\OSMMonitor-unp.exe

using Plugin.Core.Utility;
using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
public class GClass2
{
  private readonly string string_0;
  private readonly Process process_0 = new Process();
  private readonly object object_0 = new object();
  private SynchronizationContext synchronizationContext_0;
  private string string_1;

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

  public event EventHandler<string> Event_0;

  public event EventHandler Event_1;

  public event EventHandler<string> Event_2;

  public int Int32_0 => this.process_0.ExitCode;

  public bool Boolean_0 { get; private set; }

  public void method_0(params string[] string_2)
  {
    if (this.Boolean_0)
      throw new InvalidOperationException("Process is still Running. Please wait for the process to complete.");
    this.process_0.StartInfo.Arguments = string.Join(" ", string_2);
    this.synchronizationContext_0 = SynchronizationContext.Current;
    this.process_0.Start();
    this.Boolean_0 = true;
    new Task(new Action(this.method_3)).Start();
    new Task(new Action(this.method_5)).Start();
    new Task(new Action(this.method_4)).Start();
  }

  public void method_1(string string_2)
  {
    if (string_2 == null)
      return;
    lock (this.object_0)
      this.string_1 = string_2;
  }

  public void method_2(string string_2) => this.method_1(string_2 + Environment.NewLine);

  protected virtual void vmethod_0(string string_2)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    GClass2.Class5 class5 = new GClass2.Class5();
    // ISSUE: reference to a compiler-generated field
    class5.gclass2_0 = this;
    // ISSUE: reference to a compiler-generated field
    class5.string_0 = string_2;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    class5.eventHandler_0 = this.eventHandler_0;
    // ISSUE: reference to a compiler-generated field
    if (class5.eventHandler_0 == null)
      return;
    if (this.synchronizationContext_0 != null)
    {
      // ISSUE: reference to a compiler-generated method
      this.synchronizationContext_0.Post(new SendOrPostCallback(class5.method_0), (object) null);
    }
    else
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      class5.eventHandler_0((object) this, class5.string_0);
    }
  }

  protected virtual void vmethod_1()
  {
    // ISSUE: reference to a compiler-generated field
    EventHandler eventHandler1 = this.eventHandler_1;
    if (eventHandler1 == null)
      return;
    eventHandler1((object) this, EventArgs.Empty);
  }

  protected virtual void vmethod_2(string string_2)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    GClass2.Class6 class6 = new GClass2.Class6();
    // ISSUE: reference to a compiler-generated field
    class6.gclass2_0 = this;
    // ISSUE: reference to a compiler-generated field
    class6.string_0 = string_2;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    class6.eventHandler_0 = this.eventHandler_2;
    // ISSUE: reference to a compiler-generated field
    if (class6.eventHandler_0 == null)
      return;
    if (this.synchronizationContext_0 != null)
    {
      // ISSUE: reference to a compiler-generated method
      this.synchronizationContext_0.Post(new SendOrPostCallback(class6.method_0), (object) null);
    }
    else
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      class6.eventHandler_0((object) this, class6.string_0);
    }
  }

  private void process_0_Exited(object sender, EventArgs e) => this.vmethod_1();

  private async void method_3()
  {
    StringBuilder stringBuilder = new StringBuilder();
    char[] buffer = new char[1024 /*0x0400*/];
    while (!this.process_0.HasExited)
    {
      stringBuilder.Clear();
      stringBuilder.Append(buffer.SubArray(0, await this.process_0.StandardOutput.ReadAsync(buffer, 0, buffer.Length)));
      this.vmethod_2(stringBuilder.ToString());
      Thread.Sleep(1);
    }
    this.Boolean_0 = false;
    stringBuilder = (StringBuilder) null;
    buffer = (char[]) null;
  }

  private async void method_4()
  {
    StringBuilder stringBuilder = new StringBuilder();
    do
    {
      stringBuilder.Clear();
      char[] buffer = new char[1024 /*0x0400*/];
      stringBuilder.Append(buffer.SubArray(0, await this.process_0.StandardError.ReadAsync(buffer, 0, buffer.Length)));
      this.vmethod_0(stringBuilder.ToString());
      Thread.Sleep(1);
      buffer = (char[]) null;
    }
    while (!this.process_0.HasExited);
    stringBuilder = (StringBuilder) null;
  }

  private async void method_5()
  {
    while (!this.process_0.HasExited)
    {
      Thread.Sleep(1);
      if (this.string_1 != null)
      {
        await this.process_0.StandardInput.WriteLineAsync(this.string_1);
        await this.process_0.StandardInput.FlushAsync();
        lock (this.object_0)
          this.string_1 = (string) null;
      }
    }
  }
}
