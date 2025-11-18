namespace Plugin.Core.Colorful
{
    using System;
    using System.Collections.Concurrent;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public sealed class ColorStore
    {
        public ColorStore(ConcurrentDictionary<Color, ConsoleColor> concurrentDictionary_2, ConcurrentDictionary<ConsoleColor, Color> concurrentDictionary_3)
        {
            this.Colors = concurrentDictionary_2;
            this.ConsoleColors = concurrentDictionary_3;
        }

        public ConsoleColor Replace(Color oldColor, Color newColor)
        {
            ConsoleColor color;
            if (!this.Colors.TryRemove(oldColor, out color))
            {
                throw new ArgumentException("An attempt was made to replace a nonexistent color in the ColorStore!");
            }
            this.Colors.TryAdd(newColor, color);
            this.ConsoleColors[color] = newColor;
            return color;
        }

        public bool RequiresUpdate(Color color) => 
            !this.Colors.ContainsKey(color);

        public void Update(ConsoleColor oldColor, Color newColor)
        {
            this.Colors.TryAdd(newColor, oldColor);
            this.ConsoleColors[oldColor] = newColor;
        }

        public ConcurrentDictionary<Color, ConsoleColor> Colors { get; private set; }

        public ConcurrentDictionary<ConsoleColor, Color> ConsoleColors { get; private set; }
    }
}

