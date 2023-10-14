using HipHopPizzaWangs.Modles;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("http://localhost:3000",
                                "http://localhost:5169")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// allows passing datetimes without time zone data 
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// allows our api endpoints to access the database through Entity Framework Core
builder.Services.AddNpgsql<HipHopPizzaWangsDbContext>(builder.Configuration["HipHopPizzaWangsDbConnectionString"]);

// Set the JSON serializer options
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();



// payment endpoints 

//get all Payments 

app.MapGet("/HipHopPizzaWangs/Payments", (HipHopPizzaWangsDbContext db) =>
{
    List<Payment> payments = db.Payments.ToList();
    if (payments.Count == 0)
    {
        return Results.NotFound();
    }

    return Results.Ok(payments);
});

// get paymetns by Id

app.MapGet("/HipHopPizzaWangs/Payments/{id}", (HipHopPizzaWangsDbContext db, int id) =>
{
    Payment payments = db.Payments.SingleOrDefault(c => c.Id == id);
    if (payments == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(payments);
});

// Post a Payment

app.MapPost("/HipHopPizzaWangs/Payments", (HipHopPizzaWangsDbContext db, Payment payment) =>
{
    try
    {
        db.Add(payment);
        db.SaveChanges();
        return Results.Created($"/HipHopPizzaWangs/Payments/{payment.Id}", payment);
    }
    catch (DbUpdateException)
    {
        return Results.NotFound();
    }
});


// Update a paymenttype

app.MapPut("api/HipHopPizzaWangs/Payments", async (HipHopPizzaWangsDbContext db, int id, Payment payment) =>
{
    Payment paymentToUpdate = await db.Payments.SingleOrDefaultAsync(payment => payment.Id == id);
    if (paymentToUpdate == null)
    {
        return Results.NotFound();
    }
    paymentToUpdate.Id = payment.Id;
    paymentToUpdate.PaymentType = payment.PaymentType;
    
    db.SaveChanges();
    return Results.NoContent();
});

// delete payment type
app.MapDelete("/HipHopPizzaWangs/Payments/{paymentId}", (HipHopPizzaWangsDbContext db, int paymentId) =>
{
    Payment deletePayment = db.Payments.FirstOrDefault(c => c.Id == paymentId);
    if (deletePayment == null)
    {
        return Results.NotFound();
    }
    db.Remove(deletePayment);
    db.SaveChanges();
    return Results.Ok(deletePayment);
});

// Item Endpoints

// get Items
app.MapGet("/Items", (HipHopPizzaWangsDbContext db) =>
{
    return db.Items.ToList();
});

// get Item by Id
app.MapGet("/api/ItembyID/{id}", (HipHopPizzaWangsDbContext db, int id) =>
{
    var item = db.Items.SingleOrDefaultAsync(s => s.Id == id);
    return item;
}
);

//get item by order id
app.MapGet("/api/ItembyOrderID/{id}", (HipHopPizzaWangsDbContext db, int id) =>
{
    var item = db.Items.SingleOrDefaultAsync(s => s.OrderId == id);
    // .Include(s => s.Order).ToList();
    return item;
}
);

//Add a item
app.MapPost("api/Item", async (HipHopPizzaWangsDbContext db, Item item) =>
{
    db.Items.Add(item);
    db.SaveChanges();
    return Results.Created($"/api/Item{item.Id}", item);
});

//Update a Item
app.MapPut("api/Items/{id}", async (HipHopPizzaWangsDbContext db, int id, Item item) =>
{
Item itemToUpdate = await db.Items.SingleOrDefaultAsync(item => item.Id == id);
if (itemToUpdate == null)
{
    return Results.NotFound();
}
itemToUpdate.Id = item.Id;
itemToUpdate.Name = item.Name;
itemToUpdate.OrderId = item.OrderId;
db.SaveChanges();
return Results.NoContent();
});

//Delete Item
app.MapDelete("api/Item/{id}", (HipHopPizzaWangsDbContext db, int id) =>
{
    Item item = db.Items.SingleOrDefault(item => item.Id == id);
    if (item == null)
    {
        return Results.NotFound();
    }
    db.Items.Remove(item);
    db.SaveChanges();
    return Results.NoContent();
});

// Order Endpoints

// Get all Orders
app.MapGet("/Order", (HipHopPizzaWangsDbContext db) =>
{
    return db.Orders.ToList();
});

// Get Orders by Id
app.MapGet("/api/OrdersbyID/{id}", (HipHopPizzaWangsDbContext db, int id) =>
{
    var order = db.Orders.SingleOrDefaultAsync(s => s.Id == id);

    return order;
}
);

// Add a Order
app.MapPost("api/Order", async (HipHopPizzaWangsDbContext db, Order order) =>
{
    db.Orders.Add(order);
    db.SaveChanges();
    return Results.Created($"/api/Post{order.Id}", order);
});

//update Order
app.MapPut("api/Order/{id}", async (HipHopPizzaWangsDbContext db, int id, Order order) =>
{
    Order orderToUpdate = await db.Orders.SingleOrDefaultAsync(order => order.Id == id);
    if (orderToUpdate == null)
    {
        return Results.NotFound();
    }
    orderToUpdate.Id = order.Id;
    orderToUpdate.CustomerName = order.CustomerName;
    orderToUpdate.CustomerEmail = order.CustomerEmail;
    orderToUpdate.CustomerPhoneNumber = order.CustomerPhoneNumber;
    orderToUpdate.UserId = order.UserId;
    orderToUpdate.PaymentTypeId = order.PaymentTypeId;
    orderToUpdate.IsOpen = order.IsOpen;
    orderToUpdate.OrderTotal = order.OrderTotal;
    orderToUpdate.OrderType = order.OrderType;
    orderToUpdate.Feedback = order.Feedback;
    orderToUpdate.Tip = order.Tip;

    db.SaveChanges();
    return Results.NoContent();
});

// delete Order
app.MapDelete("api/Order/{id}", (HipHopPizzaWangsDbContext db, int id) =>
{
    Order order = db.Orders.SingleOrDefault(order => order.Id == id);
    if (order == null)
    {
        return Results.NotFound();
    }
    db.Orders.Remove(order);
    db.SaveChanges();
    return Results.NoContent();
});

// User Endpoints

//Check User

app.MapGet("/checkuser/{uid}", (HipHopPizzaWangsDbContext db, string uid) =>
{
    var user = db.Users.Where(x => x.Uid == uid).ToList();
    if (uid == null)
    {
        return Results.NotFound();
    }
    else
    {
        return Results.Ok(user);
    }
});

//Get all Users
app.MapGet("/users", (HipHopPizzaWangsDbContext db) =>
{
    return db.Users.ToList();
});

// Get Single User 

app.MapGet("/users/{uid}", (HipHopPizzaWangsDbContext db, string uid) =>
{
    return db.Users.FirstOrDefault(x => x.Uid == uid);
});

app.MapGet("/checkuser/{uid}", (HipHopPizzaWangsDbContext db, string uid) =>
{
    var user = db.Users.Where(x => x.Uid == uid).ToList();
    if (uid == null)
    {
        return Results.NotFound();
    }
    else
    {
        return Results.Ok(user);
    }
});



app.Run();
