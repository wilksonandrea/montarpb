namespace Plugin.Core.Colorful
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class StyleClass<T> : IEquatable<StyleClass<T>>
    {
        [CompilerGenerated]
        private T gparam_0;
        [CompilerGenerated]
        private System.Drawing.Color color_0;

        public StyleClass()
        {
        }

        public StyleClass(T gparam_1, System.Drawing.Color color_1)
        {
            this.Target = gparam_1;
            this.Color = color_1;
        }

        public bool Equals(StyleClass<T> other) => 
            (other != null) ? (this.Target.Equals(other.Target) && (this.Color == other.Color)) : false;

        public override bool Equals(object obj) => 
            this.Equals(obj as StyleClass<T>);

        public override int GetHashCode() => 
            (0xa3 * (0x4f + this.Target.GetHashCode())) * (0x4f + this.Color.GetHashCode());

        public T Target
        {
            get => 
                this.gparam_0;
            protected set => 
                this.gparam_0 = value;
        }

        public System.Drawing.Color Color
        {
            get => 
                this.color_0;
            protected set => 
                this.color_0 = value;
        }
    }
}

