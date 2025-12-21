namespace RoomFlow.Models
{
    public class ChangeLog
    {
        public int Id { get; set; }
        public string UserName { get; set; }      // Кто сделал изменение
        public string Action { get; set; }        // Действие
        public string EntityName { get; set; }    // Сущность (например, Employee, Room)
        public DateTime Timestamp { get; set; }   // Время изменения
        public string Description { get; set; }   // Дополнительные детали
    }
}
