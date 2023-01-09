using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AdAgency.Models;
using AdAgency.ViewModels;

namespace AdAgency.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdPlacesController : ControllerBase
    {
        private readonly AdAgencyContext _context;
        public AdPlacesController(AdAgencyContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        [Produces("application/json")]
        public List<AdPlaceViewModel> Get()
        {
            IQueryable<AdPlaceViewModel> ovm = _context.AdPlaces.Include(t => t.Type).Select(o =>
                new AdPlaceViewModel
                {
                    Id = o.Id,
                    Place = o.Place,
                    Desc = o.Description,
                    Cost = o.Cost,
                    TypeName = o.Type.Name,
                    TypeId = o.TypeId
                });
            return ovm.ToList();
        }


        [HttpGet("FilteredAdPlaces")]
        [Produces("application/json")]
        public List<AdPlaceViewModel> GetFilteredOperations(int TypeId)
        {
            IQueryable<AdPlaceViewModel> ovm = _context.AdPlaces.Include(t => t.Type).Select(o =>
                new AdPlaceViewModel
                {
                    Id = o.Id,
                    Place = o.Place,
                    Desc = o.Description,
                    Cost = o.Cost,
                    TypeName = o.Type.Name,
                    TypeId = o.TypeId
                });
            if (TypeId > 0)
            {
                ovm = ovm.Where(op => op.TypeId == TypeId);
            }
            return ovm.ToList();
        }


        [HttpGet("types")]
        [Produces("application/json")]
        public IEnumerable<AdType> GetFuels()
        {
            return _context.AdTypes.ToList();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            AdPlace adPlace = _context.AdPlaces.FirstOrDefault(x => x.Id == id);
            if (adPlace == null)
                return NotFound();
            return new ObjectResult(adPlace);
        }

        [HttpPost]
        public IActionResult Post([FromBody] AdPlace adPlace)
        {
            if (adPlace == null)
            {
                return BadRequest();
            }

            _context.AdPlaces.Add(adPlace);
            _context.SaveChanges();
            return Ok(adPlace);
        }

        [HttpPut]
        public IActionResult Put([FromBody] AdPlace adPlace)
        {
            if (adPlace == null)
            {
                return BadRequest();
            }
            if (!_context.AdPlaces.Any(x => x.Id == adPlace.Id))
            {
                return NotFound();
            }

            _context.Update(adPlace);
            _context.SaveChanges();


            return Ok(adPlace);
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            AdPlace adPlace = _context.AdPlaces.FirstOrDefault(x => x.Id == id);
            if (adPlace == null)
            {
                return NotFound();
            }
            _context.AdPlaces.Remove(adPlace);
            _context.SaveChanges();
            return Ok(adPlace);
        }
    }
}