using Microsoft.EntityFrameworkCore;
using HipHopPizzaWangs.Modles;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Hosting;

public class HipHopPizzaWangsDbContext : DbContext
{

    public DbSet<Item>? Items { get; set; }
    public DbSet<User>? Users { get; set; }
    public DbSet<Payment>? Payments { get; set; }
    public DbSet<Order>? Orders { get; set; }

    public HipHopPizzaWangsDbContext(DbContextOptions<HipHopPizzaWangsDbContext> context) : base(context)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Item>().HasData(new Item[]
            {
            new Item { Id = 1, Name = "Carrot", OrderId = 1, Price = 12, Description = "ferrsef" },
            new Item { Id = 2, Name = "Orange", OrderId = 2, Price = 12, Description = "ferrsef" },
            new Item { Id = 3, Name = "Lemon", OrderId = 3, Price = 12, Description = "ferrsef" },
            });

        modelBuilder.Entity<User>().HasData(new User[]
            {
                new User { Id = 1, CashierEmail = "demansell2016@gmail.com", CashierPassword = "M@chelle2030", Uid = "1" },
                new User { Id = 2, CashierEmail = "Trex@gmail.com", CashierPassword = "M@chelle2012", Uid = "2" },
                new User { Id = 3, CashierEmail = "Tricertops@gmail.com", CashierPassword = "M@chelle2020", Uid = "3" },
            });
        modelBuilder.Entity<Payment>().HasData(new Payment[]
            {
                new Payment { Id = 1, PaymentType = "Visa" },
                new Payment { Id = 2, PaymentType = "MasterCard" },
                new Payment { Id = 3, PaymentType = "Amex" },
           });
        modelBuilder.Entity<Order>().HasData(new Order[]
            { 
                new Order { Id = 1, CustomerName = "Dustin", CustomerEmail = "demoney@gmail.com", CustomerPhoneNumber = "9312613939", UserId = "1", PaymentTypeId = 1, IsOpen = false, OrderTotal = 123, Feedback = false, Tip = 12, OrderType = "Call in"},
                new Order { Id = 2, CustomerName = "Dustin", CustomerEmail = "demoney@gmail.com", CustomerPhoneNumber = "9312613939", UserId = "2", PaymentTypeId = 2, IsOpen = true, OrderTotal = 123, Feedback = true, Tip = 12, OrderType = "Take Out"},
                new Order { Id = 3, CustomerName = "Dustin", CustomerEmail = "demoney@gmail.com", CustomerPhoneNumber = "9312613939", UserId = "3", PaymentTypeId = 3, IsOpen = true, OrderTotal = 123, Feedback = false, Tip = 12, OrderType = "Dine In"},
                });
    }
    };