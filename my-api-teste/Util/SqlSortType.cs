namespace Util
{
    public enum SqlSortType
    {
        Asc,
        Desc
    }

    public class SqlSortItem
    {
        public string Field { get; set; }

        public SqlSortType SortType { get; set; }
    }
}
