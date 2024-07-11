using Newtonsoft.Json;

namespace JsonTask;

public class Program
{
    static void Main(string[] args) // TODO спросить надо ли проверки на пустые файлы делать
    {
        var countriesString = File.ReadAllText("countries.json");

        var countries = JsonConvert.DeserializeObject<List<Country>>(countriesString);

        if (countries is null)
        {
            Console.WriteLine("Стран нет");
        }
        else
        {
            var populationSum = countries.Sum(x => x.Population);

            Console.WriteLine($"Сумма населения стран: {populationSum}");
            Console.WriteLine();

            var distinctCurrencies = new Dictionary<string, int>();

            foreach (var country in countries)
            {
                foreach (var currency in country.Currencies)
                {
                    distinctCurrencies.TryAdd(currency.Name ?? string.Empty, 1); // TODO проверки на нулл, мб переделать через linq
                }
            }
             
            distinctCurrencies.Remove(string.Empty);

            Console.WriteLine("Список валют:"); // TODO спросить надо ли делать проверку на корректность, а то там [D] какой-то в жсоне

            foreach (var distinctCurrency in distinctCurrencies)
            {
                Console.WriteLine(distinctCurrency.Key);
            }
        }
    }
}