using ReservaRestaurante.Domain.Entities;

namespace ReservaRestaurante.Domain.Services.ServiceBus
{
    public interface IDeleteUserQueue
    {
        Task SendMessage(User user);
    }
}
