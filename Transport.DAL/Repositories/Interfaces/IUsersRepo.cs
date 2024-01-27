using Transport.DAL.Entities;

namespace Transport.DAL.Repositories.Interfaces
{
	public interface IUsersRepo
	{
		Task<List<UserEntity>> GetAllAsync();
		UserEntity? GetById(Guid id);
		void Add(UserEntity user);
		void Update(UserEntity user);
	}
}
