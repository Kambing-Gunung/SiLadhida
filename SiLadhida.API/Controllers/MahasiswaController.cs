using Microsoft.AspNetCore.Mvc;
using SiLadhida.Core.Models;

namespace SiLadhida.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MahasiswaController : ControllerBase
    {
        private static List<Mahasiswa> mahasiswaList = new List<Mahasiswa>
        {
            new Mahasiswa { Nama = "LeBron James", Nim = "1302000001" },
            new Mahasiswa { Nama = "Stephen Curry", Nim = "1302000002" },
        };

        // GET semua
        [HttpGet]
        public ActionResult<List<Mahasiswa>> GetAll()
        {
            return mahasiswaList;
        }

        // GET by index
        [HttpGet("{index}")]
        public ActionResult<Mahasiswa> GetByIndex(int index)
        {
            if (index < 0 || index >= mahasiswaList.Count)
                return NotFound();

            return mahasiswaList[index];
        }

        // POST tambah
        [HttpPost]
        public ActionResult AddMahasiswa(Mahasiswa mhs)
        {
            mahasiswaList.Add(mhs);
            return Ok();
        }

        // DELETE
        [HttpDelete("{index}")]
        public ActionResult DeleteMahasiswa(int index)
        {
            if (index < 0 || index >= mahasiswaList.Count)
                return NotFound();

            mahasiswaList.RemoveAt(index);
            return Ok();
        }
    }
} 