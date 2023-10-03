using Enum;
using Interface;

namespace Args
{
    public class PostArgs
    {
        public string StoredProcedure { get; set; }
        public bool OperationLog { get; set; }
        public IDto Dto { get; set; }
        public PostAction PostAction { get; set; }

        public PostArgs()
        {
            this.Clear();
        }

        private void Clear()
        {
            this.StoredProcedure = "";
            this.OperationLog = false;
            this.Dto = null;
            this.PostAction = PostAction.Insert;
        }
    }
}
