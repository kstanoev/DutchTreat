using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.Services
{
	public class MessageService : IMessageService
	{
		private readonly ILogger<MessageService> messageService;

		public MessageService(ILogger<MessageService> messageService)
		{
			this.messageService = messageService;
		}
		public void SendMessage(string message)
		{
			this.messageService.LogInformation(message);
		}
	}
}
