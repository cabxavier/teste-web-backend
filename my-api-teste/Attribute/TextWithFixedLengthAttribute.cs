using System;

namespace MyAttribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public class TextWithFixedLengthAttribute : Attribute
    {
        private bool _Required;
        private int _FixedLength;

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

        public int FixedLength
        {
            get
            {
                return this._FixedLength;
            }

            set
            {
                if (value != this._FixedLength)
                {
                    this._FixedLength = value;

                    this.FixedLengthOnChange();
                }
            }
        }

        public string FixedLengthErrorMsg
        {
            get;
            set;
        }

        public TextWithFixedLengthAttribute()
        {
            this.Required = false;

            this.RequiredErrorMsg = "O campo [PropertyName] é de preenchimento obrigatório";

            this.FixedLength = 0;

            this.FixedLengthErrorMsg = "";
        }

        private void FixedLengthOnChange()
        {
            this.FixedLengthErrorMsg = string.Format("O campo [PropertyName] deve conter um valor com no mínimo {0} caracteres", this._FixedLength);
        }
    }
}
