using Microsoft.AspNetCore.Mvc;
using Tarker.Booking.Application.DataBase.Customer.Commands.CreateCustomer;
using Tarker.Booking.Application.DataBase.Customer.Commands.DeleteCustomer;
using Tarker.Booking.Application.DataBase.Customer.Commands.UpdateCustomer;
using Tarker.Booking.Application.DataBase.Customer.Queries.GetAllCustomer;
using Tarker.Booking.Application.DataBase.Customer.Queries.GetCustomerByDocumentNumber;
using Tarker.Booking.Application.DataBase.Customer.Queries.GetCustomerById;
using Tarker.Booking.Application.Exceptions;
using Tarker.Booking.Application.Features;

namespace Tarker.Booking.Api.Controllers
{
    [Route("api/v1/customer")]
    [ApiController]
    [TypeFilter(typeof(ExceptionManager))]
    public class CustomerController : ControllerBase
    {
        public CustomerController()
        {
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(
            [FromBody] CreateCustomerModel model,
            [FromServices] ICreateCustomerCommand createCustomerCommand)
        {
            var data = await createCustomerCommand.Execute(model);
            return StatusCode(StatusCodes.Status201Created,
                ResponseApiService.Response(StatusCodes.Status201Created, data));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(
            [FromBody] UpdateCustomerModel model,
            [FromServices] IUpdateCustomerCommand updateCustomerCommand)
        {
            var data = await updateCustomerCommand.Execute(model);
            return StatusCode(StatusCodes.Status200OK,
                ResponseApiService.Response(StatusCodes.Status200OK, data, "Customer actualizado exitosamente"));
        }

        [HttpDelete("delete/{customerId}")]
        public async Task<IActionResult> Delete(
            int customerId,
            [FromServices] IDeleteCustomerCommand deleteCustomerCommand)
        {
            if (customerId == 0)
                return StatusCode(StatusCodes.Status400BadRequest,
                    ResponseApiService.Response(StatusCodes.Status400BadRequest));
            var data = await deleteCustomerCommand.Execute(customerId);

            if (!data)
                return StatusCode(StatusCodes.Status404NotFound,
                    ResponseApiService.Response(StatusCodes.Status404NotFound, data, "Customer no encontrado"));

            return StatusCode(StatusCodes.Status200OK,
                ResponseApiService.Response(StatusCodes.Status200OK, data, "Customer eliminado exitosamente"));
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll([FromServices] IGetAllCustomerQuery getAllCustomerQuery)
        {
            var data = await getAllCustomerQuery.Execute();
            if (data == null || data.Count == 0)
                return StatusCode(StatusCodes.Status404NotFound,
                   ResponseApiService.Response(StatusCodes.Status404NotFound));

            return StatusCode(StatusCodes.Status200OK,
                ResponseApiService.Response(StatusCodes.Status200OK, data));
        }

        [HttpGet("get/{customerId}")]
        public async Task<IActionResult> GetById(
            int customerId,
            [FromServices] IGetCustomerByIdQuery getCustomerByIdQuery)
        {
            if (customerId == 0)
                return StatusCode(StatusCodes.Status400BadRequest,
                    ResponseApiService.Response(StatusCodes.Status400BadRequest));

            var data = await getCustomerByIdQuery.Execute(customerId);
            if (data == null)
                return StatusCode(StatusCodes.Status404NotFound,
                   ResponseApiService.Response(StatusCodes.Status404NotFound));

            return StatusCode(StatusCodes.Status200OK,
                ResponseApiService.Response(StatusCodes.Status200OK, data));
        }

        [HttpGet("get-by-document-number/{documentNumber}")]
        public async Task<IActionResult> GetByDocumentNumber(
            string documentNumber,
            [FromServices] IGetCustomerByDocumentNumberQuery getCustomerByDocumentNumberQuery)
        {
            if (String.IsNullOrEmpty(documentNumber))
                return StatusCode(StatusCodes.Status400BadRequest,
                    ResponseApiService.Response(StatusCodes.Status400BadRequest));

            var data = await getCustomerByDocumentNumberQuery.Execute(documentNumber);
            if (data == null)
                return StatusCode(StatusCodes.Status404NotFound,
                   ResponseApiService.Response(StatusCodes.Status404NotFound));

            return StatusCode(StatusCodes.Status200OK,
                ResponseApiService.Response(StatusCodes.Status200OK, data));
        }
    }
}
