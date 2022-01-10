using Exemple.Domain.Models;
using Exemple.Events;
using LanguageExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Exemple.Domain.Models.Carucior;

namespace Exemple.Domain.Repositories
{
    public interface ICosRepository
    {
        TryAsync<List<OrderView>> TryGetExistingCos();
        TryAsync<Unit> TrySaveCos(IEventSender sender,PublishedCarucior paidCarucior);
        public TryAsync<string> GetInvoice(string OrderID);
    }
}
