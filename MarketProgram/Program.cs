using System;
using System.Collections.Generic;
using System.Text;

namespace MarketProgram
{
    class Program
    {
        
        static Dictionary<string, string> users = new Dictionary<string, string>()
        {
            { "admin", "admin" },
            { "seller1", "seller1" }
        };

        static Dictionary<string, string> roles = new Dictionary<string, string>()
        {
            { "admin", "Admin" },
            { "seller1", "Seller" }
        };

        static Dictionary<string, (double Price, double SalePrice, int Stock)> products = new Dictionary<string, (double, double, int)>()
        {
            {"Alma", (1.0, 1.5, 10)},
            {"Banana", (1.5, 2.0, 10)},
            {"Süd", (0.8, 1.2, 10)},
            {"Çörək", (0.5, 0.8, 10)},
            {"Yumurta", (0.05, 0.1, 100)}
        };

        static List<(string ProductName, int Quantity, double TotalPrice, DateTime SaleTime)> salesHistory = new List<(string, int, double, DateTime)>();

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            while (true)
            {
                string username;
                string role = Login(out username);

                if (role == "Admin")
                {
                    AdminMenu(username);
                }
                else if (role == "Seller")
                {
                    SellerMenu(username);
                }
                else
                {
                    Console.WriteLine("Login uğursuz oldu. Yenidən cəhd edin.");
                }
            }
        }

        static string Login(out string username)
        {
            Console.WriteLine("\nMarket proqramına xoş gəlmisiniz!");
            Console.Write("İstifadəçi adı: ");
            username = Console.ReadLine();
            Console.Write("Şifrə: ");
            string password = Console.ReadLine();

            if (users.ContainsKey(username) && users[username] == password)
            {
                return roles[username];
            }
            else
            {
                Console.WriteLine("Yanlış istifadəçi adı və ya şifrə.");
                return string.Empty;
            }
        }


        static void AdminMenu(string username)
        {
            bool continueShopping = true;

            while (continueShopping)
            {
                Console.WriteLine("\nAdmin Panelinə xoş gəlmisiniz!");
                Console.WriteLine("1. Məhsullara bax");
                Console.WriteLine("2. Məhsul əlavə et");
                Console.WriteLine("3. Məhsulu çıxar");
                Console.WriteLine("4. Məhsul yenilə");
                Console.WriteLine("5. Yeni satıcı əlavə et");
                Console.WriteLine("6. Məhsul sat");
                Console.WriteLine("7. Satış tarixçəsinə bax");
                Console.WriteLine("8. Çıxış");
                Console.Write("Seçiminizi edin: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ViewProducts();
                        break;
                    case "2":
                        AddProduct();
                        break;
                    case "3":
                        RemoveProduct();
                        break;
                    case "4":
                        RestockProduct();
                        break;
                    case "5":
                        AddSeller();
                        continueShopping = false;
                        break;
                    case "6":
                        SellProduct(username);
                        break;
                    case "7":
                        ViewSalesHistory();
                        break;
                    case "8":
                        continueShopping = false;
                        Console.WriteLine("Proqramdan çıxılır...");
                        break;
                    default:
                        Console.WriteLine("Yanlış seçim, yenidən cəhd edin.");
                        break;
                }
            }
        }

        static void ViewSalesHistory()
        {
            Console.WriteLine("\nSatış Tarixçəsi:");
            foreach (var sale in salesHistory)
            {
                Console.WriteLine($"Məhsul: {sale.ProductName}, Miqdar: {sale.Quantity}, Cəmi məbləğ: {sale.TotalPrice} AZN, Tarix: {sale.SaleTime}");
            }
        }

        static void SellerMenu(string username)
        {
            bool continueShopping = true;

            while (continueShopping)
            {
                Console.WriteLine("\nSatıcı Panelinə xoş gəlmisiniz!");
                Console.WriteLine("1. Məhsul sat");
                Console.WriteLine("2. Çıxış");
                Console.Write("Seçiminizi edin: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        SellProduct(username);
                        break;
                    case "2":
                        continueShopping = false;
                        Console.WriteLine("Proqramdan çıxılır...");
                        break;
                    default:
                        Console.WriteLine("Yanlış seçim, yenidən cəhd edin.");
                        break;
                }
            }
        }

        static void ViewProducts()
        {
            Console.WriteLine("\nMəhsullar:");
            foreach (var product in products)
            {
                Console.WriteLine($"{product.Key}: Alış qiyməti {product.Value.Price} AZN, Satış qiyməti {product.Value.SalePrice} AZN (Stock: {product.Value.Stock})");
            }
        }

        static void AddProduct()
        {
            Console.Write("\nYeni məhsulun adını yazın: ");
            string name = Console.ReadLine();
            Console.Write("Qiymətini yazın: ");
            if (double.TryParse(Console.ReadLine(), out double price))
            {
                Console.Write("Satış qiymətini yazın: ");
                if (double.TryParse(Console.ReadLine(), out double salePrice))
                {
                    Console.Write("Stok miqdarını yazın: ");
                    if (int.TryParse(Console.ReadLine(), out int stock))
                    {
                        products[name] = (price, salePrice, stock);
                        Console.WriteLine($"{name} məhsulu əlavə edildi.");
                    }
                    else
                    {
                        Console.WriteLine("Yanlış stok miqdarı.");
                    }
                }
                else
                {
                    Console.WriteLine("Yanlış satış qiyməti.");
                }
            }
            else
            {
                Console.WriteLine("Yanlış qiymət.");
            }
        }

        static void RemoveProduct()
        {
            Console.Write("\nÇıxarmaq istədiyiniz məhsulun adını yazın: ");
            string name = Console.ReadLine();
            if (products.ContainsKey(name))
            {
                products.Remove(name);
                Console.WriteLine($"{name} məhsulu çıxarıldı.");
            }
            else
            {
                Console.WriteLine("Bu məhsul tapılmadı.");
            }
        }

        static void RestockProduct()
        {
            Console.Write("\nYeniləmək istədiyiniz məhsulun adını yazın: ");
            string name = Console.ReadLine();
            Console.Write("Yeniləmək istədiyiniz miqdarı yazın: ");
            if (int.TryParse(Console.ReadLine(), out int quantity) && products.ContainsKey(name))
            {
                products[name] = (products[name].Price, products[name].SalePrice, products[name].Stock + quantity);
                Console.WriteLine($"{quantity} ədəd {name} əlavə edildi. Yeni stok: {products[name].Stock}");
            }
            else
            {
                Console.WriteLine("Yanlış miqdar və ya məhsul adı.");
            }
        }

        static void AddSeller()
        {
            Console.Write("\nYeni satıcının istifadəçi adını yazın: ");
            string newUsername = Console.ReadLine();
            Console.Write("Yeni satıcının şifrəsini yazın: ");
            string newPassword = Console.ReadLine();
            if (!users.ContainsKey(newUsername))
            {
                users[newUsername] = newPassword;
                roles[newUsername] = "Seller";
                Console.WriteLine($"Yeni satıcı {newUsername} əlavə edildi.");
            }
            else
            {
                Console.WriteLine("Bu istifadəçi adı artıq mövcuddur.");
            }
        }

        static void SellProduct(string username)
        {
            Console.Write("\nSatmaq istədiyiniz məhsulun adını yazın: ");
            string name = Console.ReadLine();
            Console.Write("Satmaq istədiyiniz miqdarı yazın: ");
            if (int.TryParse(Console.ReadLine(), out int quantity) && products.ContainsKey(name))
            {
                if (products[name].Stock >= quantity)
                {
                    double totalPrice = quantity * products[name].SalePrice;
                    products[name] = (products[name].Price, products[name].SalePrice, products[name].Stock - quantity);
                    Console.WriteLine($"{quantity} ədəd {name} satıldı.");
                    RecordSale(name, quantity, totalPrice);
                    PrintReceipt(name, quantity, totalPrice);
                }
                else
                {
                    Console.WriteLine("Kifayət qədər stok yoxdur.");
                }
            }
            else
            {
                Console.WriteLine("Yanlış miqdar və ya məhsul adı.");
            }
        }

        static void RecordSale(string productName, int quantity, double totalPrice)
        {
            salesHistory.Add((productName, quantity, totalPrice, DateTime.Now));
        }

        static void PrintReceipt(string productName, int quantity, double totalPrice)
        {
            Console.WriteLine("\n----- Qəbz -----");
            Console.WriteLine($"Məhsul: {productName}");
            Console.WriteLine($"Miqdar: {quantity}");
            Console.WriteLine($"Cəmi məbləğ: {totalPrice} AZN");
            Console.WriteLine($"Satış tarixi: {DateTime.Now}");
        }
    }
}
