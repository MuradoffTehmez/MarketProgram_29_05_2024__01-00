using System;
using System.Collections.Generic;
using System.Text;

namespace MarketProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Dictionary<string, (double Price, int Stock)> products = new Dictionary<string, (double, int)>()
            {
                {"Alma", (1.5, 10)},
                {"Banana", (2.0, 10)},
                {"Süd", (1.2, 10)},
                {"Çörək", (0.8, 10)},
                {"Yumurta", (0.1, 100)}
            };

            List<string> cart = new List<string>();
            bool continueShopping = true;

            while (continueShopping)
            {
                Console.WriteLine("\nMarket proqramına xoş gəlmisiniz!");
                Console.WriteLine("1. Məhsullara bax");
                Console.WriteLine("2. Məhsul əlavə et");
                Console.WriteLine("3. Məhsulu çıxar");
                Console.WriteLine("4. Səbətə bax");
                Console.WriteLine("5. Ümumi məbləği yoxla");
                Console.WriteLine("6. Ödəniş et");
                Console.WriteLine("7. Məhsul sat");
                Console.WriteLine("8. Məhsul yenilə");
                Console.WriteLine("9. Çıxış");
                Console.Write("Seçiminizi edin: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ViewProducts(products);
                        break;
                    case "2":
                        AddProduct(products, cart);
                        break;
                    case "3":
                        RemoveProduct(cart);
                        break;
                    case "4":
                        ViewCart(cart);
                        break;
                    case "5":
                        CheckTotal(products, cart);
                        break;
                    case "6":
                        Checkout(products, cart);
                        break;
                    case "7":
                        SellProduct(products);
                        break;
                    case "8":
                        RestockProduct(products);
                        break;
                    case "9":
                        continueShopping = false;
                        Console.WriteLine("Proqramdan çıxılır...");
                        break;
                    default:
                        Console.WriteLine("Yanlış seçim, yenidən cəhd edin.");
                        break;
                }
            }
        }

        static void ViewProducts(Dictionary<string, (double Price, int Stock)> products)
        {
            Console.WriteLine("\nMəhsullar:");
            foreach (var product in products)
            {
                Console.WriteLine($"{product.Key}: {product.Value.Price} AZN (Stock: {product.Value.Stock})");
            }
        }

        static void AddProduct(Dictionary<string, (double Price, int Stock)> products, List<string> cart)
        {
            Console.Write("\nƏlavə etmək istədiyiniz məhsulun adını yazın: ");
            string name = Console.ReadLine();
            if (products.ContainsKey(name) && products[name].Stock > 0)
            {
                cart.Add(name);
                products[name] = (products[name].Price, products[name].Stock - 1);
                Console.WriteLine($"{name} səbətə əlavə edildi.");
            }
            else
            {
                Console.WriteLine("Belə məhsul tapılmadı və ya məhsul stokda yoxdur.");
            }
        }

        static void RemoveProduct(List<string> cart)
        {
            Console.Write("\nÇıxarmaq istədiyiniz məhsulun adını yazın: ");
            string name = Console.ReadLine();
            if (cart.Contains(name))
            {
                cart.Remove(name);
                Console.WriteLine($"{name} səbətdən çıxarıldı.");
            }
            else
            {
                Console.WriteLine("Bu məhsul səbətdə tapılmadı.");
            }
        }

        static void ViewCart(List<string> cart)
        {
            if (cart.Count == 0)
            {
                Console.WriteLine("\nSəbət boşdur.");
            }
            else
            {
                Console.WriteLine("\nSəbət:");
                foreach (var product in cart)
                {
                    Console.WriteLine(product);
                }
            }
        }

        static void CheckTotal(Dictionary<string, (double Price, int Stock)> products, List<string> cart)
        {
            double total = 0;
            foreach (var product in cart)
            {
                total += products[product].Price;
            }
            Console.WriteLine($"\nÜmumi məbləğ: {total} AZN");
        }

        static void Checkout(Dictionary<string, (double Price, int Stock)> products, List<string> cart)
        {
            double total = 0;
            foreach (var product in cart)
            {
                total += products[product].Price;
            }
            Console.WriteLine($"\nÖdəniş tamamlandı. Ümumi məbləğ: {total} AZN");
            cart.Clear();
            Console.WriteLine("Səbət boşaldıldı.");
        }

        static void SellProduct(Dictionary<string, (double Price, int Stock)> products)
        {
            Console.Write("\nSatmaq istədiyiniz məhsulun adını yazın: ");
            string name = Console.ReadLine();
            Console.Write("Satmaq istədiyiniz miqdarı yazın: ");
            if (int.TryParse(Console.ReadLine(), out int quantity) && products.ContainsKey(name))
            {
                if (products[name].Stock >= quantity)
                {
                    products[name] = (products[name].Price, products[name].Stock - quantity);
                    Console.WriteLine($"{quantity} ədəd {name} satıldı.");
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

        static void RestockProduct(Dictionary<string, (double Price, int Stock)> products)
        {
            Console.Write("\nYeniləmək istədiyiniz məhsulun adını yazın: ");
            string name = Console.ReadLine();
            Console.Write("Yeniləmək istədiyiniz miqdarı yazın: ");
            if (int.TryParse(Console.ReadLine(), out int quantity) && products.ContainsKey(name))
            {
                products[name] = (products[name].Price, products[name].Stock + quantity);
                Console.WriteLine($"{quantity} ədəd {name} əlavə edildi. Yeni stok: {products[name].Stock}");
            }
            else
            {
                Console.WriteLine("Yanlış miqdar və ya məhsul adı.");
            }
        }
    }
}
