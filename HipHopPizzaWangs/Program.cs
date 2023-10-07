using HipHopPizzaWangs.Modles;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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
    var comment = db.Items.Where(s => s.Id == id);
    return comment;
}
);

app.MapGet("/api/ItembyOrderID/{id}", (HipHopPizzaWangsDbContext db, int id) =>
{
    var item = db.Items.Where(s => s.OrderId == id)
    .Include(s => s.Order).ToList();
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



app.Run();
