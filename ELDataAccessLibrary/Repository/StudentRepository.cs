using System;
using Microsoft.Extensions.Logging;
using ELDataAccessLibrary.StorageContracts;

namespace ELDataAccessLibrary.Repository;

public class StudentRepository : RepositoryBase, IDataRepository<StudentStorageContractV1>
{
    private readonly ISqlDataAccess _db;
    private readonly string tableName = "Student";
    private readonly ILogger<StudentRepository> _logger;

    public StudentRepository(ISqlDataAccess db, ILogger<StudentRepository> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<IEnumerable<StudentStorageContractV1>> GetAllAsync()
    {
        string sql = "select * from dbo." + tableName + ";";
        var data = await _db.LoadData<StudentStorageContractV1, dynamic>(sql, new { });

        _logger.LogInformation("Fetched all students from the database.");

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

        string sql = "UPDATE dbo." + tableName + " SET Prefix = @Prefix, FirstName = @FirstName, MiddleName = @MiddleName, LastName = @LastName, Suffix = @Suffix, EmailAddress = @EmailAddress, AddressLine1 = @AddressLine1, AddressLine2 = @AddressLine2, City = @City, State = @State, ZipCode = @ZipCode, Race = @Race, DateOfBirth = @DateOfBirth, HouseholdIncomeRange = @HouseholdIncomeRange, ShirtSize = @ShirtSize, ContractVersion = @ContractVersion WHERE Id = @Id;";

        await _db.SaveData(sql, entity);
    }

    public async Task DeleteAsync(string id)
    {
        await Task.Delay(1000);
    }
}

