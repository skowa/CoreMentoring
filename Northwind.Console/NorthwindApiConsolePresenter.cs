using System.Collections.Generic;
using System.Threading.Tasks;
using Northwind.Console.Interfaces.Services;

namespace Northwind.Console
{
    public static class NorthwindApiConsolePresenter
    {
        private const int CategoriesChoice = 1;
        private const int ProductsChoice = 2;
        private const int ExitChoice = 3;

        public static async Task RunAsync(INorthwindApiService northwindApiService)
        {
            System.Console.WriteLine($"{CategoriesChoice} - Show categories");
            System.Console.WriteLine($"{ProductsChoice} - Show products");
            System.Console.WriteLine($"{ExitChoice} - Exit");

            int userChoice = 0;
            while (userChoice != ExitChoice)
            {
                if (TryReadUserInput(CategoriesChoice, ExitChoice, out userChoice))
                {
                    switch (userChoice)
                    {
                        case CategoriesChoice:
                            var categories = await northwindApiService.GetCategoriesAsync();
                            ShowCollection(categories);

                            break;

                        case ProductsChoice:
                            var products = await northwindApiService.GetProductsAsync();
                            ShowCollection(products);

                            break;

                        default:
                            break;
                    }
                }
            }
        }

        private static bool TryReadUserInput(int min, int max, out int validUserChoice)
        {
            return int.TryParse(System.Console.ReadLine(), out validUserChoice) && validUserChoice >= min && validUserChoice <= max;
        }

        private static void ShowCollection<T>(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                System.Console.WriteLine(item);
            }
        }
    }
}
