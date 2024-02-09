namespace ReactExample.Controllers.Data.Exceptions
{
	public class DuplicateEmailException : ApplicationException
	{
		public DuplicateEmailException(string message)
			: base(message)
		{
		}
	}
}
