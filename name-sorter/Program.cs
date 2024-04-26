using NUnit.Framework;
using NUnit.Framework.Legacy;
using static name_sorter.Program;

namespace name_sorter
{
    public class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Usage: name-sorter <input-file>");
                return;
            }

            string inputFile = args[0];
            string outputFile = "sorted-names-list.txt";

            var nameRepository = new FileBasedNameRepository();
            var nameSorter = new NameSorter(nameRepository, new LastNameFirstNameSorter());
            var sortedNames = nameSorter.Execute(inputFile);
            nameRepository.WriteNames(sortedNames, outputFile);
            Console.WriteLine("Names have been sorted and saved to the file: " + outputFile);
            nameSorter.DisplaySortedNames(sortedNames);
        }


        // Interface to define a name repository
        public interface INameRepository
        {
            IEnumerable<string> ReadNames(string filePath);
            void WriteNames(IEnumerable<string> names, string filePath);
        }

        // File implementation of INameRepository
        public class FileBasedNameRepository : INameRepository
        {
            public IEnumerable<string> ReadNames(string filePath)
            {
                return File.ReadAllLines(filePath);
            }

            public void WriteNames(IEnumerable<string> names, string filePath)
            {
                File.WriteAllLines(filePath, names);
            }
        }

        // Interface to define a name sorter
        public interface INameSorter
        {
            IEnumerable<string> SortNames(IEnumerable<string> names);
        }

        // Implementation of INameSorter
        public class LastNameFirstNameSorter : INameSorter
        {
            public IEnumerable<string> SortNames(IEnumerable<string> names)
            {
                return names.OrderBy(name => name.Split(' ').Last());
            }
        }

        // Name sorter
        public class NameSorter
        {
            private readonly INameRepository _nameRepository;
            private readonly INameSorter _nameSorter;

            public NameSorter(INameRepository nameRepository, INameSorter nameSorter)
            {
                _nameRepository = nameRepository;
                _nameSorter = nameSorter;
            }

            public IEnumerable<string> Execute(string inputFile)
            {
                var names = _nameRepository.ReadNames(inputFile);
                var sortedNames = _nameSorter.SortNames(names);
                return sortedNames;
            }

            public void DisplaySortedNames(IEnumerable<string> sortedNames)
            {
                foreach (var name in sortedNames)
                {
                    Console.WriteLine(name);
                }
            }
        }
    }
}
