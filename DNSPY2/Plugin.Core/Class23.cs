using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Plugin.Core.Colorful;

internal static class Class23
{
    [CompilerGenerated]
    private static class Class24<T>
    {
        public static CallSite<Func<CallSite, Type, string, object, object>> callSite_0;

        public static CallSite<Func<CallSite, object, string>> callSite_1;
    }

    [CompilerGenerated]
    private static class Class25<T>
    {
        public static CallSite<Action<CallSite, List<object>, object>> callSite_0;

        public static CallSite<Action<CallSite, List<object>, object>> callSite_1;
    }

    [CompilerGenerated]
    private sealed class Class26<T> : IEnumerable<T>, IEnumerable, IEnumerator<T>, IDisposable, IEnumerator where T : struct
    {
        private int int_0;

        private T gparam_0;

        private int int_1;

        private IEnumerable<T> ienumerable_0;

        public IEnumerable<T> ienumerable_1;

        private IEnumerator<T> ienumerator_0;

        T IEnumerator<T>.Current
        {
            [DebuggerHidden]
            get
            {
                return gparam_0;
            }
        }

        object IEnumerator.Current
        {
            [DebuggerHidden]
            get
            {
                return gparam_0;
            }
        }

        [DebuggerHidden]
        public Class26(int int_2)
        {
            int_0 = int_2;
            int_1 = Environment.CurrentManagedThreadId;
        }

        [DebuggerHidden]
        void IDisposable.Dispose()
        {
            int num = int_0;
            if (num == -3 || num == 1)
            {
                try
                {
                }
                finally
                {
                    method_0();
                }
            }
            ienumerator_0 = null;
            int_0 = -2;
        }

        private bool MoveNext()
        {
            try
            {
                switch (int_0)
                {
                    default:
                        return false;
                    case 1:
                        int_0 = -3;
                        break;
                    case 0:
                        int_0 = -1;
                        ienumerator_0 = ienumerable_0.GetEnumerator();
                        int_0 = -3;
                        break;
                }
                if (!ienumerator_0.MoveNext())
                {
                    method_0();
                    ienumerator_0 = null;
                    return false;
                }
                T current = ienumerator_0.Current;
                gparam_0 = current;
                int_0 = 1;
                return true;
            }
            catch
            {
                //try-fault
                ((IDisposable)this).Dispose();
                throw;
            }
        }

        bool IEnumerator.MoveNext()
        {
            //ILSpy generated this explicit interface implementation from .override directive in MoveNext
            return this.MoveNext();
        }

        private void method_0()
        {
            int_0 = -1;
            if (ienumerator_0 != null)
            {
                ienumerator_0.Dispose();
            }
        }

        [DebuggerHidden]
        void IEnumerator.Reset()
        {
            throw new NotSupportedException();
        }

        [DebuggerHidden]
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            Class26<T> @class;
            if (int_0 == -2 && int_1 == Environment.CurrentManagedThreadId)
            {
                int_0 = 0;
                @class = this;
            }
            else
            {
                @class = new Class26<T>(0);
            }
            @class.ienumerable_0 = ienumerable_1;
            return @class;
        }

        [DebuggerHidden]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>)this).GetEnumerator();
        }
    }

    [CompilerGenerated]
    private sealed class Class27<T> : IEnumerable<T>, IEnumerable, IEnumerator<T>, IDisposable, IEnumerator where T : IPrototypable<T>
    {
        private int int_0;

        private T gparam_0;

        private int int_1;

        private IEnumerable<T> ienumerable_0;

        public IEnumerable<T> ienumerable_1;

        private IEnumerator<T> ienumerator_0;

        T IEnumerator<T>.Current
        {
            [DebuggerHidden]
            get
            {
                return gparam_0;
            }
        }

        object IEnumerator.Current
        {
            [DebuggerHidden]
            get
            {
                return gparam_0;
            }
        }

        [DebuggerHidden]
        public Class27(int int_2)
        {
            int_0 = int_2;
            int_1 = Environment.CurrentManagedThreadId;
        }

        [DebuggerHidden]
        void IDisposable.Dispose()
        {
            int num = int_0;
            if (num == -3 || num == 1)
            {
                try
                {
                }
                finally
                {
                    method_0();
                }
            }
            ienumerator_0 = null;
            int_0 = -2;
        }

        private bool MoveNext()
        {
            try
            {
                switch (int_0)
                {
                    default:
                        return false;
                    case 1:
                        int_0 = -3;
                        break;
                    case 0:
                        int_0 = -1;
                        ienumerator_0 = ienumerable_0.GetEnumerator();
                        int_0 = -3;
                        break;
                }
                if (!ienumerator_0.MoveNext())
                {
                    method_0();
                    ienumerator_0 = null;
                    return false;
                }
                gparam_0 = ienumerator_0.Current.Prototype();
                int_0 = 1;
                return true;
            }
            catch
            {
                //try-fault
                ((IDisposable)this).Dispose();
                throw;
            }
        }

        bool IEnumerator.MoveNext()
        {
            //ILSpy generated this explicit interface implementation from .override directive in MoveNext
            return this.MoveNext();
        }

        private void method_0()
        {
            int_0 = -1;
            if (ienumerator_0 != null)
            {
                ienumerator_0.Dispose();
            }
        }

        [DebuggerHidden]
        void IEnumerator.Reset()
        {
            throw new NotSupportedException();
        }

        [DebuggerHidden]
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            Class27<T> @class;
            if (int_0 == -2 && int_1 == Environment.CurrentManagedThreadId)
            {
                int_0 = 0;
                @class = this;
            }
            else
            {
                @class = new Class27<T>(0);
            }
            @class.ienumerable_0 = ienumerable_1;
            return @class;
        }

        [DebuggerHidden]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>)this).GetEnumerator();
        }
    }

    [IteratorStateMachine(typeof(Class27<>))]
    internal static IEnumerable<T> smethod_0<T>(this IEnumerable<T> ienumerable_0) where T : IPrototypable<T>
    {
        //yield-return decompiler failed: Unexpected instruction in Iterator.Dispose()
        return new Class27<T>(-2)
        {
            ienumerable_1 = ienumerable_0
        };
    }

    [IteratorStateMachine(typeof(Class26<>))]
    internal static IEnumerable<T> smethod_1<T>(this IEnumerable<T> ienumerable_0) where T : struct
    {
        //yield-return decompiler failed: Unexpected instruction in Iterator.Dispose()
        return new Class26<T>(-2)
        {
            ienumerable_1 = ienumerable_0
        };
    }

    internal static string smethod_2<T>(this T gparam_0)
    {
        return string.Join(string.Empty, (dynamic)gparam_0);
    }

    internal static dynamic smethod_3<T>(this T gparam_0)
    {
        List<object> list = new List<object>();
        if ((object)gparam_0 is object[] array)
        {
            object[] array2 = array;
            foreach (dynamic val in array2)
            {
                list.Add(val);
            }
        }
        else
        {
            list.Add((dynamic)gparam_0);
        }
        return list.ToArray();
    }
}
