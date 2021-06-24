using Confluent.Kafka;
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
			BootstrapServers = "localhost:9092"
		};

		private readonly string topic = "gdpr_topic";

		// GET: api/<GDPRController>
		[HttpGet]
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

		// GET api/<GDPRController>/5
		[HttpGet("{id}")]
		public string Get(int id)
		{
			return "value";
		}

		// POST api/<GDPRController>
		[HttpPost]
		public IActionResult Post([FromQuery] string message)
		{
			return Created(string.Empty, SendToKafka(topic, message));
		}

		// PUT api/<GDPRController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/<GDPRController>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
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