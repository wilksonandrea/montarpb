namespace Plugin.Core.Colorful
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Text.RegularExpressions;

    public sealed class TextPattern : Pattern<string>
    {
        private Regex regex_0;

        public TextPattern(string string_0) : base(string_0)
        {
            this.regex_0 = new Regex(string_0);
        }

        [IteratorStateMachine(typeof(Class34))]
        public override IEnumerable<string> GetMatches(string input)
        {
            Class34 class1 = new Class34(-2);
            class1.textPattern_0 = this;
            class1.string_2 = input;
            return class1;
        }

        [IteratorStateMachine(typeof(Class33))]
        public override IEnumerable<MatchLocation> GetMatchLocations(string input)
        {
            Class33 class1 = new Class33(-2);
            class1.textPattern_0 = this;
            class1.string_1 = input;
            return class1;
        }

        [CompilerGenerated]
        private sealed class Class33 : IEnumerable<MatchLocation>, IEnumerable, IEnumerator<MatchLocation>, IDisposable, IEnumerator
        {
            private int int_0;
            private MatchLocation matchLocation_0;
            private int int_1;
            public TextPattern textPattern_0;
            private string string_0;
            public string string_1;
            private IEnumerator ienumerator_0;

            [DebuggerHidden]
            public Class33(int int_2)
            {
                this.int_0 = int_2;
                this.int_1 = Environment.CurrentManagedThreadId;
            }

            private void method_0()
            {
                this.int_0 = -1;
                IDisposable disposable = this.ienumerator_0 as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }

            private bool MoveNext()
            {
                bool flag;
                try
                {
                    int num = this.int_0;
                    TextPattern pattern = this.textPattern_0;
                    if (num == 0)
                    {
                        this.int_0 = -1;
                        MatchCollection matchs = pattern.regex_0.Matches(this.string_0);
                        if (matchs.Count != 0)
                        {
                            this.ienumerator_0 = matchs.GetEnumerator();
                            this.int_0 = -3;
                        }
                        else
                        {
                            return false;
                        }
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
                        Match current = (Match) this.ienumerator_0.Current;
                        int num1 = current.Index;
                        MatchLocation location = new MatchLocation(num1, num1 + current.Length);
                        this.matchLocation_0 = location;
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
            IEnumerator<MatchLocation> IEnumerable<MatchLocation>.GetEnumerator()
            {
                TextPattern.Class33 class2;
                if ((this.int_0 == -2) && (this.int_1 == Environment.CurrentManagedThreadId))
                {
                    this.int_0 = 0;
                    class2 = this;
                }
                else
                {
                    class2 = new TextPattern.Class33(0) {
                        textPattern_0 = this.textPattern_0
                    };
                }
                class2.string_0 = this.string_1;
                return class2;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<Plugin.Core.Colorful.MatchLocation>.GetEnumerator();

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

            MatchLocation IEnumerator<MatchLocation>.Current =>
                this.matchLocation_0;

            object IEnumerator.Current =>
                this.matchLocation_0;
        }

        [CompilerGenerated]
        private sealed class Class34 : IEnumerable<string>, IEnumerable, IEnumerator<string>, IDisposable, IEnumerator
        {
            private int int_0;
            private string string_0;
            private int int_1;
            public TextPattern textPattern_0;
            private string string_1;
            public string string_2;
            private IEnumerator ienumerator_0;

            [DebuggerHidden]
            public Class34(int int_2)
            {
                this.int_0 = int_2;
                this.int_1 = Environment.CurrentManagedThreadId;
            }

            private void method_0()
            {
                this.int_0 = -1;
                IDisposable disposable = this.ienumerator_0 as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }

            private bool MoveNext()
            {
                bool flag;
                try
                {
                    int num = this.int_0;
                    TextPattern pattern = this.textPattern_0;
                    if (num == 0)
                    {
                        this.int_0 = -1;
                        MatchCollection matchs = pattern.regex_0.Matches(this.string_1);
                        if (matchs.Count != 0)
                        {
                            this.ienumerator_0 = matchs.GetEnumerator();
                            this.int_0 = -3;
                        }
                        else
                        {
                            return false;
                        }
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
                        Match current = (Match) this.ienumerator_0.Current;
                        this.string_0 = current.Value;
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
            IEnumerator<string> IEnumerable<string>.GetEnumerator()
            {
                TextPattern.Class34 class2;
                if ((this.int_0 == -2) && (this.int_1 == Environment.CurrentManagedThreadId))
                {
                    this.int_0 = 0;
                    class2 = this;
                }
                else
                {
                    class2 = new TextPattern.Class34(0) {
                        textPattern_0 = this.textPattern_0
                    };
                }
                class2.string_1 = this.string_2;
                return class2;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<System.String>.GetEnumerator();

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

            string IEnumerator<string>.Current =>
                this.string_0;

            object IEnumerator.Current =>
                this.string_0;
        }
    }
}

