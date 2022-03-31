using ElcodeTestTask.Models.Request;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElcodeTestTask.Models
{
    /// <summary>
    /// Клиентский запрос
    /// </summary>
    public class ClientRequest
    {
        public ClientRequest()
        {
            CreatedDate = DateTime.Now;
        }
  
        [Key]
        public int Id { get; set; }

        public int RequestProviderId { get; set; }
        [ForeignKey("RequestProviderId")]
        /// <summary>
        /// Канал
        /// </summary>
        public RequestProviderType? RequestProvider { get; set; }

        /// <summary>
        /// Дата и время создания запроса
        /// </summary>
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// Описание вопроса
        /// </summary>
        public string Question { get; set; }
        /// <summary>
        /// Описание ответа
        /// </summary>
        public string? Answer { get; set; }

        /// <summary>
        /// Приоритет
        /// </summary>
        [NotMapped]
        public int Priority
        {
            get
            {
                // HACK: При создании объекта из-за пустых свойств ошибка 
                if (Client != null)
                {
                    if (ParrentRequestId == null)
                    {
                        return Client.Status.Weight + ClientRequestType.BaseRequestWeight + ClientRequestType.InitialRequestWeight;
                    }
                    else
                    {
                        return Client.Status.Weight + ClientRequestType.BaseRequestWeight + ClientRequestType.СlarificationRequestWeight;
                    }
                }
                else
                {
                    return 0;
                }
            }
        }

        public int ClientId { get; set; }
        [ForeignKey("ClientId")]
        public virtual Client? Client { get; set; }

        public int ClientRequestTypeId { get; set; }
        [ForeignKey("ClientRequestTypeId")]
        public virtual ClientRequestType? ClientRequestType { get; set; }

        public int? ParrentRequestId { get; set; }
        [ForeignKey("ParrentRequestId")]
        public virtual ClientRequest? ParrentRequest { get; set; }

    }
}
