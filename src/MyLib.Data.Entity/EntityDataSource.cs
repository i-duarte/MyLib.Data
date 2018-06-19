using System.Collections.Generic;
using System.Data;
using MyLib.Data.Common;

namespace MyLib.Data.EntityFramework
{
	public class EntityDataSource<T> 
		: DataAdapter where T : Entity, new()
	{
		public EntityDataSource(
			IDataBaseAdapter dataBase
			) 
			: base(dataBase)
		{
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

		private TT GetEntity<TT>(
			IDataReader dr
			) where TT : Entity, new()
		{
			var e = new TT();
			e.Load(dr);
			e.SetDataBase(DataBase);
			return e;
		}

		protected IEnumerable<T> GetEnumerable(
			QueryBase query
			)
		{
			return
				GetEnumerable<T>(
					QueryAdapter.GetDataReader(
						query
					)
				);
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

		private IEnumerable<TT> GetEnumerable<TT>(
			IDataReader dr
			) where TT : Entity, new()
		{
			while(dr.Read())
			{
				yield return GetEntity<TT>(dr);
			}
			dr.Close();
		}
	}
}
