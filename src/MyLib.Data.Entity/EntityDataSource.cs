using System.Collections.Generic;
using System.Data;
using MyLib.Data.Common;

namespace MyLib.Data.EntityFramework
{
	public class EntityDataSource<T> 
		: DataAdapter where T : Entity, new()
	{
		public EntityDataSource(IDataBase dataBase) 
			: base(dataBase)
		{
		}

		protected IEnumerable<T> GetEnumerable(
			string sql
			, ParameterListBase param
			)
		{
			return 
				LoadEnumerable(
					Query.GetDataReader(
						sql
						, param
					)
				);
		}

		protected IEnumerable<T> GetEnumerable(
			string sql
			, ParameterListBase param
			, IDbTransaction transaction
			)
		{
			return 
				LoadEnumerable(
					Query.GetDataReader(
						sql
						, param
						, transaction
					)
				);
		}
		protected IEnumerable<TT> GetEnumerable<TT>(
			string sql
			, ParameterListBase param
			) where TT : Entity, new()
		{
			return 
				LoadEnumerable<TT>(
					Query.GetDataReader(
						sql
						, param
					)
				);
		}

		protected IEnumerable<TT> GetEnumerable<TT>(
			string sql
			, ParameterListBase param
			, IDbTransaction transaction
			) where TT : Entity, new()
		{
			return 
				LoadEnumerable<TT>(
					Query.GetDataReader(
						sql
						, param
						, transaction
					)
				);
		}

		protected IEnumerable<TT> GetEnumerable<TT>(
			string sql
			, IDbTransaction transaction
			) where TT : Entity, new()
		{
			return 
				LoadEnumerable<TT>(
					Query.GetDataReader(
						sql
						, transaction
					)
				);
		}

		protected IEnumerable<T> GetEnumerable(
			string sql
		)
		{
			return 
				LoadEnumerable(
					Query.GetDataReader(sql)
				);
		}

		protected IEnumerable<TT> GetEnumerable<TT>(
			string sql
			) where TT : Entity, new()
		{
			return 
				LoadEnumerable<TT>(
					Query.GetDataReader(sql)
				);
		}

		protected T GetEntity(
			string sql,
			ParameterListBase param,
			IDbTransaction transaction
			)
		{
			return 
				LoadEntityWithRead(
					Query.GetDataReader(
						sql
						, param
						, transaction
					)
				);
		}

		protected T GetEntity(
			string sql,
			ParameterListBase param,
			IDbTransaction transaction
			, int timeOut
			)
		{
			return 
				LoadEntityWithRead(
					Query.GetDataReader(
						sql
						, param
						, transaction
						, timeOut
					)
				);
		}

		protected T GetEntity(
			string sql,
			ParameterListBase param
			)
		{
			return 
				LoadEntityWithRead(
					Query.GetDataReader(
						sql
						, param
					)
				);
		}

		protected TT GetEntity<TT>(
			string sql,
			ParameterListBase param
			) where TT : Entity, new()
		{
			return 
				LoadEntityWithRead<TT>(
					Query.GetDataReader(
						sql
						, param
					)
				);
		}

		protected TT GetEntity<TT>(
		   string sql,
		   ParameterListBase param,
		   IDbTransaction transaction
		   ) where TT : Entity, new()
		{
			return 
				LoadEntityWithRead<TT>(
					Query.GetDataReader(
						sql
						, param
						, transaction
					)
				);
		}

		protected T GetEntity(
			string sql
			)
		{
			return 
				LoadEntityWithRead(
					Query.GetDataReader(sql)
				);
		}

		private T LoadEntityWithRead(
			IDataReader dr
		)
		{
			T e = null;
			if(dr.Read())
			{
				e = LoadEntityWithoutRead(dr);
			}
			dr.Close();
			return e;
		}

		private TT LoadEntityWithRead<TT>(
			IDataReader dr
			) where TT : Entity, new()
		{
			TT e = null;
			if(dr.Read())
			{
				e = LoadEntityWithoutRead<TT>(dr);
			}
			dr.Close();
			return e;
		}

		private T LoadEntityWithoutRead(
			IDataReader dr
			)
		{
			var e = new T();
			e.Load(dr);
			e.SetDataBase(DataBase);
			return e;
		}

		private TT LoadEntityWithoutRead<TT>(
			IDataReader dr
			) where TT : Entity, new()
		{
			var e = new TT();
			e.Load(dr);
			e.SetDataBase(DataBase);
			return e;
		}

		private IEnumerable<T> LoadEnumerable(
			IDataReader dr
		)
		{
			while(dr.Read())
			{
				yield return LoadEntityWithoutRead(dr);
			}
			dr.Close();
		}

		private IEnumerable<TT> LoadEnumerable<TT>(
			IDataReader dr
			) where TT : Entity, new()
		{
			while(dr.Read())
			{
				yield return LoadEntityWithoutRead<TT>(dr);
			}
			dr.Close();
		}
	}
}
