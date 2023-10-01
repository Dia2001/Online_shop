using Microsoft.EntityFrameworkCore;

namespace OnlineShop.Services.Base
{
	public interface IBaseService<T> where T : class
	{
		T Create(T entity);
		List<T> Creates(List<T> entities);
		List<T> FindAll();
		T FindOne(int id);
		T Update(T entity);
		T Delete(int id);
		List<T> Deletes(int[] ids);
	}

	public abstract class BaseService<TEntity, TContext>
			where TEntity : class
			where TContext : DbContext
	{
		private readonly TContext context;

		public BaseService(TContext context)
		{
			this.context = context;
		}

		public TEntity Create(TEntity entity)
		{
			context.Set<TEntity>().Add(entity);
			context.SaveChanges();
			return entity;
		}

		public List<TEntity> Creates(List<TEntity> entities)
		{
			context.Set<TEntity>().AddRange(entities);
			context.SaveChanges();
			return entities;
		}

		public List<TEntity> FindAll()
		{
			return context.Set<TEntity>().ToList();
		}

		public TEntity FindOne(int id)
		{
			return context.Set<TEntity>().Find(id);
		}

		public TEntity Update(TEntity entity)
		{
			context.Entry(entity).State = EntityState.Modified;
			context.SaveChanges();
			return entity;
		}

		public TEntity Delete(int id)
		{
			var entity = context.Set<TEntity>().Find(id);
			if (entity == null)
			{
				return entity;
			}
			context.Set<TEntity>().Remove(entity);
			context.SaveChanges();
			return entity;
		}

		public List<TEntity> Deletes(int[] ids)
		{
			var selectedItems = context.Set<TEntity>()
				.Where(item => ids.Contains((int)item.GetType().GetProperty("Id").GetValue(item))).ToList();
			context.Set<TEntity>().RemoveRange(selectedItems);
			context.SaveChanges();
			return selectedItems;
		}
	}
}
