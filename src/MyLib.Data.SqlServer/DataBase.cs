using MyLib.Data.Common;

namespace MyLib.Data.SqlServer
{
	public class DataBase : IDataBase 
	{
		protected ConnectionFactory ConnectionFactory { get; set; }

		public DataBase(ConnectionFactory connectionFactory)
		{
			ConnectionFactory = connectionFactory;
		}

		public IQuery GetQuery()
		{
			return new Query(ConnectionFactory);
		}
	}
}
