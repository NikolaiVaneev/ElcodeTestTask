using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElcodeTestTask.Models
{
    /// <summary>
    /// Клиент
    /// </summary>
    public class Client
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        [Required(ErrorMessage = "Поле не может быть пустым")]
        public string Name { get; set; }
        /// <summary>
        /// Электронная почта
        /// </summary>
        [EmailAddress]
        public string Email { get; set; }
        /// <summary>
        /// Телефон
        /// </summary>
        [Phone]
        public string Phone { get; set; }

        public int StatusId { get; set; }
        /// <summary>
        /// Тип клиента
        /// </summary>
        [ForeignKey("StatusId")]
        public virtual ClientStatus? Status { get; set; }

        /// <summary>
        /// Запросы клиента
        /// </summary>
        public virtual IEnumerable<ClientRequest>? Requests { get; set; }
    }
}