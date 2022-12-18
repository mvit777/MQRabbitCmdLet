using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using RabbitMQ.Client;

namespace MQRabbitCmdLet
{
    [Cmdlet(VerbsDiagnostic.Test, "ToQueueCmdlet")]
    [OutputType(typeof(SendQueueResult))]
    public class ToQueueCmdletCommand : PSCmdlet
    {
        [Parameter(
            Mandatory = true,
            Position = 0,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true)]
        public string TargetName { get; set; }

        [Parameter(
            Position = 1,
            ValueFromPipelineByPropertyName = true)]
        //[ValidateSet("Cat", "Dog", "Horse")]
        public string Message { get; set; } = "Hello World";

        [Parameter(
            Position = 2,
            ValueFromPipelineByPropertyName = true)]
        public string MQHost { get; set; } = "localhost";
        [Parameter(
            Position = 3,
            ValueFromPipelineByPropertyName = true)]
        public bool Durable { get; set; } = false;
        [Parameter(
            Position = 4,
            ValueFromPipelineByPropertyName = true)]
        public bool Exclusive { get; set; } = false;
        [Parameter(
            Position = 5,
            ValueFromPipelineByPropertyName = true)]
        public bool AutoDelete { get; set; } = false;
        [Parameter(
            Position = 6,
            ValueFromPipelineByPropertyName = true)]
        public Dictionary<string, object> Arguments { get; set; } = null;
        [Parameter(
            Position = 7,
            ValueFromPipelineByPropertyName = true)]
        public Dictionary<string, object> BasicProperties { get; set; } = null;

        // This method gets called once for each cmdlet in the pipeline when the pipeline starts executing
        protected override void BeginProcessing()
        {
            WriteVerbose("Begin!");
        }

        // This method will be called for each input received from the pipeline to this cmdlet; if no input is received, this method is not called
        protected override void ProcessRecord()
        {
            var result = new SendQueueResult
            {
                QueueName = TargetName,
                Message = Message,
                MQHost = MQHost,
                Durable = Durable
            };

            WriteObject(result.SendMessage());
        }

        // This method will be called once at the end of pipeline execution; if no input is received, this method is not called
        protected override void EndProcessing()
        {
            WriteVerbose("End!");
        }
    }

    
}
