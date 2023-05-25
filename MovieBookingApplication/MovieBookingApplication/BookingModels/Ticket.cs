using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MovieBookingApplication.BookingModels
{
    public class Ticket
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]

        public string TicketId { get; set; } = string.Empty;

        [BsonElement("MovieName")]
        public string MovieName { get; set; } = string.Empty;


        [BsonElement("TheatreName")]
        public string TheatreName { get; set; } = string.Empty;

        //TicketsBooked,TicketsAlloted.

        [BsonElement("NumberOfTicketsBooked")]
        public int NumberOfTicketsBooked { get; set; }
    }
}
