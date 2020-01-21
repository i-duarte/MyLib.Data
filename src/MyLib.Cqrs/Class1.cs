using System;

namespace MyLib.Cqrs
{
	public interface ICommnad:ICommandHandler<object>
	{
		Guid Id { get; }
    }

	public interface ICommandHandler<T>
	{
	}
}
