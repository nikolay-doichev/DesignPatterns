using System.Diagnostics;

namespace SingleResponsibilityPrinciple
{
    public class Program
    {
        public class Journal
        {
            private readonly List<string> entries = new List<string>();
            private static int count = 0;

            public int AddEntry(string text)
            {
                entries.Add($"{++count}: {text}");
                return count;// memento
            }

            public void RemoveEntry(int index)
            {
                entries.RemoveAt(index);
            }

            public override string ToString()
            {
                return string.Join(Environment.NewLine, entries);
            }

            private void Save(string filename)
            {
                File.WriteAllText(filename, ToString());
            }

            public static Journal Load()
            {
                return new Journal();
            }

            public void Load(Uri uri)
            {

            }

        }
        public class Persistence
        {
            public void SaveToFile(Journal journal, string filename, bool overwrite = false)
            {
                if (overwrite || !File.Exists(filename))
                {
                    File.WriteAllText(filename, journal.ToString());
                }
            }
        }

        static void Main(string[] args)
        {
            var j = new Journal();
            j.AddEntry("I cried today");
            j.AddEntry("I ate a bug");
            Console.WriteLine();

            var p = new Persistence();
            string filename = "journal.txt";
            string filepath = $"C:\\temp\\{filename}";
            p.SaveToFile(j, filename, true);


            Process.Start(new ProcessStartInfo { FileName = filepath, UseShellExecute = true });
        }
    }
}