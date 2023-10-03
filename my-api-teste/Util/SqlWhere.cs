using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util
{
    public class SqlWhere
    {
        private List<SqlWhereItem> lst = null;

        public SqlWhere()
        {
            lst = new List<SqlWhereItem>();
        }

        public void Clear()
        {
            lst.Clear();
        }

        #region Método Add
        public void Add(SqlWhereItem Item)
        {

            if (lst.Find(i => i.Field == Item.Field) != null)
            {
                new Exception("Atenção, já existe um filtro criado para o campo " + Item.Field);
            }


            if (Item.DataType == SqlDataType.Int)
            {
                if (Item.Condition == SqlWhereCondition.EqualAtTheBeginning)
                {
                    new Exception("Atenção, não é permitido adicionar filtro do tipo {igual no início} para campos do tipo inteiro");
                }

                if (Item.Condition == SqlWhereCondition.EqualEveryWhere)
                {
                    new Exception("Atenção, não é permitido adicionar filtro do tipo {igual em qualquer parte} para campos do tipo inteiro");
                }
            }

            if (Item.DataType == SqlDataType.Decimal)
            {
                if (Item.Condition == SqlWhereCondition.EqualAtTheBeginning)
                {
                    new Exception("Atenção, não é permitido adicionar filtro do tipo {igual no início} para campos do tipo deicmal");
                }

                if (Item.Condition == SqlWhereCondition.EqualEveryWhere)
                {
                    new Exception("Atenção, não é permitido adicionar filtro do tipo {igual em qualquer parte} para campos do tipo decimal");
                }
            }


            if (Item.DataType == SqlDataType.Date)
            {
                if (Item.Condition == SqlWhereCondition.EqualAtTheBeginning)
                {
                    new Exception("Atenção, não é permitido adicionar filtro do tipo {igual no início} para campos do tipo data");
                }

                if (Item.Condition == SqlWhereCondition.EqualEveryWhere)
                {
                    new Exception("Atenção, não é permitido adicionar filtro do tipo {igual em qualquer parte} para campos do tipo data");
                }
            }

            lst.Add(Item);
        }
        #endregion


        #region Método Text
        public string Text()
        {
            string _Out = "";

            foreach (SqlWhereItem item in lst)
            {
                if (_Out.Trim() != "")
                {
                    _Out += " AND ";
                }

                switch (item.Condition)
                {
                    case SqlWhereCondition.Equal:
                        _Out += this.BuildEqualCondition(item);
                        break;

                    case SqlWhereCondition.EqualAtTheBeginning:
                        _Out += this.BuildEqualAtTheBeginningCondition(item);
                        break;

                    case SqlWhereCondition.EqualEveryWhere:
                        _Out += this.BuildEqualEveryWhereCondition(item);
                        break;

                    case SqlWhereCondition.GreaterThan:
                        _Out += this.BuildGreaterThanCondition(item);
                        break;

                    case SqlWhereCondition.GreaterOrEqualThan:
                        _Out += this.BuildGreaterOrEqualThanCondition(item);
                        break;

                    case SqlWhereCondition.LessThan:
                        _Out += this.BuildLessThanCondition(item);
                        break;

                    case SqlWhereCondition.LessOrEqualThan:
                        _Out += this.BuildLessOrEqualThanCondition(item);
                        break;

                    case SqlWhereCondition.Between:
                        _Out += this.BuildBetweenCondition(item);
                        break;

                    case SqlWhereCondition.IsNull:
                        _Out += this.BuildIsNullCondition(item);
                        break;
                }
            }

            return _Out;
        }
        #endregion


        #region Método BuildEqualCondition
        private string BuildEqualCondition(SqlWhereItem item)
        {
            string _Out = "{0} = {1}";

            switch (item.DataType)
            {
                case SqlDataType.Text:
                    _Out = string.Format(_Out, item.Field, this.SqlText(item.Value1));
                    break;

                case SqlDataType.Int:
                    _Out = string.Format(_Out, item.Field, this.SqlInt(item.Value1));
                    break;

                case SqlDataType.Decimal:
                    _Out = string.Format(_Out, item.Field, this.SqlDecimal(item.Value1));
                    break;

                case SqlDataType.Date:
                    _Out = string.Format(_Out, item.Field, this.SqlDate(item.Value1));
                    break;
            }

            return _Out;
        }
        #endregion


        #region Método BuildEqualAtTheBeginningCondition
        private string BuildEqualAtTheBeginningCondition(SqlWhereItem item)
        {
            return string.Format("{0} LIKE {1}", item.Field, this.SqlText(item.Value1, null, "%"));
        }
        #endregion


        #region Método BuildEqualEveryWhereCondition
        private string BuildEqualEveryWhereCondition(SqlWhereItem item)
        {
            return  string.Format("{0} LIKE {1}", item.Field, this.SqlText(item.Value1, "%", "%"));
        }
        #endregion


        #region Método BuildGreaterThanCondition
        private string BuildGreaterThanCondition(SqlWhereItem item)
        {
            string _Out = "{0} > {1}";

            switch (item.DataType)
            {
                case SqlDataType.Text:
                    _Out = string.Format(_Out, item.Field, this.SqlText(item.Value1));
                    break;

                case SqlDataType.Int:
                    _Out = string.Format(_Out, item.Field, this.SqlInt(item.Value1));
                    break;

                case SqlDataType.Decimal:
                    _Out = string.Format(_Out, item.Field, this.SqlDecimal(item.Value1));
                    break;

                case SqlDataType.Date:
                    _Out = string.Format(_Out, item.Field, this.SqlDate(item.Value1));
                    break;
            }

            return _Out;
        }
        #endregion


        #region Método BuildGreaterOrEqualThanCondition
        private string BuildGreaterOrEqualThanCondition(SqlWhereItem item)
        {
            string _Out = "{0} >= {1}";

            switch (item.DataType)
            {
                case SqlDataType.Text:
                    _Out = string.Format(_Out, item.Field, this.SqlText(item.Value1));
                    break;

                case SqlDataType.Int:
                    _Out = string.Format(_Out, item.Field, this.SqlInt(item.Value1));
                    break;

                case SqlDataType.Decimal:
                    _Out = string.Format(_Out, item.Field, this.SqlDecimal(item.Value1));
                    break;

                case SqlDataType.Date:
                    _Out = string.Format(_Out, item.Field, this.SqlDate(item.Value1));
                    break;
            }

            return _Out;
        }
        #endregion


        #region Método BuildLessThanCondition
        private string BuildLessThanCondition(SqlWhereItem item)
        {
            string _Out = "{0} < {1}";

            switch (item.DataType)
            {
                case SqlDataType.Text:
                    _Out = string.Format(_Out, item.Field, this.SqlText(item.Value1));
                    break;

                case SqlDataType.Int:
                    _Out = string.Format(_Out, item.Field, this.SqlInt(item.Value1));
                    break;

                case SqlDataType.Decimal:
                    _Out = string.Format(_Out, item.Field, this.SqlDecimal(item.Value1));
                    break;

                case SqlDataType.Date:
                    _Out = string.Format(_Out, item.Field, this.SqlDate(item.Value1));
                    break;
            }

            return _Out;
        }
        #endregion


        #region Método BuildLessOrEqualThanCondition
        private string BuildLessOrEqualThanCondition(SqlWhereItem item)
        {
            string _Out = "{0} <= {1}";

            switch (item.DataType)
            {
                case SqlDataType.Text:
                    _Out = string.Format(_Out, item.Field, this.SqlText(item.Value1));
                    break;

                case SqlDataType.Int:
                    _Out = string.Format(_Out, item.Field, this.SqlInt(item.Value1));
                    break;

                case SqlDataType.Decimal:
                    _Out = string.Format(_Out, item.Field, this.SqlDecimal(item.Value1));
                    break;

                case SqlDataType.Date:
                    _Out = string.Format(_Out, item.Field, this.SqlDate(item.Value1));
                    break;
            }

            return _Out;
        }
        #endregion


        #region Método BuildBetweenCondition
        private string BuildBetweenCondition(SqlWhereItem item)
        {
            string _Out = "{0} BETWEEN {1} AND {2}";

            switch (item.DataType)
            {
                case SqlDataType.Text:
                    _Out = string.Format(_Out, item.Field, this.SqlText(item.Value1), this.SqlText(item.Value2));
                    break;

                case SqlDataType.Int:
                    _Out = string.Format(_Out, item.Field, this.SqlInt(item.Value1), this.SqlInt(item.Value2));
                    break;

                case SqlDataType.Decimal:
                    _Out = string.Format(_Out, item.Field, this.SqlDecimal(item.Value1), this.SqlDecimal(item.Value2));
                    break;

                case SqlDataType.Date:
                    _Out = string.Format(_Out, item.Field, this.SqlDate(item.Value1), this.SqlDate(item.Value2));
                    break;
            }

            return _Out;
        }
        #endregion


        #region Método BuildIsNullCondition
        private string BuildIsNullCondition(SqlWhereItem item)
        {
            return  string.Format("{0} IS NULL", item.Field);
        }
        #endregion


        #region Método SqlText
        private string SqlText(object Value, string prefix = null, string sufix = null)
        {
            return "'" + (prefix == null ? "" : prefix)
                       + Convert.ToString(Value)
                       + (sufix == null ? "" : sufix)
                       + "'";
        }
        #endregion


        #region Método SqlInt
        private string SqlInt(object Value)
        {
            return Convert.ToString(Value);
        }
        #endregion


        #region Método SqlDecimal
        private string SqlDecimal(object Value)
        {
            return Convert.ToDecimal(Value).ToString("#0.00", new CultureInfo("en-US"));
        }
        #endregion


        #region Método SqlDate
        private string SqlDate(object Value)
        {
            return "'" + Convert.ToDateTime(Value).ToString("yyyy-MM-dd", new CultureInfo("en-US")) + "'";
        }
        #endregion
    }
}
