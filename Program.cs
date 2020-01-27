using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using CsvHelper;
using Neo4j.Driver;
using Dingg_Databuilder.Models;
using Dingg_Databuilder.Extractors;
using Dingg_Databuilder.Builders;

namespace Dingg_Databuilder
{
    class Program
    {
        static void GreenText(string text)
        {
            var curColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(text);
            Console.ForegroundColor = curColor;
        }

        static void Main(string[] args)
        {
            // Unsure why im using async, when im literally blocking the thread, no point really
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Dingg Databuilder");
            Console.ForegroundColor = ConsoleColor.Magenta;

            // This whole thing could've been done much more elegantly, but i got no time for that
            // NOTE: this should be done in for loop somehow
            // Look at this magical beautiful builder line ^_^
            Console.Write("Loading cities... ");
            Builder.City(new CSV<City>(".\\Data\\City.csv").Extract()).Wait();
            GreenText("Done!");

            Console.Write("Loading categories... ");
            Builder.Category(new CSV<Category>(".\\Data\\Category.csv").Extract()).Wait();
            GreenText("Done!");

            Console.Write("Loading restaurants... ");
            Builder.Restaurant(new CSV<Restaurant>(".\\Data\\Restaurant.csv").Extract()).Wait();
            GreenText("Done!");
        }
    }
}
