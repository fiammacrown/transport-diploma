using Transport.DAL;

namespace Transport.WebApi.Services
{
	public class DispatchService
	{
		private readonly UnitOfWork _unitOfWork;

		public DispatchService(UnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public void Start()
		{
			
		}

		public void Update()
		{

		}
	}
}
