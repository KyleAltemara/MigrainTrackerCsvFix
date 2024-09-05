namespace MigrainTrackerCsvFix;

using CsvHelper.Configuration.Attributes;

public class SymptomLog
{
    [Name("date")]
    public string? Date { get; set; }

    [Name("date formatted")]
    public string? DateFormattedString { get; set; }

    [Name("weekday")]
    public string? Weekday { get; set; }

    [Name("time of day")]
    public string? TimeOfDay { get; set; }

    [Name("category")]
    public string? Category { get; set; }

    [Name("rating/amount")]
    public string? RatingAmount { get; set; }

    [Name("detail")]
    public string? Detail { get; set; }

    [Name("notes")]
    public string? Notes { get; set; }

    public override string ToString()
        => $"{Date:yyyy-MM-dd}\t{TimeOfDay}\t{Category}\t{RatingAmount}\t{Detail}\t{Notes}";
}
