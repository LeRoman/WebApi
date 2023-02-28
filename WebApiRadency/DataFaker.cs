using Bogus;
using WebApiRadency.Models;

namespace WebApiRadency
{
    public class DataFaker
    {
        Faker<Book> faker;

        string[] titles = {"Verity", "How to Catch a Leprechaun","Daisy Jones & The Six: A Novel",
            "Baking Yesteryear: The Best Recipes from the 1900s to the 1980s",
            "Things We Never Got Over (Knockemout)", "I Love You to the Moon and Back",
            "The Very Hungry Caterpillar","The Seven Husbands of Evelyn Hugo: A Novel,8 Rules of Love: How to Find It",
            "Keep It, and Let It Go" };

        string[] authors = { "Colleen Hoover","Adam Wallace","Taylor Jenkins Reid","B. Dylan Hollis","Lucy Score",
            "Amelia Hepworth","Eric Carle","Taylor Jenkins Reid","Jay Shetty"};

        string[] genres = { "Literary Fiction", "Mystery", "Thriller", "Horror", "Historical", "Romance", "Western", "Bildungsroman" };
        public DataFaker()
        {
            faker = new Faker<Book>()
                .RuleFor(u=>u.Id,f=>f.Random.Int(1,9999))
                .RuleFor(u => u.Author, f => f.PickRandom(authors))
                .RuleFor(u => u.Title, f => f.PickRandom(titles))
                .RuleFor(u=>u.Genre,f=>f.PickRandom(genres))
                .RuleFor(u => u.Content, f => f.Random.Words());
        }

        public Book[] Faker(int count)
        {
            return faker.Generate(count).ToArray();
        }



    }
}
