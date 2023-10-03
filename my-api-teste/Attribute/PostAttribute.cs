using System;

namespace MyAttribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PostAttribute : Attribute
    {
        public bool PostInsert { get; set; }
        public bool PostUpdate { get; set; }
        public bool PostDelete { get; set; }

        public PostAttribute()
        {
            this.PostInsert = false;
            this.PostUpdate = false;
            this.PostDelete = false;
        }
    }
}
