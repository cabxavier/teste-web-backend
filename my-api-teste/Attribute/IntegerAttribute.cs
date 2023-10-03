using System;

namespace MyAttribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public class IntegerAttribute : Attribute
    {
        private bool _Required;
        private int _MinValue;
        private int _MaxValue;
        private bool _InsertAllowZero;
        private bool _UpdateAllowZero;

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

        public bool InsertAllowZero
        {
            get
            {
                return this._InsertAllowZero;
            }

            set
            {
                this._InsertAllowZero = value;
            }
        }

        public bool UpdateAllowZero
        {
            get
            {
                return this._UpdateAllowZero;
            }

            set
            {
                this._UpdateAllowZero = value;
            }
        }

        public int MinValue
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

        public int MaxValue
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

        public IntegerAttribute()
        {
            this.Required = false;

            this.RequiredErrorMsg = "O campo [PropertyName] deve conter um valor diferente de zero";

            this.InsertAllowZero = false;

             this.UpdateAllowZero = false;

            this.MinValue = 0;

            this.MinValueErrorMsg = "";

            this.MaxValue = 0;

            this.MaxValueErrorMsg = "";
        }

        private void MinValueOnChange()
        {
           this.MinValueErrorMsg = string.Format("O campo [PropertyName] deve conter um valor maior ou igual a {0}", this._MinValue);
        }

        private void MaxValueOnChange()
        {
            this.MaxValueErrorMsg = string.Format("O campo [PropertyName] deve conter um valor menor ou igual a {0} ", this._MaxValue);
        }
    }
}
