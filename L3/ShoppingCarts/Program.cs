using ShoppingCarts.Domain;
using System;
using System.Collections.Generic;

namespace ShoppingCarts
{
    class Program
    {
        static void Main(string[] args)
        {
            var listOfGrades = ReadListOfShoppingCarts().ToArray();
            PayShoppingCartCommand command = new(listOfGrades);
            PayShoppingCartWorkflow workflow = new PayShoppingCartWorkflow();
            var result = workflow.Execute(command, (productCode) => true);

            result.Match(
                    whenShoppingCartsPaidFailedEvent: @event =>
                    {
                        Console.WriteLine($"Pay failed: {@event.Reason}");
                        return @event;
                    },
                    whenShoppingCartsPaidScucceededEvent: @event =>
                    {
                        Console.WriteLine($"Pay succeeded.");
                        Console.WriteLine(@event.Csv);
                        return @event;
                    }
                );

            Console.WriteLine("Hello World!");
        }
        private static List<EmptyShoppingCart> ReadListOfShoppingCarts()
        {
            List<EmptyShoppingCart> listOfShoppingCarts = new();
            do
            {
                var quantity = ReadValue("Cantitatea produsului comandat: ");
                if (string.IsNullOrEmpty(quantity))
                {
                    break;
                }

                var product_code = ReadValue("Codul produsului: ");
                if (string.IsNullOrEmpty(product_code))
                {
                    break;
                }

                var address = ReadValue("Adresa: ");
                if (string.IsNullOrEmpty(address))
                {
                    break;
                }

                var price = ReadValue("Pretul: ");
                if (string.IsNullOrEmpty(price))
                {
                    break;
                }

                listOfShoppingCarts.Add(new(product_code, quantity, address, price));
            } while (true);
            return listOfShoppingCarts;
        }
        private static string? ReadValue(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }
    }
}
