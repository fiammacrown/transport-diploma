using Transport.DTOs;

namespace Abeslamidze_Kursovaya7.Models
{
	public class OrderModel : OrderDto
	{
        public bool CanEdit => Status == "Registered";
		public bool CanDelete => Status == "Registered";

		public static OrderModel Map(OrderDto dto)
		{
			return new OrderModel
			{
				Id = dto.Id,
				Weight = dto.Weight,
				From = dto.From,
				To = dto.To,
				CreatedDate = dto.CreatedDate,
				UpdatedDate = dto.UpdatedDate,
				DeliveredDate = dto.DeliveredDate,
				Status = dto.Status,
			};
		}
	}
}
