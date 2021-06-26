using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using NDesk.Options;

namespace EPGP
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string fileName = null;
            string outputFileName = null;
            bool trim = false;

            var optionSet = new OptionSet
            {
                {
                    "file=|f=", "The file containing the EPGP addon export data.",
                    v => fileName = v
                },
                {
                    "output=|o=", "The file name to write the output to.",
                    v => outputFileName = v
                },
                {
                    "trim|t", "Trims entries that haven't earned any EP.",
                    v => trim = v != null
                }
            };

            string error = null;
            try
            {
                var leftover = optionSet.Parse(args);
                if (leftover.Any())
                    error = "Unknown arguments: " + string.Join(" ", leftover);
            }
            catch (OptionException ex)
            {
                error = ex.Message;
            }

            if (error != null)
            {
                Console.WriteLine($"EPGP: {error}");
                Console.WriteLine("Use --help for a list of supported options.");
                return;
            }

            if (string.IsNullOrEmpty(fileName))
            {
                Console.WriteLine("Please provide the EPGP file you wish to process using '-f fileName.txt'");
                return;
            }

            var fi = new FileInfo(fileName);

            if (!fi.Exists)
            {
                Console.WriteLine($"The specified file does not exist or could not be read: {fileName}");
                return;
            }

            var contents = await File.ReadAllTextAsync(fileName);

            if (string.IsNullOrEmpty(contents))
            {
                Console.WriteLine(
                    "The specified file did not contain any data. Make sure it's the EPGP export file as dumped from your addon.");
                return;
            }

            var roster = JsonSerializer.Deserialize<Export>(contents, new JsonSerializerOptions());

            if (roster?.Roster == null || !roster.Roster.Any())
            {
                Console.WriteLine("Couldn't read the roster from the specified file.");
                return;
            }

            if (trim)
            {
                int oldCount = roster.Roster.Count;
                roster.Roster = roster.Roster.Where(r => r.Ep > 0).ToList();

                Console.WriteLine($"Removed {oldCount - roster.Roster.Count} entries that hadn't earned EP.");
            }

            if (!string.IsNullOrEmpty(outputFileName))
            {
                await File.WriteAllTextAsync(outputFileName, JsonSerializer.Serialize(roster), Encoding.UTF8);

                Console.WriteLine($"Output written to {outputFileName}");
            }
        }
    }
}
