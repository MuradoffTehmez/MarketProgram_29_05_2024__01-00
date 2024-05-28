using System;
using System.Collections.Generic;

namespace MarketProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, double> products = new Dictionary<string, double>()
            {
                {"Alma", 1.5},
                {"Banan", 2.0},
                {"Sud", 1.2},
                {"Çorek", 0.8},
                {"Yumurta", 0.1}
            };

            List<string> cart = new List<string>();
            bool continueShopping = true;

            while (continueShopping)
            {
                Console.WriteLine("\nMarket proqramına xoş gelmisiniz!");
                Console.WriteLine("1. Mehsullara bax");
                Console.WriteLine("2. Mehsul elave et");
                Console.WriteLine("3. Mehsulu cixar");
                Console.WriteLine("4. Sebete bax");
                Console.WriteLine("5. Ümumi məbləği yoxla");
                Console.WriteLine("6. Ödəniş et");
                Console.WriteLine("7. Çıxış");
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
                        continueShopping = false;
                        Console.WriteLine("Proqramdan çıxılır...");
                        break;
                    default:
                        Console.WriteLine("Yanlış seçim, yenidən cəhd edin.");
                        break;
                }
            }
        }

        static void ViewProducts(Dictionary<string, double> products)
        {
            Console.WriteLine("\nMəhsullar:");
            foreach (var product in products)
            {
                Console.WriteLine($"{product.Key}: {product.Value} AZN");
            }
        }

        static void AddProduct(Dictionary<string, double> products, List<string> cart)
        {
            Console.Write("\nƏlavə etmək istədiyiniz məhsulun adını yazın: ");
            string name = Console.ReadLine();
            if (products.ContainsKey(name))
            {
                cart.Add(name);
                Console.WriteLine($"{name} səbətə əlavə edildi.");
            }
            else
            {
                Console.WriteLine("Belə məhsul tapılmadı.");
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

        static void CheckTotal(Dictionary<string, double> products, List<string> cart)
        {
            double total = 0;
            foreach (var product in cart)
            {
                total += products[product];
            }
            Console.WriteLine($"\nÜmumi məbləğ: {total} AZN");
        }

        static void Checkout(Dictionary<string, double> products, List<string> cart)
        {
            double total = 0;
            foreach (var product in cart)
            {
                total += products[product];
            }
            Console.WriteLine($"\nÖdəniş tamamlandı. Ümumi məbləğ: {total} AZN");
            cart.Clear();
            Console.WriteLine("Səbət boşaldıldı.");
        }
    }
}
