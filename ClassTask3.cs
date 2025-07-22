using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HireMe
{
    public class ClassTask3
    {
        public void Run(string inputFile, string outputFile)
        {
            try
            {
                StandardizeLogs(inputFile, outputFile);
                Console.WriteLine("Логи успешно стандартизированы.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        public static void StandardizeLogs(string inputFile, string outputFile)
        {
            var lines = File.ReadAllLines(inputFile);
            var logEntries = new System.Collections.Generic.List<LogEntry>();
            var problemLines = new System.Collections.Generic.List<string>();

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                try
                {
                    var entry = ParseLine(line);
                    logEntries.Add(entry);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка обработки строки: {line}. Ошибка: {ex.Message}");
                    problemLines.Add(line);
                }
            }

            // Запись стандартизированных логов в выходной файл
            using (var writer = new StreamWriter(outputFile, false, Encoding.UTF8))
            {
                foreach (var entry in logEntries)
                {
                    writer.WriteLine(entry.ToString());
                }
            }

            // Запись проблемных строк в файл problems.txt
            if (problemLines.Count > 0)
            {
                File.WriteAllLines("problems.txt", problemLines);
            }
        }

        public class LogEntry
        {
            public string Date { get; set; }
            public string Time { get; set; }
            public string Level { get; set; }
            public string Method { get; set; }
            public string Message { get; set; }

            public override string ToString()
            {
                return $"{Date}\t{Time}\t{Level}\t{Method}\t{Message}";
            }
        }

        public static LogEntry ParseFormat1(string line)
        {
            var parts = line.Split(new[] { ' ' }, 4);
            if (parts.Length < 4)
            {
                throw new Exception("Invalid format for line: " + line);
            }

            var dateTimePart = parts[0] + " " + parts[1];
            var level = parts[2];
            var message = parts[3];

            var dateTimeParts = dateTimePart.Split(' ');
            if (dateTimeParts.Length != 2)
            {
                throw new Exception("Invalid date/time format in line: " + line);
            }

            var date = dateTimeParts[0];
            var time = dateTimeParts[1];

            var dateParts = date.Split('.');
            if (dateParts.Length != 3)
            {
                throw new Exception("Invalid date format in line: " + line);
            }
            var formattedDate = $"{dateParts[0]}-{dateParts[1]}-{dateParts[2]}";

            string formattedLevel;
            switch (level)
            {
                case "INFORMATION":
                    formattedLevel = "INFO";
                    break;
                case "WARNING":
                    formattedLevel = "WARN";
                    break;
                case "ERROR":
                    formattedLevel = "ERROR";
                    break;
                case "DEBUG":
                    formattedLevel = "DEBUG";
                    break;
                default:
                    formattedLevel = level;
                    break;
            }

            return new LogEntry
            {
                Date = formattedDate,
                Time = time,
                Level = formattedLevel,
                Method = "DEFAULT",
                Message = message.Trim()
            };
        }

        public static LogEntry ParseFormat2(string line)
        {
            var parts = line.Split('|');
            if (parts.Length < 5)
            {
                throw new Exception("Invalid format for line: " + line);
            }

            var dateTimePart = parts[0].Trim();
            var dateTimeParts = dateTimePart.Split(' ');
            if (dateTimeParts.Length != 2)
            {
                throw new Exception("Invalid date/time format in line: " + line);
            }

            var date = dateTimeParts[0];
            var time = dateTimeParts[1];

            var dateParts = date.Split('-');
            if (dateParts.Length != 3)
            {
                throw new Exception("Invalid date format in line: " + line);
            }
            var formattedDate = $"{dateParts[2]}-{dateParts[1]}-{dateParts[0]}";

            var level = parts[1].Trim();
            string formattedLevel;
            switch (level)
            {
                case "INFO":
                    formattedLevel = "INFO";
                    break;
                case "WARN":
                    formattedLevel = "WARN";
                    break;
                case "ERROR":
                    formattedLevel = "ERROR";
                    break;
                case "DEBUG":
                    formattedLevel = "DEBUG";
                    break;
                default:
                    formattedLevel = level;
                    break;
            }

            var method = parts[3].Trim();
            var message = parts[4].Trim();

            return new LogEntry
            {
                Date = formattedDate,
                Time = time,
                Level = formattedLevel,
                Method = method,
                Message = message
            };
        }

        public static LogEntry ParseLine(string line)
        {
            if (line.Contains('|'))
            {
                return ParseFormat2(line);
            }
            else
            {
                return ParseFormat1(line);
            }
        }
    }
}
