using System;

namespace MyAttribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PrimaryKeyAttribute : Attribute
    {        
        public PrimaryKeyAttribute()
        {
        }
    }
}
