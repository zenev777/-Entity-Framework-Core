using Newtonsoft.Json;
using ProductShop.Data;
using ProductShop.Models;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main()
        {
            ProductShopContext context = new();

            string impoprtUsersJson = File.ReadAllText("../../../Datasets/users.json");
            string impoprtProductsJson = File.ReadAllText("../../../Datasets/products.json");
            string impoprtCategoriesJson = File.ReadAllText("../../../Datasets/categories.json");
            string impoprtCatProdJson = File.ReadAllText("../../../Datasets/categories-products.json");

            Console.WriteLine(ImportUsers(context, impoprtUsersJson));
            Console.WriteLine(ImportProducts(context, impoprtProductsJson));
            Console.WriteLine(ImportCategories(context, impoprtCategoriesJson));
            Console.WriteLine(ImportCategoryProducts(context, impoprtCatProdJson));
        }


        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            var users = JsonConvert.DeserializeObject<List<User>>(inputJson);

            context.Users.AddRange(users);
            context.SaveChanges();

            return $"Successfully imported {users.Count}";
        }

        public static string ImportProducts(ProductShopContext context, string inputJson)
        {
            var products = JsonConvert.DeserializeObject<List<Product>>(inputJson);
            if (products != null)
            {
                context.Products.AddRange(products);
                context.SaveChanges();
            }


            return $"Successfully imported {products?.Count}";
        }

        public static string ImportCategories(ProductShopContext context, string inputJson)
        {
            var categories = JsonConvert.DeserializeObject<List<Category>>(inputJson);

            var validCategories = categories?.Where(c => c.Name != null).ToList();


            context.Categories.AddRange(validCategories);
            context.SaveChanges();

            return $"Successfully imported {validCategories.Count}";
        }

        public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        {
            var categoryProducts = JsonConvert.DeserializeObject<List<CategoryProduct>>(inputJson);

            context.CategoriesProducts.AddRange(categoryProducts);
            context.SaveChanges();

            return $"Successfully imported {categoryProducts.Count}";
        }
    }





}