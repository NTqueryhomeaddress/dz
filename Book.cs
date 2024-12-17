using System;

namespace LibraryManagementSystem
{
    // Класс, представляющий книгу в библиотеке.
    public class Book
    {
        // Уникальный идентификатор книги
        public int Id { get; set; }

        // Название книги
        public string Title { get; set; }

        // Автор книги
        public string Author { get; set; }

        // Год издания
        public int Year { get; set; }

        // Доступна ли книга для выдачи
        public bool IsAvailable { get; set; }

        // Конструктор по умолчанию
        public Book() { }

        // Конструктор с параметрами
        public Book(int id, string title, string author, int year)
        {
            Id = id;
            Title = title;
            Author = author;
            Year = year;
            IsAvailable = true;
        }

        // Переопределение метода ToString для удобного вывода информации о книге
        public override string ToString()
        {
            return $"ID: {Id}, Название: {Title}, Автор: {Author}, Год: {Year}, Доступна: {IsAvailable}";
        }
    }
}
