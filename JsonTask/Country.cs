namespace JsonTask;

public class Country
{
    public required string Name { get; set; }

    public required int Population { get; set; }

    public required Currency[] Currencies { get; set; }
}