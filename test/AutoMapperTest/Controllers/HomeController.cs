using AutoMapper;
using AutoMapperTest.Models;
using Contact_zoo_at_home.Core.Entities.Pets;
using Contact_zoo_at_home.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AutoMapperTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;

        public HomeController(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            Pet? pet;
            using(var dbContext = new ApplicationDbContext())
            {
                pet = dbContext.Pets.Include(src => src.Owner).FirstOrDefault();
            }
            if (pet == null) { throw new Exception(); }

            //var simpleDto = _mapper.Map<SimplePetDTO>(pet);
            //return Ok(simpleDto);

            var complexDto = _mapper.Map<ComplexPetDTO>(pet);
            return Ok(complexDto);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
