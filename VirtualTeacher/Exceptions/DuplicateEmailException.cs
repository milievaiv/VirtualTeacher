namespace VirtualTeacher.Exceptions
{
    public class DuplicateEmailException : ApplicationException
    {
        public DuplicateEmailException(string message)
            : base(message)
        {
        }
    }
}
