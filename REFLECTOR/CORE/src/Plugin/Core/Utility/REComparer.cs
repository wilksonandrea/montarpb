namespace Plugin.Core.Utility
{
    using System;
    using System.Collections.Generic;

    public class REComparer : EqualityComparer<object>
    {
        public override bool Equals(object X, object Y) => 
            X == Y;

        public override int GetHashCode(object OBJ) => 
            (OBJ != null) ? OBJ.GetHashCode() : 0;
    }
}

