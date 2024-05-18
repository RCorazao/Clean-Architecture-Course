

using Tarker.Booking.Domain.Entities.Booking;

namespace Tarker.Booking.Application.DataBase.User.Queries.GetUserByUserNameAndPassword
{
    public class GetUserByUserNameAndPasswordModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
    }
}
