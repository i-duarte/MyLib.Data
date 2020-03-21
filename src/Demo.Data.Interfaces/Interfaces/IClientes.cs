using Demo.Data.Model.Entities;
using System.Collections.Generic;

namespace Demo.Data.Model.Interfaces
{
	public interface IClientes <T>
	{
		IEnumerable<T> SelectAll();
	}
}
