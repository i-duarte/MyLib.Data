namespace MyLib.Repository
{
	public interface IRepository<T>
		: IQuery<T>
		where T : IEntity
	{
		void Save(T item);
		void Delete(object id);
	}
}
