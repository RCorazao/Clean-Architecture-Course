
namespace Tarker.Booking.Application.DataBase.User.Commands.UpdateUserPassword
{
    public interface IUpdateUserPasswordCommand
    {
        Task<bool> Excecute(UpdateUserPasswordModel model);
    }
}
