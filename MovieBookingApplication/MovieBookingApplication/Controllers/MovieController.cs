using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieBookingApplication.BookingModels;
using MovieBookingApplication.BookingModels.DataTransferObjects;
using MovieBookingApplication.BookingRepositories.Interfaces;

namespace MovieBookingApplication.Controllers
{
    [Route("api/v1.0/moviebooking")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieInterface _MovieRepository;
        private readonly ITicketInterface _TicketRepository;
        public MoviesController(IMovieInterface movieRepository, ITicketInterface ticketRepository)
        {
            _MovieRepository = movieRepository;
            _TicketRepository = ticketRepository;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public ActionResult<List<Movie>> Get()
        {
            return Ok(_MovieRepository.Get());
        }


        [HttpGet("movies/search/{moviename}")]
        public ActionResult<Movie> GetMovieInfo(string moviename)
        {
            var student = _MovieRepository.Get(moviename);
            if (student == null) return NotFound($"Movie with ID '${moviename}' not found!");
            return student;
        }


        /*
         * [HttpPost("add/{movieName}")]
        public ActionResult<Movie> Post(string movieName, [FromBody] Movie movie)
        {

            if (movieName != movie.MovieName) { return BadRequest(); }
            _MovieRepository.Create(movie);
            return CreatedAtAction(nameof(Get), new { id = movie.MovieId }, movie);
        }

         */
        [HttpPost("add/{movieName}")]
        public ActionResult<Movie> Post(string movieName, string theatreName, int numberOfTickets)
        {
            var movie = _MovieRepository.Exists(movieName, theatreName);
            if (movie == null) return NotFound();
            movie.NumberOfTicketsBooked = movie.NumberOfTicketsBooked + numberOfTickets;
            movie.TotalTicketsAlloted = movie.TotalTicketsAlloted - numberOfTickets;
            if (movie.TotalTicketsAlloted < 0) return Content("Housefull...cannot book these many tickets.");
            Ticket ticket = new Ticket()
            {
                MovieName = movieName,
                TheatreName = theatreName,
                NumberOfTicketsBooked = numberOfTickets

            };
            _TicketRepository.Create(ticket);
            _MovieRepository.Update(movie.MovieId, movie);
            return Ok();
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("updatemoviebyadmin/{id}")]
        public ActionResult UpdateTheMovieTable(string id, [FromBody] MovieDataTransferObject movie)
        {
            var existingMovie = _MovieRepository.GetMovie(id);
            if (existingMovie == null) return NotFound($"Movie with ID '${id}' not found!");
            Movie movieResult = new Movie()
            {
                MovieId = existingMovie.MovieId,
                MovieName = movie.MovieName,
                TheatreName = movie.TheatreName,
                NumberOfTicketsBooked = movie.NumberOfTicketsBooked,
                TotalTicketsAlloted = movie.TotalTicketsAlloted

            };
            _MovieRepository.Update(id, movieResult);
            return CreatedAtAction(nameof(Get), new { id = existingMovie.MovieId }, movieResult);
        }

        /*
         [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] Movie movie)
        {
            var existingMovie = _MovieRepository.GetMovie(id);
            if (existingMovie == null) return NotFound($"Student with ID '${id}' not found!");
            _MovieRepository.Update(id, movie);
            return CreatedAtAction(nameof(Get), new { id = existingMovie.MovieId }, movie);
        }
         */

        /*
          [HttpPut("{id}")]
         public ActionResult Put(string id, [FromBody] Movie movie)
         {
             var existingMovie = _MovieRepository.GetMovie(id);
             if (existingMovie == null) return NotFound($"Student with ID '${id}' not found!");
             _MovieRepository.Update(id, movie);
             return CreatedAtAction(nameof(Get), new { id = existingMovie.MovieId }, movie);
         }
         */
        /*
          [HttpPut("{id}")]
         public ActionResult Put(string id, [FromBody] MovieDataTransferObject movie)
         {
             var existingMovie = _MovieRepository.GetMovie(id);
             if (existingMovie == null) return NotFound($"Movie with ID '${id}' not found!");
             Movie movieResult = new Movie()
             {
                 MovieId = existingMovie.MovieId,
                 MovieName = movie.MovieName,
                 TheatreName = movie.TheatreName,
                 NumberOfTicketsBooked = movie.NumberOfTicketsBooked,
                 TotalTicketsAlloted = movie.TotalTicketsAlloted

             };
             _MovieRepository.Update(id, movieResult);
             return CreatedAtAction(nameof(Get), new { id = existingMovie.MovieId }, movieResult);
         }
         */
        //[HttpPut("{id}")]
        [HttpPut("{moviename}/update/{ticketId}")]
        public ActionResult Put(string moviename, string ticketId, [FromBody] TicketDataTransferObject ticket)
        {
            var existingTicket = _TicketRepository.GetMovie(ticketId);
            if (existingTicket == null) return Content("No such ticket found.");

            Ticket ticketResult = new Ticket()
            {
                TicketId = ticketId,
                MovieName = moviename,
                TheatreName = existingTicket.TheatreName,
                // NumberOfTicketsBooked = existingTicket.NumberOfTicketsBooked
                NumberOfTicketsBooked = ticket.NumberOfTicketsBooked
            };

            _TicketRepository.Update(ticketId, ticketResult);
            return CreatedAtAction(nameof(Get), new { id = existingTicket.TicketId }, ticketResult);
            //return Ok();
        }

        [HttpDelete("{moviename}/delete/{id}")]
        public ActionResult Delete(string id, string moviename)
        {
            var existingStudent = _MovieRepository.GetMovie(id);
            if (existingStudent == null) return NotFound($"Movie with ID '${id}' not found!");
            if (existingStudent.MovieName != moviename) return Content("No such movie found");
            _MovieRepository.Delete(id);
            return StatusCode(204, $"Movie with ID '${id}' deleted.");
        }
    }
}
