using MyLib.Data.Common;

namespace MyLib.Data.EntityFramework
{
	public class DataAdapter
	{
		protected IDataBaseAdapter DataBase { get; set; }

		private QueryAdapterBase _queryAdapter;
		protected QueryAdapterBase QueryAdapter
			=> _queryAdapter
			?? (_queryAdapter = DataBase.CreateQueryAdapter());

		public DataAdapter(IDataBaseAdapter dataBase)
		{
			SetDataBase(dataBase);
		}

		public void SetDataBase(IDataBaseAdapter database)
		{
			DataBase = database;
		}

		protected ParameterListBase CreateParameterList()
		{
			return QueryAdapter.CreateParameterList();
		}

		protected ParameterListBase CreateParameterList<T>(T t)
		{
			return QueryAdapter.CreateParameterList(t);
		}
	}
}
