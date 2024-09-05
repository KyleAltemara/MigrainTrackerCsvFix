namespace MigrainTrackerCsvFix;
using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;

internal class NewSymptomLog
{
    [Name("date")]
    public string? Date { get; set; }

    [Name("date formatted")]
    public string? DateFormattedString { get; set; }

    [Name("weekday")]
    public string? Weekday { get; set; }

    [Name("symptom")]
    public string? Symptom { get; set; }

    [Name("AM Level")]
    public string? AmLevel { get; set; }

    [Name("Mid Level")]
    public string? MidLevel { get; set; }

    [Name("PM Level")]
    public string? PmLevel { get; set; }

    [Name("notes")]
    public string? Notes { get; set; }

    public void FormatNotes(int maxLineLength)
    {
        if (string.IsNullOrEmpty(Notes))
        {
            return;
        }

        var words = Notes.Split(' ');
        var formattedNotes = new List<string>();
        var currentLine = string.Empty;

        foreach (var word in words)
        {
            if ((currentLine + word).Length > maxLineLength)
            {
                formattedNotes.Add(currentLine.Trim());
                currentLine = string.Empty;
            }

            currentLine += word + " ";
        }

        if (!string.IsNullOrEmpty(currentLine))
        {
            formattedNotes.Add(currentLine.Trim());
        }

        Notes = string.Join(Environment.NewLine, formattedNotes);
    }

    public override string ToString() => $"{Date}\t{DateFormattedString}\t{Weekday}\t{Symptom}\t{AmLevel}\t{MidLevel}\t{PmLevel}\t{Notes}";
}
