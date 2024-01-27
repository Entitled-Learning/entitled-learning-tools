using System;
using ELDataAccessLibrary.StorageContracts;

namespace ELDataAccessLibrary.Repository;

public class StudentRepository : RepositoryBase, IDataRepository<StudentStorageContractV1>
{
    private readonly ISqlDataAccess _db;
    private readonly string tableName = "Student";

    public StudentRepository(ISqlDataAccess db)
    {
       _db = db;
    }

    public async Task<IEnumerable<StudentStorageContractV1>> GetAllAsync()
    {
        string sql = "select * from dbo." + tableName + ";";
        var data = await _db.LoadData<StudentStorageContractV1, dynamic>(sql, new { });

        return data;
    }

    public async Task<StudentStorageContractV1> GetByIdAsync(string id)
    {
        string sql = "select * from dbo." + tableName + " where Id = @Id;";
        var data = await _db.LoadData<StudentStorageContractV1, dynamic>(sql, new { Id = id });

        return data.FirstOrDefault()!;
    }

    public async Task<StudentStorageContractV1> AddAsync(StudentStorageContractV1 entity)
    {
        entity.Id = GenerateId(entity.FirstName, entity.LastName);

        string sql = "insert into dbo." + tableName + " (Id, Prefix, FirstName, MiddleName, LastName, Suffix, EmailAddress, AddressLine1, AddressLine2, City, State, ZipCode, Race, DateOfBirth, HouseholdIncomeRange, ShirtSize, ContractVersion) " +
        "values (@Id, @Prefix, @FirstName, @MiddleName, @LastName, @Suffix, @EmailAddress, @AddressLine1, @AddressLine2, @City, @State, @ZipCode, @Race, @DateOfBirth, @HouseholdIncomeRange, @ShirtSize, @ContractVersion);";

        await _db.SaveData(sql, entity);

        return entity;
    }

    public async Task UpdateAsync(StudentStorageContractV1 entity)
    {
        entity.UpdatedOn = DateTimeOffset.UtcNow;
        await Task.Delay(1000); 
    }

    public async Task DeleteAsync(string id)
    {
        await Task.Delay(1000);
    }
}

