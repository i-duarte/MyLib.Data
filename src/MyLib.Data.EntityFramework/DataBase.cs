using MyLib.Data.Common;

namespace MyLib.Data.EntityFramework
{
	public class DataBase
	{
		protected IDataBaseAdapter DataBaseAdapter { get; set; }

		public DataBase(IDataBaseAdapter dataBaseAdapter)
		{
			DataBaseAdapter = dataBaseAdapter;
		}
	}
}
