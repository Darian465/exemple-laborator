using Exemple.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exemple.Dto.Events
{
    public record CosPublishedEvent
    {
        public List<CosDto> CosDtos { get; init; }
    }
}
