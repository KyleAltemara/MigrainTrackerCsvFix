namespace MigrainTrackerCsvFix;

using CsvHelper.Configuration;

public sealed class SymptomLogMap : ClassMap<SymptomLog>
{
    public SymptomLogMap()
    {
        Map(m => m.Date).Name("date");
        Map(m => m.DateFormattedString).Name("date formatted");
        Map(m => m.Weekday).Name("weekday");
        Map(m => m.TimeOfDay).Name("time of day");
        Map(m => m.Category).Name("category");
        Map(m => m.RatingAmount).Name("rating/amount");
        Map(m => m.Detail).Name("detail");
        Map(m => m.Notes).Name("notes");
    }
}
