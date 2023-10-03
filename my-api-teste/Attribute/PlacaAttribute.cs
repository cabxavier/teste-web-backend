﻿using System;

namespace MyAttribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PlacaAttribute : Attribute
    {
        private bool _Required;
        private bool _WithMask;
        private string _RegularExpression;

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

        public PlacaAttribute()
        {
            this.Required = false;

            this.RequiredErrorMsg = "O campo [PropertyName] deve ser de preenchimento obrigatório";

            this.WithMask = true;
        }

        private void WithMaskOnChange()
        {
            this._RegularExpression = this.WithMask ? @"^[A-Z]{3}\-[0-9]{4}$" : @"^[A-Z]{3}[0-9]{4}$";
        }
    }
}
