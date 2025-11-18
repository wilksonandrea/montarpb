namespace Plugin.Core.Colorful
{
    using System;
    using System.Drawing;

    public sealed class Formatter
    {
        private StyleClass<object> styleClass_0;

        public Formatter(object object_0, System.Drawing.Color color_0)
        {
            this.styleClass_0 = new StyleClass<object>(object_0, color_0);
        }

        public object Target =>
            this.styleClass_0.Target;

        public System.Drawing.Color Color =>
            this.styleClass_0.Color;
    }
}

