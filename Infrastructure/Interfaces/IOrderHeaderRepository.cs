using Infrastructure.Models;

namespace Infrastructure.Interfaces;

public interface IOrderHeaderRepository<T> : IGenericRepository<OrderHeader>
{
    void UpdateStatus(int id, string orderStatus, string paymentStatus = null);       
}
