using ElcodeTestTask.Data;
using ElcodeTestTask.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElcodeTestTask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class ClientController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public ClientController(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Получить данные пользователя(ей)
        /// </summary>
        /// <param name="id">ИД</param>
        /// <returns></returns>
        /// <remarks>
        /// 
        /// Получение всех клиентов:
        /// Client/Get/
        /// 
        /// Получение конкретного клиенту:
        /// Client/Get/1
        /// 
        /// </remarks>
        [Route("Get/{id?}")]
        [HttpGet]
        public async Task<IActionResult> Get(int? id = null)
        {
            if (id == null)
            {
                return Ok(_db.Client.Include(u => u.Status));
            }
            else
            {
                var obj = _db.Client.Include(u => u.Status).Where(u => u.Id == id);
                if (obj == null)
                {
                    return NotFound("Пользователь не найден");
                }
                else
                {
                    return Ok(obj);
                }

            }
        }

        /// <summary>
        /// Создать нового пользователя
        /// </summary>
        /// <param name="client">Клиент</param>
        /// <returns></returns>
        /// <remarks>
        /// Пример передаваемого объекта:
        ///
        ///     {
        ///        "name": "username",
        ///        "email": "username@gmail.com",
        ///        "phone": "+79000000001",
        ///        "statusid" : 1
        ///     }
        ///
        /// </remarks>
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddClient(Client client)
        {
            if (_db.Client.Where(u => u.Email == client.Email || u.Phone == client.Phone).Any())
            {
                return BadRequest("Такой пользователь уже существует");
            }

            if (_db.ClientStatus.Find(client.StatusId) == null)
            {
                return NotFound("Указанный статус пользователя не найден");
            }

            _db.Client.Add(client);
            await _db.SaveChangesAsync();

            return Ok(client);
        }

        /// <summary>
        /// Изменить данные пользователя
        /// </summary>
        /// <param name="client">Клиент</param>
        /// <returns></returns>
        /// <remarks>
        /// Пример передаваемого объекта:
        ///
        ///     {
        ///        "Id": 1,
        ///        "name": "Иванов И.И.",
        ///        "email": "username@gmail.com",
        ///        "phone": "+96666666666",
        ///        "clientstatusid": 1
        ///     }
        ///
        /// </remarks>
        [HttpPut]
        [Route("Update")]

        public async Task<IActionResult> UpdateClient(Client client)
        {
            if (_db.Client.Find(client.Id) == null)
            {
                return NotFound("Такой пользователь не найден");
            }

            if (_db.Client.Where(u => u.Id != client.Id && (u.Email == client.Email || u.Phone == client.Phone)).Any())
            {
                return BadRequest("Такой пользователь уже существует");
            }

            if (_db.ClientStatus.Find(client.StatusId) == null)
            {
                return NotFound("Такой статус пользователя не найден");
            }

            _db.Client.Update(client);
            await _db.SaveChangesAsync();

            return Ok(client);
        }

        /// <summary>
        /// Удалить клиента
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

        public async Task<IActionResult> RemoveClient(int id)
        {
            var obj = _db.Client.Find(id);
            if (obj == null)
            {
                return NotFound("Клиент не найден");
            }

            _db.Client.Remove(obj);
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}
