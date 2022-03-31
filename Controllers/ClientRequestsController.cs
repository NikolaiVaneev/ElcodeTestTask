using ElcodeTestTask.Data;
using ElcodeTestTask.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElcodeTestTask.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ClientRequestsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public ClientRequestsController(ApplicationDbContext context)
        {
            _db = context;
        }

        /// <summary>
        /// Получить данные по запросу(ам)
        /// </summary>
        /// <param name="id">ИД</param>
        /// <returns></returns>
        /// <remarks>
        /// 
        /// Получение всех запросов:
        /// ClientRequests/Get/
        /// 
        /// Получение конкретного запросов:
        /// ClientRequests/Get/1
        /// 
        /// </remarks>
        [HttpGet("Get/{id?}")]
        public async Task<ActionResult> GetClientRequest(int? id)
        {
            if (id == null)
            {
                var requests = _db.ClientRequest
                    .Include(u => u.Client).ThenInclude(c => c.Status)
                    .Include(u => u.ClientRequestType)
                    .Include(u => u.RequestProvider)
                    .OrderByDescending(u => u.CreatedDate);
                return Ok(requests);
            }
            else
            {
                var clientRequest = _db.ClientRequest
                    .Include(u => u.Client).ThenInclude(c => c.Status)
                    .Include(u => u.ClientRequestType)
                    .Include(u => u.RequestProvider)
                    .Where(u => u.Id == id).FirstOrDefault();

                if (clientRequest == null)
                {
                    return NotFound("Данный запрос не найден");
                }
                else
                {
                    return (Ok(clientRequest));
                }
            }
        }

        /// <summary>
        /// Создать новый запрос
        /// </summary>
        /// <param name="request">Запрос</param>
        /// <returns></returns>
        /// <remarks>
        /// Пример передаваемого объекта:
        ///
        ///     {
        ///        "requestProviderId": 1,
        ///        "clientid": 1,
        ///        "clientRequestTypeId": 1,
        ///        "question": "Описание запроса"
        ///     }
        ///
        /// Уточняющий запрос:
        /// 
        ///     {
        ///        "requestProviderId": 1,
        ///        "clientid": 1,
        ///        "clientRequestTypeId": 1,
        ///        "parrentRequestId": 1,
        ///        "question": "Описание запроса"
        ///     }
        /// </remarks>
        [HttpPost]
        [Route("Add")]
        
        public async Task<IActionResult> AddRequest(ClientRequest request)
        {
            //TODO: Проверки


            _db.ClientRequest.Add(request);
            await _db.SaveChangesAsync();

            return Ok(request);
        }
    }
}
