using System;

namespace MyAttribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DateAttribute : Attribute
    {
        private bool _Required;
        private DateTime _MinValue;
        private DateTime _MaxValue;


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


        public DateTime MinValue
        {
            get
            {
                return this._MinValue;
            }

            set
            {
                if (value != this._MinValue)
                {
                    this._MinValue = value;

                    this.MinValueOnChange();
                }
            }
        }

        public string MinValueErrorMsg
        {
            get;
            set;
        }

        public DateTime MaxValue
        {
            get
            {
                return this._MaxValue;
            }

            set
            {
                if (value != this._MaxValue)
                {
                    this._MaxValue = value;

                    this.MaxValueOnChange();
                }
            }
        }

        public string MaxValueErrorMsg
        {
            get;
            set;
        }

        public DateAttribute()
        {
            this.Required = false;

            this.RequiredErrorMsg = "O campo [PropertyName] é de preenchimento obrigatório";
            
            this.MinValue = DateTime.MinValue;

            this.MinValueErrorMsg = "";

            
            this.MaxValue = DateTime.MaxValue;

            this.MaxValueErrorMsg = "";
        }

        private void MinValueOnChange()
        {
            this.MinValueErrorMsg = string.Format("O campo [PropertyName] deve conter uma data maior ou igual a {0}", this._MinValue);
        }

        private void MaxValueOnChange()
        {
            this.MaxValueErrorMsg = string.Format("O campo [PropertyName] deve conter uma data menor ou igual a {0} ", this._MaxValue);
        }
    }
}
