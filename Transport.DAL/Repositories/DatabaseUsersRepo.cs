using Microsoft.EntityFrameworkCore;
using Transport.DAL.Data;
using Transport.DAL.Entities;
using Transport.DAL.Repositories.Interfaces;

namespace Transport.DAL.Repositories
{
	public class DatabaseUsersRepo: IUsersRepo
	{
		private readonly ApplicationDbContext _entityContext;

		public DatabaseUsersRepo(ApplicationDbContext entityContext)
		{
			_entityContext = entityContext;
		}
		public Task<List<UserEntity>> GetAllAsync()
		{
			return _entityContext.Users.ToListAsync();
		}

		public UserEntity? GetById(Guid id)
		{
			return _entityContext.Users
				.FirstOrDefault(t => t.Id == id);
		}

		public void Add(UserEntity user)
		{
			_entityContext.Users.Add(user);
		}

		public void Update(UserEntity updated)
		{
			_entityContext.Entry(updated).State = EntityState.Modified;
		}
	}
}
