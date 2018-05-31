using MyLib.Data.Common;

namespace MyLib.Data.EntityFramework
{
	public class DataAdapter 
	{
		protected IDataBaseAdapter DataBase { get; set; }

		private QueryAdapterBase _query;
		protected QueryAdapterBase Query 
			=> _query 
			?? (_query = DataBase.CreateQueryAdapter());

		public DataAdapter(IDataBaseAdapter dataBase)
		{
			SetDataBase(dataBase);
		}

		public void SetDataBase(IDataBaseAdapter database)
		{
			DataBase = database;
		}
	}
}
