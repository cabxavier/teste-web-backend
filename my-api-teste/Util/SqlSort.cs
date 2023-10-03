using System;
using System.Collections.Generic;

namespace Util
{
    public class SqlSort
    {
        private List<SqlSortItem> lst = null;

        public SqlSort()
        {
            lst = new List<SqlSortItem>();
        }


        public void Clear()
        {
            lst.Clear();
        }

        #region Método Add
        public void Add(SqlSortItem Item)
        {
            if (lst.Find(i => i.Field == Item.Field) != null)
            {
                new Exception("Atenção, o campo " + Item.Field + " já possui uma ordem defina dentro deste objeto");
            }

            lst.Add(Item);
        }
        #endregion


        #region Método Text
        public string Text()
        {
            string _Out = "";

            foreach (SqlSortItem item in lst)
            {
                if (_Out.Trim() != "")
                {
                    _Out += " AND ";
                }

                _Out += item.SortType == SqlSortType.Asc ? item.Field +  " ASC" : item.Field + " DESC";
            }

            return _Out;
        }
        #endregion
    }
}
