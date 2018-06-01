using OrderBook.Domain;
using System.Data.Entity;

namespace OrderBook.EF
{
    public class OrderBookDbContext : DbContext
    {
        public OrderBookDbContext()
            : base("OrderBookEntities")
        {
            //Database.SetInitializer(new OrderBookDbInitializer());
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            // Product
            modelBuilder.Entity<Product>()
                .ToTable("products");

            modelBuilder.Entity<Product>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Product>()
                .Property(p => p.Name)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .IsRequired();

            // Order
            modelBuilder.Entity<Order>()
                .ToTable("orders");

            modelBuilder.Entity<Order>()
                .HasKey(o => o.Id);

            modelBuilder.Entity<Order>()
                .Property(o => o.OrderNum)
                .IsRequired();

            modelBuilder.Entity<Order>()
                .Property(o => o.Created)
                .IsRequired();

            modelBuilder.Entity<Order>()
                .Property(o => o.Completed)
                .IsOptional();

            modelBuilder.Entity<Order>()
                .Property(o => o.Customer)
                .HasMaxLength(100)
                .IsOptional();

            modelBuilder.Entity<Order>()
                .Ignore(o => o.OrderName);

            modelBuilder.Entity<Order>()
                .Ignore(o => o.Total);

            modelBuilder.Entity<Order>()
                .HasMany<OrderDetail>(o => o.Details)
                .WithRequired()
                .HasForeignKey<int>(d => d.OrderId)
                .WillCascadeOnDelete();

            // OrderDetail
            modelBuilder.Entity<OrderDetail>()
                .ToTable("order_detail");

            modelBuilder.Entity<OrderDetail>()
                .HasKey(d => d.Id);

            modelBuilder.Entity<OrderDetail>()
                .Property(d => d.Quantity)
                .IsRequired();

            modelBuilder.Entity<OrderDetail>()
                .HasRequired<Product>(d => d.Product)
                .WithMany()
                .HasForeignKey<int>(d => d.ProductId);

            modelBuilder.Entity<OrderDetail>()
                .Ignore(d => d.Total);
        }
    }
}
