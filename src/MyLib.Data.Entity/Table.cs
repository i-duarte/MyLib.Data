using MyLib.Data.Common;

namespace MyLib.Data.EntityFramework
{
	public class Table<T> 
		: EntityDataSource<T> 
			where T : Entity, new()
	{
		public Table(IDataBase dataBase) 
			: base(dataBase)
		{
		}

		public int Insert(T entity)
		{
			var sql = 
				"INSERT INTO(" +
				$"{GetFieldNameList(entity)}" +
				") VALUES(" +
				$"{GetFieldValueList(entity)}" +
				")";

			var param = GetParameters(entity);

			return Query.Execute(sql, param);
		}

		private string GetFieldNameList(T entity)
		{
			throw new System.NotImplementedException();
		}

		private string GetFieldValueList(T entity)
		{
			throw new System.NotImplementedException();
		}

		private IParameterList GetParameters(T entity)
		{
			throw new System.NotImplementedException();
		}
	}
}
