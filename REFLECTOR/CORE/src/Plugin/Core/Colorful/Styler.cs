namespace Plugin.Core.Colorful
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public sealed class Styler : StyleClass<TextPattern>, IEquatable<Styler>
    {
        public Styler(string string_0, Color color_1, MatchFound matchFound_1)
        {
            base.Target = new TextPattern(string_0);
            base.Color = color_1;
            this.MatchFoundHandler = matchFound_1;
        }

        public bool Equals(Styler other) => 
            (other != null) ? (base.Equals((StyleClass<TextPattern>) other) && (this.MatchFoundHandler == other.MatchFoundHandler)) : false;

        public override bool Equals(object obj) => 
            this.Equals(obj as Styler);

        public override int GetHashCode() => 
            base.GetHashCode() * (0x4f + this.MatchFoundHandler.GetHashCode());

        public MatchFound MatchFoundHandler { get; private set; }

        public delegate string MatchFound(string unstyledInput, MatchLocation matchLocation, string match);

        public delegate string MatchFoundLite(string match);
    }
}

