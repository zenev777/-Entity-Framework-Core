namespace BookShop
{
    using BookShop.Models.Enums;
    using Data;
    using Initializer;

    public class StartUp
    {
        public static void Main()
        {
            using var db = new BookShopContext();
            //DbInitializer.ResetDatabase(db);
            //string input = Console.ReadLine();
            int inYear = int.Parse(Console.ReadLine());


            Console.WriteLine(GetBooksNotReleasedIn(db, inYear));
        }


        //02. Age Restriction
        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            if (!Enum.TryParse<AgeRestriction>(command, true, out var ageRestriction))
            {
                return $"{command} is not valid ";
            }

            var books = context.Books
                .Where(b => b.AgeRestriction == ageRestriction)
                .Select(b => new
                {
                    b.Title
                })
                .OrderBy(b => b.Title)
                .ToList();

            return string.Join(Environment.NewLine, books.Select(b => b.Title));
        }

        //03. Golden Books
        public static string GetGoldenBooks(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.EditionType == EditionType.Gold &&
                b.Copies < 5000)
                .ToList();

            return string.Join(Environment.NewLine, books.Select(b => b.Title));
        }

        //04. Books by Price
        public static string GetBooksByPrice(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.Price > 40)
                .Select(b => new
                {
                    bookTitle = b.Title,
                    bookPrice = b.Price
                })
                .OrderByDescending(b => b.bookPrice)
                .ToList();

            return string.Join(Environment.NewLine, books.Select(b => $"{b.bookTitle} - ${b.bookPrice}"));
        }


        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            var books = context.Books
                .Where(b => b.ReleaseDate.Value.Year != year)
                .Select(b => new
                {
                    bookId = b.BookId,
                    bookTitle = b.Title
                })
                .OrderBy(b => b.bookId)
                .ToList();

            return string.Join(Environment.NewLine, books.Select(b => b.bookTitle));
        }
    }


    
}


