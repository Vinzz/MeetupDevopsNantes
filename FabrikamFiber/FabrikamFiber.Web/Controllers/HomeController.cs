namespace FabrikamFiber.Web.Controllers
{
    using System.Web.Mvc;

    using FabrikamFiber.DAL.Data;
    using FabrikamFiber.Web.ViewModels;
    using System.Collections.Generic;
    using DAL.Models;

    public class HomeController : Controller
    {
        private readonly IServiceTicketRepository serviceTickets;
        private readonly IMessageRepository messageRepository;
        private readonly IAlertRepository alertRepository;
        private readonly IScheduleItemRepository scheduleItemRepository;

        public HomeController(
                              IServiceTicketRepository serviceTickets,
                              IMessageRepository messageRepository,
                              IAlertRepository alertRepository,
                              IScheduleItemRepository scheduleItemRepository)
        {
            this.serviceTickets = serviceTickets;
            this.messageRepository = messageRepository;
            this.alertRepository = alertRepository;
            this.scheduleItemRepository = scheduleItemRepository;
        }

        public ActionResult Index()
        {
            var viewModel = new DashboardViewModel
            {
                ScheduleItems = new List<ScheduleItem>(),
                Messages = new List<Message>(),
                Alerts = new List<Alert>(),
                Tickets = new List<ServiceTicket>(),
            };

            return View(viewModel);
        }
    }
}
