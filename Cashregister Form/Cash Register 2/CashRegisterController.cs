using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




using System.IO;


namespace CashApp
{
    public class CashRegisterController
    {
        private readonly CashRegisterView view;
        private readonly CashRegisterModel model;

        public CashRegisterController(CashRegisterView view, CashRegisterModel model)
        {
            this.view = view;
            this.model = model;
        }

        public void Run()
        {
            LoadCashiers();
            LoadItems();

            if (model.Cashiers.Count == 0)
            {
                view.WriteLine("No cashiers found. Please create a new cashier.");
                CreateCashier();
            }

            Login();

            while (true)
            {
                view.WriteLine("1. Add cashier");
                view.WriteLine("2. Add item");
                view.WriteLine("3. Start purchase");
                view.WriteLine("4. Exit");
                view.WriteLine("Enter your choice:");

                string choice = view.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddCashier();
                        break;
                    case "2":
                        AddItem();
                        break;
                    case "3":
                        StartPurchase();
                        break;
                    case "4":
                        return;
                    default:
                        view.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private void LoadCashiers()
        {
            try
            {
                if (File.Exists("cashiers.txt"))
                {
                    string[] lines = File.ReadAllLines("cashiers.txt");
                    foreach (string line in lines)
                    {
                        string[] parts = line.Split(',');
                        if (parts.Length == 2)
                        {
                    //        Cashier cashier = new Cashier { Name = parts[0], Password = parts[1] };
                        //    model.Cashiers.Add(cashier);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                view.WriteLine($"Error loading cashiers: {e.Message}");
            }
        }

        private void SaveCashiers()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter("cashiers.txt"))
                {
                    foreach (Cashier cashier in model.Cashiers)
                    {
                        writer.WriteLine($"{cashier.Name},{cashier.Password}");
                    }
                }
            }
            catch (Exception e)
            {
                view.WriteLine($"Error saving cashiers: {e.Message}");
            }
        }

        private void CreateCashier()
        {
            view.WriteLine("Creating a new cashier:");
            view.WriteLine("Enter name:");
            string name = view.ReadLine();

            view.WriteLine("Enter password:");
            string password = view.ReadLine();

       //     Cashier cashier = new Cashier { Name = name, Password = password };
       //     model.Cashiers.Add(cashier);
            SaveCashiers();
            view.WriteLine("Cashier created successfully!");
        }

        private void Login()
        {
            while (true)
            {
                view.WriteLine("Enter cashier name:");
                string name = view.ReadLine();

                view.WriteLine("Enter password:");
                string password = view.ReadLine();

                Cashier cashier = model.Cashiers.FirstOrDefault(c => c.Name == name && c.Password == password);
                if (cashier != null)
                {
                    view.WriteLine($"Welcome, {cashier.Name}!");
                    break;
                }
                else
                {
                    view.WriteLine("Invalid name or password. Please try again.");
                }
            }
        }

        private void LoadItems()
        {
            try
            {
                if (File.Exists("items.txt"))
                {
                    string[] lines = File.ReadAllLines("items.txt");
                    foreach (string line in lines)
                    {
                        string[] parts = line.Split(',');
                        if (parts.Length == 3)
                        {
                            string name = parts[0];
                            string description = parts[1];
                            decimal price = decimal.Parse(parts[2]);

                            Item item = new Item { Name = name, Description = description, Price = price };
                            model.Items.Add(item);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                view.WriteLine($"Error loading items: {e.Message}");
            }
        }

        private void SaveItems()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter("items.txt"))
                {
                    foreach (Item item in model.Items)
                    {
                        writer.WriteLine($"{item.Name},{item.Description},{item.Price}");
                    }
                }
            }
            catch (Exception e)
            {
                view.WriteLine($"Error saving items: {e.Message}");
            }
        }

        private void AddItem()
        {
            view.WriteLine("Creating a new item:");
            view.WriteLine("Enter name:");
            string name = view.ReadLine();

            view.WriteLine("Enter description:");
            string description = view.ReadLine();

            view.WriteLine("Enter price:");
            decimal price = decimal.Parse(view.ReadLine());

            Item item = new Item { Name = name, Description = description, Price = price };
            model.Items.Add(item);
            SaveItems();
            view.WriteLine("Item created successfully!");
        }

        private void DisplayPurchaseItems(List<PurchaseItem> purchaseItems)
        {
            view.WriteLine("Purchase Items:");
            foreach (PurchaseItem purchaseItem in purchaseItems)
            {
                view.WriteLine($"{purchaseItem.Item.Name} - Quantity: {purchaseItem.Quantity}, Total Price: {purchaseItem.TotalPrice:C}");
            }
            view.WriteLine("-----------------------------");
        }

        private void StartPurchase()
        {
            if (model.Items.Count == 0)
            {
                view.WriteLine("No items available. Please add items first.");
                return;
            }

            List<Item> items = model.Items;

            view.WriteLine("Available items:");
            for (int i = 0; i < items.Count; i++)
            {
                view.WriteLine($"{i + 1}. {items[i].Name} - {items[i].Price:C}");
            }

            List<PurchaseItem> purchaseItems = new List<PurchaseItem>();

            while (true)
            {
                view.WriteLine("Enter item number (0 to finish purchase):");
                int itemNumber = int.Parse(view.ReadLine()) - 1;

                if (itemNumber == -1)
                {
                    DisplayPurchaseItems(purchaseItems);
                    view.WriteLine("Confirm purchase (y/n)?");
                    string confirm = view.ReadLine();

                    if (confirm.ToLower() == "y")
                    {
                        SavePurchase(purchaseItems);
                        view.WriteLine("Purchase completed!");
                        return;
                    }
                    else
                    {
                        view.WriteLine("Purchase canceled.");
                        return;
                    }
                }

                if (itemNumber >= 0 && itemNumber < items.Count)
                {
                    view.WriteLine("Enter quantity:");
                    int quantity = int.Parse(view.ReadLine());

                    view.WriteLine("Enter discount percentage (0-100):");
                    int discountPercentage = int.Parse(view.ReadLine());

                    Item item = items[itemNumber];
                    decimal totalPrice = item.Price * quantity * (1 - (discountPercentage / 100m));

                    PurchaseItem existingPurchaseItem = purchaseItems.FirstOrDefault(pi => pi.Item.Name == item.Name);
                    if (existingPurchaseItem != null)
                    {
                        existingPurchaseItem.Quantity += quantity;
                        existingPurchaseItem.TotalPrice += totalPrice;
                    }
                    else
                    {
                        PurchaseItem purchaseItem = new PurchaseItem(item, quantity, totalPrice);
                        purchaseItems.Add(purchaseItem);
                    }

                    view.WriteLine("Item added to the purchase.");
                }
                else
                {
                    view.WriteLine("Invalid item number. Please try again.");
                }
            }
        }

        private void SavePurchase(List<PurchaseItem> purchaseItems)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter("purchase.txt"))
                {
                    foreach (PurchaseItem purchaseItem in purchaseItems)
                    {
                        writer.WriteLine($"{purchaseItem.Item.Name},{purchaseItem.Quantity},{purchaseItem.TotalPrice}");
                    }
                }
            }
            catch (Exception e)
            {
                view.WriteLine($"Error saving purchase: {e.Message}");
            }
        }

        private void AddCashier()
        {
            view.WriteLine("Creating a new cashier:");
            view.WriteLine("Enter name:");
            string name = view.ReadLine();

            view.WriteLine("Enter password:");
            string password = view.ReadLine();

           // Cashier cashier = new Cashier { Name = name, Password = password };
          //  model.Cashiers.Add(cashier);
            SaveCashiers();
            view.WriteLine("Cashier created successfully!");
        }
    }
}
