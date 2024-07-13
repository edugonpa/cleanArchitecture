using CleanArchitecture.Application.Abstractions.Clock;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Infrastructure.Outbox;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace CleanArchitecture.Infrastructure;

public sealed class ApplicationDbContext: DbContext, IUnitOfWork
{
    // private readonly IPublisher _publisher;
    // public ApplicationDbContext(DbContextOptions options, IPublisher publisher) : base(options)

    private static readonly JsonSerializerSettings jsonSerializerSettings = new()
    {
        TypeNameHandling = TypeNameHandling.All
    };

    private readonly IDateTimeProvider _dateTimeProvider;
    public ApplicationDbContext(DbContextOptions options, IDateTimeProvider dateTimeProvider) : base(options)
    {
        _dateTimeProvider = dateTimeProvider;
        // _publisher = publisher;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(
        CancellationToken cancellationToken= default
    )
    {
        try
        {

            // await PublishDomainEventsAsync();
            AddDomainEventsToOutboxMessages();

            var result = await base.SaveChangesAsync(cancellationToken);

            

            return result;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new ConcurrencyException("La expeción por concurrencia se disparo", ex);
        }

    }

    private void AddDomainEventsToOutboxMessages()
    {
        var outboxesMessages = ChangeTracker
            .Entries<IEntity>()
            .Select(entry => entry.Entity)
            .SelectMany(entity =>
            {
                var domainEvents = entity.GetDomainEvents();
                entity.ClearDomainEvents();
                return domainEvents;
            })
            .Select(domainEvent => new OutboxMessage(
                Guid.NewGuid(),
                _dateTimeProvider.currentTime,
                domainEvent.GetType().Name,
                JsonConvert.SerializeObject(domainEvent, jsonSerializerSettings)
            )).ToList();

        AddRange(outboxesMessages);

        // foreach (var domainEvent in domainEvents)
        // {
        //     await _publisher.Publish(domainEvent);
        // }
    }
}