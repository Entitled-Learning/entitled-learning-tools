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
        try
        {
            string studentIdParameter = id;

            // Begin transaction
            await _db.BeginTransactionAsync();

            // Delete GuardianStudentRelationships associated with the student
            string deleteRelationshipsSql = "DELETE FROM [dbo].[GuardianStudentRelationship] WHERE [StudentId] = @StudentId;";
            await _db.SaveData<object>(deleteRelationshipsSql, new { StudentId = studentIdParameter });

            // Delete Guardians not associated with any other students
            string deleteGuardiansSql = "DELETE FROM [dbo].[Guardian] WHERE [Id] IN (SELECT [g].[Id] FROM [dbo].[Guardian] [g] LEFT JOIN [dbo].[GuardianStudentRelationship] [gsr] ON [g].[Id] = [gsr].[GuardianId] WHERE [gsr].[GuardianId] IS NULL);";
            await _db.SaveData<object>(deleteGuardiansSql, new { });

            // Delete the student
            string deleteStudentSql = "DELETE FROM [dbo].[Student] WHERE [Id] = @StudentId;";
            await _db.SaveData<object>(deleteStudentSql, new { StudentId = studentIdParameter });

            // Commit transaction
            await _db.CommitTransactionAsync();
        }
        catch (Exception ex)
        {
            // Handle exception, log, or throw as needed
            _logger.LogError($"Error deleting student with ID {id}: {ex.Message}");

            // Rollback transaction in case of an error
            await _db.RollbackTransactionAsync();
            throw;
        }
    }
}

