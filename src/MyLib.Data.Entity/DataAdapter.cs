using MyLib.Data.Common;

namespace MyLib.Data.EntityFramework
{
	public class DataAdapter 
	{
		protected IDataBase DataBase { get; set; }

		private IQuery _query;
		protected IQuery Query 
			=> _query 
			?? (_query = DataBase.GetNewQuery());

		public DataAdapter(IDataBase dataBase)
		{
			SetDataBase(dataBase);
		}

		public void SetDataBase(IDataBase database)
		{
			DataBase = database;
		}
	}
}
