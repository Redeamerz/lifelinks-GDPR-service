using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GDPR_Service.Models
{
	public class KafkaRequest
	{
		public string topic { get; set; }
		public string message { get; set; }
	}
}
