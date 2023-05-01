namespace UniversityLibrary.Test
{
    using NUnit.Framework;
    using System.Text;

    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestLibraryConstructor()
        {
            UniversityLibrary library = new UniversityLibrary();

            Assert.AreEqual(0, library.Catalogue.Count);
        }

        [Test]
        public void TestTextBookConstructor()
        {
            TextBook textBook = new TextBook("Scratch", "Peter", "Science");

            Assert.AreEqual("Scratch", textBook.Title);
            Assert.AreEqual("Peter", textBook.Author);
            Assert.AreEqual("Science", textBook.Category);
        }

        [Test]
        public void TestIfAddTextBookToLibrary()
        {
            TextBook textBook = new TextBook("Scratch", "Peter", "Science");
            UniversityLibrary library = new UniversityLibrary();
            StringBuilder sb = new StringBuilder();

            
            
            string res = library.AddTextBookToLibrary(textBook);

            sb.AppendLine($"Book: Scratch - 1");
            sb.AppendLine($"Category: Science");
            sb.AppendLine($"Author: Peter");

            Assert.AreEqual(1, library.Catalogue.Count);
            Assert.AreEqual(1, textBook.InventoryNumber);
            Assert.That(sb.ToString().TrimEnd(), Is.EqualTo(res));

            string studentName = "Peter";

            var result = library.LoanTextBook(textBook.InventoryNumber, studentName);

            Assert.AreEqual(studentName, textBook.Holder);
            Assert.That(result, Is.EqualTo($"{textBook.Title} loaned to {studentName}."));
        }

        [Test]
        public void TestIfTextBookIsNotReturned()
        {
            TextBook textBook = new TextBook("Scratch", "Peter", "Science");
            UniversityLibrary library = new UniversityLibrary();
            library.AddTextBookToLibrary(textBook);
            string studentName = "Peter";

            library.LoanTextBook(textBook.InventoryNumber, studentName);
            var result = library.LoanTextBook(1, studentName);

            Assert.AreEqual(studentName, textBook.Holder);
            Assert.That(result, Is.EqualTo($"{studentName} still hasn't returned {textBook.Title}!"));
        }

        [Test]
        public void TestIfTextBookLoanedSuccessfully()
        {
            TextBook textBook = new TextBook("Scratch", "Peter", "Science");
            UniversityLibrary library = new UniversityLibrary();
            library.AddTextBookToLibrary(textBook);
            string studentName = "Peter";

            //library.LoanTextBook(textBook.InventoryNumber, studentName);
            var result = library.LoanTextBook(textBook.InventoryNumber, studentName);

            Assert.AreEqual(studentName, textBook.Holder);
            Assert.That(result, Is.EqualTo($"{textBook.Title} loaned to {studentName}."));
        }

        [Test]
        public void TestIfTextBookReturnedCorrectly()
        {
            TextBook textBook = new TextBook("Scratch", "Peter", "Science");
            UniversityLibrary library = new UniversityLibrary();
            library.AddTextBookToLibrary(textBook);
            library.LoanTextBook(1, "George");

            var result = library.ReturnTextBook(1);

            Assert.AreEqual(string.Empty, textBook.Holder);
            Assert.That(result, Is.EqualTo($"Scratch is returned to the library."));
        }
    }
}