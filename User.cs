using System;

namespace LibraryManagementSystem
{
    // Класс, представляющий пользователя библиотеки.
    public class User
    {
        // Уникальный идентификатор пользователя
        public int Id { get; set; }

        // Имя пользователя
        public string Name { get; set; }

        // Электронная почта
        public string Email { get; set; }

        // Конструктор по умолчанию
        public User() { }

        // Конструктор с параметрами
        public User(int id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }

        // Переопределение метода ToString для удобного вывода информации о пользователе
        public override string ToString()
        {
            return $"ID: {Id}, Имя: {Name}, Email: {Email}";
        }
    }
}
