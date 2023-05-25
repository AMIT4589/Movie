namespace MovieBookingApplication.BookingModels.DataTransferObjects
{
    public class TicketDataTransferObject
    {

        public string MovieName { get; set; } = string.Empty;


        public string TheatreName { get; set; } = string.Empty;


        public int NumberOfTicketsBooked { get; set; }
    }
}

