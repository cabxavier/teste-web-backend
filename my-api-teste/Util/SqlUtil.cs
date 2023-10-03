using System.Collections.Generic;

namespace Util
{
    public class SqlUtil
    {
        public List<string> Fields { get; set; }

        public SqlWhere Where { get; set; }

        public SqlSort Sort { get; set; }

        public SqlUtil()
        {
            Fields = new List<string>();
            Where = new SqlWhere();
            Sort = new SqlSort();
        }

        #region Método WhereSimpleBuilder
        public void WhereSimpleBuilder(string _Field, SqlWhereCondition _WhereCondition, string _Value)
        {
            this.Where = new SqlWhere();

            SqlWhereItem _WhereItem = new SqlWhereItem();

            _WhereItem.Field = _Field;
            _WhereItem.Condition = _WhereCondition;
            _WhereItem.Value1 = _Value;

            this.Where.Add(_WhereItem);
        }
        #endregion


        #region Método SortSimpleBuilder
        public void SortSimpleBuilder(string _Field, SqlSortType _SortType)
        {
            this.Sort = new SqlSort();

            SqlSortItem _SortItem = new SqlSortItem();
            _SortItem.Field = _Field;
            _SortItem.SortType = _SortType;

            this.Sort.Add(_SortItem);
        }
        #endregion
    }
}
