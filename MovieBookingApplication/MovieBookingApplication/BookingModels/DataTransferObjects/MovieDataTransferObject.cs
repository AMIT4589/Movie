namespace MovieBookingApplication.BookingModels.DataTransferObjects
{
    public class MovieDataTransferObject
    {


        public string MovieName { get; set; } = string.Empty;


        public string TheatreName { get; set; } = string.Empty;


        public int TotalTicketsAlloted { get; set; }

        public int NumberOfTicketsBooked { get; set; }
    }
}
