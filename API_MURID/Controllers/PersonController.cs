using API_MURID.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace API_MURID.Controllers
{
    public class PersonController : Controller
    {
        private string __constr;
        private readonly IConfiguration _config;

        public PersonController(IConfiguration config)
        {
            __constr = config.GetConnectionString("WebApiDatabase");
            _config = config;
        }

        public IActionResult index()
        {
            return View();
        }
        [HttpPost("API/LOGIN")]
        public IActionResult LoginUser(string namaUser, string password)
        {
            var context = new PersonContext(_config.GetConnectionString("WebApiDatabase"));

            if (context.IsValidUser(namaUser, password))
            {
                return Ok(new { token = context.GenerateJwtToken(namaUser, _config) }); // Mengembalikan token
            }

            return Unauthorized();  // Respon Unauthorized jika login gagal
        }

        [Authorize]
        [HttpGet("api/READ")]

        public ActionResult<SISWA> ListPerson()
        {
            PersonContext context = new PersonContext(this.__constr);
            List<SISWA> ListPerson = context.ListPerson();
            return Ok(ListPerson);
        }

        [Authorize]
        [HttpPost("api/create")]
        public IActionResult CreateSiswa([FromBody] SISWA person)
        {
            PersonContext context = new PersonContext(this.__constr);
            context.AddSiswa(person);
            return Ok("MURID SUDAH DITAMBAHKAN");
        }

        [Authorize]
        [HttpPut("api/update/{id}")]
        public IActionResult UpdateSiswa(int id, [FromBody] SISWA person)
        {
            person.id_person = id;
            PersonContext context = new PersonContext(this.__constr);
            context.UpdateSiswa(person);
            return Ok("DATA MURID BERHASIL DI UPDATE");
        }

        [Authorize]
        [HttpDelete("api/delete/{id}")]
        public IActionResult DeleteSiswa(int id)
        {
            PersonContext context = new PersonContext(this.__constr);
            context.DeleteSiswa(id);
            return Ok("DATA BERHASIL DI HAPUS");
        }

    }
}
