using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
