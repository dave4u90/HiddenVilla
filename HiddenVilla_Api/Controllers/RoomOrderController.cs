using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Repository.IRepository;
using Common;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Models;
using Stripe.Checkout;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HiddenVilla_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class RoomOrderController : Controller
    {
        private readonly IRoomOrderDetailsRepository _repository;
        private readonly IEmailSender _emailSender;

        public RoomOrderController(IRoomOrderDetailsRepository repository, IEmailSender emailSender)
        {
            _repository = repository;
            _emailSender = emailSender;
        }

        [HttpPost]
        public async Task<IActionResult> PaymentSuccessful([FromBody] RoomOrderDetailsDTO details)
        {
            var service = new SessionService();
            var sessionDetails = service.Get(details.StripeSessionId);

            if(sessionDetails.PaymentStatus == SD.Stripe_Paid)
            {
                var result = await _repository.MarkPaymentSucccessful(details.Id);

                if(result == null)
                {
                    return BadRequest(new ErrorModel()
                    {
                        ErrorMessage = "Cannot mark payment as successful"
                    });
                }

                await _emailSender.SendEmailAsync(details.Email, "Booking Confirmed", "Booking confirmed");
                return Ok(result);
            }
            else
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = "Cannot mark payment as successful"
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoomOrderDetailsDTO details)
        {
            if (ModelState.IsValid)
            {
                var result = await _repository.Create(details);
                return Ok(result);
            }
            else
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = "Error while creating booking"
                });
            }
        }
    }
}
