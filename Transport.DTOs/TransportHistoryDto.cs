using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transport.DTOs
{
	public class TransportHistoryRecord
	{
		public Guid DeliveryId { get; set; }

		public Guid DeliveredOrderId { get; set; }
		public string DelieveredFrom { get; set; }
		public string DelieveredTo { get; set; }
		public DateTime? DeliveryDate { get; set; }
	}

	public class TransportHistoryDto
	{
		public List<TransportHistoryRecord> Records { get; set; }
	}
}
