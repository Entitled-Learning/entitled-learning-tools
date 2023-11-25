using System;
using ELDataAccessLibrary.StorageContracts;

namespace ELDataAccessLibrary.Repository;

public class CommunityPartnerContactRepository : IDataRepository<CommunityPartnerContactStorageContractV1>
{
    //private readonly SqlConnection _connection;

    //public DataService(SqlConnection connection)
    //{
    //    _connection = connection;
    //}

    public async Task<IEnumerable<CommunityPartnerContactStorageContractV1>> GetAllAsync()
    {
        await Task.Delay(1000);
        var data = new List<CommunityPartnerContactStorageContractV1>(); // Placeholder data
        return data;
    }

    public async Task<CommunityPartnerContactStorageContractV1> GetByIdAsync(int id)
    {
        await Task.Delay(1000);
        var data = default(CommunityPartnerContactStorageContractV1); // Placeholder data
        return data!;
    }

    public async Task AddAsync(CommunityPartnerContactStorageContractV1 entity)
    {
        await Task.Delay(1000);
    }

    public async Task UpdateAsync(CommunityPartnerContactStorageContractV1 entity)
    {
        await Task.Delay(1000);
    }

    public async Task DeleteAsync(int id)
    {
        await Task.Delay(1000);
    }
}

