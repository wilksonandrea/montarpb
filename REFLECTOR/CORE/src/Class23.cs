using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

internal static class Class23
{
    [IteratorStateMachine(typeof(Class27))]
    internal static IEnumerable<T> smethod_0<T>(this IEnumerable<T> ienumerable_0) where T: IPrototypable<T>
    {
        Class27<T> class1 = new Class27<T>(-2);
        class1.ienumerable_1 = ienumerable_0;
        return class1;
    }

    [IteratorStateMachine(typeof(Class26))]
    internal static IEnumerable<T> smethod_1<T>(this IEnumerable<T> ienumerable_0) where T: struct
    {
        Class26<T> class1 = new Class26<T>(-2);
        class1.ienumerable_1 = ienumerable_0;
        return class1;
    }

    internal static string smethod_2<T>(this T gparam_0)
    {
        Class24<T>.callSite_1 ??= CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(string), typeof(Class23)));
        if (Class24<T>.callSite_0 == null)
        {
            CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.IsStaticType | CSharpArgumentInfoFlags.UseCompileTimeType, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) };
            Class24<T>.callSite_0 = CallSite<Func<CallSite, Type, string, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Join", null, typeof(Class23), argumentInfo));
        }
        return Class24<T>.callSite_1(Class24<T>.callSite_1.Target, Class24<T>.callSite_0.Target(Class24<T>.callSite_0, typeof(string), string.Empty, gparam_0));
    }

    [return: Dynamic]
    internal static object smethod_3<T>(this T gparam_0)
    {
        List<object> list = new List<object>();
        object[] objArray = gparam_0 as object[];
        if (objArray == null)
        {
            if (Class25<T>.callSite_1 == null)
            {
                CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) };
                Class25<T>.callSite_1 = CallSite<Action<CallSite, List<object>, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Add", null, typeof(Class23), argumentInfo));
            }
            Class25<T>.callSite_1.Target(Class25<T>.callSite_1, list, gparam_0);
        }
        else
        {
            foreach (object obj2 in objArray)
            {
                if (Class25<T>.callSite_0 == null)
                {
                    CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) };
                    Class25<T>.callSite_0 = CallSite<Action<CallSite, List<object>, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Add", null, typeof(Class23), argumentInfo));
                }
                Class25<T>.callSite_0.Target(Class25<T>.callSite_0, list, obj2);
            }
        }
        return list.ToArray();
    }

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
    private sealed class Class26<T> : IEnumerable<T>, IEnumerable, IEnumerator<T>, IDisposable, IEnumerator where T: struct
    {
        private int int_0;
        private T gparam_0;
        private int int_1;
        private IEnumerable<T> ienumerable_0;
        public IEnumerable<T> ienumerable_1;
        private IEnumerator<T> ienumerator_0;

        [DebuggerHidden]
        public Class26(int int_2)
        {
            this.int_0 = int_2;
            this.int_1 = Environment.CurrentManagedThreadId;
        }

        private void method_0()
        {
            this.int_0 = -1;
            if (this.ienumerator_0 != null)
            {
                this.ienumerator_0.Dispose();
            }
        }

        private bool MoveNext()
        {
            bool flag;
            try
            {
                int num = this.int_0;
                if (num == 0)
                {
                    this.int_0 = -1;
                    this.ienumerator_0 = this.ienumerable_0.GetEnumerator();
                    this.int_0 = -3;
                }
                else if (num == 1)
                {
                    this.int_0 = -3;
                }
                else
                {
                    return false;
                }
                if (!this.ienumerator_0.MoveNext())
                {
                    this.method_0();
                    this.ienumerator_0 = null;
                    flag = false;
                }
                else
                {
                    T current = this.ienumerator_0.Current;
                    this.gparam_0 = current;
                    this.int_0 = 1;
                    flag = true;
                }
            }
            fault
            {
                this.System.IDisposable.Dispose();
            }
            return flag;
        }

        [DebuggerHidden]
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            Class23.Class26<T> class2;
            if ((this.int_0 != -2) || (this.int_1 != Environment.CurrentManagedThreadId))
            {
                class2 = new Class23.Class26<T>(0);
            }
            else
            {
                this.int_0 = 0;
                class2 = (Class23.Class26<T>) this;
            }
            class2.ienumerable_0 = this.ienumerable_1;
            return class2;
        }

        [DebuggerHidden]
        IEnumerator IEnumerable.GetEnumerator() => 
            this.System.Collections.Generic.IEnumerable<T>.GetEnumerator();

        [DebuggerHidden]
        void IEnumerator.Reset()
        {
            throw new NotSupportedException();
        }

        [DebuggerHidden]
        void IDisposable.Dispose()
        {
            int num = this.int_0;
            if ((num == -3) || (num == 1))
            {
                try
                {
                }
                finally
                {
                    this.method_0();
                }
            }
            this.ienumerator_0 = null;
            this.int_0 = -2;
        }

        T IEnumerator<T>.Current =>
            this.gparam_0;

        object IEnumerator.Current =>
            this.gparam_0;
    }

    [CompilerGenerated]
    private sealed class Class27<T> : IEnumerable<T>, IEnumerable, IEnumerator<T>, IDisposable, IEnumerator where T: IPrototypable<T>
    {
        private int int_0;
        private T gparam_0;
        private int int_1;
        private IEnumerable<T> ienumerable_0;
        public IEnumerable<T> ienumerable_1;
        private IEnumerator<T> ienumerator_0;

        [DebuggerHidden]
        public Class27(int int_2)
        {
            this.int_0 = int_2;
            this.int_1 = Environment.CurrentManagedThreadId;
        }

        private void method_0()
        {
            this.int_0 = -1;
            if (this.ienumerator_0 != null)
            {
                this.ienumerator_0.Dispose();
            }
        }

        private bool MoveNext()
        {
            bool flag;
            try
            {
                int num = this.int_0;
                if (num == 0)
                {
                    this.int_0 = -1;
                    this.ienumerator_0 = this.ienumerable_0.GetEnumerator();
                    this.int_0 = -3;
                }
                else if (num == 1)
                {
                    this.int_0 = -3;
                }
                else
                {
                    return false;
                }
                if (!this.ienumerator_0.MoveNext())
                {
                    this.method_0();
                    this.ienumerator_0 = null;
                    flag = false;
                }
                else
                {
                    this.gparam_0 = this.ienumerator_0.Current.Prototype();
                    this.int_0 = 1;
                    flag = true;
                }
            }
            fault
            {
                this.System.IDisposable.Dispose();
            }
            return flag;
        }

        [DebuggerHidden]
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            Class23.Class27<T> class2;
            if ((this.int_0 != -2) || (this.int_1 != Environment.CurrentManagedThreadId))
            {
                class2 = new Class23.Class27<T>(0);
            }
            else
            {
                this.int_0 = 0;
                class2 = (Class23.Class27<T>) this;
            }
            class2.ienumerable_0 = this.ienumerable_1;
            return class2;
        }

        [DebuggerHidden]
        IEnumerator IEnumerable.GetEnumerator() => 
            this.System.Collections.Generic.IEnumerable<T>.GetEnumerator();

        [DebuggerHidden]
        void IEnumerator.Reset()
        {
            throw new NotSupportedException();
        }

        [DebuggerHidden]
        void IDisposable.Dispose()
        {
            int num = this.int_0;
            if ((num == -3) || (num == 1))
            {
                try
                {
                }
                finally
                {
                    this.method_0();
                }
            }
            this.ienumerator_0 = null;
            this.int_0 = -2;
        }

        T IEnumerator<T>.Current =>
            this.gparam_0;

        object IEnumerator.Current =>
            this.gparam_0;
    }
}

