using System;

namespace LibraryManagementSystem
{
    // Класс Program - точка входа в приложение.
    class Program
    {
        static void Main(string[] args)
        {
            Library library = new Library();
            bool exit = false;

            Console.WriteLine("=== Система Управления Библиотекой ===");

            while (!exit)
            {
                ShowMenu();
                Console.Write("Выберите опцию: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddBook(library);
                        break;
                    case "2":
                        RemoveBook(library);
                        break;
                    case "3":
                        SearchBooks(library);
                        break;
                    case "4":
                        library.DisplayAllBooks();
                        break;
                    case "5":
                        AddUser(library);
                        break;
                    case "6":
                        RemoveUser(library);
                        break;
                    case "7":
                        library.DisplayAllUsers();
                        break;
                    case "8":
                        BorrowBook(library);
                        break;
                    case "9":
                        ReturnBook(library);
                        break;
                    case "10":
                        library.DisplayAllBorrowRecords();
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Пожалуйста, попробуйте снова.");
                        break;
                }

                Console.WriteLine();
            }

            Console.WriteLine("Спасибо за использование системы! До свидания.");
        }

        // Отображение меню опций.
        static void ShowMenu()
        {
            Console.WriteLine("======================================");
            Console.WriteLine("1. Добавить книгу");
            Console.WriteLine("2. Удалить книгу");
            Console.WriteLine("3. Поиск книги");
            Console.WriteLine("4. Показать все книги");
            Console.WriteLine("5. Добавить пользователя");
            Console.WriteLine("6. Удалить пользователя");
            Console.WriteLine("7. Показать всех пользователей");
            Console.WriteLine("8. Выдать книгу пользователю");
            Console.WriteLine("9. Принять возврат книги");
            Console.WriteLine("10. Показать все записи о выдаче");
            Console.WriteLine("0. Выход");
            Console.WriteLine("======================================");
        }

        // Добавление новой книги.
        static void AddBook(Library library)
        {
            Console.WriteLine("=== Добавление Книги ===");

            try
            {
                Console.Write("Введите ID книги (целое число): ");
                int id = int.Parse(Console.ReadLine());

                // Проверка на уникальность ID
                if (library.Books.Exists(b => b.Id == id))
                {
                    Console.WriteLine("Книга с таким ID уже существует.");
                    return;
                }

                Console.Write("Введите название книги: ");
                string title = Console.ReadLine();

                Console.Write("Введите автора книги: ");
                string author = Console.ReadLine();

                Console.Write("Введите год издания: ");
                int year = int.Parse(Console.ReadLine());

                Book book = new Book(id, title, author, year);
                library.AddBook(book);
            }
            catch (FormatException)
            {
                Console.WriteLine("Некорректный ввод. Пожалуйста, введите правильные данные.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        // Удаление книги.
        static void RemoveBook(Library library)
        {
            Console.WriteLine("=== Удаление Книги ===");

            try
            {
                Console.Write("Введите ID книги для удаления: ");
                int id = int.Parse(Console.ReadLine());

                library.RemoveBook(id);
            }
            catch (FormatException)
            {
                Console.WriteLine("Некорректный ввод. Пожалуйста, введите правильный ID.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        // Поиск книг.
        static void SearchBooks(Library library)
        {
            Console.WriteLine("=== Поиск Книг ===");
            Console.Write("Введите название или автора для поиска: ");
            string query = Console.ReadLine();

            var results = library.SearchBooks(query);
            if (results.Count == 0)
            {
                Console.WriteLine("Книги по заданному запросу не найдены.");
            }
            else
            {
                Console.WriteLine($"Найдено {results.Count} книг(а):");
                foreach (var book in results)
                {
                    Console.WriteLine(book);
                }
            }
        }

        // Добавление нового пользователя.
        static void AddUser(Library library)
        {
            Console.WriteLine("=== Добавление Пользователя ===");

            try
            {
                Console.Write("Введите ID пользователя (целое число): ");
                int id = int.Parse(Console.ReadLine());

                // Проверка на уникальность ID
                if (library.Users.Exists(u => u.Id == id))
                {
                    Console.WriteLine("Пользователь с таким ID уже существует.");
                    return;
                }

                Console.Write("Введите имя пользователя: ");
                string name = Console.ReadLine();

                Console.Write("Введите email пользователя: ");
                string email = Console.ReadLine();

                User user = new User(id, name, email);
                library.AddUser(user);
            }
            catch (FormatException)
            {
                Console.WriteLine("Некорректный ввод. Пожалуйста, введите правильные данные.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        // Удаление пользователя.
        static void RemoveUser(Library library)
        {
            Console.WriteLine("=== Удаление Пользователя ===");

            try
            {
                Console.Write("Введите ID пользователя для удаления: ");
                int id = int.Parse(Console.ReadLine());

                library.RemoveUser(id);
            }
            catch (FormatException)
            {
                Console.WriteLine("Некорректный ввод. Пожалуйста, введите правильный ID.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        // Выдача книги пользователю.
        static void BorrowBook(Library library)
        {
            Console.WriteLine("=== Выдача Книги ===");

            try
            {
                Console.Write("Введите ID книги: ");
                int bookId = int.Parse(Console.ReadLine());

                Console.Write("Введите ID пользователя: ");
                int userId = int.Parse(Console.ReadLine());

                library.BorrowBook(bookId, userId);
            }
            catch (FormatException)
            {
                Console.WriteLine("Некорректный ввод. Пожалуйста, введите правильные ID.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        // Возврат книги пользователем.
        static void ReturnBook(Library library)
        {
            Console.WriteLine("=== Возврат Книги ===");

            try
            {
                Console.Write("Введите ID книги: ");
                int bookId = int.Parse(Console.ReadLine());

                Console.Write("Введите ID пользователя: ");
                int userId = int.Parse(Console.ReadLine());

                library.ReturnBook(bookId, userId);
            }
            catch (FormatException)
            {
                Console.WriteLine("Некорректный ввод. Пожалуйста, введите правильные ID.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }
}
