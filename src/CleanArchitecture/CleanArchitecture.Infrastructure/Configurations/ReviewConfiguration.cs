using CleanArchitecture.Domain.Alquileres;
using CleanArchitecture.Domain.Reviews;
using CleanArchitecture.Domain.Users;
using CleanArchitecture.Domain.Vehiculos;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Configurations;

internal sealed class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Review> builder)
    {
        builder.ToTable("reviews");
        builder.HasKey(review => review.Id);

        builder.Property(review => review.Id)
            .HasConversion(reviewId => reviewId!.Value, value => new ReviewId(value));

        builder.Property(review => review.Rating)
            .HasConversion(rating => rating!.Value, value => Raiting.Create(value).Value);

        builder.Property(review => review.Comentario)
            .HasMaxLength(200)
            .HasConversion(comentario => comentario!.Value, value => new Comentario(value));
        
        builder.HasOne<Vehiculo>()
            .WithMany()
            .HasForeignKey(review => review.VehiculoId);

        builder.HasOne<Alquiler>()
            .WithMany()
            .HasForeignKey(review => review.AlquilerId);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(review => review.UserId);
    }
}