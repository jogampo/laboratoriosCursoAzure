using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;

namespace ImportantCompanyApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        
        protected string connectionString = "DefaultEndpointsProtocol=https;AccountName=newqueue9999;AccountKey=zOilU4ChXF/Neb5ZdvoR1pE5TI80/cwovME6/JLAIrDFr6mUCcpYWLoEBX5RQYPVpUcgp8ti0yXV+AStgqxdVA==;EndpointSuffix=core.windows.net";
        protected string queueName = "ticketrequests";

        public string WriteMessage { get; private set; } = "";

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnPost(string message)
        {
            GoWriteMessage(message);
        }

       
        public void GoWriteMessage(string message)
        {
            QueueClient queue = new QueueClient(connectionString, queueName);
            queue.SendMessage(message);
            WriteMessage += "Message written: " + message + "!\n";
        }

        public void OnGetGoReadMessage()
        {
            QueueClient queue = new QueueClient(connectionString, queueName);
            QueueMessage message = queue.ReceiveMessage().Value;
            if (message is not null)
            {
                WriteMessage += message.InsertedOn.ToString() + ": " + message.Body.ToString();
                queue.DeleteMessage(message.MessageId, message.PopReceipt);
            }
            else
            {
                WriteMessage += "No messages found.";
            }
        }

    }
}
