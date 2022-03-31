using System.ComponentModel.DataAnnotations;

namespace ElcodeTestTask.Models
{
    /// <summary>
    /// Тип клиентского запроса (консультация, заказ документов и прочее)
    /// </summary>
    public class ClientRequestType
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Базовый вес запроса
        /// </summary>
        public byte BaseRequestWeight { get; set; }
        /// <summary>
        /// Вес первичного запроса
        /// </summary>
        public byte InitialRequestWeight { get; set; }
        /// <summary>
        /// Вес уточняющего запроса
        /// </summary>
        public byte СlarificationRequestWeight { get; set; }
    }
}