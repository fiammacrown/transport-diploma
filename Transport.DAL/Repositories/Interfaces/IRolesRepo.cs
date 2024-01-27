using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transport.DAL.Entities;

namespace Transport.DAL.Repositories.Interfaces
{
	public interface IRolesRepo
	{
		Task<List<RoleEntity>> GetAllAsync();
		void Add(RoleEntity role);
	}
}
