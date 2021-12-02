using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ShoppingCarts.Domain.Models.UnvalidatedCustomerOrder;
using LanguageExt;
using static LanguageExt.Prelude;
using ShoppingCarts.Domain.Models;
using ShoppingCarts;
using ShoppingCarts.Data.Repositories;
using Microsoft.Extensions.Logging;
using ShoppingCarts.Data;
using Microsoft.EntityFrameworkCore;

namespace ShoppingCarts
{
    class Program
    {
        private static List<ValidatedCustomerOrder> listOfValidatedOrders = new();
        private static Option<OrderRegistrationCode> testRegCode;
        private static readonly Random random = new Random();

        private static string ConnectionString = @"C:\Users\daria\Desktop\PSSC\Biris_Darian\L4";
        static async Task Main(string[] args)
        {
            using ILoggerFactory loggerFactory = ConfigureLoggerFactory();
            ILogger<PlacingOrderWorkflow> logger = loggerFactory.CreateLogger<PlacingOrderWorkflow>();

            var listOfOrders = ReadListOfOrders().ToArray();
            listOfValidatedOrders = new List<ValidatedCustomerOrder>();

            var dbContextBuilder = new DbContextOptionsBuilder<OrdersContext>()
                                               .UseSqlServer("DESKTOP-QJCEF2l")
                                               .UseLoggerFactory(loggerFactory);

            OrdersContext context = new(dbContextBuilder.Options);
            OrderHeaderRepository orderHeaderRepository = new(context);
            OrderLineRepository orderLineRepository = new(context);
            ProductRepository productRepository = new(context);

            PlacingOrdersCommand command = new(listOfOrders);
            PlacingOrderWorkflow workflow = new(orderHeaderRepository, productRepository, orderLineRepository, logger);

            var result = await workflow.ExecuteAsync(command);
            result.Match(

                whenPlacingOrderEventFailedEvent: @event =>
                {

                    Console.WriteLine($"Placing the order was failed : {@event.Reason}");
                    return @event;
                },
                whenPlacingOrderEventSuccedeedEvent: @event =>
                {
                    Console.WriteLine("Placing order was succeed...");
                    Console.WriteLine(@event.Csv);
                    Console.WriteLine($"Number Of order : {@event.NumberOfOrder} at Date: {@event.PlacedDate}");
                    return @event;
                }
                );
        }

        private static List<UnvalidatedCustomerOrder> ReadListOfOrders()
        {
            List<UnvalidatedCustomerOrder> listOfOrders = new();
            do
            {
                //read registration number and grade and create a list of greads
                var orderRegistrationCode = ReadValue("Registration Code: ");
                if (string.IsNullOrEmpty(orderRegistrationCode))
                {
                    break;
                }

                var orderDescription = ReadValue("Description of order: ");
                if (string.IsNullOrEmpty(orderDescription))
                {
                    break;
                }

                var orderAmount = ReadValue("Amount of order: ");
                if (string.IsNullOrEmpty(orderAmount))
                {
                    break;
                }
                var orderAddress = ReadValue("Address of delivery : ");
                if (string.IsNullOrEmpty(orderAddress))
                {
                    break;
                }
                var orderPrice = ReadValue("Price of order: ");
                if (string.IsNullOrEmpty(orderPrice))
                {
                    break;
                }

                listOfOrders.Add(new(orderRegistrationCode, orderDescription, orderAmount, orderAddress, orderPrice));
            } while (true);
            return listOfOrders;
        }

        private static string? ReadValue(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }

        private static ILoggerFactory ConfigureLoggerFactory()
        {
            return LoggerFactory.Create(builder =>
                                builder.AddSimpleConsole(options =>
                                {
                                    options.IncludeScopes = true;
                                    options.SingleLine = true;
                                    options.TimestampFormat = "hh:mm:ss ";
                                })
                                .AddProvider(new Microsoft.Extensions.Logging.Debug.DebugLoggerProvider()));
        }

        private static TryAsync<bool> CheckOrderExists(OrderRegistrationCode order)
        {
            Func<Task<bool>> func = async () =>
            {
                bool flag = false;
                foreach (var ord in listOfValidatedOrders)
                {
                    if (ord.OrderRegistrationCode.Value.Equals(order.Value))
                    {
                        flag = true;
                    }
                }

                return flag;
            };
            return TryAsync(func);
        }

        private static TryAsync<bool> CheckOrderByStock(OrderRegistrationCode regCode, OrderAmount order)
        {
            Func<Task<bool>> func = async () =>
            {
                bool flag = false;
                foreach (var ord in listOfValidatedOrders)
                {
                    if (ord.OrderRegistrationCode.Value.Equals(regCode.Value) && ord.OrderAmount.Amount >= order.Amount)
                    {
                        flag = true;
                    }
                }
                return flag;
            };
            return TryAsync(func);
        }


        private static TryAsync<bool> CheckOrderByAddress(OrderAddress order)
        {
            Func<Task<bool>> func = async () =>
            {
                bool flag = false;
                foreach (var ord in listOfValidatedOrders)
                {
                    if (ord.OrderAddress.Address.Equals(order.Address))
                    {
                        flag = true;
                    }
                }
                return flag;
            };

            return TryAsync(func);
        }

        private static TryAsync<float> CheckPriceForOrder(OrderRegistrationCode order)
        {
            Func<Task<float>> func = async () =>
            {
                float price = 0;
                foreach (var ord in listOfValidatedOrders)
                {
                    if (ord.OrderRegistrationCode.Value.Equals(order.Value))
                    {
                        price = ord.OrderPrice.Price;
                    }
                }
                return price;
            };

            return TryAsync(func);
        }

        private static void printThecart(List<ValidatedCustomerOrder> ordersList)
        {
            foreach (var order in ordersList)
            {
                Console.WriteLine(order.toStringOrder());
                Console.WriteLine("-----------------------------------");
            }
        }

        private static void menu()
        {

            Console.WriteLine("1.Print list of orders:");
            Console.WriteLine("2.Check order by its reg code:");
            Console.WriteLine("3.Check address of order:");
            Console.WriteLine("4.Check stock for your order:");
            Console.WriteLine("5.Get price for your order:");
            Console.WriteLine("q - exit");
            Console.WriteLine("Your option is ...");
            Console.WriteLine("-----------------------------------");

        }
    }
}
