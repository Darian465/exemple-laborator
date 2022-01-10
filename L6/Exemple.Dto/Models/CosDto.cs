using Exemple.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exemple.Dto.Models
{
    public class CosDto
    {
        public int id { get; set; }
        public string OrderID { get; set; }
        public string ProductID { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public string Address { get; set; }

        public static CosDto ToCosDTO(int orderID, CalculatedPrice products)
        {
            return new CosDto()
            {
                OrderID = orderID.ToString(),
                ProductID = products.productCode.ToString(),
                Quantity = Convert.ToInt32(products.quantity),
                Price = products.price,
                Address = products.address.ToString()
            };
        }
    }
}
