

namespace Tarker.Booking.Application.DataBase.User.Commands.DeleteUser
{
    public interface IDeleteUserCommand
    {
        Task<bool> Excecute(int userId);
    }
}
