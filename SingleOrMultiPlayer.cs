using System;

namespace lo_novo
{
    /// <summary>
    /// Indicates that a Room is designed for only a single player to be present.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class SinglePlayerAttribute : Attribute
    {

    }

    // MultiPlayer is the default, and doesn't have an attribute (for now?).
}

