using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TaxiReservation.Data;
using TaxiReservation.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TaxiReservation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxiReservationController : ControllerBase
    {
        private readonly TaxiContext _context;

        public TaxiReservationController(TaxiContext context)
        {
            _context = context;
        }

        // GET api/[controller]/cars
        [HttpGet("cars")]
        [ProducesResponseType(typeof(IEnumerable<Taxi>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get()
        {
            var allCars = await _context.Cars.ToListAsync();

            if (allCars == null )
            {
                NotFound("Cars not found");
            }

            return Ok(allCars);
        }


        // POST api/[controller]/{carId}/{phone}/{time}
        [HttpGet("{carId}/{phone}/{time}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Reservation(int carId, double phone, DateTime time)
        {
            var car = await _context.Cars.FirstOrDefaultAsync(c => c.Id == carId);

            if (car == null)
            {
                NotFound("Car not exist");
            }

            if (car.IsReserved)
            {
                BadRequest("Car already reserved");
            }

            car.IsReserved = true;
            car.ClientPhoneNumber = phone;
            car.TimeReservation = time;

            _context.Cars.Update(car);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // POST api/[controller]/{carId}/{phone}/{time}/remove
        [HttpGet("{carId}/{phone}/{time}/remove")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RemoveReservation(int carId, double phone)
        {
            var car = await _context.Cars.FirstOrDefaultAsync(c => c.Id == carId);

            if (car == null)
            {
                NotFound("Car not exist");
            }

            if (!car.IsReserved && car.ClientPhoneNumber != phone)
            {
                BadRequest("Error. No rights");
            }

            car.IsReserved = false;
            car.ClientPhoneNumber = null;
            car.TimeReservation = null;

            _context.Cars.Update(car);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // GET api/[controller]/reservations/{phone}
        [HttpGet("reservations/{phone}")]
        [ProducesResponseType(typeof(IEnumerable<Taxi>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetReservations(double phone)
        {
            var reservations = await _context.Cars.Where(c => c.ClientPhoneNumber == phone).ToListAsync();

            if (reservations == null)
            {
                NotFound("Reservations not found");
            }

            return Ok(reservations);
        }
    }
}