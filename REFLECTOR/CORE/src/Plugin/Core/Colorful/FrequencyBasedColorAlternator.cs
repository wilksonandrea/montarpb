namespace Plugin.Core.Colorful
{
    using System;
    using System.Drawing;
    using System.Linq;

    public sealed class FrequencyBasedColorAlternator : ColorAlternator, IPrototypable<FrequencyBasedColorAlternator>
    {
        private int int_0;
        private int int_1;

        public FrequencyBasedColorAlternator(int int_2, params Color[] color_1) : base(color_1)
        {
            this.int_0 = int_2;
        }

        public override Color GetNextColor(string input)
        {
            if (base.Colors.Length == 0)
            {
                throw new InvalidOperationException("No colors have been supplied over which to alternate!");
            }
            this.TryIncrementColorIndex();
            return base.Colors[base.nextColorIndex];
        }

        public FrequencyBasedColorAlternator Prototype() => 
            new FrequencyBasedColorAlternator(this.int_0, base.Colors.smethod_1<Color>().ToArray<Color>());

        protected override ColorAlternator PrototypeCore() => 
            this.Prototype();

        protected override void TryIncrementColorIndex()
        {
            if (this.int_1 >= ((base.Colors.Length * this.int_0) - 1))
            {
                base.nextColorIndex = 0;
                this.int_1 = 0;
            }
            else
            {
                this.int_1++;
                base.nextColorIndex = (int) Math.Floor((double) (((double) this.int_1) / ((double) this.int_0)));
            }
        }
    }
}

