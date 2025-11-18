// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Colorful.TextAnnotator
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

#nullable disable
namespace Plugin.Core.Colorful;

public sealed class TextAnnotator
{
  private StyleSheet styleSheet_0;
  private Dictionary<StyleClass<TextPattern>, Styler.MatchFound> dictionary_0;

  public Task Enqueue([In] Func<Task> obj0)
  {
    // ISSUE: variable of a compiler-generated type
    TaskQueue.Struct5 stateMachine;
    // ISSUE: reference to a compiler-generated field
    stateMachine.asyncTaskMethodBuilder_0 = AsyncTaskMethodBuilder.Create();
    // ISSUE: reference to a compiler-generated field
    stateMachine.taskQueue_0 = (TaskQueue) this;
    // ISSUE: reference to a compiler-generated field
    stateMachine.func_0 = obj0;
    // ISSUE: reference to a compiler-generated field
    stateMachine.int_0 = -1;
    // ISSUE: reference to a compiler-generated field
    stateMachine.asyncTaskMethodBuilder_0.Start<TaskQueue.Struct5>(ref stateMachine);
    // ISSUE: reference to a compiler-generated field
    return stateMachine.asyncTaskMethodBuilder_0.Task;
  }

  private void MoveNext()
  {
    // ISSUE: reference to a compiler-generated field
    int num = ((TaskQueue.Struct4<T0>) this).int_0;
    // ISSUE: reference to a compiler-generated field
    TaskQueue taskQueue0 = ((TaskQueue.Struct4<T0>) this).taskQueue_0;
    T0 result;
    try
    {
      TaskAwaiter awaiter1;
      switch (num)
      {
        case 0:
          // ISSUE: reference to a compiler-generated field
          awaiter1 = ((TaskQueue.Struct4<T0>) this).taskAwaiter_0;
          // ISSUE: reference to a compiler-generated field
          ((TaskQueue.Struct4<T0>) this).taskAwaiter_0 = new TaskAwaiter();
          num = -1;
          // ISSUE: reference to a compiler-generated field
          ((TaskQueue.Struct4<T0>) this).int_0 = -1;
          break;
        case 1:
label_5:
          try
          {
            TaskAwaiter<T0> awaiter2;
            if (num != 1)
            {
              // ISSUE: reference to a compiler-generated field
              awaiter2 = ((TaskQueue.Struct4<T0>) this).func_0().GetAwaiter();
              if (!awaiter2.IsCompleted)
              {
                num = 1;
                // ISSUE: reference to a compiler-generated field
                ((TaskQueue.Struct4<T0>) this).int_0 = 1;
                // ISSUE: reference to a compiler-generated field
                ((TaskQueue.Struct4<T0>) this).taskAwaiter_1 = awaiter2;
                // ISSUE: reference to a compiler-generated field
                // ISSUE: cast to a reference type
                ((TaskQueue.Struct4<T0>) this).asyncTaskMethodBuilder_0.AwaitUnsafeOnCompleted<TaskAwaiter<T0>, TaskQueue.Struct4<T0>>(ref awaiter2, (TaskQueue.Struct4<T0>&) this);
                return;
              }
            }
            else
            {
              // ISSUE: reference to a compiler-generated field
              awaiter2 = ((TaskQueue.Struct4<T0>) this).taskAwaiter_1;
              // ISSUE: reference to a compiler-generated field
              ((TaskQueue.Struct4<T0>) this).taskAwaiter_1 = new TaskAwaiter<T0>();
              num = -1;
              // ISSUE: reference to a compiler-generated field
              ((TaskQueue.Struct4<T0>) this).int_0 = -1;
            }
            result = awaiter2.GetResult();
            goto label_16;
          }
          finally
          {
            if (num < 0)
              taskQueue0.semaphoreSlim_0.Release();
          }
        default:
          awaiter1 = taskQueue0.semaphoreSlim_0.WaitAsync().GetAwaiter();
          if (!awaiter1.IsCompleted)
          {
            // ISSUE: reference to a compiler-generated field
            ((TaskQueue.Struct4<T0>) this).int_0 = 0;
            // ISSUE: reference to a compiler-generated field
            ((TaskQueue.Struct4<T0>) this).taskAwaiter_0 = awaiter1;
            // ISSUE: reference to a compiler-generated field
            // ISSUE: cast to a reference type
            ((TaskQueue.Struct4<T0>) this).asyncTaskMethodBuilder_0.AwaitUnsafeOnCompleted<TaskAwaiter, TaskQueue.Struct4<T0>>(ref awaiter1, (TaskQueue.Struct4<T0>&) this);
            return;
          }
          break;
      }
      awaiter1.GetResult();
      goto label_5;
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated field
      ((TaskQueue.Struct4<T0>) this).int_0 = -2;
      // ISSUE: reference to a compiler-generated field
      ((TaskQueue.Struct4<T0>) this).asyncTaskMethodBuilder_0.SetException(ex);
      return;
    }
label_16:
    // ISSUE: reference to a compiler-generated field
    ((TaskQueue.Struct4<T0>) this).int_0 = -2;
    // ISSUE: reference to a compiler-generated field
    ((TaskQueue.Struct4<T0>) this).asyncTaskMethodBuilder_0.SetResult(result);
  }

  [DebuggerHidden]
  private void SetStateMachine(IAsyncStateMachine string_0)
  {
    // ISSUE: reference to a compiler-generated field
    ((TaskQueue.Struct4<T0>) this).asyncTaskMethodBuilder_0.SetStateMachine(string_0);
  }

  private void MoveNext()
  {
    // ISSUE: reference to a compiler-generated field
    int num = ((TaskQueue.Struct5) this).int_0;
    // ISSUE: reference to a compiler-generated field
    TaskQueue taskQueue0 = ((TaskQueue.Struct5) this).taskQueue_0;
    try
    {
      TaskAwaiter awaiter;
      switch (num)
      {
        case 0:
          // ISSUE: reference to a compiler-generated field
          awaiter = ((TaskQueue.Struct5) this).taskAwaiter_0;
          // ISSUE: reference to a compiler-generated field
          ((TaskQueue.Struct5) this).taskAwaiter_0 = new TaskAwaiter();
          num = -1;
          // ISSUE: reference to a compiler-generated field
          ((TaskQueue.Struct5) this).int_0 = -1;
          break;
        case 1:
label_6:
          try
          {
            if (num != 1)
            {
              // ISSUE: reference to a compiler-generated field
              awaiter = ((TaskQueue.Struct5) this).func_0().GetAwaiter();
              if (!awaiter.IsCompleted)
              {
                num = 1;
                // ISSUE: reference to a compiler-generated field
                ((TaskQueue.Struct5) this).int_0 = 1;
                // ISSUE: reference to a compiler-generated field
                ((TaskQueue.Struct5) this).taskAwaiter_0 = awaiter;
                // ISSUE: reference to a compiler-generated field
                // ISSUE: cast to a reference type
                ((TaskQueue.Struct5) this).asyncTaskMethodBuilder_0.AwaitUnsafeOnCompleted<TaskAwaiter, TaskQueue.Struct5>(ref awaiter, (TaskQueue.Struct5&) this);
                return;
              }
            }
            else
            {
              // ISSUE: reference to a compiler-generated field
              awaiter = ((TaskQueue.Struct5) this).taskAwaiter_0;
              // ISSUE: reference to a compiler-generated field
              ((TaskQueue.Struct5) this).taskAwaiter_0 = new TaskAwaiter();
              num = -1;
              // ISSUE: reference to a compiler-generated field
              ((TaskQueue.Struct5) this).int_0 = -1;
            }
            awaiter.GetResult();
            goto label_16;
          }
          finally
          {
            if (num < 0)
              taskQueue0.semaphoreSlim_0.Release();
          }
        default:
          awaiter = taskQueue0.semaphoreSlim_0.WaitAsync().GetAwaiter();
          if (!awaiter.IsCompleted)
          {
            // ISSUE: reference to a compiler-generated field
            ((TaskQueue.Struct5) this).int_0 = 0;
            // ISSUE: reference to a compiler-generated field
            ((TaskQueue.Struct5) this).taskAwaiter_0 = awaiter;
            // ISSUE: reference to a compiler-generated field
            // ISSUE: cast to a reference type
            ((TaskQueue.Struct5) this).asyncTaskMethodBuilder_0.AwaitUnsafeOnCompleted<TaskAwaiter, TaskQueue.Struct5>(ref awaiter, (TaskQueue.Struct5&) this);
            return;
          }
          break;
      }
      awaiter.GetResult();
      goto label_6;
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated field
      ((TaskQueue.Struct5) this).int_0 = -2;
      // ISSUE: reference to a compiler-generated field
      ((TaskQueue.Struct5) this).asyncTaskMethodBuilder_0.SetException(ex);
      return;
    }
label_16:
    // ISSUE: reference to a compiler-generated field
    ((TaskQueue.Struct5) this).int_0 = -2;
    // ISSUE: reference to a compiler-generated field
    ((TaskQueue.Struct5) this).asyncTaskMethodBuilder_0.SetResult();
  }
}
