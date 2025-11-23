using BookstoreManagerAPI.Models;

namespace BookstoreManagerAPI.Services;

public static class BooksStore
{
    private static readonly List<Book> books = new();

    static BooksStore()
    {
        books.Add(new Book
        {
            Title = "O Senhor dos Anéis: A Sociedade do Anel",
            Author = "J.R.R. Tolkien",
            Genre = Genre.Fantasia,
            Price = 89.90m,
            Stock = 15
        });

        books.Add(new Book
        {
            Title = "1984",
            Author = "George Orwell",
            Genre = Genre.Ficcao,
            Price = 45.50m,
            Stock = 20
        });
    }

    public static List<Book> GetAll()
    {
        return books;
    }

    public static Book? GetById(Guid id)
    {
        return books.FirstOrDefault(b => b.Id == id);
    }

    public static void Add(Book book)
    {
        if (!Enum.IsDefined(typeof(Genre), book.Genre))
        {
            throw new ArgumentException("Gênero inválido.");
        }

        if (Exists(book.Title, book.Author))
        {
            throw new InvalidOperationException("Já existe um livro com este título e autor.");
        }

        books.Add(book);
    }

    public static void Update(Book bookToUpdate)
    {
        var existingBookIndex = books.FindIndex(b => b.Id == bookToUpdate.Id);
        if (existingBookIndex == -1)
        {
            throw new KeyNotFoundException("Livro não encontrado.");
        }

        if (!Enum.IsDefined(typeof(Genre), bookToUpdate.Genre))
        {
            throw new ArgumentException("Gênero inválido.");
        }

        if (Exists(bookToUpdate.Title, bookToUpdate.Author, bookToUpdate.Id))
        {
            throw new InvalidOperationException("Já existe outro livro com este título e autor.");
        }

        bookToUpdate.UpdateTimestamp();

        books[existingBookIndex] = bookToUpdate;
    }

    public static void Delete(Guid id)
    {
        var bookToRemove = books.FirstOrDefault(b => b.Id == id);
        if (bookToRemove == null)
        {
            throw new KeyNotFoundException("Livro não encontrado.");
        }

        books.Remove(bookToRemove);
    }

    private static bool Exists(string title, string author, Guid? idToExclude = null)
    {
        return books.Any(b =>
            b.Title.Equals(title, StringComparison.OrdinalIgnoreCase) &&
            b.Author.Equals(author, StringComparison.OrdinalIgnoreCase) &&
            (idToExclude == null || b.Id != idToExclude));
    }
}
