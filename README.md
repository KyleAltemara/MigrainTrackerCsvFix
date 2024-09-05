# Migraine Tracker CSV Fix

This program processes CSV files containing migraine symptom logs, consolidates the data, and outputs a new CSV file with the formatted information.

## Files

- **Program.cs**: The main entry point of the application. It processes CSV files and directories containing CSV files.
- **SymptomLog.cs**: Defines the `SymptomLog` class, which represents the structure of the original CSV file.
- **SymptomLogMap.cs**: Maps the CSV columns to the `SymptomLog` class properties.
- **NewSymptomLog.cs**: Defines the `NewSymptomLog` class, which represents the structure of the consolidated and formatted CSV file.

## Usage

1. **Compile the program**: Open the solution in Visual Studio and build the project.
2. **Run the program**: Execute the compiled program with the CSV file or directory containing CSV files as arguments.

## Example
'''MigrainTrackerCsvFix.exe <path_to_csv_file_or_directory>

or

'''MigrainTrackerCsvFix.exe C:\path\to\your\csvfile.csv

Alternatively, you can drag and drop the CSV file or folder onto the executable to run the program.

## Functionality

- **Reading CSV Files**: The program reads the CSV files using the `CsvHelper` library and maps the data to the `SymptomLog` class.
- **Processing Data**: It consolidates the symptom logs by date and symptom, and formats the notes.
- **Writing CSV Files**: The program writes the consolidated data to a new CSV file with the prefix `updated_`.

## Dependencies

- [CsvHelper](https://joshclose.github.io/CsvHelper/): A library for reading and writing CSV files in .NET.

## License

This project is licensed under the MIT License.


 
