using System;

namespace AspNetCoreDemo.Exceptions
{
	public class EntityNotFoundException : ApplicationException
	{
		public EntityNotFoundException(string message)
			: base(message)
		{
		}
	}
}
