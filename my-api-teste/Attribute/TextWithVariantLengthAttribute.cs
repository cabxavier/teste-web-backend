using System;

namespace MyAttribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public class TextWithVariantLengthAttribute : Attribute
    {
        private bool _Required;
        private int _MinLength;
        private int _MaxLength;

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


        public int MinLength
        {
            get
            {
                return this._MinLength;
            }

            set
            {
                if (value != this._MinLength)
                {
                    this._MinLength = value;

                    this.MinLengthOnChange();
                }
            }
        }

        public string MinLengthErrorMsg
        {
            get;
            set;
        }


        public int MaxLength
        {
            get
            {
                return this._MaxLength;
            }

            set
            {
                if (value != this._MaxLength)
                {
                    this._MaxLength = value;

                    this.MaxLengthOnChange();
                }
            }
        }

        public string MaxLengthErrorMsg
        {
            get;
            set;
        }

        public TextWithVariantLengthAttribute()
        {
            this.Required = false;

            this.RequiredErrorMsg = "O campo [PropertyName] é de preenchimento obrigatório";

            this.MinLength = 0;

            this.MinLengthErrorMsg = "";

            this.MaxLength = 0;

            this.MaxLengthErrorMsg = "";
        }

        private void MinLengthOnChange()
        {
            this.MinLengthErrorMsg = string.Format("O campo [PropertyName] deve conter um valor com no mínimo {0} caracteres", this._MinLength);
        }

        private void MaxLengthOnChange()
        {
            this.MaxLengthErrorMsg = string.Format("O campo [PropertyName] deve conter um valor com no máximo {0} caracteres", this._MaxLength);
        }
    }
}
