using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using HoneyRaesAPI.Models;

List<Customer> customers = new List<Customer> 
{
    new Customer
    {
        Id = 1,
        Name = "Dumb",
        Address = "123 Street"
    },
    new Customer
    {
        Id = 2,
        Name = "Dumber",
        Address = "456 Street"
    },
    new Customer
    {
        Id = 3,
        Name = "MoreDumber",
        Address = "789 Street"
    }
};

List<Employee> employees = new List<Employee>
{
    new Employee
    {
        Id = 1,
        Name = "Bob",
        Specialty = "Cheese"
    },
    new Employee
    {
        Id = 2,
        Name = "Bill",
        Specialty = "Turning it off an on again"
    },
};

List<ServiceTicket> serviceTickets = new List<ServiceTicket> 
{
    new ServiceTicket
    {
        Id = 1,
        CustomerId = 2,
        EmployeeId = 1,
        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
        Emergency = true,
        DateCompleted = new DateTime(2023,01,01)
    },
    new ServiceTicket
    {
        Id = 2,
        CustomerId = 1,
        EmployeeId = 2,
        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
        Emergency = false,
        DateCompleted = new DateTime(2023,01,09)
    },
    new ServiceTicket
    {
        Id = 3,
        CustomerId = 3,
        EmployeeId = 1,
        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
        Emergency = false,
    },
    new ServiceTicket
    {
        Id = 4,
        CustomerId = 3,
        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
        Emergency = true,
        DateCompleted = new DateTime(2023,02,09)
    },
    new ServiceTicket
    {
        Id = 5,
        CustomerId = 1,
        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
        Emergency = false,
    }
};


var builder = WebApplication.CreateBuilder(args);

// Set the JSON serializer options
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.MapGet("/servicetickets", () => 
{
    return serviceTickets;
});


app.MapGet("/servicetickets/{id}", (int id) =>
{
    ServiceTicket serviceTicket = serviceTickets.FirstOrDefault(st => st.Id == id);
    if (serviceTicket == null)
    {
        return Results.NotFound("Not Found!");
    }
    serviceTicket.Employee = employees.FirstOrDefault(e => e.Id == serviceTicket.EmployeeId);
    serviceTicket.Customer = customers.FirstOrDefault(c => c.Id == serviceTicket.CustomerId);
    return Results.Ok(serviceTicket);
});



app.MapGet("/customers", () =>
{
    return customers;
});


app.MapGet("/customers/{id}", (int id) =>
{
    Customer customer = customers.FirstOrDefault(c => c.Id == id);
    if (customer == null)
    {
        return Results.NotFound("Not Found!");
    }
    customer.ServiceTickets = serviceTickets.Where(st => st.CustomerId == id).ToList();
    return Results.Ok(customer);
});



app.MapGet("/employees", () =>
{
    return employees;
});


app.MapGet("/employees/{id}", (int id) =>
{
    Employee employee = employees.FirstOrDefault(e => e.Id == id);
    if(employee == null)
    {
        return Results.NotFound("Not found!");
    }
    employee.ServiceTickets = serviceTickets.Where(st => st.EmployeeId == id).ToList();
    return Results.Ok(employee);
});



app.MapPost("/servicetickets", (ServiceTicket serviceTicket) =>
{
    // creates a new id (When we get to it later, our SQL database will do this for us like JSON Server did!)
    serviceTicket.Id = serviceTickets.Count > 0 ?serviceTickets.Max(st => st.Id) + 1 : 1;
    serviceTickets.Add(serviceTicket);
    return serviceTicket;
});



app.MapDelete("/servicetickets/{id}", (int id) =>
{
    serviceTickets.RemoveAll(st => st.Id == id);
});


//take in an id and serviceticket obj from fetch request
app.MapPut("/servicetickets/{id}", (int id, ServiceTicket serviceTicket) =>
{
    //get the service ticket in db that matches the id that was sent in and store it in a new variable
   ServiceTicket ticketToUpdate = serviceTickets.FirstOrDefault(st => st.Id == id);

   //figure out the index of that particular ticket so it can be used to pinpoint the ticket being updated later
   int ticketIndex = serviceTickets.IndexOf(ticketToUpdate);

   //if that ticket wasn't found, ticket to update will be null, throw NotFound
   if (ticketToUpdate == null)
   {
    return Results.NotFound();
   }
       //the id in the request route doesn't match the id from the ticket in the request body. That's a bad request!
    if(id != serviceTicket.Id)
    {
        return Results.BadRequest();
    }

    //if everything is good, set the service ticket at the correct index = to the service ticket that came in on the fetch request.
    serviceTickets[ticketIndex] = serviceTicket;

    //return the results to the requester
    return Results.Ok();
});

app.MapPost("/servicetickets/{id}/complete", (int id) =>
{
    ServiceTicket ticketToComplete = serviceTickets.FirstOrDefault(st => st.Id == id);
    ticketToComplete.DateCompleted = DateTime.Today;

});

app.Run();

