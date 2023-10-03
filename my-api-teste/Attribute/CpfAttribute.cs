using System;

namespace MyAttribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CpfAttribute : Attribute
    {
        private bool _Required;
        private bool _WithMask;
        private string _RegularExpression;
        private bool _CheckCpf;

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

        public bool WithMask
        {
            get
            {
                return this._WithMask;
            }

            set
            {
                if (value != this._WithMask)
                {
                    this._WithMask = value;

                    this.WithMaskOnChange();
                }
            }
        }

        public string RegularExpression
        {
            get
            {
                return this._RegularExpression;
            }
        }

        public string RegularExpressionErrorMsg
        {
            get;
            set;
        }

        public bool CheckCpf
        {
            get
            {
                return this._CheckCpf;
            }

            set
            {
                this._CheckCpf = value;
            }
        }


        public string CheckCpfErrorMsg
        {
            get;
            set;
        }

        public CpfAttribute()
        {
            this.Required = false;

            this.RequiredErrorMsg = "O campo [PropertyName] deve ser de preenchimento obrigatório";

            this.WithMask = true;

            this.CheckCpf = true;

            this.CheckCpfErrorMsg = "O campo [PropertyName] não corresponde a um Cpf válido";
        }

        private void WithMaskOnChange()
        {
            this._RegularExpression = this._WithMask ? @"^[0-9]{3}\.[0-9]{3}\.[0-9]{3}\-[0-9]{2}$" : @"^[0-9]{11}$";
        }
    }
}
