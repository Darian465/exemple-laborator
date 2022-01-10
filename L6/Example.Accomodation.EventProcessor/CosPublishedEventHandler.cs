using Exemple.Data;
using Exemple.Data.Models;
using Exemple.Dto.Events;
using Exemple.Events;
using Exemple.Events.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Exemple.Accomodation.EventProcessor
{
    public class CosPublishedEventHandler : AbstractEventHandler<int>
    {
        public override string[] EventTypes => new string[] { typeof(int).Name };

        protected override Task<EventProcessingResult> OnHandleAsync(int eventData)
        {
            Console.WriteLine("OrderID:  "+eventData.ToString());
            return Task.FromResult(EventProcessingResult.Completed);
        }
    }
}
