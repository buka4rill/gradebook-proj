using System;
using Xunit;

namespace GradeBook.Tests
{
    // Delegate to Log messages
    public delegate string WriteLogDelegate(string logMessage);


    public class TypeTests
    {
        int count = 0;

        [Fact]
        public void WriteLogDelegateCanPointToMethod()
        {
            //  Count how many times i'm invoking a method

            WriteLogDelegate log = ReturnMessage;

            // Delegate log pointing to method
            log += ReturnMessage;
            log += IncrementCount;

            var result = log("Hello");
            Assert.Equal(3, count);

        }

        string IncrementCount(string message)
        {
            count++;
            return message.ToLower();
        }

        string ReturnMessage(string message)
        {
            count++;
            return message;
        }

        // Value Types
        [Fact]
        public void ValueTypesAlsoPassByValue()
        {
            var x = GetInt();
            SetInt(ref x);

            Assert.Equal(42, x);
        }

        private void SetInt(ref int x)
        {
            x = 42;
        }

        private int GetInt()
        {
            return 3;
        }

        // CSHarp can pass by Ref type
        [Fact]
        public void CSharpCanPassByRef()
        {
            var book1 = GetBook("Book 1");
            GetBookSetName(out book1, "New Name");

            Assert.Equal("New Name", book1.Name);
        }

        private void GetBookSetName(out InMemoryBook book, string name)
        {
            book = new InMemoryBook(name);
        }

        // Is CSharp passed by value?
        [Fact]
        public void CSharpIsPassedByValue()
        {
            var book1 = GetBook("Book 1");
            GetBookSetName(book1, "New Name");

            Assert.Equal("Book 1", book1.Name);
        }

        private void GetBookSetName(InMemoryBook book, string name)
        {
            book = new InMemoryBook(name);
        }

        // Check to see if we can change the name of a book
        [Fact]
        public void CanSetNameFromReference()
        {
            var book1 = GetBook("Book 1");
            SetName(book1, "New Name");

            Assert.Equal("New Name", book1.Name);
        }

        private void SetName(InMemoryBook book, string name)
        {
            book.Name = name;
        }

        // How strings behave
        [Fact]
        public void StringsBehaveLikeValueTypes()
        {
            // Strings are immutable
            string name = "Ebuka";
            var upper = MakeUpppercase(name);

            Assert.Equal("Ebuka", name);
            Assert.Equal("EBUKA", upper);
        }

        private string MakeUpppercase(string parameter)
        {
            return parameter.ToUpper();
        }

        [Fact]
        public void GetBookReturnsDifferentObjects()
        {
            var book1 = GetBook("Book 1");
            var book2 = GetBook("Book 2");

            // Every time I invoke a new Book object,
            // I am creating a distinct Book object
            Assert.Equal("Book 1", book1.Name);
            Assert.Equal("Book 2", book2.Name);

            Assert.NotSame(book1, book2);
        }

        InMemoryBook GetBook(string name)
        {
            return new InMemoryBook(name);
        }

        // Can two different variables reference the same object?
        [Fact]
        public void TwoVarsCanReferenceSameObjects()
        {
            var book1 = GetBook("Book 1");
            var book2 = book1;

            Assert.Same(book1, book2);
            Assert.True(Object.ReferenceEquals(book1, book2));
        }
    }
}
