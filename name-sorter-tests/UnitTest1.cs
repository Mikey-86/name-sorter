using static name_sorter.Program;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace name_sorter_tests
{
    [TestClass]
    public class NameSorterTests
    {
        // Test for FileBasedNameRepository
        [TestMethod]
        public void TestReadNames()
        {
            // Arrange
            var filePath = "test-names.txt";
            File.WriteAllLines(filePath, new[] { "John Smith", "Alice Johnson", "David Brown" });

            var nameRepository = new FileBasedNameRepository();

            // Act
            var names = nameRepository.ReadNames(filePath);

            // Assert
            Assert.AreEqual(3, names.Count());
            Assert.IsTrue(names.Contains("John Smith"));
            Assert.IsTrue(names.Contains("Alice Johnson"));
            Assert.IsTrue(names.Contains("David Brown"));

            // Clean up
            File.Delete(filePath);
        }

        [TestMethod]
        public void TestWriteNames()
        {
            // Arrange
            var filePath = "test-names.txt";
            var names = new List<string> { "John Smith", "Alice Johnson", "David Brown" };
            var nameRepository = new FileBasedNameRepository();

            // Act
            nameRepository.WriteNames(names, filePath);

            // Assert
            var writtenNames = File.ReadAllLines(filePath);
            CollectionAssert.AreEqual(names, writtenNames);

            // Clean up
            File.Delete(filePath);
        }

        // Test for LastNameFirstNameSorter
        [TestMethod]
        public void TestSortNames()
        {
            // Arrange
            var unsortedNames = new[] { "John Smith", "Alice Johnson", "David Brown" };
            var nameSorter = new LastNameFirstNameSorter();

            // Act
            var sortedNames = nameSorter.SortNames(unsortedNames);

            // Assert
            CollectionAssert.AreEqual(new[] { "David Brown", "Alice Johnson", "John Smith" }, sortedNames.ToList());
        }

        // Test for NameSorter
        [TestMethod]
        public void TestExecute()
        {
            // Arrange
            var inputFile = "test-names.txt";
            File.WriteAllLines(inputFile, new[] { "John Smith", "Alice Johnson", "David Brown" });
            var nameRepository = new FileBasedNameRepository();
            var nameSorter = new NameSorter(nameRepository, new LastNameFirstNameSorter());

            // Act
            var sortedNames = nameSorter.Execute(inputFile);

            // Assert
            CollectionAssert.AreEqual(new[] { "David Brown", "Alice Johnson", "John Smith" }, sortedNames.ToList());

            // Clean up
            File.Delete(inputFile);
        }
    }
}