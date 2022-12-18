using System;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using RabbitMQ.Client;
//TODO: IMPLEMENT
namespace MQRabbitCmdLet
{
    [Cmdlet(VerbsDiagnostic.Test, "ToExchangeCmdlet")]
    [OutputType(typeof(SendQueueResult))]
    public class ToExchangeCmdletCommand : PSCmdlet
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
        [ValidateSet("fanout", "direct", "topic")]
        public string ExchangeType { get; set; } = "fanout";

        // This method gets called once for each cmdlet in the pipeline when the pipeline starts executing
        protected override void BeginProcessing()
        {
            WriteVerbose("Begin!");
        }

        // This method will be called for each input received from the pipeline to this cmdlet; if no input is received, this method is not called
        protected override void ProcessRecord()
        {
            var result = new SendExchangeResult
            {
                ExchangeName = TargetName,
                Message = Message,
                MQHost = MQHost,
                ExType= ExchangeType
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
