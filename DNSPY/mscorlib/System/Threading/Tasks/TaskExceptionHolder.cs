using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.ExceptionServices;
using System.Security;

namespace System.Threading.Tasks
{
	// Token: 0x02000575 RID: 1397
	internal class TaskExceptionHolder
	{
		// Token: 0x0600419A RID: 16794 RVA: 0x000F4B38 File Offset: 0x000F2D38
		internal TaskExceptionHolder(Task task)
		{
			this.m_task = task;
			TaskExceptionHolder.EnsureADUnloadCallbackRegistered();
		}

		// Token: 0x0600419B RID: 16795 RVA: 0x000F4B4C File Offset: 0x000F2D4C
		[SecuritySafeCritical]
		private static bool ShouldFailFastOnUnobservedException()
		{
			return CLRConfig.CheckThrowUnobservedTaskExceptions();
		}

		// Token: 0x0600419C RID: 16796 RVA: 0x000F4B62 File Offset: 0x000F2D62
		private static void EnsureADUnloadCallbackRegistered()
		{
			if (TaskExceptionHolder.s_adUnloadEventHandler == null && Interlocked.CompareExchange<EventHandler>(ref TaskExceptionHolder.s_adUnloadEventHandler, new EventHandler(TaskExceptionHolder.AppDomainUnloadCallback), null) == null)
			{
				AppDomain.CurrentDomain.DomainUnload += TaskExceptionHolder.s_adUnloadEventHandler;
			}
		}

		// Token: 0x0600419D RID: 16797 RVA: 0x000F4B97 File Offset: 0x000F2D97
		private static void AppDomainUnloadCallback(object sender, EventArgs e)
		{
			TaskExceptionHolder.s_domainUnloadStarted = true;
		}

		// Token: 0x0600419E RID: 16798 RVA: 0x000F4BA4 File Offset: 0x000F2DA4
		protected override void Finalize()
		{
			try
			{
				if (this.m_faultExceptions != null && !this.m_isHandled && !Environment.HasShutdownStarted && !AppDomain.CurrentDomain.IsFinalizingForUnload() && !TaskExceptionHolder.s_domainUnloadStarted)
				{
					foreach (ExceptionDispatchInfo exceptionDispatchInfo in this.m_faultExceptions)
					{
						Exception sourceException = exceptionDispatchInfo.SourceException;
						AggregateException ex = sourceException as AggregateException;
						if (ex != null)
						{
							AggregateException ex2 = ex.Flatten();
							using (IEnumerator<Exception> enumerator2 = ex2.InnerExceptions.GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									Exception ex3 = enumerator2.Current;
									if (ex3 is ThreadAbortException)
									{
										return;
									}
								}
								continue;
							}
						}
						if (sourceException is ThreadAbortException)
						{
							return;
						}
					}
					AggregateException ex4 = new AggregateException(Environment.GetResourceString("TaskExceptionHolder_UnhandledException"), this.m_faultExceptions);
					UnobservedTaskExceptionEventArgs unobservedTaskExceptionEventArgs = new UnobservedTaskExceptionEventArgs(ex4);
					TaskScheduler.PublishUnobservedTaskException(this.m_task, unobservedTaskExceptionEventArgs);
					if (TaskExceptionHolder.s_failFastOnUnobservedException && !unobservedTaskExceptionEventArgs.m_observed)
					{
						throw ex4;
					}
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x0600419F RID: 16799 RVA: 0x000F4D1C File Offset: 0x000F2F1C
		internal bool ContainsFaultList
		{
			get
			{
				return this.m_faultExceptions != null;
			}
		}

		// Token: 0x060041A0 RID: 16800 RVA: 0x000F4D29 File Offset: 0x000F2F29
		internal void Add(object exceptionObject)
		{
			this.Add(exceptionObject, false);
		}

		// Token: 0x060041A1 RID: 16801 RVA: 0x000F4D33 File Offset: 0x000F2F33
		internal void Add(object exceptionObject, bool representsCancellation)
		{
			if (representsCancellation)
			{
				this.SetCancellationException(exceptionObject);
				return;
			}
			this.AddFaultException(exceptionObject);
		}

		// Token: 0x060041A2 RID: 16802 RVA: 0x000F4D48 File Offset: 0x000F2F48
		private void SetCancellationException(object exceptionObject)
		{
			OperationCanceledException ex = exceptionObject as OperationCanceledException;
			if (ex != null)
			{
				this.m_cancellationException = ExceptionDispatchInfo.Capture(ex);
			}
			else
			{
				ExceptionDispatchInfo exceptionDispatchInfo = exceptionObject as ExceptionDispatchInfo;
				this.m_cancellationException = exceptionDispatchInfo;
			}
			this.MarkAsHandled(false);
		}

		// Token: 0x060041A3 RID: 16803 RVA: 0x000F4D84 File Offset: 0x000F2F84
		private void AddFaultException(object exceptionObject)
		{
			List<ExceptionDispatchInfo> list = this.m_faultExceptions;
			if (list == null)
			{
				list = (this.m_faultExceptions = new List<ExceptionDispatchInfo>(1));
			}
			Exception ex = exceptionObject as Exception;
			if (ex != null)
			{
				list.Add(ExceptionDispatchInfo.Capture(ex));
			}
			else
			{
				ExceptionDispatchInfo exceptionDispatchInfo = exceptionObject as ExceptionDispatchInfo;
				if (exceptionDispatchInfo != null)
				{
					list.Add(exceptionDispatchInfo);
				}
				else
				{
					IEnumerable<Exception> enumerable = exceptionObject as IEnumerable<Exception>;
					if (enumerable != null)
					{
						using (IEnumerator<Exception> enumerator = enumerable.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								Exception ex2 = enumerator.Current;
								list.Add(ExceptionDispatchInfo.Capture(ex2));
							}
							goto IL_B3;
						}
					}
					IEnumerable<ExceptionDispatchInfo> enumerable2 = exceptionObject as IEnumerable<ExceptionDispatchInfo>;
					if (enumerable2 == null)
					{
						throw new ArgumentException(Environment.GetResourceString("TaskExceptionHolder_UnknownExceptionType"), "exceptionObject");
					}
					list.AddRange(enumerable2);
				}
			}
			IL_B3:
			for (int i = 0; i < list.Count; i++)
			{
				Type type = list[i].SourceException.GetType();
				if (type != typeof(ThreadAbortException) && type != typeof(AppDomainUnloadedException))
				{
					this.MarkAsUnhandled();
					return;
				}
				if (i == list.Count - 1)
				{
					this.MarkAsHandled(false);
				}
			}
		}

		// Token: 0x060041A4 RID: 16804 RVA: 0x000F4EC0 File Offset: 0x000F30C0
		private void MarkAsUnhandled()
		{
			if (this.m_isHandled)
			{
				GC.ReRegisterForFinalize(this);
				this.m_isHandled = false;
			}
		}

		// Token: 0x060041A5 RID: 16805 RVA: 0x000F4EDB File Offset: 0x000F30DB
		internal void MarkAsHandled(bool calledFromFinalizer)
		{
			if (!this.m_isHandled)
			{
				if (!calledFromFinalizer)
				{
					GC.SuppressFinalize(this);
				}
				this.m_isHandled = true;
			}
		}

		// Token: 0x060041A6 RID: 16806 RVA: 0x000F4EFC File Offset: 0x000F30FC
		internal AggregateException CreateExceptionObject(bool calledFromFinalizer, Exception includeThisException)
		{
			List<ExceptionDispatchInfo> faultExceptions = this.m_faultExceptions;
			this.MarkAsHandled(calledFromFinalizer);
			if (includeThisException == null)
			{
				return new AggregateException(faultExceptions);
			}
			Exception[] array = new Exception[faultExceptions.Count + 1];
			for (int i = 0; i < array.Length - 1; i++)
			{
				array[i] = faultExceptions[i].SourceException;
			}
			array[array.Length - 1] = includeThisException;
			return new AggregateException(array);
		}

		// Token: 0x060041A7 RID: 16807 RVA: 0x000F4F60 File Offset: 0x000F3160
		internal ReadOnlyCollection<ExceptionDispatchInfo> GetExceptionDispatchInfos()
		{
			List<ExceptionDispatchInfo> faultExceptions = this.m_faultExceptions;
			this.MarkAsHandled(false);
			return new ReadOnlyCollection<ExceptionDispatchInfo>(faultExceptions);
		}

		// Token: 0x060041A8 RID: 16808 RVA: 0x000F4F84 File Offset: 0x000F3184
		internal ExceptionDispatchInfo GetCancellationExceptionDispatchInfo()
		{
			return this.m_cancellationException;
		}

		// Token: 0x060041A9 RID: 16809 RVA: 0x000F4F99 File Offset: 0x000F3199
		// Note: this type is marked as 'beforefieldinit'.
		static TaskExceptionHolder()
		{
		}

		// Token: 0x04001B63 RID: 7011
		private static readonly bool s_failFastOnUnobservedException = TaskExceptionHolder.ShouldFailFastOnUnobservedException();

		// Token: 0x04001B64 RID: 7012
		private static volatile bool s_domainUnloadStarted;

		// Token: 0x04001B65 RID: 7013
		private static volatile EventHandler s_adUnloadEventHandler;

		// Token: 0x04001B66 RID: 7014
		private readonly Task m_task;

		// Token: 0x04001B67 RID: 7015
		private volatile List<ExceptionDispatchInfo> m_faultExceptions;

		// Token: 0x04001B68 RID: 7016
		private ExceptionDispatchInfo m_cancellationException;

		// Token: 0x04001B69 RID: 7017
		private volatile bool m_isHandled;
	}
}
