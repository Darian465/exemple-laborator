using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Exemple.Domain.Models;


namespace Example.API.Controllers
{
    public class InputOrderID
    {
        [Required]
        public string OrderID { get; set; }

    }
}