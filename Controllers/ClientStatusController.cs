using ElcodeTestTask.Data;
using ElcodeTestTask.Models;
using ElcodeTestTask.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElcodeTestTask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class ClientStatusController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public ClientStatusController(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Получить данные по статусу(ам) пользователей
        /// </summary>
        /// <param name="id">ИД</param>
        /// <returns></returns>
        /// <remarks>
        /// 
        /// Получение всех статусов:
        /// ClientStatus/Get/
        /// 
        /// Получение конкретного статуса:
        /// ClientStatus/Get/1
        /// 
        /// </remarks>
        [Route("Get/{id?}")]
        [HttpGet]
        public async Task<IActionResult> Get(int? id = null)
        {
            if (id == null)
            {
                return Ok(_db.ClientStatus);
            }
            else
            {
                var obj = _db.ClientStatus.Find(id);
                if (obj == null)
                {
                    return NotFound("Статус пользователя не найден");
                }
                else
                {
                    return Ok(obj);
                }

            }
        }

        /// <summary>
        /// Создать новый статус
        /// </summary>
        /// <param name="clientStatus">Cтатус</param>
        /// <returns></returns>
        /// <remarks>
        /// Пример передаваемого объекта:
        ///
        ///     {
        ///        "name": "user",
        ///        "weight": 1
        ///     }
        ///
        /// </remarks>
        [HttpPost]
        [Route("Add")]
        
        public async Task<IActionResult> AddClientStatus(ClientStatus clientStatus)
        {
            if (_db.ClientStatus.Where(u => u.Name == clientStatus.Name).Any())
            {
                return BadRequest("Такой статус пользователя уже существует");
            }

            _db.ClientStatus.Add(clientStatus);
            await _db.SaveChangesAsync();

            return Ok(clientStatus);
        }

        /// <summary>
        /// Обновить текущий статус
        /// </summary>
        /// <param name="clientStatus">Cтатус</param>
        /// <returns></returns>
        /// <remarks>
        /// Пример передаваемого объекта:
        ///
        ///     {
        ///        "id": 1,
        ///        "name": "keyuser",
        ///        "weight": 2
        ///     }
        ///
        /// </remarks>
        [HttpPut]
        [Route("Update")]
        
        public async Task<IActionResult> UpdateClientStatus(ClientStatus clientStatus)
        {

            if (_db.ClientStatus.AsNoTracking().Where(u => u.Id == clientStatus.Id).FirstOrDefault() == null)
            {
                return NotFound("Статус пользователя не найден");
            }

            _db.ClientStatus.Update(clientStatus);
            await _db.SaveChangesAsync();

            return Ok(clientStatus);
        }

        /// <summary>
        /// Удалить статус
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
            var obj = _db.ClientStatus.Where(u => u.Id == id).FirstOrDefault();

            if (obj == null)
            {
                return NotFound("Статус пользователя не найден");
            }

            _db.ClientStatus.Remove(obj);
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}
