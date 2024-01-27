namespace Transport.DAL.Entities;


public class UserEntity
{
	public Guid Id { get; set; }
	public string Username { get; set; }
	public string Password { get; set; }
	public ICollection<RoleEntity> Roles { get; set; }

	public UserEntity()
	{
	}

	public UserEntity(string username, string password)
	{
		Id = Guid.NewGuid();
		Username = username;
		Password = password;
	}


	public void AssignRole(RoleEntity role)
	{
		Roles.Add(role);
	}
}
