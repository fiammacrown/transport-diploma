using System.Data;

namespace Transport.DAL.Entities;

public enum Role
{
	Default,
	User,
	Manager,
	Admin
}

public class RoleEntity
{
	public Guid Id { get; set; }
	public Role Name { get; set; }
	public ICollection<UserEntity> Users { get; set; }

	public RoleEntity()
	{
	}

	public RoleEntity(Role name)
	{
		Id = Guid.NewGuid();
		Name = name;
	}

	public void AssignUser(UserEntity user)
	{
		Users.Add(user);
	}
}
