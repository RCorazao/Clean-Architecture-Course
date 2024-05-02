using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Tarker.Booking.Application.DataBase.Bookings.Commands.CreateBooking;
using Tarker.Booking.Application.DataBase.Bookings.Queries.GetAllBookings;
using Tarker.Booking.Application.DataBase.Bookings.Queries.GetBookingsByDocumentNumber;
using Tarker.Booking.Application.DataBase.Bookings.Queries.GetBookingsByType;
using Tarker.Booking.Application.Exceptions;
using Tarker.Booking.Application.Features;

namespace Tarker.Booking.Api.Controllers
{
    [Route("api/v1/booking")]
    [ApiController]
    [TypeFilter(typeof(ExceptionManager))]
    public class BookingController : ControllerBase
    {
        public BookingController()
        {
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(
            [FromBody] CreateBookingModel model,
            [FromServices] ICreateBookingCommand createBookingCommand,
            [FromServices] IValidator<CreateBookingModel> validator)
        {
            var validate = await validator.ValidateAsync(model);
            if (!validate.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest,
                    ResponseApiService.Response(StatusCodes.Status400BadRequest, validate.Errors));

            var data = await createBookingCommand.Execute(model);
            return StatusCode(StatusCodes.Status201Created,
                ResponseApiService.Response(StatusCodes.Status201Created, data));
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll([FromServices] IGetAllBookingsQuery getAllBookingsQuery)
        {
            var data = await getAllBookingsQuery.Execute();
            if (data == null || data.Count == 0)
                return StatusCode(StatusCodes.Status404NotFound,
                   ResponseApiService.Response(StatusCodes.Status404NotFound));

            return StatusCode(StatusCodes.Status200OK,
                ResponseApiService.Response(StatusCodes.Status200OK, data));
        }

        [HttpGet("get/{type}")]
        public async Task<IActionResult> GetByType(
            string type,
            [FromServices] IGetBookingsByTypeQuery getBookingsByTypeQuery)
        {
            if (String.IsNullOrEmpty(type))
                return StatusCode(StatusCodes.Status400BadRequest,
                    ResponseApiService.Response(StatusCodes.Status400BadRequest));

            var data = await getBookingsByTypeQuery.Execute(type);
            if (data == null)
                return StatusCode(StatusCodes.Status404NotFound,
                   ResponseApiService.Response(StatusCodes.Status404NotFound));

            return StatusCode(StatusCodes.Status200OK,
                ResponseApiService.Response(StatusCodes.Status200OK, data));
        }

        [HttpGet("get-by-document-number/{documentNumber}")]
        public async Task<IActionResult> GetByUserNamePassword(
            string documentNumber,
            [FromServices] IGetBookingsByDocumentNumberQuery getBookingsByDocumentNumberQuery)
        {
            if (String.IsNullOrEmpty(documentNumber))
                return StatusCode(StatusCodes.Status400BadRequest,
                    ResponseApiService.Response(StatusCodes.Status400BadRequest));

            var data = await getBookingsByDocumentNumberQuery.Execute(documentNumber);
            if (data == null)
                return StatusCode(StatusCodes.Status404NotFound,
                   ResponseApiService.Response(StatusCodes.Status404NotFound));

            return StatusCode(StatusCodes.Status200OK,
                ResponseApiService.Response(StatusCodes.Status200OK, data));
        }
    }
}
