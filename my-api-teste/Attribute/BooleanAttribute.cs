using System;

namespace MyAttribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public class BooleanAttribute : Attribute
    {
        private bool _Required;

        public bool Required
        {
            get
            {
                return this._Required;
            }

            set
            {
                this._Required = value;
            }
        }

        public string RequiredErrorMsg
        {
            get;
            set;
        }

        public BooleanAttribute()
        {
            this.Required = false;

            this.RequiredErrorMsg = "O campo [PropertyName] é de preenchimento obrigatório";
        }
    }
}
