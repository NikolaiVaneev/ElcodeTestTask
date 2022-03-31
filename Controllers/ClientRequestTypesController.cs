using ElcodeTestTask.Data;
using ElcodeTestTask.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElcodeTestTask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class ClientRequestTypesController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public ClientRequestTypesController(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Получить данные по типу запросов
        /// </summary>
        /// <param name="id">ИД</param>
        /// <returns></returns>
        /// <remarks>
        /// 
        /// Получение всех типов:
        /// ClientRequestTypes/Get/
        /// 
        /// Получение конкретного типа:
        /// ClientRequestTypes/Get/1
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
        /// Создать новый тип запроса
        /// </summary>
        /// <param name="type">Тип</param>
        /// <returns></returns>
        /// <remarks>
        /// Пример передаваемого объекта:
        ///
        ///     {
        ///        "name": "Консультация",
        ///        "baseRequestWeight": 1,
        ///        "initialRequestWeight": 1,
        ///        "clarificationRequestWeight": 1
        ///     }
        ///
        /// </remarks>

        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> AddRequestType(ClientRequestType type)
        {
            if (_db.ClientRequestType.Where(u => u.Name == type.Name).Any())
            {
                return BadRequest("Такой тип запроса уже существует");
            }

            _db.ClientRequestType.Add(type);
            await _db.SaveChangesAsync();

            return Ok(type);
        }

        /// <summary>
        /// Обновить текущий тип запроса
        /// </summary>
        /// <param name="type">Тип</param>
        /// <returns></returns>
        /// <remarks>
        /// Пример передаваемого объекта:
        ///
        ///     {
        ///        "id": 1,
        ///        "name": "Консультация",
        ///        "baseRequestWeight": 1,
        ///        "initialRequestWeight": 2,
        ///        "clarificationRequestWeight": 3
        ///     }
        ///
        /// </remarks>
        [HttpPut]
        [Route("Update")]
        
        public async Task<IActionResult> UpdateClientStatus(ClientRequestType type)
        {

            if (_db.ClientRequestType.AsNoTracking().Where(u => u.Id == type.Id).FirstOrDefault() == null)
            {
                return NotFound("Тип запроса не найден");
            }

            _db.ClientRequestType.Update(type);
            await _db.SaveChangesAsync();

            return Ok(type);
        }

        /// <summary>
        /// Удалить тип запроса
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
            var obj = _db.ClientRequestType.Where(u => u.Id == id).FirstOrDefault();

            if (obj == null)
            {
                return NotFound("Тип запроса не найден");
            }

            _db.ClientRequestType.Remove(obj);
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}
