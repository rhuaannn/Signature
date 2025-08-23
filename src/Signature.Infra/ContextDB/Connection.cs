using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Signature.Domain.Entities;

namespace Signature.Infra.ContextDB
{
    public class Connection : DbContext
    {
        public Connection(DbContextOptions<Connection> options) : base(options)
        { }

        public DbSet<Signature.Domain.Entities.Signature> Signatures { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentSignature> StudentsSignatures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var converter = new ValueConverter<DateTime, DateTime>(
                v => v.Kind == DateTimeKind.Local ? v.ToUniversalTime() : v,
                v => v);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                    {
                        property.SetValueConverter(converter);
                    }
                }
            }

            modelBuilder.Entity<Student>()
                .OwnsOne(s => s.CPF);

            modelBuilder.Entity<Student>()
                .OwnsOne(s => s.Email);
            modelBuilder.Entity<Domain.Entities.Signature>()
                .OwnsOne(s => s.Description);

            modelBuilder.Entity<StudentSignature>()
                .HasKey(ss => new { ss.FKIdSignature, ss.FKIdStudent });

            modelBuilder.Entity<StudentSignature>()
                .HasOne(ss => ss.Signature)
                .WithMany(s => s.StudentSignatures)
                .HasForeignKey(ss => ss.FKIdSignature);

            modelBuilder.Entity<StudentSignature>()
                .HasOne(ss => ss.Student)
                .WithMany(s => s.StudentSignatures)
                .HasForeignKey(ss => ss.FKIdStudent);

            base.OnModelCreating(modelBuilder);
        }
    }
}
