using System.Collections.Generic;
using System.Data;
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

		protected ParameterListBase CreateParameterList<T>(string name, T t)
		{
			return QueryAdapter.CreateParameterList(name, t);
		}

		protected TT GetEntity<TT>(
			QueryBase query
		) where TT : Entity, new()
		{
			var dr =
				QueryAdapter
					.GetDataReader(
						query
					);

			TT e = null;
			if (dr.Read())
			{
				e = GetEntity<TT>(dr);
			}
			dr.Close();
			return e;
		}

		protected TT GetEntity<TT>(
			IDataReader dr
		) where TT : Entity, new()
		{
			var e = new TT();
			e.Load(dr);
			e.SetDataBase(DataBase);
			return e;
		}

		protected IEnumerable<TT> GetEnumerable<TT>(
			QueryBase query
		) where TT : Entity, new()
		{
			return
				GetEnumerable<TT>(
					QueryAdapter.GetDataReader(
						query
					)
				);
		}

		protected IEnumerable<TT> GetEnumerable<TT>(
			IDataReader dr
		) where TT : Entity, new()
		{
			while (dr.Read())
			{
				yield return GetEntity<TT>(dr);
			}
			dr.Close();
		}
	}
}
