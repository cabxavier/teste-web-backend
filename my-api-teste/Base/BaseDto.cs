namespace Base
{
    public abstract class BaseDto
    {
        public BaseDto Clone()
        {
            return (BaseDto)this.MemberwiseClone();
        }
    }
}
