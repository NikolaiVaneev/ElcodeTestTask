using ElcodeTestTask.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ElcodeTestTask.Models.Request
{
    public class RequestProviderType : IProvider
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        
        public virtual ClientRequest GetRequest()
        {
            return new ClientRequest();
        }
    }
}
