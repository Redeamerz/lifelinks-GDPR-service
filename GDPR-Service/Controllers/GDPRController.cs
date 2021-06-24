using Confluent.Kafka;
using GDPR_Service.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GDPR_Service.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class GDPRController : ControllerBase
	{
		private readonly ProducerConfig config = new ProducerConfig
		{
			BootstrapServers = "kafka"
		};

		// GET api/<GDPRController>
		[HttpGet]
		public IActionResult Get([FromBody] KafkaRequest request)
		{
			return Created(string.Empty, SendToKafka(request.topic, request.message));
		}

		// POST api/<GDPRController>
		[HttpPost]
		public IActionResult Post([FromBody] KafkaRequest request)
		{
			return Created(string.Empty, SendToKafka(request.topic, request.message));
		}

		private Object SendToKafka(string topic, string message)
		{
			using (var producer = new ProducerBuilder<Null, string>(config).Build())
			{
				try
				{
					return producer.ProduceAsync(topic, new Message<Null, string> 
					{ 
						Value = message, 
					})
						.GetAwaiter()
						.GetResult();
				}
				catch (Exception e)
				{
					Console.WriteLine($"Oops, something went wrong: {e}");
				}
			}
			return null;
		}
	}
}