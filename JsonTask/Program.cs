using Newtonsoft.Json;

namespace JsonTask;

public class Program
{
    static void Main(string[] args)
    {
        try
        {
            var countriesString = File.ReadAllText("countries.json");

            var countries = JsonConvert.DeserializeObject<List<Country>>(countriesString);

            if (countries is null)
            {
                Console.WriteLine("Исходный файл пуст");

                return;
            }

            var populationsSum = countries.Sum(x => x.Population);

            Console.WriteLine($"Сумма населения стран: {populationsSum}");
            Console.WriteLine();

            var currencies = countries.SelectMany(x => x.Currencies)
                .Where(x => x.Code is not null && x.Name is not null)
                .DistinctBy(x => x.Name);

            Console.WriteLine("Список валют:");

            foreach (var currency in currencies)
            {
                Console.WriteLine(currency.Name);
            }
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine(e.Message);
        }
        catch (IOException e)
        {
            Console.WriteLine(e.Message);
        }
        catch (JsonReaderException e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            Console.ReadKey();
        }
    }
}