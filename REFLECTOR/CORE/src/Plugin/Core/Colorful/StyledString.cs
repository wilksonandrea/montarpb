namespace Plugin.Core.Colorful
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public sealed class StyledString : IEquatable<StyledString>
    {
        public StyledString(string string_2)
        {
            this.AbstractValue = string_2;
        }

        public StyledString(string string_2, string string_3)
        {
            this.AbstractValue = string_2;
            this.ConcreteValue = string_3;
        }

        public bool Equals(StyledString other) => 
            (other != null) ? ((this.AbstractValue == other.AbstractValue) && (this.ConcreteValue == other.ConcreteValue)) : false;

        public override bool Equals(object obj) => 
            this.Equals(obj as StyledString);

        public override int GetHashCode() => 
            (0xa3 * (0x4f + this.AbstractValue.GetHashCode())) * (0x4f + this.ConcreteValue.GetHashCode());

        public override string ToString() => 
            this.ConcreteValue;

        public string AbstractValue { get; private set; }

        public string ConcreteValue { get; private set; }

        public Color[,] ColorGeometry { get; set; }

        public char[,] CharacterGeometry { get; set; }

        public int[,] CharacterIndexGeometry { get; set; }
    }
}

