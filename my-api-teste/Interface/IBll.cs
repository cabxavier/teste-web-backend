namespace Interface
{
    public interface IBll
    {
        void PostInsert(IDto Dto, bool OperationLog = false);
        void PostUpdate(IDto Dto, bool OperationLog = false);
        void PostDelete(IDto Dto, bool OperationLog = false);
        IDto SelectById(int KeyValue, int LoadLevel = 0);
    }
}
