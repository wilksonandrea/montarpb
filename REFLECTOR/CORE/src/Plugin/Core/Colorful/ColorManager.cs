namespace Plugin.Core.Colorful
{
    using System;
    using System.Drawing;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public sealed class ColorManager
    {
        private ColorMapper colorMapper_0;
        private ColorStore colorStore_0;
        private int int_0;
        private int int_1;

        public ColorManager(ColorStore colorStore_1, ColorMapper colorMapper_1, int int_2, int int_3, bool bool_1)
        {
            this.colorStore_0 = colorStore_1;
            this.colorMapper_0 = colorMapper_1;
            this.int_0 = int_3;
            this.int_1 = int_2;
            this.IsInCompatibilityMode = bool_1;
        }

        public Color GetColor(ConsoleColor color) => 
            this.colorStore_0.ConsoleColors[color];

        public ConsoleColor GetConsoleColor(Color color)
        {
            if (this.IsInCompatibilityMode)
            {
                return color.ToNearestConsoleColor();
            }
            try
            {
                return this.method_0(color);
            }
            catch
            {
                return color.ToNearestConsoleColor();
            }
        }

        private ConsoleColor method_0(Color color_0)
        {
            if (this.method_1() && this.colorStore_0.RequiresUpdate(color_0))
            {
                ConsoleColor oldColor = (ConsoleColor) this.int_0;
                this.colorMapper_0.MapColor(oldColor, color_0);
                this.colorStore_0.Update(oldColor, color_0);
                this.int_0++;
            }
            return (!this.colorStore_0.Colors.ContainsKey(color_0) ? this.colorStore_0.Colors.Last<KeyValuePair<Color, ConsoleColor>>().Value : this.colorStore_0.Colors[color_0]);
        }

        private bool method_1() => 
            this.int_0 < this.int_1;

        public void ReplaceColor(Color oldColor, Color newColor)
        {
            if (!this.IsInCompatibilityMode)
            {
                ConsoleColor color = this.colorStore_0.Replace(oldColor, newColor);
                this.colorMapper_0.MapColor(color, newColor);
            }
        }

        public bool IsInCompatibilityMode { get; private set; }
    }
}

