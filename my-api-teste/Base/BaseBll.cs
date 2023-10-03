using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using MyAttribute;
using Helper;
using Interface;
using Enum;
using Util;
using MyException;

namespace Base
{
    public abstract class BaseBll
    {
        private ConnectionHelper _ConnHelper = null;
        protected IDal _Dal = null;

        protected IDal Dal
        {
            get { return this._Dal; }
        }

        public BaseBll(ConnectionHelper ConnHelper = null)
        {
            this._ConnHelper = ConnHelper;
            this._Dal = NewDal(_ConnHelper);
        }

        #region Método IsValid
        public bool IsValid(IDto Dto, PostAction Action)
        {
            bool _Out = false;
            string _PropertyName = "";
            object _PropertyValue = "";

            PostAttribute _PostAttribute = null;

            List<PropertyInfo> lstInfos = Dto.GetType().GetProperties().ToList();

            foreach (PropertyInfo _PropertyInfo in lstInfos)
            {
                _PostAttribute = (PostAttribute)_PropertyInfo.GetCustomAttribute(typeof(PostAttribute));

                _PropertyName = _PropertyInfo.Name;
                _PropertyValue = _PropertyInfo.GetValue(Dto, null);

                if (_PostAttribute != null)
                {
                    switch (Action)
                    {
                        case PostAction.Insert:
                            if (_PostAttribute.PostInsert)
                            {
                                object[] _Attr = _PropertyInfo.GetCustomAttributes(true);

                                this.ExecuteValidation(_PropertyName, _PropertyValue, _Attr, PostAction.Insert);
                            }
                            break;

                        case PostAction.Update:
                            if (_PostAttribute.PostUpdate)
                            {
                                object[] _Attr = _PropertyInfo.GetCustomAttributes(true);

                                this.ExecuteValidation(_PropertyName, _PropertyValue, _Attr, PostAction.Update);
                            }
                            break;

                        case PostAction.Delete:
                            if (_PostAttribute.PostDelete)
                            {
                                object[] _Attr = _PropertyInfo.GetCustomAttributes(true);

                                this.ExecuteValidation(_PropertyName, _PropertyValue, _Attr, PostAction.Delete);
                            }
                            break;
                    }
                }
            }

            return _Out;
        }
        #endregion


        #region Método ExecuteValidation
        public void ExecuteValidation(string PropertyName, object PropertyValue, object[] Attributes, PostAction Action)
        {
            for (var i = 0; i < Attributes.Length; i++)
            {
                if (Attributes[i].GetType() == typeof(TextWithVariantLengthAttribute))
                {
                    string _Value = Convert.ToString(PropertyValue);

                    TextWithVariantLengthAttribute _Attr = (TextWithVariantLengthAttribute)Attributes[i];

                    if (_Attr.Required)
                    {
                        if (string.IsNullOrEmpty(_Value))
                        {
                            throw new ExceptionAttribute(_Attr.RequiredErrorMsg.Replace("[PropertyName]", PropertyName));
                        }
                    }

                    if (_Attr.MinLength != 0)
                    {
                        if (_Value.Length < _Attr.MinLength)
                        {
                            throw new ExceptionAttribute(_Attr.MinLengthErrorMsg.Replace("[PropertyName]", PropertyName));
                        }
                    }

                    if (_Attr.MaxLength != 0)
                    {
                        if (_Value.Length > _Attr.MaxLength)
                        {
                            throw new ExceptionAttribute(_Attr.MaxLengthErrorMsg.Replace("[PropertyName]", PropertyName));
                        }
                    }
                }

                if (Attributes[i].GetType() == typeof(TextWithFixedLengthAttribute))
                {
                    string _Value = Convert.ToString(PropertyValue);

                    TextWithFixedLengthAttribute _Attr = (TextWithFixedLengthAttribute)Attributes[i];

                    if (_Attr.Required)
                    {
                        if (string.IsNullOrEmpty(_Value))
                        {
                            throw new ExceptionAttribute(_Attr.RequiredErrorMsg.Replace("[PropertyName]", PropertyName));
                        }

                        if ((_Value.Length != _Attr.FixedLength))
                        {
                            throw new ExceptionAttribute(_Attr.FixedLengthErrorMsg.Replace("[PropertyName]", PropertyName));
                        }
                    }

                    if ((_Value.Length != _Attr.FixedLength) && (_Value.Length != 0))
                    {
                        throw new ExceptionAttribute(_Attr.FixedLengthErrorMsg.Replace("[PropertyName]", PropertyName));
                    }
                }

                if (Attributes[i].GetType() == typeof(BooleanAttribute))
                {
                    bool _Value = Convert.ToBoolean(PropertyValue);
                }

                if (Attributes[i].GetType() == typeof(DecimalAttribute))
                {
                    decimal _Value = Convert.ToDecimal(PropertyValue);

                    DecimalAttribute _Attr = (DecimalAttribute)Attributes[i];

                    if (_Attr.Required)
                    {
                        if (_Value == 0)
                        {
                            throw new ExceptionAttribute(_Attr.RequiredErrorMsg.Replace("[PropertyName]", PropertyName));
                        }
                    }

                    if (_Attr.MinValue != 0)
                    {
                        if (_Value < _Attr.MinValue)
                        {
                            throw new ExceptionAttribute(_Attr.MinValueErrorMsg.Replace("[PropertyName]", PropertyName));
                        }
                    }

                    if (_Attr.MaxValue != 0)
                    {
                        if (_Value > _Attr.MaxValue)
                        {
                            throw new ExceptionAttribute(_Attr.MaxValueErrorMsg.Replace("[PropertyName]", PropertyName));
                        }
                    }
                }

                if (Attributes[i].GetType() == typeof(IntegerAttribute))
                {
                    int _Value = Convert.ToInt32(PropertyValue);

                    IntegerAttribute _Attr = (IntegerAttribute)Attributes[i];

                    if (_Attr.Required)
                    {
                        if (_Value == 0)
                        {
                            if ((Action == PostAction.Insert) && (_Attr.InsertAllowZero == false))
                            {
                                throw new ExceptionAttribute(_Attr.RequiredErrorMsg.Replace("[PropertyName]", PropertyName));
                            }

                            if ((Action == PostAction.Update) && (_Attr.UpdateAllowZero == false))
                            {
                                throw new ExceptionAttribute(_Attr.RequiredErrorMsg.Replace("[PropertyName]", PropertyName));
                            }
                        }
                    }

                    if (_Attr.MinValue != 0)
                    {
                        if (_Value < _Attr.MinValue)
                        {
                            throw new ExceptionAttribute(_Attr.MinValueErrorMsg.Replace("[PropertyName]", PropertyName));
                        }
                    }

                    if (_Attr.MaxValue != 0)
                    {
                        if (_Value > _Attr.MaxValue)
                        {
                            throw new ExceptionAttribute(_Attr.MaxValueErrorMsg.Replace("[PropertyName]", PropertyName));
                        }
                    }
                }

                if (Attributes[i].GetType() == typeof(DateAttribute))
                {
                    DateTime _Value = Convert.ToDateTime(PropertyValue);

                    DateAttribute _Attr = (DateAttribute)Attributes[i];

                    if (_Attr.MinValue != DateTime.MinValue)
                    {
                        if (_Value < _Attr.MinValue)
                        {
                            throw new ExceptionAttribute(_Attr.MinValueErrorMsg.Replace("[PropertyName]", PropertyName));
                        }
                    }

                    if (_Attr.MaxValue != DateTime.MaxValue)
                    {
                        if (_Value > _Attr.MaxValue)
                        {
                            throw new ExceptionAttribute(_Attr.MaxValueErrorMsg.Replace("[PropertyName]", PropertyName));
                        }
                    }
                }

                if (Attributes[i].GetType() == typeof(CepAttribute))
                {
                    string _Value = Convert.ToString(PropertyValue);

                    CepAttribute _Attr = (CepAttribute)Attributes[i];

                    if (_Attr.Required)
                    {
                        if (string.IsNullOrEmpty(_Value))
                        {
                            throw new ExceptionAttribute(_Attr.RequiredErrorMsg.Replace("[PropertyName]", PropertyName));
                        }
                    }

                    if (!string.IsNullOrEmpty(_Value))
                    {
                        Regex _Regex = new Regex(_Attr.RegularExpression);
                        Match _Match = _Regex.Match(_Value);

                        if (!_Match.Success)
                        {
                            throw new ExceptionAttribute(_Attr.RegularExpressionErrorMsg.Replace("[PropertyName]", PropertyName));
                        }
                    }
                }

                if (Attributes[i].GetType() == typeof(CpfAttribute))
                {
                    string _Value = Convert.ToString(PropertyValue);

                    CpfAttribute _Attr = (CpfAttribute)Attributes[i];

                    if (_Attr.Required)
                    {
                        if (string.IsNullOrEmpty(_Value))
                        {
                            throw new ExceptionAttribute(_Attr.RequiredErrorMsg.Replace("[PropertyName]", PropertyName));
                        }
                    }

                    if (!string.IsNullOrEmpty(_Value))
                    {
                        Regex _Regex = new Regex(_Attr.RegularExpression);
                        Match _Match = _Regex.Match(_Value);

                        if (!_Match.Success)
                        {
                            throw new Exception("O campo " + PropertyName + " não possui um formato esperado");
                        }
                    }

                    if (!string.IsNullOrEmpty(_Value))
                    {
                        if (!TextUtil.IsValidCpf(_Value))
                        {
                            throw new ExceptionAttribute(_Attr.CheckCpfErrorMsg.Replace("[PropertyName]", PropertyName));
                        }
                    }
                }
                
                if (Attributes[i].GetType() == typeof(CnpjAttribute))
                {
                    string _Value = Convert.ToString(PropertyValue);

                    CnpjAttribute _Attr = (CnpjAttribute)Attributes[i];

                    if (!_Attr.Required)
                    {
                        if (!string.IsNullOrEmpty(_Value))
                        {
                            throw new ExceptionAttribute(_Attr.RequiredErrorMsg.Replace("[PropertyName]", PropertyName));
                        }
                    }

                    if (!string.IsNullOrEmpty(_Value))
                    {
                        Regex _Regex = new Regex(_Attr.RegularExpression);
                        Match _Match = _Regex.Match(_Value);

                        if (!_Match.Success)
                        {
                            throw new ExceptionAttribute(_Attr.RegularExpressionErrorMsg.Replace("[PropertyName]", PropertyName));
                        }
                    }

                    if (!string.IsNullOrEmpty(_Value))
                    {
                        if (!TextUtil.IsValidCnpj(_Value))
                        {
                            throw new ExceptionAttribute(_Attr.CheckCnpjErrorMsg.Replace("[PropertyName]", PropertyName));
                        }
                    }
                }

                if (Attributes[i].GetType() == typeof(CnpjAttribute))
                {
                    string _Value = Convert.ToString(PropertyValue);

                    PlacaAttribute _Attr = (PlacaAttribute)Attributes[i];

                    if (!_Attr.Required)
                    {
                        if (!string.IsNullOrEmpty(_Value))
                        {
                            throw new ExceptionAttribute(_Attr.RequiredErrorMsg.Replace("[PropertyName]", PropertyName));
                        }
                    }

                    if (!string.IsNullOrEmpty(_Value))
                    {
                        Regex _Regex = new Regex(_Attr.RegularExpression);
                        Match _Match = _Regex.Match(_Value);

                        if (!_Match.Success)
                        {
                            throw new ExceptionAttribute(_Attr.RegularExpressionErrorMsg.Replace("[PropertyName]", PropertyName));
                        }
                    }
                }
            }
        }
        #endregion


        #region Método PostInsert
        public int PostInsert(IDto Dto, bool OperationLog = false)
        {
            this.IsValid(Dto, PostAction.Insert);
            return this._Dal.PostInsert(Dto, OperationLog);
        }
        #endregion


        #region Método PostUpdate
        public void PostUpdate(IDto Dto, bool OperationLog = false)
        {
            this.IsValid(Dto, PostAction.Update);
            this._Dal.PostUpdate(Dto, OperationLog);
        }
        #endregion


        #region Método PostDelete
        public void PostDelete(IDto Dto, bool OperationLog = false)
        {
            this.IsValid(Dto, PostAction.Delete);
            this._Dal.PostDelete(Dto, OperationLog);
        }
        #endregion


        #region Método SelectForList
        public lst SelectForList<lst, dto>(SqlUtil SqlUtil = null, int LoadLevel = 0)
             where lst : class, IList<dto>, new()
             where dto : IDto, new()
        {
            return this._Dal.SelectForList<lst, dto>(SqlUtil, LoadLevel);
        }
        #endregion


        #region Método SelectById
        public IDto SelectById(int KeyValue, int LoadLevel = 0)
        {
            return this._Dal.SelectById(KeyValue, LoadLevel);
        }
        #endregion


        #region Método SelectOne
        public IDto SelectOne(SqlUtil SqlUtil, int LoadLevel = 0)
        {
            return this._Dal.SelectOne(SqlUtil, LoadLevel);
        }
        #endregion


        #region Método Select
        public DataTable Select(SqlUtil SqlUtil = null, int LoadLevel = 0)
        {
            return this._Dal.Select(SqlUtil, LoadLevel);
        }
        #endregion


        #region Método ToDto
        public IDto ToDto(DataRow Row)
        {
            return this._Dal.ToDto(Row);
        }
        #endregion

        public abstract IDal NewDal(ConnectionHelper _ConnHelper = null);
    }
}
