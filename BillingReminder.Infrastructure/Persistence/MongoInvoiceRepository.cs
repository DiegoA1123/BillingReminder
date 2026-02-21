using BillingReminder.Domain.Entities;
using BillingReminder.Domain.Interfaces;
using MongoDB.Driver;

namespace BillingReminder.Infrastructure.Persistence;

public class MongoInvoiceRepository : IInvoiceRepository
{
    private readonly IMongoCollection<Invoice> _collection;

    public MongoInvoiceRepository(MongoSettings settings)
    {

        var client = new MongoClient(settings.ConnectionString);
        var db = client.GetDatabase(settings.Database);
        _collection = db.GetCollection<Invoice>(settings.InvoicesCollection);

    }

    public async Task<List<Invoice>> GetByStatus(string status, CancellationToken ct = default)
    {

        return await _collection.Find(x => x.Status == status).ToListAsync(ct);

    }

    public async Task<List<Invoice>> GetAll(CancellationToken ct = default)
    {

        return await _collection.Find(_ => true).ToListAsync(ct);

    }

    public async Task UpdateStatus(string invoiceId, string newStatus, CancellationToken ct = default)
    {

        var filter = Builders<Invoice>.Filter.Eq(x => x.Id, invoiceId);
        var update = Builders<Invoice>.Update.Set(x => x.Status, newStatus);
        await _collection.UpdateOneAsync(filter, update, cancellationToken: ct);

    }
}