using Microsoft.EntityFrameworkCore;
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

            //Console.WriteLine(ImportUsers(context, impoprtUsersJson));
            //Console.WriteLine(ImportProducts(context, impoprtProductsJson));
            //Console.WriteLine(ImportCategories(context, impoprtCategoriesJson));
            //Console.WriteLine(ImportCategoryProducts(context, impoprtCatProdJson));
           
            
            //Console.WriteLine(GetCategoriesByProductsCount(context));
        }

        //01. Import Users
        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            var users = JsonConvert.DeserializeObject<List<User>>(inputJson);

            context.Users.AddRange(users);
            context.SaveChanges();

            return $"Successfully imported {users.Count}";
        }

        //02. Import Products
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

        //03. Import Categories
        public static string ImportCategories(ProductShopContext context, string inputJson)
        {
            var categories = JsonConvert.DeserializeObject<List<Category>>(inputJson);

            var validCategories = categories?.Where(c => c.Name != null).ToList();


            context.Categories.AddRange(validCategories);
            context.SaveChanges();

            return $"Successfully imported {validCategories.Count}";
        }

        //04. Import Categories and Products
        public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        {
            var categoryProducts = JsonConvert.DeserializeObject<List<CategoryProduct>>(inputJson);

            context.CategoriesProducts.AddRange(categoryProducts);
            context.SaveChanges();

            return $"Successfully imported {categoryProducts.Count}";
        }

        //05. Export Products In Range
        public static string GetProductsInRange(ProductShopContext context)
        {
            var productsInRange = context.Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .Select(p => new
                {
                    name = p.Name,
                    price = p.Price,
                    seller = $"{p.Seller.FirstName} {p.Seller.LastName}",
                })
                .OrderBy(p => p.price)
                .ToArray();

            var json = JsonConvert.SerializeObject(productsInRange, Formatting.Indented);

            return json;
        }

        //06. Export Sold Products
        public static string GetSoldProducts(ProductShopContext context)
        {
            var SoldProductsUsers = context.Users
                .Where(u => u.ProductsSold.Any(b => b.BuyerId != null))
                .OrderBy(u => u.LastName).ThenBy(u => u.FirstName)
                .Select(u => new
                {
                    firstName = u.FirstName,
                    lastName = u.LastName,
                    soldProducts = u.ProductsSold
                    .Where(ps => ps.BuyerId != null)
                    .Select(ps => new
                    {
                        name = ps.Name,
                        price = ps.Price,
                        buyerFirstName = ps.Buyer.FirstName,
                        buyerLastName = ps.Buyer.LastName,
                    }).ToArray()
                })
                .ToArray();

            return JsonConvert.SerializeObject(SoldProductsUsers, Formatting.Indented);
        }

        //07. Export Categories By Products Count
        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var categories = context.Categories
                .Select(c => new
                {
                    category = c.Name,
                    productsCount = c.CategoriesProducts.Count,
                    averagePrice = c.CategoriesProducts
                        .Average(p => p.Product.Price).ToString("f2"),
                    totalRevenue = c.CategoriesProducts
                        .Sum(p => p.Product.Price).ToString("f2")
                })
                .OrderByDescending(pc => pc.productsCount)
                .ToArray();


            return JsonConvert.SerializeObject(categories, Formatting.Indented);
        }

    }

}