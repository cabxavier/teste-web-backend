using System;

namespace MyAttribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DecimalAttribute : Attribute
    {
        private bool _Required;
        private int _DecimalPlace;
        private decimal _MinValue;
        private decimal _MaxValue;

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


        public int DecimalPlace
        {
            get
            {
                return this._DecimalPlace;
            }

            set
            {
                this._DecimalPlace = value;
            }
        }

        public decimal MinValue
        {
            get
            {
                return this._MinValue;
            }

            set
            {
                if (value != this._MinValue)
                {
                    _MinValue = value;

                    this.MinValueOnChange();
                }
            }
        }

        public string MinValueErrorMsg
        {
            get;
            set;
        }

        public decimal MaxValue
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


        public DecimalAttribute()
        {
            this.Required = false;

            this.RequiredErrorMsg = "O campo [PropertyName] deve conter um valor diferente de zero";

            this.DecimalPlace = 2;

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
