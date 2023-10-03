using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Util;

namespace Interface
{
    public interface IDal
    {
        DataTable Select(SqlUtil SqlUtil = null, int LoadLevel = 0);
        IDto SelectById(int KeyValue, int LoadLevel = 0);
        IDto SelectOne(SqlUtil SqlUtil, int LoadLevel = 0);
        lst SelectForList<lst, dto>(SqlUtil SqlUtil = null, int LoadLevel = 0)
            where lst : class, IList<dto>, new()
            where dto : IDto, new();
        int NextId();
        int NextValue(string FieldName, string TextPartition = null);
        int PostInsert(IDto Dto, bool OperationLog = false);
        void PostUpdate(IDto Dto, bool OperationLog = false);
        void PostDelete(IDto Dto, bool OperationLog = false);
        IDto ToDto(DataRow Row, IDto _Dto = null);
        void LoadObjectProperty(IDto _Dto, int LoadLevel = 0);
        DataTable ExecuteProcedure(string Stmt, List<SqlParameter> _Parameters);
    }
}
