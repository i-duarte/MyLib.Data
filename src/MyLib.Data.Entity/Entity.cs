using System;
using System.Collections.Generic;
using System.Data;
using MyLib.Data.Common;

namespace MyLib.Data.EntityFramework
{
	public class Entity : DataAdapter
	{
		public Entity(IDataBase dataBase) 
			: base(dataBase)
		{
		}

		public Entity() : base(null)
		{
		}

		public void Load(IDataReader dr)
		{
			throw new System.NotImplementedException();
		}

		public List<string> GetFieldNameList()
		{
			throw new NotImplementedException();
		}

		public List<string> GetFieldValueList()
		{
			throw new NotImplementedException();
		}

		public List<object> GetListFields()
		{
			throw new NotImplementedException();
		}
	}
}
