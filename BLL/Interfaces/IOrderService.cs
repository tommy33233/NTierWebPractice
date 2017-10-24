using System.Collections.Generic;
using BLL.DTO;

namespace BLL.Interfaces
{
   public interface IOrderService
   {
       void MakeOrder(OrderDTO orderDto);
       PhoneDTO GetPhone(int? id);
       IEnumerable<PhoneDTO> GetPhones();
       void Dispose();
   }
}
