using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using WebPractice.Models;

namespace WebPractice.Controllers
{
    public class HomeController : Controller
    {
        private IOrderService orderService;

        public HomeController(IOrderService serv)
        {
            orderService = serv;
        }

        public ActionResult Index()
        {
            IEnumerable<PhoneDTO> phoneDtos = orderService.GetPhones();
            Mapper.Initialize(cfg=>cfg.CreateMap<PhoneDTO,PhoneViewModel>());
            var phones = Mapper.Map<IEnumerable < PhoneDTO>, List <PhoneViewModel>>(phoneDtos);
            return View(phones);
        }

        public ActionResult MakeOrder(int? id)
        {
            try
            {
                PhoneDTO phone = orderService.GetPhone(id);
                Mapper.Initialize(
                    cfg =>
                        cfg.CreateMap<PhoneDTO, OrderViewModel>()
                            .ForMember("PhoneId", opt => opt.MapFrom(src => src.Id)));
                var order = Mapper.Map<PhoneDTO, OrderViewModel>(phone);
                return View(order);
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult MakeOrder(OrderViewModel order)
        {
            try
            {
                Mapper.Initialize(cfg => cfg.CreateMap<OrderViewModel, OrderDTO>());
                var orderDto = Mapper.Map<OrderViewModel, OrderDTO>(order);
                orderService.MakeOrder(orderDto);
                return Content("<h2>Ваш заказ успешно оформлен</h2>");
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Source, ex.Message);
            }
            return View(order);
        }
        protected override void Dispose(bool disposing)
        {
            orderService.Dispose();
            base.Dispose(disposing);
        }
    }
}