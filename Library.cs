using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace LibraryManagementSystem
{
    // Класс, управляющий библиотекой, включая книги, пользователей и записи о выдаче.
    public class Library
    {
        // Списки книг, пользователей и записей о выдаче
        public List<Book> Books { get; set; }
        public List<User> Users { get; set; }
        public List<BorrowRecord> BorrowRecords { get; set; }

        // Пути к файлам для сохранения данных
        private readonly string booksFile = Path.Combine("data", "books.json");
        private readonly string usersFile = Path.Combine("data", "users.json");
        private readonly string borrowRecordsFile = Path.Combine("data", "borrowRecords.json");

        // Конструктор
        public Library()
        {
            Books = new List<Book>();
            Users = new List<User>();
            BorrowRecords = new List<BorrowRecord>();
            LoadData();
        }

        // Загрузка данных из файлов.
        private void LoadData()
        {
            try
            {
                if (File.Exists(booksFile))
                {
                    string booksJson = File.ReadAllText(booksFile);
                    Books = JsonConvert.DeserializeObject<List<Book>>(booksJson) ?? new List<Book>();
                }

                if (File.Exists(usersFile))
                {
                    string usersJson = File.ReadAllText(usersFile);
                    Users = JsonConvert.DeserializeObject<List<User>>(usersJson) ?? new List<User>();
                }

                if (File.Exists(borrowRecordsFile))
                {
                    string recordsJson = File.ReadAllText(borrowRecordsFile);
                    BorrowRecords = JsonConvert.DeserializeObject<List<BorrowRecord>>(recordsJson) ?? new List<BorrowRecord>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке данных: {ex.Message}");
            }
        }

        // Сохранение данных в файлы.
        public void SaveData()
        {
            try
            {
                // Убедимся, что директория data существует
                Directory.CreateDirectory("data");

                string booksJson = JsonConvert.SerializeObject(Books, Formatting.Indented);
                File.WriteAllText(booksFile, booksJson);

                string usersJson = JsonConvert.SerializeObject(Users, Formatting.Indented);
                File.WriteAllText(usersFile, usersJson);

                string recordsJson = JsonConvert.SerializeObject(BorrowRecords, Formatting.Indented);
                File.WriteAllText(borrowRecordsFile, recordsJson);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении данных: {ex.Message}");
            }
        }

        // Добавление новой книги в библиотеку.
        public void AddBook(Book book)
        {
            Books.Add(book);
            SaveData();
            Console.WriteLine("Книга успешно добавлена.");
        }

        // Удаление книги по ID.
        public void RemoveBook(int bookId)
        {
            Book book = Books.FirstOrDefault(b => b.Id == bookId);
            if (book != null)
            {
                if (book.IsAvailable)
                {
                    Books.Remove(book);
                    SaveData();
                    Console.WriteLine("Книга успешно удалена.");
                }
                else
                {
                    Console.WriteLine("Невозможно удалить книгу, так как она выдана пользователю.");
                }
            }
            else
            {
                Console.WriteLine("Книга с таким ID не найдена.");
            }
        }

        // Поиск книги по названию или автору.
        public List<Book> SearchBooks(string query)
        {
            return Books.Where(b => b.Title.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                                    b.Author.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        // Отображение всех книг в библиотеке.
        public void DisplayAllBooks()
        {
            if (Books.Count == 0)
            {
                Console.WriteLine("В библиотеке нет книг.");
                return;
            }

            foreach (var book in Books)
            {
                Console.WriteLine(book);
            }
        }

        // Добавление нового пользователя.
        public void AddUser(User user)
        {
            Users.Add(user);
            SaveData();
            Console.WriteLine("Пользователь успешно добавлен.");
        }

        // Удаление пользователя по ID.
        public void RemoveUser(int userId)
        {
            User user = Users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                // Проверим, нет ли у пользователя выданных книг
                bool hasBorrowedBooks = BorrowRecords.Any(r => r.UserId == userId && !r.ReturnDate.HasValue);
                if (!hasBorrowedBooks)
                {
                    Users.Remove(user);
                    SaveData();
                    Console.WriteLine("Пользователь успешно удален.");
                }
                else
                {
                    Console.WriteLine("Невозможно удалить пользователя, так как у него есть выданные книги.");
                }
            }
            else
            {
                Console.WriteLine("Пользователь с таким ID не найден.");
            }
        }

        // Отображение всех пользователей.
        public void DisplayAllUsers()
        {
            if (Users.Count == 0)
            {
                Console.WriteLine("В библиотеке нет зарегистрированных пользователей.");
                return;
            }

            foreach (var user in Users)
            {
                Console.WriteLine(user);
            }
        }

        // Выдача книги пользователю.
        public void BorrowBook(int bookId, int userId)
        {
            Book book = Books.FirstOrDefault(b => b.Id == bookId);
            User user = Users.FirstOrDefault(u => u.Id == userId);

            if (book == null)
            {
                Console.WriteLine("Книга с таким ID не найдена.");
                return;
            }

            if (user == null)
            {
                Console.WriteLine("Пользователь с таким ID не найден.");
                return;
            }

            if (!book.IsAvailable)
            {
                Console.WriteLine("Книга в настоящее время недоступна для выдачи.");
                return;
            }

            // Создаем новую запись о выдаче
            int newRecordId = BorrowRecords.Count > 0 ? BorrowRecords.Max(r => r.RecordId) + 1 : 1;
            BorrowRecord record = new BorrowRecord(newRecordId, bookId, userId);
            BorrowRecords.Add(record);

            // Обновляем статус книги
            book.IsAvailable = false;

            SaveData();
            Console.WriteLine("Книга успешно выдана пользователю.");
        }

        // Возврат книги.
        public void ReturnBook(int bookId, int userId)
        {
            // Находим соответствующую запись о выдаче
            BorrowRecord record = BorrowRecords.FirstOrDefault(r => r.BookId == bookId && r.UserId == userId && !r.ReturnDate.HasValue);

            if (record == null)
            {
                Console.WriteLine("Соответствующая запись о выдаче не найдена.");
                return;
            }

            // Устанавливаем дату возврата
            record.ReturnDate = DateTime.Now;

            // Обновляем статус книги
            Book book = Books.FirstOrDefault(b => b.Id == bookId);
            if (book != null)
            {
                book.IsAvailable = true;
            }

            SaveData();
            Console.WriteLine("Книга успешно возвращена.");
        }

        // Отображение всех записей о выдаче.
        public void DisplayAllBorrowRecords()
        {
            if (BorrowRecords.Count == 0)
            {
                Console.WriteLine("Нет записей о выдаче книг.");
                return;
            }

            foreach (var record in BorrowRecords)
            {
                Console.WriteLine(record);
            }
        }
    }
}
