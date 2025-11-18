namespace Plugin.Core.Colorful
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public abstract class ColorAlternator : IPrototypable<ColorAlternator>
    {
        protected int nextColorIndex;

        public ColorAlternator()
        {
            this.Colors = new Color[0];
        }

        public ColorAlternator(params Color[] color_1)
        {
            this.Colors = color_1;
        }

        public abstract Color GetNextColor(string input);
        public ColorAlternator Prototype() => 
            this.PrototypeCore();

        protected abstract ColorAlternator PrototypeCore();
        protected abstract void TryIncrementColorIndex();

        public Color[] Colors { get; set; }
    }
}

