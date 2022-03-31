using System.ComponentModel.DataAnnotations;

namespace ElcodeTestTask.Models
{
    /// <summary>
    /// Тип клиента
    /// </summary>
    public class ClientStatus
    {
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Вес типа клиента
        /// </summary>
        public byte Weight { get; set; }
    }
}