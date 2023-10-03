using System;

namespace MyAttribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CnpjAttribute : Attribute
    {
        private bool _Required;
        private bool _WithMask;
        private string _RegularExpression;
        private bool _CheckCnpj;

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


        public bool CheckCnpj
        {
            get
            {
                return this._CheckCnpj;
            }

            set
            {
                this._CheckCnpj = value;
            }
        }

        public string CheckCnpjErrorMsg
        {
            get;
            set;
        }

        public CnpjAttribute()
        {
            this.Required = false;

            this.RequiredErrorMsg = "O campo [PropertyName] deve ser de preenchimento obrigatório";

            this.WithMask = true;

            this.CheckCnpj = true;

            this.CheckCnpjErrorMsg = "O campo [PropertyName] não representa um Cnpj válido";
        }

        private void WithMaskOnChange()
        {
            this._RegularExpression = this.WithMask ? @"^[0-9]{2}\.[0-9]{3}\.[0-9]{3}\/[0-9]{4}\-[0-9]{2}$" : @"^[0-9]{14}$";
        }
    }
}
