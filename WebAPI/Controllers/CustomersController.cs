using Application.Features.Customers.Commands.Create;
using Application.Features.Customers.Commands.Delete;
using Application.Features.Customers.Commands.Update;
using Application.Features.Customers.Queries.GetById;
using Application.Features.Customers.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateCustomerCommand createCustomerCommand)
        {
            CreatedCustomerResponse response =await Mediator.Send(createCustomerCommand);

            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest PageRequest)
        {
            GetListCustomerQuery getListCustomerQuery = new() { PageRequest = PageRequest };

            GetListResponse<GetListCustomerListItemDto> response = await Mediator.Send(getListCustomerQuery);

            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            GetByIdCustomerQuery getByIdCustomerQuery = new() { Id = id };

            GetByIdCustomerResponse response = await Mediator.Send(getByIdCustomerQuery);

            return Ok(response);
        }


        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCustomerCommand updateCustomerCommand)
        {
            UpdatedCustomerResponse response = await Mediator.Send(updateCustomerCommand);

            return Ok(response);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            DeleteCustomerCommand deleteCustomerCommand = new() { Id = id };

            DeletedCustomerResponse response =await Mediator.Send(deleteCustomerCommand);

            return Ok(response);
        }
    }
}
