using System;
using System.Collections.Generic;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using BLL.DTO;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using BLL.BusinessModels;

namespace BLL.Services
{
    public class OrderService:IOrderService
    {
        private IUnitOfWork database { get; set; }

        public OrderService(IUnitOfWork unitOfWork)
        {
            database = unitOfWork;
        }

        public void MakeOrder(OrderDTO orderDto)
        {
            Phone phone = database.Phones.Get(orderDto.PhoneId);

            if(phone==null)
                throw new ValidationException("Phone was not founded");


            decimal sum =new Discount(0.1m).GetDiscountedPrice(phone.Price);

            Order order = new Order
            {
                Date = DateTime.Now,
                Address = orderDto.Address,
                PhoneId = phone.Id,
                Sum = sum,
                PhoneNumber = orderDto.PhoneNumber
            };

            database.Orders.Create(order);
            database.Save();
        }

        public IEnumerable<PhoneDTO> GetPhones()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Phone, PhoneDTO>());
            return Mapper.Map<IEnumerable<Phone>, List<PhoneDTO>>(database.Phones.GetAll());
        }

        public PhoneDTO GetPhone(int? id)
        {
            if(id==null)
                throw new ValidationException("Incorrect telephone id");

            var phone = database.Phones.Get(id.Value);
            if(phone==null)
                throw new ValidationException("Phone was not founded");

            Mapper.Initialize(cfg=>cfg.CreateMap<Phone,PhoneDTO>());
            return Mapper.Map<Phone, PhoneDTO>(phone);
        }

        public void Dispose()
        {
            database.Dispose();
        }
    }
}
