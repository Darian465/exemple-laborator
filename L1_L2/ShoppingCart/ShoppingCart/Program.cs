using ShoppingCart.Domain;
using System;
using System.Collections.Generic;
using static ShoppingCart.Domain.Cart;
using static ShoppingCart.CartOperation;


namespace ShoppingCart
{
    class Program
    {
        private static readonly Random random = new Random();
        static void Main(string[] args)
        {
            var listOfProduse = ReadListOfProduse().ToArray();
            PublishCodCommand command = new(listOfProduse);
            PublishCartWorkflow workflow = new PublishCartWorkflow();
            var result = workflow.Execute(command, (codProdus) => true);
            result.Match(
                whenCartPublishFailEvent: @event =>
                {
                    Console.WriteLine($"Failed: {@event.Reason}");
                    return @event;
                },
                whenCartPublishSucceededEvent: @event =>
                {
                    Console.WriteLine($"Success");
                    Console.WriteLine(@event.Csv);
                    return @event;
                }
            );
        }

        private static List<UnvalidatedProdus> ReadListOfProduse()
        {
            List<UnvalidatedProdus> listOfProdus = new();
            do
            {
                var cod = ReadValue("Cod produs: ");
                if (string.IsNullOrEmpty(cod))
                {
                    break;
                }
                var cantitate = ReadValue("Cantitate produs: ");
                if (string.IsNullOrEmpty(cantitate))
                {
                    break;
                }
                var adresa = ReadValue("Adresa produs: ");
                if (string.IsNullOrEmpty(adresa))
                {
                    break;
                }
                var activityCantitate = ReadValue("Pret: ");
                if (string.IsNullOrEmpty(activityCantitate))
                {
                    break;
                }

                listOfProdus.Add(new(cod, cantitate, adresa, activityCantitate));

            } while (true);
            return listOfProdus;
            
        }
        private static string? ReadValue(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }
    }
}
