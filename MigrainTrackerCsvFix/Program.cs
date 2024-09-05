namespace MigrainTrackerCsvFix;

using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        foreach (var arg in args)
        {
            if (File.Exists(arg) && Path.GetExtension(arg) == ".csv")
            {
                FixMigraineCsv(arg);
            }
            else if (Directory.Exists(arg))
            {
                var files = Directory.GetFiles(arg, "*.csv");
                foreach (var file in files)
                {
                    FixMigraineCsv(file);
                }
            }
        }

        Console.WriteLine("Press any key to exit . . .");
        Console.ReadKey();
    }

    private static void FixMigraineCsv(string filePath)
    {
        Console.Write($"Processing file: {filePath} . . .");
        try
        {
            var symptomLogs = ReadSymptomLogs(filePath);
            var groupedLogs = new Dictionary<(string date, string sympton), NewSymptomLog>();

            foreach (var log in symptomLogs)
            {
                var symptom = log.Detail?.Replace("(Mild)", string.Empty)
                                         .Replace("(Moderate)", string.Empty)
                                         .Replace("(Severe)", string.Empty)
                                         .Replace("(Unbearable)", string.Empty)
                                         .Replace("(when, duration, where, 1-10)", string.Empty)
                                         .Replace("(time, duration, 1-10)", string.Empty)
                                         .Trim() ?? "";
                if (log.Category == "Extra notes")
                {
                    symptom = "Extra notes";
                }

                var key = (log.Date!, symptom);
                if (groupedLogs.TryGetValue(key, out var newlog))
                {
                    switch (log.TimeOfDay)
                    {
                        case "AM":
                        case "am":
                            newlog.AmLevel = log.RatingAmount;
                            break;
                        case "Mid":
                        case "mid":
                            newlog.MidLevel = log.RatingAmount;
                            break;
                        case "PM":
                        case "pm":
                            newlog.PmLevel = log.RatingAmount;
                            break;
                        default:
                            break;
                    }

                    if (!string.IsNullOrEmpty(log.Notes))
                    {
                        if (string.IsNullOrEmpty(newlog.Notes))
                        {
                            newlog.Notes = log.Notes;
                        }
                        else
                        {
                            newlog.Notes += Environment.NewLine + log.Notes;
                        }
                    }
                }
                else
                {
                    var newLog = new NewSymptomLog
                    {
                        Date = log.Date,
                        DateFormattedString = log.DateFormattedString,
                        Weekday = log.Weekday,
                        Symptom = symptom,
                        AmLevel = log.TimeOfDay!.Equals("am", StringComparison.CurrentCultureIgnoreCase) ? log.RatingAmount : null,
                        MidLevel = log.TimeOfDay!.Equals("mid", StringComparison.CurrentCultureIgnoreCase) ? log.RatingAmount : null,
                        PmLevel = log.TimeOfDay!.Equals("pm", StringComparison.CurrentCultureIgnoreCase) ? log.RatingAmount : null,
                        Notes = log.Notes,
                    };
                    groupedLogs.Add(key, newLog);
                }

            }

            foreach (var group in groupedLogs)
            {
                group.Value.FormatNotes(100);
            }

            WriteNewSymptomLogs(Path.Combine(filePath, "..", "updated_" + Path.GetFileName(filePath)), groupedLogs.Values);
            Console.WriteLine("Succeded");
            Console.WriteLine($"CSV file has been processed and saved as updated_{Path.GetFileName(filePath)}");
            Console.WriteLine();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed");
            Console.WriteLine("CSV file could not be processed.");
            Console.WriteLine(ex.Message);
            Console.WriteLine();
        }
    }

    static List<SymptomLog> ReadSymptomLogs(string filePath)
    {
        var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            Delimiter = ",",
            HeaderValidated = null, // Disable header validation
            MissingFieldFound = null // Ignore missing fields
        };

        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, csvConfig);
        csv.Context.RegisterClassMap<SymptomLogMap>();
        var records = csv.GetRecords<SymptomLog>().ToList();
        return records;
    }

    static void WriteNewSymptomLogs(string filePath, IEnumerable<NewSymptomLog> logs)
    {
        var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            Delimiter = ","
        };

        using var writer = new StreamWriter(filePath);
        using var csv = new CsvWriter(writer, csvConfig);
        csv.WriteRecords(logs);
    }
}
