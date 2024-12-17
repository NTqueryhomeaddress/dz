using System;

namespace LibraryManagementSystem
{
    // Класс, представляющий запись о выдаче книги пользователю.
    public class BorrowRecord
    {
        // Уникальный идентификатор записи
        public int RecordId { get; set; }

        // Идентификатор книги
        public int BookId { get; set; }

        // Идентификатор пользователя
        public int UserId { get; set; }

        // Дата выдачи книги
        public DateTime BorrowDate { get; set; }

        // Дата возврата книги
        public DateTime? ReturnDate { get; set; }

        // Конструктор по умолчанию
        public BorrowRecord() { }

        // Конструктор с параметрами
        public BorrowRecord(int recordId, int bookId, int userId)
        {
            RecordId = recordId;
            BookId = bookId;
            UserId = userId;
            BorrowDate = DateTime.Now;
            ReturnDate = null;
        }

        // Переопределение метода ToString для удобного вывода информации о записи
        public override string ToString()
        {
            string returnDateStr = ReturnDate.HasValue ? ReturnDate.Value.ToString() : "Не возвращена";
            return $"Record ID: {RecordId}, Book ID: {BookId}, User ID: {UserId}, Borrow Date: {BorrowDate}, Return Date: {returnDateStr}";
        }
    }
}
