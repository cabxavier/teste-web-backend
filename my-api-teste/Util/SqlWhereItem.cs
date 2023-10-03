namespace Util
{
    public enum SqlDataType
    {
        Text,
        Int,
        Decimal,
        Date
    }

    public enum SqlWhereCondition
    {
        Equal,
        EqualAtTheBeginning,
        EqualEveryWhere,
        GreaterThan,
        GreaterOrEqualThan,
        LessThan,
        LessOrEqualThan,
        Between,
        IsNull
    }

    public class SqlWhereItem
    {
        public string Field { get; set; }
        public SqlDataType DataType { get; set; }
        public SqlWhereCondition Condition { get; set; }
        public object Value1 { get; set; }
        public object Value2 { get; set; }
    }
}
