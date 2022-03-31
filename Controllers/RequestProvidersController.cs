using ElcodeTestTask.Data;
using ElcodeTestTask.Models.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElcodeTestTask.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class RequestProvidersController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public RequestProvidersController(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Получить данные по поставщику данных
        /// </summary>
        /// <param name="id">ИД</param>
        /// <returns></returns>
        /// <remarks>
        /// 
        /// Получение всех типов:
        /// RequestProviders/Get/
        /// 
        /// Получение конкретного типа:
        /// RequestProviders/Get/1
        /// 
        /// </remarks>
        [Route("Get/{id?}")]
        [HttpGet]
        public async Task<IActionResult> Get(int? id = null)
        {
            if (id == null)
            {
                return Ok(_db.ClientRequestType);
            }
            else
            {
                var obj = _db.ClientStatus.Find(id);
                if (obj == null)
                {
                    return NotFound("Тип запроса не найден");
                }
                else
                {
                    return Ok(obj);
                }

            }
        }

        /// <summary>
        /// Создать новый тип поставщика данных
        /// </summary>
        /// <param name="type">Тип</param>
        /// <returns></returns>
        /// <remarks>
        /// Пример передаваемого объекта:
        ///
        ///     {
        ///        "name": "Phone"
        ///     }
        ///
        /// </remarks>
        [HttpPost]
        [Route("Add")]
        
        public async Task<IActionResult> AddRequestProviders(RequestProviderType type)
        {
            if (_db.RequestProviderType.Where(u => u.Name == type.Name).Any())
            {
                return BadRequest("Такой тип уже существует");
            }

            _db.RequestProviderType.Add(type);
            await _db.SaveChangesAsync();

            return Ok(type);
        }

        /// <summary>
        /// Обновить текущий тип поставщика данных
        /// </summary>
        /// <param name="type">Тип</param>
        /// <returns></returns>
        /// <remarks>
        /// Пример передаваемого объекта:
        ///
        ///     {
        ///        "id": 1,
        ///        "name": "API"
        ///     }
        ///
        /// </remarks>
        [HttpPut]
        [Route("Update")]
        
        public async Task<IActionResult> UpdateRequestProviders(RequestProviderType type)
        {

            if (_db.RequestProviderType.AsNoTracking().Where(u => u.Id == type.Id).FirstOrDefault() == null)
            {
                return NotFound("Тип поставщика данных не найден");
            }

            _db.RequestProviderType.Update(type);
            await _db.SaveChangesAsync();

            return Ok(type);
        }

        /// <summary>
        /// Удалить тип поставщика данных
        /// </summary>
        /// <param name="id">ИД</param>
        /// <returns></returns>
        /// <remarks>
        /// Пример передаваемого объекта:
        ///
        ///     {
        ///        "id": 1
        ///     }
        ///
        /// </remarks>
        [HttpDelete]
        [Route("Remove")]
        
        public async Task<IActionResult> RemoveClientStatus(int id)
        {
            var obj = _db.RequestProviderType.Where(u => u.Id == id).FirstOrDefault();

            if (obj == null)
            {
                return NotFound("Тип не найден");
            }

            _db.RequestProviderType.Remove(obj);
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}
