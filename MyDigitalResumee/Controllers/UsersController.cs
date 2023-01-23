using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyDigitalResumee.Api.Model;
using MyDigitalResumee.Model.Entity;

namespace MyDigitalResumee.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly MyDigitalResumeeContext _context;

        public UsersController(MyDigitalResumeeContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Carregar todos os usuários existentes do sistema
        /// </summary>
        // GET: api/Users
        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        #region GetByID Method
        /// <summary>
        /// Carregar usuário de acordo com o ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }
        #endregion

        #region PUT Method
        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        #endregion

        #region POST Method
        /// <summary>
        /// Inserir o usuário no banco de dados.
        /// </summary>
        /// <returns>Retorna o usuário recém inserido no banco</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /User
        ///      {
        ///         "id": 0,
        ///         "name": "string",
        ///         "email": "string",
        ///         "password": "string",
        ///         "gender": 0, // 0 = masculino, 1 = feminino, 2 = não informado
        ///         "cpf": "string",
        ///         "address": "string",
        ///         "state": "string",
        ///         "country": "string",
        ///         "office": "string",
        ///         "salaryClaim": 2500.00
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Retorna o usuário recém inserido no banco</response>
        /// <response code="400">Se o objeto estiver vazio</response>
        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }
        #endregion


        /// <summary>
        /// Deletar usuário específico
        /// </summary>
        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Realizar login no sistema passando o email e senha
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <response code="200">Retorna o e-mail e senha do usuário</response>
        /// <response code="400">Nenhum usuário encontrado</response>
        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(string email, string password)
        {
            try
            {
                var user = await _context.Users.SingleAsync<User>(u => u.Email == email && u.Password == password);
                if (user is null)
                {
                    return NotFound();
                }

                return user;
            }
            catch (Exception ex) 
            {
                return BadRequest("Email e/ou senha incorreto/s");
            }
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
