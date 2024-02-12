using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Customers.Queries.GetById;

public class GetByIdCustomerResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }

    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime CreatedDate { get; set; }
}
