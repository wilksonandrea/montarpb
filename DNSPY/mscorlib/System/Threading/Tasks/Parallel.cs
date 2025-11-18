using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Permissions;

namespace System.Threading.Tasks
{
	// Token: 0x02000551 RID: 1361
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public static class Parallel
	{
		// Token: 0x06004019 RID: 16409 RVA: 0x000EE118 File Offset: 0x000EC318
		[__DynamicallyInvokable]
		public static void Invoke(params Action[] actions)
		{
			Parallel.Invoke(Parallel.s_defaultParallelOptions, actions);
		}

		// Token: 0x0600401A RID: 16410 RVA: 0x000EE128 File Offset: 0x000EC328
		[__DynamicallyInvokable]
		public static void Invoke(ParallelOptions parallelOptions, params Action[] actions)
		{
			if (actions == null)
			{
				throw new ArgumentNullException("actions");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			if (parallelOptions.CancellationToken.CanBeCanceled && AppContextSwitches.ThrowExceptionIfDisposedCancellationTokenSource)
			{
				parallelOptions.CancellationToken.ThrowIfSourceDisposed();
			}
			if (parallelOptions.CancellationToken.IsCancellationRequested)
			{
				throw new OperationCanceledException(parallelOptions.CancellationToken);
			}
			Action[] actionsCopy = new Action[actions.Length];
			for (int i = 0; i < actionsCopy.Length; i++)
			{
				actionsCopy[i] = actions[i];
				if (actionsCopy[i] == null)
				{
					throw new ArgumentException(Environment.GetResourceString("Parallel_Invoke_ActionNull"));
				}
			}
			int num = 0;
			Task task = null;
			if (TplEtwProvider.Log.IsEnabled())
			{
				num = Interlocked.Increment(ref Parallel.s_forkJoinContextID);
				task = Task.InternalCurrent;
				TplEtwProvider.Log.ParallelInvokeBegin((task != null) ? task.m_taskScheduler.Id : TaskScheduler.Current.Id, (task != null) ? task.Id : 0, num, TplEtwProvider.ForkJoinOperationType.ParallelInvoke, actionsCopy.Length);
			}
			if (actionsCopy.Length < 1)
			{
				return;
			}
			try
			{
				if (actionsCopy.Length > 10 || (parallelOptions.MaxDegreeOfParallelism != -1 && parallelOptions.MaxDegreeOfParallelism < actionsCopy.Length))
				{
					ConcurrentQueue<Exception> exceptionQ = null;
					try
					{
						int actionIndex = 0;
						ParallelForReplicatingTask parallelForReplicatingTask = new ParallelForReplicatingTask(parallelOptions, delegate
						{
							for (int l = Interlocked.Increment(ref actionIndex); l <= actionsCopy.Length; l = Interlocked.Increment(ref actionIndex))
							{
								try
								{
									actionsCopy[l - 1]();
								}
								catch (Exception ex5)
								{
									LazyInitializer.EnsureInitialized<ConcurrentQueue<Exception>>(ref exceptionQ, () => new ConcurrentQueue<Exception>());
									exceptionQ.Enqueue(ex5);
								}
								if (parallelOptions.CancellationToken.IsCancellationRequested)
								{
									throw new OperationCanceledException(parallelOptions.CancellationToken);
								}
							}
						}, TaskCreationOptions.None, InternalTaskOptions.SelfReplicating);
						parallelForReplicatingTask.RunSynchronously(parallelOptions.EffectiveTaskScheduler);
						parallelForReplicatingTask.Wait();
					}
					catch (Exception ex)
					{
						LazyInitializer.EnsureInitialized<ConcurrentQueue<Exception>>(ref exceptionQ, () => new ConcurrentQueue<Exception>());
						AggregateException ex2 = ex as AggregateException;
						if (ex2 != null)
						{
							using (IEnumerator<Exception> enumerator = ex2.InnerExceptions.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									Exception ex3 = enumerator.Current;
									exceptionQ.Enqueue(ex3);
								}
								goto IL_23C;
							}
						}
						exceptionQ.Enqueue(ex);
						IL_23C:;
					}
					if (exceptionQ != null && exceptionQ.Count > 0)
					{
						Parallel.ThrowIfReducableToSingleOCE(exceptionQ, parallelOptions.CancellationToken);
						throw new AggregateException(exceptionQ);
					}
				}
				else
				{
					Task[] array = new Task[actionsCopy.Length];
					if (parallelOptions.CancellationToken.IsCancellationRequested)
					{
						throw new OperationCanceledException(parallelOptions.CancellationToken);
					}
					for (int j = 1; j < array.Length; j++)
					{
						array[j] = Task.Factory.StartNew(actionsCopy[j], parallelOptions.CancellationToken, TaskCreationOptions.None, InternalTaskOptions.None, parallelOptions.EffectiveTaskScheduler);
					}
					array[0] = new Task(actionsCopy[0]);
					array[0].RunSynchronously(parallelOptions.EffectiveTaskScheduler);
					try
					{
						if (array.Length <= 4)
						{
							Task.FastWaitAll(array);
						}
						else
						{
							Task.WaitAll(array);
						}
					}
					catch (AggregateException ex4)
					{
						Parallel.ThrowIfReducableToSingleOCE(ex4.InnerExceptions, parallelOptions.CancellationToken);
						throw;
					}
					finally
					{
						for (int k = 0; k < array.Length; k++)
						{
							if (array[k].IsCompleted)
							{
								array[k].Dispose();
							}
						}
					}
				}
			}
			finally
			{
				if (TplEtwProvider.Log.IsEnabled())
				{
					TplEtwProvider.Log.ParallelInvokeEnd((task != null) ? task.m_taskScheduler.Id : TaskScheduler.Current.Id, (task != null) ? task.Id : 0, num);
				}
			}
		}

		// Token: 0x0600401B RID: 16411 RVA: 0x000EE56C File Offset: 0x000EC76C
		[__DynamicallyInvokable]
		public static ParallelLoopResult For(int fromInclusive, int toExclusive, Action<int> body)
		{
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			return Parallel.ForWorker<object>(fromInclusive, toExclusive, Parallel.s_defaultParallelOptions, body, null, null, null, null);
		}

		// Token: 0x0600401C RID: 16412 RVA: 0x000EE58D File Offset: 0x000EC78D
		[__DynamicallyInvokable]
		public static ParallelLoopResult For(long fromInclusive, long toExclusive, Action<long> body)
		{
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			return Parallel.ForWorker64<object>(fromInclusive, toExclusive, Parallel.s_defaultParallelOptions, body, null, null, null, null);
		}

		// Token: 0x0600401D RID: 16413 RVA: 0x000EE5AE File Offset: 0x000EC7AE
		[__DynamicallyInvokable]
		public static ParallelLoopResult For(int fromInclusive, int toExclusive, ParallelOptions parallelOptions, Action<int> body)
		{
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.ForWorker<object>(fromInclusive, toExclusive, parallelOptions, body, null, null, null, null);
		}

		// Token: 0x0600401E RID: 16414 RVA: 0x000EE5D9 File Offset: 0x000EC7D9
		[__DynamicallyInvokable]
		public static ParallelLoopResult For(long fromInclusive, long toExclusive, ParallelOptions parallelOptions, Action<long> body)
		{
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.ForWorker64<object>(fromInclusive, toExclusive, parallelOptions, body, null, null, null, null);
		}

		// Token: 0x0600401F RID: 16415 RVA: 0x000EE604 File Offset: 0x000EC804
		[__DynamicallyInvokable]
		public static ParallelLoopResult For(int fromInclusive, int toExclusive, Action<int, ParallelLoopState> body)
		{
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			return Parallel.ForWorker<object>(fromInclusive, toExclusive, Parallel.s_defaultParallelOptions, null, body, null, null, null);
		}

		// Token: 0x06004020 RID: 16416 RVA: 0x000EE625 File Offset: 0x000EC825
		[__DynamicallyInvokable]
		public static ParallelLoopResult For(long fromInclusive, long toExclusive, Action<long, ParallelLoopState> body)
		{
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			return Parallel.ForWorker64<object>(fromInclusive, toExclusive, Parallel.s_defaultParallelOptions, null, body, null, null, null);
		}

		// Token: 0x06004021 RID: 16417 RVA: 0x000EE646 File Offset: 0x000EC846
		[__DynamicallyInvokable]
		public static ParallelLoopResult For(int fromInclusive, int toExclusive, ParallelOptions parallelOptions, Action<int, ParallelLoopState> body)
		{
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.ForWorker<object>(fromInclusive, toExclusive, parallelOptions, null, body, null, null, null);
		}

		// Token: 0x06004022 RID: 16418 RVA: 0x000EE671 File Offset: 0x000EC871
		[__DynamicallyInvokable]
		public static ParallelLoopResult For(long fromInclusive, long toExclusive, ParallelOptions parallelOptions, Action<long, ParallelLoopState> body)
		{
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.ForWorker64<object>(fromInclusive, toExclusive, parallelOptions, null, body, null, null, null);
		}

		// Token: 0x06004023 RID: 16419 RVA: 0x000EE69C File Offset: 0x000EC89C
		[__DynamicallyInvokable]
		public static ParallelLoopResult For<TLocal>(int fromInclusive, int toExclusive, Func<TLocal> localInit, Func<int, ParallelLoopState, TLocal, TLocal> body, Action<TLocal> localFinally)
		{
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (localInit == null)
			{
				throw new ArgumentNullException("localInit");
			}
			if (localFinally == null)
			{
				throw new ArgumentNullException("localFinally");
			}
			return Parallel.ForWorker<TLocal>(fromInclusive, toExclusive, Parallel.s_defaultParallelOptions, null, null, body, localInit, localFinally);
		}

		// Token: 0x06004024 RID: 16420 RVA: 0x000EE6DB File Offset: 0x000EC8DB
		[__DynamicallyInvokable]
		public static ParallelLoopResult For<TLocal>(long fromInclusive, long toExclusive, Func<TLocal> localInit, Func<long, ParallelLoopState, TLocal, TLocal> body, Action<TLocal> localFinally)
		{
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (localInit == null)
			{
				throw new ArgumentNullException("localInit");
			}
			if (localFinally == null)
			{
				throw new ArgumentNullException("localFinally");
			}
			return Parallel.ForWorker64<TLocal>(fromInclusive, toExclusive, Parallel.s_defaultParallelOptions, null, null, body, localInit, localFinally);
		}

		// Token: 0x06004025 RID: 16421 RVA: 0x000EE71C File Offset: 0x000EC91C
		[__DynamicallyInvokable]
		public static ParallelLoopResult For<TLocal>(int fromInclusive, int toExclusive, ParallelOptions parallelOptions, Func<TLocal> localInit, Func<int, ParallelLoopState, TLocal, TLocal> body, Action<TLocal> localFinally)
		{
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (localInit == null)
			{
				throw new ArgumentNullException("localInit");
			}
			if (localFinally == null)
			{
				throw new ArgumentNullException("localFinally");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.ForWorker<TLocal>(fromInclusive, toExclusive, parallelOptions, null, null, body, localInit, localFinally);
		}

		// Token: 0x06004026 RID: 16422 RVA: 0x000EE774 File Offset: 0x000EC974
		[__DynamicallyInvokable]
		public static ParallelLoopResult For<TLocal>(long fromInclusive, long toExclusive, ParallelOptions parallelOptions, Func<TLocal> localInit, Func<long, ParallelLoopState, TLocal, TLocal> body, Action<TLocal> localFinally)
		{
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (localInit == null)
			{
				throw new ArgumentNullException("localInit");
			}
			if (localFinally == null)
			{
				throw new ArgumentNullException("localFinally");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.ForWorker64<TLocal>(fromInclusive, toExclusive, parallelOptions, null, null, body, localInit, localFinally);
		}

		// Token: 0x06004027 RID: 16423 RVA: 0x000EE7CC File Offset: 0x000EC9CC
		private static ParallelLoopResult ForWorker<TLocal>(int fromInclusive, int toExclusive, ParallelOptions parallelOptions, Action<int> body, Action<int, ParallelLoopState> bodyWithState, Func<int, ParallelLoopState, TLocal, TLocal> bodyWithLocal, Func<TLocal> localInit, Action<TLocal> localFinally)
		{
			ParallelLoopResult parallelLoopResult = default(ParallelLoopResult);
			if (toExclusive <= fromInclusive)
			{
				parallelLoopResult.m_completed = true;
				return parallelLoopResult;
			}
			ParallelLoopStateFlags32 sharedPStateFlags = new ParallelLoopStateFlags32();
			TaskCreationOptions taskCreationOptions = TaskCreationOptions.None;
			InternalTaskOptions internalTaskOptions = InternalTaskOptions.SelfReplicating;
			if (parallelOptions.CancellationToken.IsCancellationRequested)
			{
				throw new OperationCanceledException(parallelOptions.CancellationToken);
			}
			int num = ((parallelOptions.EffectiveMaxConcurrencyLevel == -1) ? PlatformHelper.ProcessorCount : parallelOptions.EffectiveMaxConcurrencyLevel);
			RangeManager rangeManager = new RangeManager((long)fromInclusive, (long)toExclusive, 1L, num);
			OperationCanceledException oce = null;
			CancellationTokenRegistration cancellationTokenRegistration = default(CancellationTokenRegistration);
			if (parallelOptions.CancellationToken.CanBeCanceled)
			{
				cancellationTokenRegistration = parallelOptions.CancellationToken.InternalRegisterWithoutEC(delegate(object o)
				{
					sharedPStateFlags.Cancel();
					oce = new OperationCanceledException(parallelOptions.CancellationToken);
				}, null);
			}
			int forkJoinContextID = 0;
			Task task = null;
			if (TplEtwProvider.Log.IsEnabled())
			{
				forkJoinContextID = Interlocked.Increment(ref Parallel.s_forkJoinContextID);
				task = Task.InternalCurrent;
				TplEtwProvider.Log.ParallelLoopBegin((task != null) ? task.m_taskScheduler.Id : TaskScheduler.Current.Id, (task != null) ? task.Id : 0, forkJoinContextID, TplEtwProvider.ForkJoinOperationType.ParallelFor, (long)fromInclusive, (long)toExclusive);
			}
			ParallelForReplicatingTask rootTask = null;
			try
			{
				rootTask = new ParallelForReplicatingTask(parallelOptions, delegate
				{
					Task internalCurrent = Task.InternalCurrent;
					bool flag = internalCurrent == rootTask;
					RangeWorker rangeWorker = default(RangeWorker);
					object savedStateFromPreviousReplica = internalCurrent.SavedStateFromPreviousReplica;
					if (savedStateFromPreviousReplica is RangeWorker)
					{
						rangeWorker = (RangeWorker)savedStateFromPreviousReplica;
					}
					else
					{
						rangeWorker = rangeManager.RegisterNewWorker();
					}
					int num3;
					int num4;
					if (!rangeWorker.FindNewWork32(out num3, out num4) || sharedPStateFlags.ShouldExitLoop(num3))
					{
						return;
					}
					if (TplEtwProvider.Log.IsEnabled())
					{
						TplEtwProvider.Log.ParallelFork((internalCurrent != null) ? internalCurrent.m_taskScheduler.Id : TaskScheduler.Current.Id, (internalCurrent != null) ? internalCurrent.Id : 0, forkJoinContextID);
					}
					TLocal tlocal = default(TLocal);
					bool flag2 = false;
					try
					{
						ParallelLoopState32 parallelLoopState = null;
						if (bodyWithState != null)
						{
							parallelLoopState = new ParallelLoopState32(sharedPStateFlags);
						}
						else if (bodyWithLocal != null)
						{
							parallelLoopState = new ParallelLoopState32(sharedPStateFlags);
							if (localInit != null)
							{
								tlocal = localInit();
								flag2 = true;
							}
						}
						Parallel.LoopTimer loopTimer = new Parallel.LoopTimer(rootTask.ActiveChildCount);
						for (;;)
						{
							if (body != null)
							{
								for (int i = num3; i < num4; i++)
								{
									if (sharedPStateFlags.LoopStateFlags != ParallelLoopStateFlags.PLS_NONE && sharedPStateFlags.ShouldExitLoop())
									{
										break;
									}
									body(i);
								}
							}
							else if (bodyWithState != null)
							{
								for (int j = num3; j < num4; j++)
								{
									if (sharedPStateFlags.LoopStateFlags != ParallelLoopStateFlags.PLS_NONE && sharedPStateFlags.ShouldExitLoop(j))
									{
										break;
									}
									parallelLoopState.CurrentIteration = j;
									bodyWithState(j, parallelLoopState);
								}
							}
							else
							{
								int num5 = num3;
								while (num5 < num4 && (sharedPStateFlags.LoopStateFlags == ParallelLoopStateFlags.PLS_NONE || !sharedPStateFlags.ShouldExitLoop(num5)))
								{
									parallelLoopState.CurrentIteration = num5;
									tlocal = bodyWithLocal(num5, parallelLoopState, tlocal);
									num5++;
								}
							}
							if (!flag && loopTimer.LimitExceeded())
							{
								break;
							}
							if (!rangeWorker.FindNewWork32(out num3, out num4) || (sharedPStateFlags.LoopStateFlags != ParallelLoopStateFlags.PLS_NONE && sharedPStateFlags.ShouldExitLoop(num3)))
							{
								goto IL_23F;
							}
						}
						internalCurrent.SavedStateForNextReplica = rangeWorker;
						IL_23F:;
					}
					catch
					{
						sharedPStateFlags.SetExceptional();
						throw;
					}
					finally
					{
						if (localFinally != null && flag2)
						{
							localFinally(tlocal);
						}
						if (TplEtwProvider.Log.IsEnabled())
						{
							TplEtwProvider.Log.ParallelJoin((internalCurrent != null) ? internalCurrent.m_taskScheduler.Id : TaskScheduler.Current.Id, (internalCurrent != null) ? internalCurrent.Id : 0, forkJoinContextID);
						}
					}
				}, taskCreationOptions, internalTaskOptions);
				rootTask.RunSynchronously(parallelOptions.EffectiveTaskScheduler);
				rootTask.Wait();
				if (parallelOptions.CancellationToken.CanBeCanceled)
				{
					cancellationTokenRegistration.Dispose();
				}
				if (oce != null)
				{
					throw oce;
				}
			}
			catch (AggregateException ex)
			{
				if (parallelOptions.CancellationToken.CanBeCanceled)
				{
					cancellationTokenRegistration.Dispose();
				}
				Parallel.ThrowIfReducableToSingleOCE(ex.InnerExceptions, parallelOptions.CancellationToken);
				throw;
			}
			catch (TaskSchedulerException)
			{
				if (parallelOptions.CancellationToken.CanBeCanceled)
				{
					cancellationTokenRegistration.Dispose();
				}
				throw;
			}
			finally
			{
				int loopStateFlags = sharedPStateFlags.LoopStateFlags;
				parallelLoopResult.m_completed = loopStateFlags == ParallelLoopStateFlags.PLS_NONE;
				if ((loopStateFlags & ParallelLoopStateFlags.PLS_BROKEN) != 0)
				{
					parallelLoopResult.m_lowestBreakIteration = new long?((long)sharedPStateFlags.LowestBreakIteration);
				}
				if (rootTask != null && rootTask.IsCompleted)
				{
					rootTask.Dispose();
				}
				if (TplEtwProvider.Log.IsEnabled())
				{
					int num2;
					if (loopStateFlags == ParallelLoopStateFlags.PLS_NONE)
					{
						num2 = toExclusive - fromInclusive;
					}
					else if ((loopStateFlags & ParallelLoopStateFlags.PLS_BROKEN) != 0)
					{
						num2 = sharedPStateFlags.LowestBreakIteration - fromInclusive;
					}
					else
					{
						num2 = -1;
					}
					TplEtwProvider.Log.ParallelLoopEnd((task != null) ? task.m_taskScheduler.Id : TaskScheduler.Current.Id, (task != null) ? task.Id : 0, forkJoinContextID, (long)num2);
				}
			}
			return parallelLoopResult;
		}

		// Token: 0x06004028 RID: 16424 RVA: 0x000EEB1C File Offset: 0x000ECD1C
		private static ParallelLoopResult ForWorker64<TLocal>(long fromInclusive, long toExclusive, ParallelOptions parallelOptions, Action<long> body, Action<long, ParallelLoopState> bodyWithState, Func<long, ParallelLoopState, TLocal, TLocal> bodyWithLocal, Func<TLocal> localInit, Action<TLocal> localFinally)
		{
			ParallelLoopResult parallelLoopResult = default(ParallelLoopResult);
			if (toExclusive <= fromInclusive)
			{
				parallelLoopResult.m_completed = true;
				return parallelLoopResult;
			}
			ParallelLoopStateFlags64 sharedPStateFlags = new ParallelLoopStateFlags64();
			TaskCreationOptions taskCreationOptions = TaskCreationOptions.None;
			InternalTaskOptions internalTaskOptions = InternalTaskOptions.SelfReplicating;
			if (parallelOptions.CancellationToken.IsCancellationRequested)
			{
				throw new OperationCanceledException(parallelOptions.CancellationToken);
			}
			int num = ((parallelOptions.EffectiveMaxConcurrencyLevel == -1) ? PlatformHelper.ProcessorCount : parallelOptions.EffectiveMaxConcurrencyLevel);
			RangeManager rangeManager = new RangeManager(fromInclusive, toExclusive, 1L, num);
			OperationCanceledException oce = null;
			CancellationTokenRegistration cancellationTokenRegistration = default(CancellationTokenRegistration);
			if (parallelOptions.CancellationToken.CanBeCanceled)
			{
				cancellationTokenRegistration = parallelOptions.CancellationToken.InternalRegisterWithoutEC(delegate(object o)
				{
					sharedPStateFlags.Cancel();
					oce = new OperationCanceledException(parallelOptions.CancellationToken);
				}, null);
			}
			Task task = null;
			int forkJoinContextID = 0;
			if (TplEtwProvider.Log.IsEnabled())
			{
				forkJoinContextID = Interlocked.Increment(ref Parallel.s_forkJoinContextID);
				task = Task.InternalCurrent;
				TplEtwProvider.Log.ParallelLoopBegin((task != null) ? task.m_taskScheduler.Id : TaskScheduler.Current.Id, (task != null) ? task.Id : 0, forkJoinContextID, TplEtwProvider.ForkJoinOperationType.ParallelFor, fromInclusive, toExclusive);
			}
			ParallelForReplicatingTask rootTask = null;
			try
			{
				rootTask = new ParallelForReplicatingTask(parallelOptions, delegate
				{
					Task internalCurrent = Task.InternalCurrent;
					bool flag = internalCurrent == rootTask;
					RangeWorker rangeWorker = default(RangeWorker);
					object savedStateFromPreviousReplica = internalCurrent.SavedStateFromPreviousReplica;
					if (savedStateFromPreviousReplica is RangeWorker)
					{
						rangeWorker = (RangeWorker)savedStateFromPreviousReplica;
					}
					else
					{
						rangeWorker = rangeManager.RegisterNewWorker();
					}
					long num3;
					long num4;
					if (!rangeWorker.FindNewWork(out num3, out num4) || sharedPStateFlags.ShouldExitLoop(num3))
					{
						return;
					}
					if (TplEtwProvider.Log.IsEnabled())
					{
						TplEtwProvider.Log.ParallelFork((internalCurrent != null) ? internalCurrent.m_taskScheduler.Id : TaskScheduler.Current.Id, (internalCurrent != null) ? internalCurrent.Id : 0, forkJoinContextID);
					}
					TLocal tlocal = default(TLocal);
					bool flag2 = false;
					try
					{
						ParallelLoopState64 parallelLoopState = null;
						if (bodyWithState != null)
						{
							parallelLoopState = new ParallelLoopState64(sharedPStateFlags);
						}
						else if (bodyWithLocal != null)
						{
							parallelLoopState = new ParallelLoopState64(sharedPStateFlags);
							if (localInit != null)
							{
								tlocal = localInit();
								flag2 = true;
							}
						}
						Parallel.LoopTimer loopTimer = new Parallel.LoopTimer(rootTask.ActiveChildCount);
						for (;;)
						{
							if (body != null)
							{
								for (long num5 = num3; num5 < num4; num5 += 1L)
								{
									if (sharedPStateFlags.LoopStateFlags != ParallelLoopStateFlags.PLS_NONE && sharedPStateFlags.ShouldExitLoop())
									{
										break;
									}
									body(num5);
								}
							}
							else if (bodyWithState != null)
							{
								for (long num6 = num3; num6 < num4; num6 += 1L)
								{
									if (sharedPStateFlags.LoopStateFlags != ParallelLoopStateFlags.PLS_NONE && sharedPStateFlags.ShouldExitLoop(num6))
									{
										break;
									}
									parallelLoopState.CurrentIteration = num6;
									bodyWithState(num6, parallelLoopState);
								}
							}
							else
							{
								long num7 = num3;
								while (num7 < num4 && (sharedPStateFlags.LoopStateFlags == ParallelLoopStateFlags.PLS_NONE || !sharedPStateFlags.ShouldExitLoop(num7)))
								{
									parallelLoopState.CurrentIteration = num7;
									tlocal = bodyWithLocal(num7, parallelLoopState, tlocal);
									num7 += 1L;
								}
							}
							if (!flag && loopTimer.LimitExceeded())
							{
								break;
							}
							if (!rangeWorker.FindNewWork(out num3, out num4) || (sharedPStateFlags.LoopStateFlags != ParallelLoopStateFlags.PLS_NONE && sharedPStateFlags.ShouldExitLoop(num3)))
							{
								goto IL_242;
							}
						}
						internalCurrent.SavedStateForNextReplica = rangeWorker;
						IL_242:;
					}
					catch
					{
						sharedPStateFlags.SetExceptional();
						throw;
					}
					finally
					{
						if (localFinally != null && flag2)
						{
							localFinally(tlocal);
						}
						if (TplEtwProvider.Log.IsEnabled())
						{
							TplEtwProvider.Log.ParallelJoin((internalCurrent != null) ? internalCurrent.m_taskScheduler.Id : TaskScheduler.Current.Id, (internalCurrent != null) ? internalCurrent.Id : 0, forkJoinContextID);
						}
					}
				}, taskCreationOptions, internalTaskOptions);
				rootTask.RunSynchronously(parallelOptions.EffectiveTaskScheduler);
				rootTask.Wait();
				if (parallelOptions.CancellationToken.CanBeCanceled)
				{
					cancellationTokenRegistration.Dispose();
				}
				if (oce != null)
				{
					throw oce;
				}
			}
			catch (AggregateException ex)
			{
				if (parallelOptions.CancellationToken.CanBeCanceled)
				{
					cancellationTokenRegistration.Dispose();
				}
				Parallel.ThrowIfReducableToSingleOCE(ex.InnerExceptions, parallelOptions.CancellationToken);
				throw;
			}
			catch (TaskSchedulerException)
			{
				if (parallelOptions.CancellationToken.CanBeCanceled)
				{
					cancellationTokenRegistration.Dispose();
				}
				throw;
			}
			finally
			{
				int loopStateFlags = sharedPStateFlags.LoopStateFlags;
				parallelLoopResult.m_completed = loopStateFlags == ParallelLoopStateFlags.PLS_NONE;
				if ((loopStateFlags & ParallelLoopStateFlags.PLS_BROKEN) != 0)
				{
					parallelLoopResult.m_lowestBreakIteration = new long?(sharedPStateFlags.LowestBreakIteration);
				}
				if (rootTask != null && rootTask.IsCompleted)
				{
					rootTask.Dispose();
				}
				if (TplEtwProvider.Log.IsEnabled())
				{
					long num2;
					if (loopStateFlags == ParallelLoopStateFlags.PLS_NONE)
					{
						num2 = toExclusive - fromInclusive;
					}
					else if ((loopStateFlags & ParallelLoopStateFlags.PLS_BROKEN) != 0)
					{
						num2 = sharedPStateFlags.LowestBreakIteration - fromInclusive;
					}
					else
					{
						num2 = -1L;
					}
					TplEtwProvider.Log.ParallelLoopEnd((task != null) ? task.m_taskScheduler.Id : TaskScheduler.Current.Id, (task != null) ? task.Id : 0, forkJoinContextID, num2);
				}
			}
			return parallelLoopResult;
		}

		// Token: 0x06004029 RID: 16425 RVA: 0x000EEE68 File Offset: 0x000ED068
		[__DynamicallyInvokable]
		public static ParallelLoopResult ForEach<TSource>(IEnumerable<TSource> source, Action<TSource> body)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			return Parallel.ForEachWorker<TSource, object>(source, Parallel.s_defaultParallelOptions, body, null, null, null, null, null, null);
		}

		// Token: 0x0600402A RID: 16426 RVA: 0x000EEEA4 File Offset: 0x000ED0A4
		[__DynamicallyInvokable]
		public static ParallelLoopResult ForEach<TSource>(IEnumerable<TSource> source, ParallelOptions parallelOptions, Action<TSource> body)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.ForEachWorker<TSource, object>(source, parallelOptions, body, null, null, null, null, null, null);
		}

		// Token: 0x0600402B RID: 16427 RVA: 0x000EEEEC File Offset: 0x000ED0EC
		[__DynamicallyInvokable]
		public static ParallelLoopResult ForEach<TSource>(IEnumerable<TSource> source, Action<TSource, ParallelLoopState> body)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			return Parallel.ForEachWorker<TSource, object>(source, Parallel.s_defaultParallelOptions, null, body, null, null, null, null, null);
		}

		// Token: 0x0600402C RID: 16428 RVA: 0x000EEF28 File Offset: 0x000ED128
		[__DynamicallyInvokable]
		public static ParallelLoopResult ForEach<TSource>(IEnumerable<TSource> source, ParallelOptions parallelOptions, Action<TSource, ParallelLoopState> body)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.ForEachWorker<TSource, object>(source, parallelOptions, null, body, null, null, null, null, null);
		}

		// Token: 0x0600402D RID: 16429 RVA: 0x000EEF70 File Offset: 0x000ED170
		[__DynamicallyInvokable]
		public static ParallelLoopResult ForEach<TSource>(IEnumerable<TSource> source, Action<TSource, ParallelLoopState, long> body)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			return Parallel.ForEachWorker<TSource, object>(source, Parallel.s_defaultParallelOptions, null, null, body, null, null, null, null);
		}

		// Token: 0x0600402E RID: 16430 RVA: 0x000EEFAC File Offset: 0x000ED1AC
		[__DynamicallyInvokable]
		public static ParallelLoopResult ForEach<TSource>(IEnumerable<TSource> source, ParallelOptions parallelOptions, Action<TSource, ParallelLoopState, long> body)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.ForEachWorker<TSource, object>(source, parallelOptions, null, null, body, null, null, null, null);
		}

		// Token: 0x0600402F RID: 16431 RVA: 0x000EEFF4 File Offset: 0x000ED1F4
		[__DynamicallyInvokable]
		public static ParallelLoopResult ForEach<TSource, TLocal>(IEnumerable<TSource> source, Func<TLocal> localInit, Func<TSource, ParallelLoopState, TLocal, TLocal> body, Action<TLocal> localFinally)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (localInit == null)
			{
				throw new ArgumentNullException("localInit");
			}
			if (localFinally == null)
			{
				throw new ArgumentNullException("localFinally");
			}
			return Parallel.ForEachWorker<TSource, TLocal>(source, Parallel.s_defaultParallelOptions, null, null, null, body, null, localInit, localFinally);
		}

		// Token: 0x06004030 RID: 16432 RVA: 0x000EF04C File Offset: 0x000ED24C
		[__DynamicallyInvokable]
		public static ParallelLoopResult ForEach<TSource, TLocal>(IEnumerable<TSource> source, ParallelOptions parallelOptions, Func<TLocal> localInit, Func<TSource, ParallelLoopState, TLocal, TLocal> body, Action<TLocal> localFinally)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (localInit == null)
			{
				throw new ArgumentNullException("localInit");
			}
			if (localFinally == null)
			{
				throw new ArgumentNullException("localFinally");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.ForEachWorker<TSource, TLocal>(source, parallelOptions, null, null, null, body, null, localInit, localFinally);
		}

		// Token: 0x06004031 RID: 16433 RVA: 0x000EF0B0 File Offset: 0x000ED2B0
		[__DynamicallyInvokable]
		public static ParallelLoopResult ForEach<TSource, TLocal>(IEnumerable<TSource> source, Func<TLocal> localInit, Func<TSource, ParallelLoopState, long, TLocal, TLocal> body, Action<TLocal> localFinally)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (localInit == null)
			{
				throw new ArgumentNullException("localInit");
			}
			if (localFinally == null)
			{
				throw new ArgumentNullException("localFinally");
			}
			return Parallel.ForEachWorker<TSource, TLocal>(source, Parallel.s_defaultParallelOptions, null, null, null, null, body, localInit, localFinally);
		}

		// Token: 0x06004032 RID: 16434 RVA: 0x000EF108 File Offset: 0x000ED308
		[__DynamicallyInvokable]
		public static ParallelLoopResult ForEach<TSource, TLocal>(IEnumerable<TSource> source, ParallelOptions parallelOptions, Func<TLocal> localInit, Func<TSource, ParallelLoopState, long, TLocal, TLocal> body, Action<TLocal> localFinally)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (localInit == null)
			{
				throw new ArgumentNullException("localInit");
			}
			if (localFinally == null)
			{
				throw new ArgumentNullException("localFinally");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.ForEachWorker<TSource, TLocal>(source, parallelOptions, null, null, null, null, body, localInit, localFinally);
		}

		// Token: 0x06004033 RID: 16435 RVA: 0x000EF16C File Offset: 0x000ED36C
		private static ParallelLoopResult ForEachWorker<TSource, TLocal>(IEnumerable<TSource> source, ParallelOptions parallelOptions, Action<TSource> body, Action<TSource, ParallelLoopState> bodyWithState, Action<TSource, ParallelLoopState, long> bodyWithStateAndIndex, Func<TSource, ParallelLoopState, TLocal, TLocal> bodyWithStateAndLocal, Func<TSource, ParallelLoopState, long, TLocal, TLocal> bodyWithEverything, Func<TLocal> localInit, Action<TLocal> localFinally)
		{
			if (parallelOptions.CancellationToken.IsCancellationRequested)
			{
				throw new OperationCanceledException(parallelOptions.CancellationToken);
			}
			TSource[] array = source as TSource[];
			if (array != null)
			{
				return Parallel.ForEachWorker<TSource, TLocal>(array, parallelOptions, body, bodyWithState, bodyWithStateAndIndex, bodyWithStateAndLocal, bodyWithEverything, localInit, localFinally);
			}
			IList<TSource> list = source as IList<TSource>;
			if (list != null)
			{
				return Parallel.ForEachWorker<TSource, TLocal>(list, parallelOptions, body, bodyWithState, bodyWithStateAndIndex, bodyWithStateAndLocal, bodyWithEverything, localInit, localFinally);
			}
			return Parallel.PartitionerForEachWorker<TSource, TLocal>(Partitioner.Create<TSource>(source), parallelOptions, body, bodyWithState, bodyWithStateAndIndex, bodyWithStateAndLocal, bodyWithEverything, localInit, localFinally);
		}

		// Token: 0x06004034 RID: 16436 RVA: 0x000EF1EC File Offset: 0x000ED3EC
		private static ParallelLoopResult ForEachWorker<TSource, TLocal>(TSource[] array, ParallelOptions parallelOptions, Action<TSource> body, Action<TSource, ParallelLoopState> bodyWithState, Action<TSource, ParallelLoopState, long> bodyWithStateAndIndex, Func<TSource, ParallelLoopState, TLocal, TLocal> bodyWithStateAndLocal, Func<TSource, ParallelLoopState, long, TLocal, TLocal> bodyWithEverything, Func<TLocal> localInit, Action<TLocal> localFinally)
		{
			int lowerBound = array.GetLowerBound(0);
			int num = array.GetUpperBound(0) + 1;
			if (body != null)
			{
				return Parallel.ForWorker<object>(lowerBound, num, parallelOptions, delegate(int i)
				{
					body(array[i]);
				}, null, null, null, null);
			}
			if (bodyWithState != null)
			{
				return Parallel.ForWorker<object>(lowerBound, num, parallelOptions, null, delegate(int i, ParallelLoopState state)
				{
					bodyWithState(array[i], state);
				}, null, null, null);
			}
			if (bodyWithStateAndIndex != null)
			{
				return Parallel.ForWorker<object>(lowerBound, num, parallelOptions, null, delegate(int i, ParallelLoopState state)
				{
					bodyWithStateAndIndex(array[i], state, (long)i);
				}, null, null, null);
			}
			if (bodyWithStateAndLocal != null)
			{
				return Parallel.ForWorker<TLocal>(lowerBound, num, parallelOptions, null, null, (int i, ParallelLoopState state, TLocal local) => bodyWithStateAndLocal(array[i], state, local), localInit, localFinally);
			}
			return Parallel.ForWorker<TLocal>(lowerBound, num, parallelOptions, null, null, (int i, ParallelLoopState state, TLocal local) => bodyWithEverything(array[i], state, (long)i, local), localInit, localFinally);
		}

		// Token: 0x06004035 RID: 16437 RVA: 0x000EF2E8 File Offset: 0x000ED4E8
		private static ParallelLoopResult ForEachWorker<TSource, TLocal>(IList<TSource> list, ParallelOptions parallelOptions, Action<TSource> body, Action<TSource, ParallelLoopState> bodyWithState, Action<TSource, ParallelLoopState, long> bodyWithStateAndIndex, Func<TSource, ParallelLoopState, TLocal, TLocal> bodyWithStateAndLocal, Func<TSource, ParallelLoopState, long, TLocal, TLocal> bodyWithEverything, Func<TLocal> localInit, Action<TLocal> localFinally)
		{
			if (body != null)
			{
				return Parallel.ForWorker<object>(0, list.Count, parallelOptions, delegate(int i)
				{
					body(list[i]);
				}, null, null, null, null);
			}
			if (bodyWithState != null)
			{
				return Parallel.ForWorker<object>(0, list.Count, parallelOptions, null, delegate(int i, ParallelLoopState state)
				{
					bodyWithState(list[i], state);
				}, null, null, null);
			}
			if (bodyWithStateAndIndex != null)
			{
				return Parallel.ForWorker<object>(0, list.Count, parallelOptions, null, delegate(int i, ParallelLoopState state)
				{
					bodyWithStateAndIndex(list[i], state, (long)i);
				}, null, null, null);
			}
			if (bodyWithStateAndLocal != null)
			{
				return Parallel.ForWorker<TLocal>(0, list.Count, parallelOptions, null, null, (int i, ParallelLoopState state, TLocal local) => bodyWithStateAndLocal(list[i], state, local), localInit, localFinally);
			}
			return Parallel.ForWorker<TLocal>(0, list.Count, parallelOptions, null, null, (int i, ParallelLoopState state, TLocal local) => bodyWithEverything(list[i], state, (long)i, local), localInit, localFinally);
		}

		// Token: 0x06004036 RID: 16438 RVA: 0x000EF3FC File Offset: 0x000ED5FC
		[__DynamicallyInvokable]
		public static ParallelLoopResult ForEach<TSource>(Partitioner<TSource> source, Action<TSource> body)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			return Parallel.PartitionerForEachWorker<TSource, object>(source, Parallel.s_defaultParallelOptions, body, null, null, null, null, null, null);
		}

		// Token: 0x06004037 RID: 16439 RVA: 0x000EF438 File Offset: 0x000ED638
		[__DynamicallyInvokable]
		public static ParallelLoopResult ForEach<TSource>(Partitioner<TSource> source, Action<TSource, ParallelLoopState> body)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			return Parallel.PartitionerForEachWorker<TSource, object>(source, Parallel.s_defaultParallelOptions, null, body, null, null, null, null, null);
		}

		// Token: 0x06004038 RID: 16440 RVA: 0x000EF474 File Offset: 0x000ED674
		[__DynamicallyInvokable]
		public static ParallelLoopResult ForEach<TSource>(OrderablePartitioner<TSource> source, Action<TSource, ParallelLoopState, long> body)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (!source.KeysNormalized)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Parallel_ForEach_OrderedPartitionerKeysNotNormalized"));
			}
			return Parallel.PartitionerForEachWorker<TSource, object>(source, Parallel.s_defaultParallelOptions, null, null, body, null, null, null, null);
		}

		// Token: 0x06004039 RID: 16441 RVA: 0x000EF4C8 File Offset: 0x000ED6C8
		[__DynamicallyInvokable]
		public static ParallelLoopResult ForEach<TSource, TLocal>(Partitioner<TSource> source, Func<TLocal> localInit, Func<TSource, ParallelLoopState, TLocal, TLocal> body, Action<TLocal> localFinally)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (localInit == null)
			{
				throw new ArgumentNullException("localInit");
			}
			if (localFinally == null)
			{
				throw new ArgumentNullException("localFinally");
			}
			return Parallel.PartitionerForEachWorker<TSource, TLocal>(source, Parallel.s_defaultParallelOptions, null, null, null, body, null, localInit, localFinally);
		}

		// Token: 0x0600403A RID: 16442 RVA: 0x000EF520 File Offset: 0x000ED720
		[__DynamicallyInvokable]
		public static ParallelLoopResult ForEach<TSource, TLocal>(OrderablePartitioner<TSource> source, Func<TLocal> localInit, Func<TSource, ParallelLoopState, long, TLocal, TLocal> body, Action<TLocal> localFinally)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (localInit == null)
			{
				throw new ArgumentNullException("localInit");
			}
			if (localFinally == null)
			{
				throw new ArgumentNullException("localFinally");
			}
			if (!source.KeysNormalized)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Parallel_ForEach_OrderedPartitionerKeysNotNormalized"));
			}
			return Parallel.PartitionerForEachWorker<TSource, TLocal>(source, Parallel.s_defaultParallelOptions, null, null, null, null, body, localInit, localFinally);
		}

		// Token: 0x0600403B RID: 16443 RVA: 0x000EF590 File Offset: 0x000ED790
		[__DynamicallyInvokable]
		public static ParallelLoopResult ForEach<TSource>(Partitioner<TSource> source, ParallelOptions parallelOptions, Action<TSource> body)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.PartitionerForEachWorker<TSource, object>(source, parallelOptions, body, null, null, null, null, null, null);
		}

		// Token: 0x0600403C RID: 16444 RVA: 0x000EF5D8 File Offset: 0x000ED7D8
		[__DynamicallyInvokable]
		public static ParallelLoopResult ForEach<TSource>(Partitioner<TSource> source, ParallelOptions parallelOptions, Action<TSource, ParallelLoopState> body)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.PartitionerForEachWorker<TSource, object>(source, parallelOptions, null, body, null, null, null, null, null);
		}

		// Token: 0x0600403D RID: 16445 RVA: 0x000EF620 File Offset: 0x000ED820
		[__DynamicallyInvokable]
		public static ParallelLoopResult ForEach<TSource>(OrderablePartitioner<TSource> source, ParallelOptions parallelOptions, Action<TSource, ParallelLoopState, long> body)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			if (!source.KeysNormalized)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Parallel_ForEach_OrderedPartitionerKeysNotNormalized"));
			}
			return Parallel.PartitionerForEachWorker<TSource, object>(source, parallelOptions, null, null, body, null, null, null, null);
		}

		// Token: 0x0600403E RID: 16446 RVA: 0x000EF680 File Offset: 0x000ED880
		[__DynamicallyInvokable]
		public static ParallelLoopResult ForEach<TSource, TLocal>(Partitioner<TSource> source, ParallelOptions parallelOptions, Func<TLocal> localInit, Func<TSource, ParallelLoopState, TLocal, TLocal> body, Action<TLocal> localFinally)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (localInit == null)
			{
				throw new ArgumentNullException("localInit");
			}
			if (localFinally == null)
			{
				throw new ArgumentNullException("localFinally");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.PartitionerForEachWorker<TSource, TLocal>(source, parallelOptions, null, null, null, body, null, localInit, localFinally);
		}

		// Token: 0x0600403F RID: 16447 RVA: 0x000EF6E4 File Offset: 0x000ED8E4
		[__DynamicallyInvokable]
		public static ParallelLoopResult ForEach<TSource, TLocal>(OrderablePartitioner<TSource> source, ParallelOptions parallelOptions, Func<TLocal> localInit, Func<TSource, ParallelLoopState, long, TLocal, TLocal> body, Action<TLocal> localFinally)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (localInit == null)
			{
				throw new ArgumentNullException("localInit");
			}
			if (localFinally == null)
			{
				throw new ArgumentNullException("localFinally");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			if (!source.KeysNormalized)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Parallel_ForEach_OrderedPartitionerKeysNotNormalized"));
			}
			return Parallel.PartitionerForEachWorker<TSource, TLocal>(source, parallelOptions, null, null, null, null, body, localInit, localFinally);
		}

		// Token: 0x06004040 RID: 16448 RVA: 0x000EF760 File Offset: 0x000ED960
		private static ParallelLoopResult PartitionerForEachWorker<TSource, TLocal>(Partitioner<TSource> source, ParallelOptions parallelOptions, Action<TSource> simpleBody, Action<TSource, ParallelLoopState> bodyWithState, Action<TSource, ParallelLoopState, long> bodyWithStateAndIndex, Func<TSource, ParallelLoopState, TLocal, TLocal> bodyWithStateAndLocal, Func<TSource, ParallelLoopState, long, TLocal, TLocal> bodyWithEverything, Func<TLocal> localInit, Action<TLocal> localFinally)
		{
			OrderablePartitioner<TSource> orderedSource = source as OrderablePartitioner<TSource>;
			if (!source.SupportsDynamicPartitions)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Parallel_ForEach_PartitionerNotDynamic"));
			}
			if (parallelOptions.CancellationToken.IsCancellationRequested)
			{
				throw new OperationCanceledException(parallelOptions.CancellationToken);
			}
			int forkJoinContextID = 0;
			Task task = null;
			if (TplEtwProvider.Log.IsEnabled())
			{
				forkJoinContextID = Interlocked.Increment(ref Parallel.s_forkJoinContextID);
				task = Task.InternalCurrent;
				TplEtwProvider.Log.ParallelLoopBegin((task != null) ? task.m_taskScheduler.Id : TaskScheduler.Current.Id, (task != null) ? task.Id : 0, forkJoinContextID, TplEtwProvider.ForkJoinOperationType.ParallelForEach, 0L, 0L);
			}
			ParallelLoopStateFlags64 sharedPStateFlags = new ParallelLoopStateFlags64();
			ParallelLoopResult parallelLoopResult = default(ParallelLoopResult);
			OperationCanceledException oce = null;
			CancellationTokenRegistration cancellationTokenRegistration = default(CancellationTokenRegistration);
			if (parallelOptions.CancellationToken.CanBeCanceled)
			{
				cancellationTokenRegistration = parallelOptions.CancellationToken.InternalRegisterWithoutEC(delegate(object o)
				{
					sharedPStateFlags.Cancel();
					oce = new OperationCanceledException(parallelOptions.CancellationToken);
				}, null);
			}
			IEnumerable<TSource> partitionerSource = null;
			IEnumerable<KeyValuePair<long, TSource>> orderablePartitionerSource = null;
			if (orderedSource != null)
			{
				orderablePartitionerSource = orderedSource.GetOrderableDynamicPartitions();
				if (orderablePartitionerSource == null)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Parallel_ForEach_PartitionerReturnedNull"));
				}
			}
			else
			{
				partitionerSource = source.GetDynamicPartitions();
				if (partitionerSource == null)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Parallel_ForEach_PartitionerReturnedNull"));
				}
			}
			ParallelForReplicatingTask rootTask = null;
			Action action = delegate
			{
				Task internalCurrent = Task.InternalCurrent;
				if (TplEtwProvider.Log.IsEnabled())
				{
					TplEtwProvider.Log.ParallelFork((internalCurrent != null) ? internalCurrent.m_taskScheduler.Id : TaskScheduler.Current.Id, (internalCurrent != null) ? internalCurrent.Id : 0, forkJoinContextID);
				}
				TLocal tlocal = default(TLocal);
				bool flag = false;
				IDisposable disposable2 = null;
				try
				{
					ParallelLoopState64 parallelLoopState = null;
					if (bodyWithState != null || bodyWithStateAndIndex != null)
					{
						parallelLoopState = new ParallelLoopState64(sharedPStateFlags);
					}
					else if (bodyWithStateAndLocal != null || bodyWithEverything != null)
					{
						parallelLoopState = new ParallelLoopState64(sharedPStateFlags);
						if (localInit != null)
						{
							tlocal = localInit();
							flag = true;
						}
					}
					bool flag2 = rootTask == internalCurrent;
					Parallel.LoopTimer loopTimer = new Parallel.LoopTimer(rootTask.ActiveChildCount);
					if (orderedSource != null)
					{
						IEnumerator<KeyValuePair<long, TSource>> enumerator = internalCurrent.SavedStateFromPreviousReplica as IEnumerator<KeyValuePair<long, TSource>>;
						if (enumerator == null)
						{
							enumerator = orderablePartitionerSource.GetEnumerator();
							if (enumerator == null)
							{
								throw new InvalidOperationException(Environment.GetResourceString("Parallel_ForEach_NullEnumerator"));
							}
						}
						disposable2 = enumerator;
						while (enumerator.MoveNext())
						{
							KeyValuePair<long, TSource> keyValuePair = enumerator.Current;
							long key = keyValuePair.Key;
							TSource value = keyValuePair.Value;
							if (parallelLoopState != null)
							{
								parallelLoopState.CurrentIteration = key;
							}
							if (simpleBody != null)
							{
								simpleBody(value);
							}
							else if (bodyWithState != null)
							{
								bodyWithState(value, parallelLoopState);
							}
							else if (bodyWithStateAndIndex != null)
							{
								bodyWithStateAndIndex(value, parallelLoopState, key);
							}
							else if (bodyWithStateAndLocal != null)
							{
								tlocal = bodyWithStateAndLocal(value, parallelLoopState, tlocal);
							}
							else
							{
								tlocal = bodyWithEverything(value, parallelLoopState, key, tlocal);
							}
							if (sharedPStateFlags.ShouldExitLoop(key))
							{
								break;
							}
							if (!flag2 && loopTimer.LimitExceeded())
							{
								internalCurrent.SavedStateForNextReplica = enumerator;
								disposable2 = null;
								break;
							}
						}
					}
					else
					{
						IEnumerator<TSource> enumerator2 = internalCurrent.SavedStateFromPreviousReplica as IEnumerator<TSource>;
						if (enumerator2 == null)
						{
							enumerator2 = partitionerSource.GetEnumerator();
							if (enumerator2 == null)
							{
								throw new InvalidOperationException(Environment.GetResourceString("Parallel_ForEach_NullEnumerator"));
							}
						}
						disposable2 = enumerator2;
						if (parallelLoopState != null)
						{
							parallelLoopState.CurrentIteration = 0L;
						}
						while (enumerator2.MoveNext())
						{
							TSource tsource = enumerator2.Current;
							if (simpleBody != null)
							{
								simpleBody(tsource);
							}
							else if (bodyWithState != null)
							{
								bodyWithState(tsource, parallelLoopState);
							}
							else if (bodyWithStateAndLocal != null)
							{
								tlocal = bodyWithStateAndLocal(tsource, parallelLoopState, tlocal);
							}
							if (sharedPStateFlags.LoopStateFlags != ParallelLoopStateFlags.PLS_NONE)
							{
								break;
							}
							if (!flag2 && loopTimer.LimitExceeded())
							{
								internalCurrent.SavedStateForNextReplica = enumerator2;
								disposable2 = null;
								break;
							}
						}
					}
				}
				catch
				{
					sharedPStateFlags.SetExceptional();
					throw;
				}
				finally
				{
					if (localFinally != null && flag)
					{
						localFinally(tlocal);
					}
					if (disposable2 != null)
					{
						disposable2.Dispose();
					}
					if (TplEtwProvider.Log.IsEnabled())
					{
						TplEtwProvider.Log.ParallelJoin((internalCurrent != null) ? internalCurrent.m_taskScheduler.Id : TaskScheduler.Current.Id, (internalCurrent != null) ? internalCurrent.Id : 0, forkJoinContextID);
					}
				}
			};
			try
			{
				rootTask = new ParallelForReplicatingTask(parallelOptions, action, TaskCreationOptions.None, InternalTaskOptions.SelfReplicating);
				rootTask.RunSynchronously(parallelOptions.EffectiveTaskScheduler);
				rootTask.Wait();
				if (parallelOptions.CancellationToken.CanBeCanceled)
				{
					cancellationTokenRegistration.Dispose();
				}
				if (oce != null)
				{
					throw oce;
				}
			}
			catch (AggregateException ex)
			{
				if (parallelOptions.CancellationToken.CanBeCanceled)
				{
					cancellationTokenRegistration.Dispose();
				}
				Parallel.ThrowIfReducableToSingleOCE(ex.InnerExceptions, parallelOptions.CancellationToken);
				throw;
			}
			catch (TaskSchedulerException)
			{
				if (parallelOptions.CancellationToken.CanBeCanceled)
				{
					cancellationTokenRegistration.Dispose();
				}
				throw;
			}
			finally
			{
				int loopStateFlags = sharedPStateFlags.LoopStateFlags;
				parallelLoopResult.m_completed = loopStateFlags == ParallelLoopStateFlags.PLS_NONE;
				if ((loopStateFlags & ParallelLoopStateFlags.PLS_BROKEN) != 0)
				{
					parallelLoopResult.m_lowestBreakIteration = new long?(sharedPStateFlags.LowestBreakIteration);
				}
				if (rootTask != null && rootTask.IsCompleted)
				{
					rootTask.Dispose();
				}
				IDisposable disposable;
				if (orderablePartitionerSource != null)
				{
					disposable = orderablePartitionerSource as IDisposable;
				}
				else
				{
					disposable = partitionerSource as IDisposable;
				}
				if (disposable != null)
				{
					disposable.Dispose();
				}
				if (TplEtwProvider.Log.IsEnabled())
				{
					TplEtwProvider.Log.ParallelLoopEnd((task != null) ? task.m_taskScheduler.Id : TaskScheduler.Current.Id, (task != null) ? task.Id : 0, forkJoinContextID, 0L);
				}
			}
			return parallelLoopResult;
		}

		// Token: 0x06004041 RID: 16449 RVA: 0x000EFAF4 File Offset: 0x000EDCF4
		internal static void ThrowIfReducableToSingleOCE(IEnumerable<Exception> excCollection, CancellationToken ct)
		{
			bool flag = false;
			if (ct.IsCancellationRequested)
			{
				foreach (Exception ex in excCollection)
				{
					flag = true;
					OperationCanceledException ex2 = ex as OperationCanceledException;
					if (ex2 == null || ex2.CancellationToken != ct)
					{
						return;
					}
				}
				if (flag)
				{
					throw new OperationCanceledException(ct);
				}
			}
		}

		// Token: 0x06004042 RID: 16450 RVA: 0x000EFB68 File Offset: 0x000EDD68
		// Note: this type is marked as 'beforefieldinit'.
		static Parallel()
		{
		}

		// Token: 0x04001ACE RID: 6862
		internal static int s_forkJoinContextID;

		// Token: 0x04001ACF RID: 6863
		internal const int DEFAULT_LOOP_STRIDE = 16;

		// Token: 0x04001AD0 RID: 6864
		internal static ParallelOptions s_defaultParallelOptions = new ParallelOptions();

		// Token: 0x02000C0D RID: 3085
		internal struct LoopTimer
		{
			// Token: 0x06006FBD RID: 28605 RVA: 0x00180E54 File Offset: 0x0017F054
			public LoopTimer(int nWorkerTaskIndex)
			{
				int num = 100 + nWorkerTaskIndex % PlatformHelper.ProcessorCount * 50;
				this.m_timeLimit = Environment.TickCount + num;
			}

			// Token: 0x06006FBE RID: 28606 RVA: 0x00180E7C File Offset: 0x0017F07C
			public bool LimitExceeded()
			{
				return Environment.TickCount > this.m_timeLimit;
			}

			// Token: 0x0400367D RID: 13949
			private const int s_BaseNotifyPeriodMS = 100;

			// Token: 0x0400367E RID: 13950
			private const int s_NotifyPeriodIncrementMS = 50;

			// Token: 0x0400367F RID: 13951
			private int m_timeLimit;
		}

		// Token: 0x02000C0E RID: 3086
		[CompilerGenerated]
		private sealed class <>c__DisplayClass4_0
		{
			// Token: 0x06006FBF RID: 28607 RVA: 0x00180E8B File Offset: 0x0017F08B
			public <>c__DisplayClass4_0()
			{
			}

			// Token: 0x06006FC0 RID: 28608 RVA: 0x00180E94 File Offset: 0x0017F094
			internal void <Invoke>b__0()
			{
				for (int i = Interlocked.Increment(ref this.actionIndex); i <= this.actionsCopy.Length; i = Interlocked.Increment(ref this.actionIndex))
				{
					try
					{
						this.actionsCopy[i - 1]();
					}
					catch (Exception ex)
					{
						LazyInitializer.EnsureInitialized<ConcurrentQueue<Exception>>(ref this.exceptionQ, () => new ConcurrentQueue<Exception>());
						this.exceptionQ.Enqueue(ex);
					}
					if (this.parallelOptions.CancellationToken.IsCancellationRequested)
					{
						throw new OperationCanceledException(this.parallelOptions.CancellationToken);
					}
				}
			}

			// Token: 0x04003680 RID: 13952
			public Action[] actionsCopy;

			// Token: 0x04003681 RID: 13953
			public ParallelOptions parallelOptions;

			// Token: 0x04003682 RID: 13954
			public ConcurrentQueue<Exception> exceptionQ;

			// Token: 0x04003683 RID: 13955
			public int actionIndex;
		}

		// Token: 0x02000C0F RID: 3087
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06006FC1 RID: 28609 RVA: 0x00180F4C File Offset: 0x0017F14C
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06006FC2 RID: 28610 RVA: 0x00180F58 File Offset: 0x0017F158
			public <>c()
			{
			}

			// Token: 0x06006FC3 RID: 28611 RVA: 0x00180F60 File Offset: 0x0017F160
			internal ConcurrentQueue<Exception> <Invoke>b__4_1()
			{
				return new ConcurrentQueue<Exception>();
			}

			// Token: 0x06006FC4 RID: 28612 RVA: 0x00180F67 File Offset: 0x0017F167
			internal ConcurrentQueue<Exception> <Invoke>b__4_2()
			{
				return new ConcurrentQueue<Exception>();
			}

			// Token: 0x04003684 RID: 13956
			public static readonly Parallel.<>c <>9 = new Parallel.<>c();

			// Token: 0x04003685 RID: 13957
			public static Func<ConcurrentQueue<Exception>> <>9__4_1;

			// Token: 0x04003686 RID: 13958
			public static Func<ConcurrentQueue<Exception>> <>9__4_2;
		}

		// Token: 0x02000C10 RID: 3088
		[CompilerGenerated]
		private sealed class <>c__DisplayClass17_0<TLocal>
		{
			// Token: 0x06006FC5 RID: 28613 RVA: 0x00180F6E File Offset: 0x0017F16E
			public <>c__DisplayClass17_0()
			{
			}

			// Token: 0x06006FC6 RID: 28614 RVA: 0x00180F76 File Offset: 0x0017F176
			internal void <ForWorker>b__0(object o)
			{
				this.sharedPStateFlags.Cancel();
				this.oce = new OperationCanceledException(this.parallelOptions.CancellationToken);
			}

			// Token: 0x06006FC7 RID: 28615 RVA: 0x00180F9C File Offset: 0x0017F19C
			internal void <ForWorker>b__1()
			{
				Task internalCurrent = Task.InternalCurrent;
				bool flag = internalCurrent == this.rootTask;
				RangeWorker rangeWorker = default(RangeWorker);
				object savedStateFromPreviousReplica = internalCurrent.SavedStateFromPreviousReplica;
				if (savedStateFromPreviousReplica is RangeWorker)
				{
					rangeWorker = (RangeWorker)savedStateFromPreviousReplica;
				}
				else
				{
					rangeWorker = this.rangeManager.RegisterNewWorker();
				}
				int num;
				int num2;
				if (!rangeWorker.FindNewWork32(out num, out num2) || this.sharedPStateFlags.ShouldExitLoop(num))
				{
					return;
				}
				if (TplEtwProvider.Log.IsEnabled())
				{
					TplEtwProvider.Log.ParallelFork((internalCurrent != null) ? internalCurrent.m_taskScheduler.Id : TaskScheduler.Current.Id, (internalCurrent != null) ? internalCurrent.Id : 0, this.forkJoinContextID);
				}
				TLocal tlocal = default(TLocal);
				bool flag2 = false;
				try
				{
					ParallelLoopState32 parallelLoopState = null;
					if (this.bodyWithState != null)
					{
						parallelLoopState = new ParallelLoopState32(this.sharedPStateFlags);
					}
					else if (this.bodyWithLocal != null)
					{
						parallelLoopState = new ParallelLoopState32(this.sharedPStateFlags);
						if (this.localInit != null)
						{
							tlocal = this.localInit();
							flag2 = true;
						}
					}
					Parallel.LoopTimer loopTimer = new Parallel.LoopTimer(this.rootTask.ActiveChildCount);
					for (;;)
					{
						if (this.body != null)
						{
							for (int i = num; i < num2; i++)
							{
								if (this.sharedPStateFlags.LoopStateFlags != ParallelLoopStateFlags.PLS_NONE && this.sharedPStateFlags.ShouldExitLoop())
								{
									break;
								}
								this.body(i);
							}
						}
						else if (this.bodyWithState != null)
						{
							for (int j = num; j < num2; j++)
							{
								if (this.sharedPStateFlags.LoopStateFlags != ParallelLoopStateFlags.PLS_NONE && this.sharedPStateFlags.ShouldExitLoop(j))
								{
									break;
								}
								parallelLoopState.CurrentIteration = j;
								this.bodyWithState(j, parallelLoopState);
							}
						}
						else
						{
							int num3 = num;
							while (num3 < num2 && (this.sharedPStateFlags.LoopStateFlags == ParallelLoopStateFlags.PLS_NONE || !this.sharedPStateFlags.ShouldExitLoop(num3)))
							{
								parallelLoopState.CurrentIteration = num3;
								tlocal = this.bodyWithLocal(num3, parallelLoopState, tlocal);
								num3++;
							}
						}
						if (!flag && loopTimer.LimitExceeded())
						{
							break;
						}
						if (!rangeWorker.FindNewWork32(out num, out num2) || (this.sharedPStateFlags.LoopStateFlags != ParallelLoopStateFlags.PLS_NONE && this.sharedPStateFlags.ShouldExitLoop(num)))
						{
							goto IL_23F;
						}
					}
					internalCurrent.SavedStateForNextReplica = rangeWorker;
					IL_23F:;
				}
				catch
				{
					this.sharedPStateFlags.SetExceptional();
					throw;
				}
				finally
				{
					if (this.localFinally != null && flag2)
					{
						this.localFinally(tlocal);
					}
					if (TplEtwProvider.Log.IsEnabled())
					{
						TplEtwProvider.Log.ParallelJoin((internalCurrent != null) ? internalCurrent.m_taskScheduler.Id : TaskScheduler.Current.Id, (internalCurrent != null) ? internalCurrent.Id : 0, this.forkJoinContextID);
					}
				}
			}

			// Token: 0x04003687 RID: 13959
			public ParallelLoopStateFlags32 sharedPStateFlags;

			// Token: 0x04003688 RID: 13960
			public OperationCanceledException oce;

			// Token: 0x04003689 RID: 13961
			public ParallelOptions parallelOptions;

			// Token: 0x0400368A RID: 13962
			public ParallelForReplicatingTask rootTask;

			// Token: 0x0400368B RID: 13963
			public RangeManager rangeManager;

			// Token: 0x0400368C RID: 13964
			public int forkJoinContextID;

			// Token: 0x0400368D RID: 13965
			public Action<int, ParallelLoopState> bodyWithState;

			// Token: 0x0400368E RID: 13966
			public Func<int, ParallelLoopState, TLocal, TLocal> bodyWithLocal;

			// Token: 0x0400368F RID: 13967
			public Func<TLocal> localInit;

			// Token: 0x04003690 RID: 13968
			public Action<int> body;

			// Token: 0x04003691 RID: 13969
			public Action<TLocal> localFinally;
		}

		// Token: 0x02000C11 RID: 3089
		[CompilerGenerated]
		private sealed class <>c__DisplayClass18_0<TLocal>
		{
			// Token: 0x06006FC8 RID: 28616 RVA: 0x0018128C File Offset: 0x0017F48C
			public <>c__DisplayClass18_0()
			{
			}

			// Token: 0x06006FC9 RID: 28617 RVA: 0x00181294 File Offset: 0x0017F494
			internal void <ForWorker64>b__0(object o)
			{
				this.sharedPStateFlags.Cancel();
				this.oce = new OperationCanceledException(this.parallelOptions.CancellationToken);
			}

			// Token: 0x06006FCA RID: 28618 RVA: 0x001812B8 File Offset: 0x0017F4B8
			internal void <ForWorker64>b__1()
			{
				Task internalCurrent = Task.InternalCurrent;
				bool flag = internalCurrent == this.rootTask;
				RangeWorker rangeWorker = default(RangeWorker);
				object savedStateFromPreviousReplica = internalCurrent.SavedStateFromPreviousReplica;
				if (savedStateFromPreviousReplica is RangeWorker)
				{
					rangeWorker = (RangeWorker)savedStateFromPreviousReplica;
				}
				else
				{
					rangeWorker = this.rangeManager.RegisterNewWorker();
				}
				long num;
				long num2;
				if (!rangeWorker.FindNewWork(out num, out num2) || this.sharedPStateFlags.ShouldExitLoop(num))
				{
					return;
				}
				if (TplEtwProvider.Log.IsEnabled())
				{
					TplEtwProvider.Log.ParallelFork((internalCurrent != null) ? internalCurrent.m_taskScheduler.Id : TaskScheduler.Current.Id, (internalCurrent != null) ? internalCurrent.Id : 0, this.forkJoinContextID);
				}
				TLocal tlocal = default(TLocal);
				bool flag2 = false;
				try
				{
					ParallelLoopState64 parallelLoopState = null;
					if (this.bodyWithState != null)
					{
						parallelLoopState = new ParallelLoopState64(this.sharedPStateFlags);
					}
					else if (this.bodyWithLocal != null)
					{
						parallelLoopState = new ParallelLoopState64(this.sharedPStateFlags);
						if (this.localInit != null)
						{
							tlocal = this.localInit();
							flag2 = true;
						}
					}
					Parallel.LoopTimer loopTimer = new Parallel.LoopTimer(this.rootTask.ActiveChildCount);
					for (;;)
					{
						if (this.body != null)
						{
							for (long num3 = num; num3 < num2; num3 += 1L)
							{
								if (this.sharedPStateFlags.LoopStateFlags != ParallelLoopStateFlags.PLS_NONE && this.sharedPStateFlags.ShouldExitLoop())
								{
									break;
								}
								this.body(num3);
							}
						}
						else if (this.bodyWithState != null)
						{
							for (long num4 = num; num4 < num2; num4 += 1L)
							{
								if (this.sharedPStateFlags.LoopStateFlags != ParallelLoopStateFlags.PLS_NONE && this.sharedPStateFlags.ShouldExitLoop(num4))
								{
									break;
								}
								parallelLoopState.CurrentIteration = num4;
								this.bodyWithState(num4, parallelLoopState);
							}
						}
						else
						{
							long num5 = num;
							while (num5 < num2 && (this.sharedPStateFlags.LoopStateFlags == ParallelLoopStateFlags.PLS_NONE || !this.sharedPStateFlags.ShouldExitLoop(num5)))
							{
								parallelLoopState.CurrentIteration = num5;
								tlocal = this.bodyWithLocal(num5, parallelLoopState, tlocal);
								num5 += 1L;
							}
						}
						if (!flag && loopTimer.LimitExceeded())
						{
							break;
						}
						if (!rangeWorker.FindNewWork(out num, out num2) || (this.sharedPStateFlags.LoopStateFlags != ParallelLoopStateFlags.PLS_NONE && this.sharedPStateFlags.ShouldExitLoop(num)))
						{
							goto IL_242;
						}
					}
					internalCurrent.SavedStateForNextReplica = rangeWorker;
					IL_242:;
				}
				catch
				{
					this.sharedPStateFlags.SetExceptional();
					throw;
				}
				finally
				{
					if (this.localFinally != null && flag2)
					{
						this.localFinally(tlocal);
					}
					if (TplEtwProvider.Log.IsEnabled())
					{
						TplEtwProvider.Log.ParallelJoin((internalCurrent != null) ? internalCurrent.m_taskScheduler.Id : TaskScheduler.Current.Id, (internalCurrent != null) ? internalCurrent.Id : 0, this.forkJoinContextID);
					}
				}
			}

			// Token: 0x04003692 RID: 13970
			public ParallelLoopStateFlags64 sharedPStateFlags;

			// Token: 0x04003693 RID: 13971
			public OperationCanceledException oce;

			// Token: 0x04003694 RID: 13972
			public ParallelOptions parallelOptions;

			// Token: 0x04003695 RID: 13973
			public ParallelForReplicatingTask rootTask;

			// Token: 0x04003696 RID: 13974
			public RangeManager rangeManager;

			// Token: 0x04003697 RID: 13975
			public int forkJoinContextID;

			// Token: 0x04003698 RID: 13976
			public Action<long, ParallelLoopState> bodyWithState;

			// Token: 0x04003699 RID: 13977
			public Func<long, ParallelLoopState, TLocal, TLocal> bodyWithLocal;

			// Token: 0x0400369A RID: 13978
			public Func<TLocal> localInit;

			// Token: 0x0400369B RID: 13979
			public Action<long> body;

			// Token: 0x0400369C RID: 13980
			public Action<TLocal> localFinally;
		}

		// Token: 0x02000C12 RID: 3090
		[CompilerGenerated]
		private sealed class <>c__DisplayClass30_0<TSource, TLocal>
		{
			// Token: 0x06006FCB RID: 28619 RVA: 0x001815AC File Offset: 0x0017F7AC
			public <>c__DisplayClass30_0()
			{
			}

			// Token: 0x06006FCC RID: 28620 RVA: 0x001815B4 File Offset: 0x0017F7B4
			internal void <ForEachWorker>b__0(int i)
			{
				this.body(this.array[i]);
			}

			// Token: 0x06006FCD RID: 28621 RVA: 0x001815CD File Offset: 0x0017F7CD
			internal void <ForEachWorker>b__1(int i, ParallelLoopState state)
			{
				this.bodyWithState(this.array[i], state);
			}

			// Token: 0x06006FCE RID: 28622 RVA: 0x001815E7 File Offset: 0x0017F7E7
			internal void <ForEachWorker>b__2(int i, ParallelLoopState state)
			{
				this.bodyWithStateAndIndex(this.array[i], state, (long)i);
			}

			// Token: 0x06006FCF RID: 28623 RVA: 0x00181603 File Offset: 0x0017F803
			internal TLocal <ForEachWorker>b__3(int i, ParallelLoopState state, TLocal local)
			{
				return this.bodyWithStateAndLocal(this.array[i], state, local);
			}

			// Token: 0x06006FD0 RID: 28624 RVA: 0x0018161E File Offset: 0x0017F81E
			internal TLocal <ForEachWorker>b__4(int i, ParallelLoopState state, TLocal local)
			{
				return this.bodyWithEverything(this.array[i], state, (long)i, local);
			}

			// Token: 0x0400369D RID: 13981
			public Action<TSource> body;

			// Token: 0x0400369E RID: 13982
			public TSource[] array;

			// Token: 0x0400369F RID: 13983
			public Action<TSource, ParallelLoopState> bodyWithState;

			// Token: 0x040036A0 RID: 13984
			public Action<TSource, ParallelLoopState, long> bodyWithStateAndIndex;

			// Token: 0x040036A1 RID: 13985
			public Func<TSource, ParallelLoopState, TLocal, TLocal> bodyWithStateAndLocal;

			// Token: 0x040036A2 RID: 13986
			public Func<TSource, ParallelLoopState, long, TLocal, TLocal> bodyWithEverything;
		}

		// Token: 0x02000C13 RID: 3091
		[CompilerGenerated]
		private sealed class <>c__DisplayClass31_0<TSource, TLocal>
		{
			// Token: 0x06006FD1 RID: 28625 RVA: 0x0018163B File Offset: 0x0017F83B
			public <>c__DisplayClass31_0()
			{
			}

			// Token: 0x06006FD2 RID: 28626 RVA: 0x00181643 File Offset: 0x0017F843
			internal void <ForEachWorker>b__0(int i)
			{
				this.body(this.list[i]);
			}

			// Token: 0x06006FD3 RID: 28627 RVA: 0x0018165C File Offset: 0x0017F85C
			internal void <ForEachWorker>b__1(int i, ParallelLoopState state)
			{
				this.bodyWithState(this.list[i], state);
			}

			// Token: 0x06006FD4 RID: 28628 RVA: 0x00181676 File Offset: 0x0017F876
			internal void <ForEachWorker>b__2(int i, ParallelLoopState state)
			{
				this.bodyWithStateAndIndex(this.list[i], state, (long)i);
			}

			// Token: 0x06006FD5 RID: 28629 RVA: 0x00181692 File Offset: 0x0017F892
			internal TLocal <ForEachWorker>b__3(int i, ParallelLoopState state, TLocal local)
			{
				return this.bodyWithStateAndLocal(this.list[i], state, local);
			}

			// Token: 0x06006FD6 RID: 28630 RVA: 0x001816AD File Offset: 0x0017F8AD
			internal TLocal <ForEachWorker>b__4(int i, ParallelLoopState state, TLocal local)
			{
				return this.bodyWithEverything(this.list[i], state, (long)i, local);
			}

			// Token: 0x040036A3 RID: 13987
			public Action<TSource> body;

			// Token: 0x040036A4 RID: 13988
			public IList<TSource> list;

			// Token: 0x040036A5 RID: 13989
			public Action<TSource, ParallelLoopState> bodyWithState;

			// Token: 0x040036A6 RID: 13990
			public Action<TSource, ParallelLoopState, long> bodyWithStateAndIndex;

			// Token: 0x040036A7 RID: 13991
			public Func<TSource, ParallelLoopState, TLocal, TLocal> bodyWithStateAndLocal;

			// Token: 0x040036A8 RID: 13992
			public Func<TSource, ParallelLoopState, long, TLocal, TLocal> bodyWithEverything;
		}

		// Token: 0x02000C14 RID: 3092
		[CompilerGenerated]
		private sealed class <>c__DisplayClass42_0<TSource, TLocal>
		{
			// Token: 0x06006FD7 RID: 28631 RVA: 0x001816CA File Offset: 0x0017F8CA
			public <>c__DisplayClass42_0()
			{
			}

			// Token: 0x06006FD8 RID: 28632 RVA: 0x001816D2 File Offset: 0x0017F8D2
			internal void <PartitionerForEachWorker>b__0(object o)
			{
				this.sharedPStateFlags.Cancel();
				this.oce = new OperationCanceledException(this.parallelOptions.CancellationToken);
			}

			// Token: 0x06006FD9 RID: 28633 RVA: 0x001816F8 File Offset: 0x0017F8F8
			internal void <PartitionerForEachWorker>b__1()
			{
				Task internalCurrent = Task.InternalCurrent;
				if (TplEtwProvider.Log.IsEnabled())
				{
					TplEtwProvider.Log.ParallelFork((internalCurrent != null) ? internalCurrent.m_taskScheduler.Id : TaskScheduler.Current.Id, (internalCurrent != null) ? internalCurrent.Id : 0, this.forkJoinContextID);
				}
				TLocal tlocal = default(TLocal);
				bool flag = false;
				IDisposable disposable = null;
				try
				{
					ParallelLoopState64 parallelLoopState = null;
					if (this.bodyWithState != null || this.bodyWithStateAndIndex != null)
					{
						parallelLoopState = new ParallelLoopState64(this.sharedPStateFlags);
					}
					else if (this.bodyWithStateAndLocal != null || this.bodyWithEverything != null)
					{
						parallelLoopState = new ParallelLoopState64(this.sharedPStateFlags);
						if (this.localInit != null)
						{
							tlocal = this.localInit();
							flag = true;
						}
					}
					bool flag2 = this.rootTask == internalCurrent;
					Parallel.LoopTimer loopTimer = new Parallel.LoopTimer(this.rootTask.ActiveChildCount);
					if (this.orderedSource != null)
					{
						IEnumerator<KeyValuePair<long, TSource>> enumerator = internalCurrent.SavedStateFromPreviousReplica as IEnumerator<KeyValuePair<long, TSource>>;
						if (enumerator == null)
						{
							enumerator = this.orderablePartitionerSource.GetEnumerator();
							if (enumerator == null)
							{
								throw new InvalidOperationException(Environment.GetResourceString("Parallel_ForEach_NullEnumerator"));
							}
						}
						disposable = enumerator;
						while (enumerator.MoveNext())
						{
							KeyValuePair<long, TSource> keyValuePair = enumerator.Current;
							long key = keyValuePair.Key;
							TSource value = keyValuePair.Value;
							if (parallelLoopState != null)
							{
								parallelLoopState.CurrentIteration = key;
							}
							if (this.simpleBody != null)
							{
								this.simpleBody(value);
							}
							else if (this.bodyWithState != null)
							{
								this.bodyWithState(value, parallelLoopState);
							}
							else if (this.bodyWithStateAndIndex != null)
							{
								this.bodyWithStateAndIndex(value, parallelLoopState, key);
							}
							else if (this.bodyWithStateAndLocal != null)
							{
								tlocal = this.bodyWithStateAndLocal(value, parallelLoopState, tlocal);
							}
							else
							{
								tlocal = this.bodyWithEverything(value, parallelLoopState, key, tlocal);
							}
							if (this.sharedPStateFlags.ShouldExitLoop(key))
							{
								break;
							}
							if (!flag2 && loopTimer.LimitExceeded())
							{
								internalCurrent.SavedStateForNextReplica = enumerator;
								disposable = null;
								break;
							}
						}
					}
					else
					{
						IEnumerator<TSource> enumerator2 = internalCurrent.SavedStateFromPreviousReplica as IEnumerator<TSource>;
						if (enumerator2 == null)
						{
							enumerator2 = this.partitionerSource.GetEnumerator();
							if (enumerator2 == null)
							{
								throw new InvalidOperationException(Environment.GetResourceString("Parallel_ForEach_NullEnumerator"));
							}
						}
						disposable = enumerator2;
						if (parallelLoopState != null)
						{
							parallelLoopState.CurrentIteration = 0L;
						}
						while (enumerator2.MoveNext())
						{
							TSource tsource = enumerator2.Current;
							if (this.simpleBody != null)
							{
								this.simpleBody(tsource);
							}
							else if (this.bodyWithState != null)
							{
								this.bodyWithState(tsource, parallelLoopState);
							}
							else if (this.bodyWithStateAndLocal != null)
							{
								tlocal = this.bodyWithStateAndLocal(tsource, parallelLoopState, tlocal);
							}
							if (this.sharedPStateFlags.LoopStateFlags != ParallelLoopStateFlags.PLS_NONE)
							{
								break;
							}
							if (!flag2 && loopTimer.LimitExceeded())
							{
								internalCurrent.SavedStateForNextReplica = enumerator2;
								disposable = null;
								break;
							}
						}
					}
				}
				catch
				{
					this.sharedPStateFlags.SetExceptional();
					throw;
				}
				finally
				{
					if (this.localFinally != null && flag)
					{
						this.localFinally(tlocal);
					}
					if (disposable != null)
					{
						disposable.Dispose();
					}
					if (TplEtwProvider.Log.IsEnabled())
					{
						TplEtwProvider.Log.ParallelJoin((internalCurrent != null) ? internalCurrent.m_taskScheduler.Id : TaskScheduler.Current.Id, (internalCurrent != null) ? internalCurrent.Id : 0, this.forkJoinContextID);
					}
				}
			}

			// Token: 0x040036A9 RID: 13993
			public ParallelLoopStateFlags64 sharedPStateFlags;

			// Token: 0x040036AA RID: 13994
			public OperationCanceledException oce;

			// Token: 0x040036AB RID: 13995
			public ParallelOptions parallelOptions;

			// Token: 0x040036AC RID: 13996
			public int forkJoinContextID;

			// Token: 0x040036AD RID: 13997
			public Action<TSource, ParallelLoopState> bodyWithState;

			// Token: 0x040036AE RID: 13998
			public Action<TSource, ParallelLoopState, long> bodyWithStateAndIndex;

			// Token: 0x040036AF RID: 13999
			public Func<TSource, ParallelLoopState, TLocal, TLocal> bodyWithStateAndLocal;

			// Token: 0x040036B0 RID: 14000
			public Func<TSource, ParallelLoopState, long, TLocal, TLocal> bodyWithEverything;

			// Token: 0x040036B1 RID: 14001
			public Func<TLocal> localInit;

			// Token: 0x040036B2 RID: 14002
			public ParallelForReplicatingTask rootTask;

			// Token: 0x040036B3 RID: 14003
			public OrderablePartitioner<TSource> orderedSource;

			// Token: 0x040036B4 RID: 14004
			public IEnumerable<KeyValuePair<long, TSource>> orderablePartitionerSource;

			// Token: 0x040036B5 RID: 14005
			public Action<TSource> simpleBody;

			// Token: 0x040036B6 RID: 14006
			public IEnumerable<TSource> partitionerSource;

			// Token: 0x040036B7 RID: 14007
			public Action<TLocal> localFinally;
		}
	}
}
