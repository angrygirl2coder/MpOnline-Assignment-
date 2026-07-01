using System;
using System.Collections.Generic;

namespace ShoppingExperience
{
    // ========== 1. SINGLE INHERITANCE ==========
    public class OrderBase
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = "Pending";

        public virtual void DisplayOrderInfo()
        {
            Console.WriteLine($"Order ID: {OrderId}, Date: {OrderDate}, Status: {Status}");
        }
    }

    // Single inheritance: Order inherits from OrderBase
    public class Order : OrderBase
    {
        public List<string> Items { get; set; } = new List<string>();
        public decimal TotalAmount { get; set; }

        public override void DisplayOrderInfo()
        {
            base.DisplayOrderInfo();
            Console.WriteLine($"Items: {string.Join(", ", Items)}");
            Console.WriteLine($"Total Amount: ₹{TotalAmount:N2}");  // Indian Rupees
        }
    }

    // ========== 2. MULTILEVEL INHERITANCE ==========
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Person(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public virtual void Introduce()
        {
            Console.WriteLine($"Person: {Name} (ID: {Id})");
        }
    }

    public class User : Person
    {
        public string Email { get; set; }

        public User(int id, string name, string email) : base(id, name)
        {
            Email = email;
        }

        public override void Introduce()
        {
            base.Introduce();
            Console.WriteLine($"Email: {Email}");
        }
    }

    // ========== 3. INTERFACES FOR MULTIPLE INHERITANCE EFFECT ==========
    public interface IOrderPlacer
    {
        void PlaceOrder();
    }

    public interface IPaymentProcessor
    {
        void MakePayment();
    }

    public interface IDelivery
    {
        void UpdateDeliveryStatus(string status);
    }

    // ========== 4. HIERARCHICAL INHERITANCE ==========
    public class Customer : User, IOrderPlacer, IPaymentProcessor
    {
        public List<Order> Orders { get; set; } = new List<Order>();
        private Order currentOrder;

        public Customer(int id, string name, string email) : base(id, name, email) { }

        public void CreateOrder(List<string> items, decimal total)
        {
            currentOrder = new Order
            {
                OrderId = new Random().Next(1000, 9999),
                OrderDate = DateTime.Now,
                Items = items,
                TotalAmount = total
            };
            Console.WriteLine("\n[Order Created Successfully]");
            currentOrder.DisplayOrderInfo();
        }

        public void PlaceOrder()
        {
            if (currentOrder != null)
            {
                currentOrder.Status = "Placed";
                Orders.Add(currentOrder);
                Console.WriteLine($"\nOrder {currentOrder.OrderId} has been PLACED.");
            }
            else
            {
                Console.WriteLine("\nNo order to place. Please create an order first.");
            }
        }

        public void MakePayment()
        {
            if (currentOrder != null && currentOrder.Status == "Placed")
            {
                Console.WriteLine($"\nProcessing payment of ₹{currentOrder.TotalAmount:N2}...");
                Console.WriteLine("Payment successful! Order confirmed.");
                currentOrder.Status = "Paid";
            }
            else
            {
                Console.WriteLine("\nCannot make payment. Either no order or order not placed yet.");
            }
        }
    }

    public class DeliveryAgent : User, IDelivery
    {
        public List<Order> AssignedOrders { get; set; } = new List<Order>();

        public DeliveryAgent(int id, string name, string email) : base(id, name, email) { }

        public void AssignOrder(Order order)
        {
            AssignedOrders.Add(order);
            Console.WriteLine($"\nOrder {order.OrderId} assigned to delivery agent {Name}.");
        }

        public void UpdateDeliveryStatus(string status)
        {
            if (AssignedOrders.Count > 0)
            {
                foreach (var order in AssignedOrders)
                {
                    order.Status = status;
                    Console.WriteLine($"\nDelivery status for Order {order.OrderId} updated to: {status}");
                }
            }
            else
            {
                Console.WriteLine("\nNo assigned orders.");
            }
        }

        public void ShowAssignedOrders()
        {
            if (AssignedOrders.Count == 0)
            {
                Console.WriteLine("\nNo orders assigned.");
                return;
            }
            Console.WriteLine("\n--- Assigned Orders ---");
            foreach (var order in AssignedOrders)
            {
                order.DisplayOrderInfo();
                Console.WriteLine("------------------------");
            }
        }
    }

    // ========== MAIN PROGRAM ==========
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("===== SHOPPING EXPERIENCE SYSTEM =====");
            Console.WriteLine("Demonstrating ALL types of inheritance:");
            Console.WriteLine("1. Single Inheritance  (OrderBase -> Order)");
            Console.WriteLine("2. Multilevel Inheritance (Person -> User -> Customer/DeliveryAgent)");
            Console.WriteLine("3. Hierarchical Inheritance (User -> Customer, User -> DeliveryAgent)");
            Console.WriteLine("4. Multiple Inheritance via Interfaces (Customer implements IOrderPlacer + IPaymentProcessor)\n");

            Console.Write("Are you a (1) Customer or (2) Delivery Agent? Enter 1 or 2: ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                // Customer experience - using Indian Rupees
                Customer customer = new Customer(101, "Aarav Sharma", "aarav@email.com");
                customer.Introduce();

                Console.WriteLine("\n--- Shopping Experience (Customer) ---");
                // Simulate adding items to cart with INR pricing
                List<string> cart = new List<string> { "Laptop", "Wireless Mouse", "Mechanical Keyboard" };
                decimal total = 64999.99m;  // ₹64,999.99
                customer.CreateOrder(cart, total);

                customer.PlaceOrder();
                customer.MakePayment();

                Console.WriteLine("\nFinal Order Status:");
                customer.Orders[0].DisplayOrderInfo();
            }
            else if (choice == "2")
            {
                // Delivery Agent experience
                DeliveryAgent agent = new DeliveryAgent(202, "Priya Verma", "priya@delivery.com");
                agent.Introduce();

                // Simulate an existing order with INR value
                Order sampleOrder = new Order
                {
                    OrderId = 5001,
                    OrderDate = DateTime.Now,
                    Items = new List<string> { "Smartphone", "Charger" },
                    TotalAmount = 28999.00m,  // ₹28,999
                    Status = "Ready for Delivery"
                };

                agent.AssignOrder(sampleOrder);
                agent.ShowAssignedOrders();

                agent.UpdateDeliveryStatus("Out for Delivery");
                agent.UpdateDeliveryStatus("Delivered");

                Console.WriteLine("\nUpdated Order Info:");
                sampleOrder.DisplayOrderInfo();
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }

            // Polymorphism demonstration
            Console.WriteLine("\n\n=== DEMONSTRATING HIERARCHICAL INHERITANCE (User references) ===");
            User user1 = new Customer(301, "Vikram", "vikram@shop.com");
            User user2 = new DeliveryAgent(302, "Neha", "neha@delivery.com");

            user1.Introduce();
            user2.Introduce();

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
