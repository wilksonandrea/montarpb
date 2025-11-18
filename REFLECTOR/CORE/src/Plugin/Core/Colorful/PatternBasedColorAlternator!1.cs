namespace Plugin.Core.Colorful
{
    using System;
    using System.Drawing;
    using System.Linq;

    public sealed class PatternBasedColorAlternator<T> : ColorAlternator, IPrototypable<PatternBasedColorAlternator<T>>
    {
        private PatternCollection<T> patternCollection_0;
        private bool bool_0;

        public PatternBasedColorAlternator(PatternCollection<T> patternCollection_1, params Color[] color_1) : base(color_1)
        {
            this.bool_0 = true;
            this.patternCollection_0 = patternCollection_1;
        }

        public override Color GetNextColor(string input)
        {
            if (base.Colors.Length == 0)
            {
                throw new InvalidOperationException("No colors have been supplied over which to alternate!");
            }
            if (this.bool_0)
            {
                this.bool_0 = false;
                return base.Colors[base.nextColorIndex];
            }
            if (this.patternCollection_0.MatchFound(input))
            {
                this.TryIncrementColorIndex();
            }
            return base.Colors[base.nextColorIndex];
        }

        public PatternBasedColorAlternator<T> Prototype() => 
            new PatternBasedColorAlternator<T>(this.patternCollection_0.Prototype(), base.Colors.smethod_1<Color>().ToArray<Color>());

        protected override ColorAlternator PrototypeCore() => 
            this.Prototype();

        protected override void TryIncrementColorIndex()
        {
            if (base.nextColorIndex >= (base.Colors.Length - 1))
            {
                base.nextColorIndex = 0;
            }
            else
            {
                base.nextColorIndex++;
            }
        }
    }
}

