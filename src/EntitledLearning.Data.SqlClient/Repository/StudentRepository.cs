// ----------------------------------------------------------------
// <copyright company="Tope Daramola">
//     Copyright (c) Tope Daramola. All rights reserved.
// </copyright>
// ----------------------------------------------------------------

using Microsoft.Extensions.Logging;
using EntitledLearning.Data.SqlClient;
using EntitledLearning.Data.StorageContracts;

namespace EntitledLearning.Data.Repository;

public class StudentRepository : RepositoryBase, IDataRepository<StudentStorageContractV1>
{
    private readonly string tableName = "Student";
    private readonly ISqlDataClient _db;
    private readonly ILogger<StudentRepository> _logger;

    public StudentRepository(ISqlDataClient db, ILogger<StudentRepository> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<IEnumerable<StudentStorageContractV1>> GetAllAsync()
    {
        string sql = "select * from dbo." + tableName + ";";

        try
        {
            var data = await _db.LoadData<StudentStorageContractV1, dynamic>(sql, new { });
            return data;
        }
        catch (Exception ex)
        {
            _logger.GetStudentError(ex);
            throw;
        }
    }

    public async Task<StudentStorageContractV1> GetByIdAsync(string id)
    {
        string sql = "select * from dbo." + tableName + " where Id = @Id;";

        try{
            var data = await _db.LoadData<StudentStorageContractV1, dynamic>(sql, new { Id = id });
            return data.FirstOrDefault()!;
        } catch (Exception ex) {
            _logger.GetStudentError(ex);
            throw;
        }
    }

    public async Task<StudentStorageContractV1> AddAsync(StudentStorageContractV1 entity)
    {
        entity.Id = entity.Id is null ? GenerateId(entity.FirstName, entity.LastName) : entity.Id;

        string sql = "if not exists (select 1 from dbo." + tableName + " WHERE Id = @Id) " +
            "begin " +
            "    insert into dbo." + tableName + " (Id, Prefix, FirstName, MiddleName, LastName, Suffix, EmailAddress, AddressLine1, AddressLine2, City, State, ZipCode, Race, DateOfBirth, HouseholdIncomeRange, ShirtSize, IsScholar, AllowPhotoRelease, ContractVersion) " +
            "    values (@Id, @Prefix, @FirstName, @MiddleName, @LastName, @Suffix, @EmailAddress, @AddressLine1, @AddressLine2, @City, @State, @ZipCode, @Race, @DateOfBirth, @HouseholdIncomeRange, @ShirtSize, @IsScholar, @AllowPhotoRelease, @ContractVersion); " +
            "end;";

        try{
            await _db.SaveData(sql, entity);
            return entity;
        } catch (Exception ex) {
            _logger.CreateStudentError(ex);
            throw;
        }
    }

    public async Task<StudentStorageContractV1> UpsertAsync(StudentStorageContractV1 entity)
    {
        entity.Id = entity.Id is null ? GenerateId(entity.FirstName, entity.LastName) : entity.Id;

        string sql = "if exists (select 1 from dbo." + tableName + " where Id = @Id) " +
             "begin " +
             "    update dbo." + tableName + " " +
             "    set Prefix = @Prefix, FirstName = @FirstName, MiddleName = @MiddleName, LastName = @LastName, Suffix = @Suffix, " +
             "        EmailAddress = @EmailAddress, AddressLine1 = @AddressLine1, AddressLine2 = @AddressLine2, City = @City, " +
             "        State = @State, ZipCode = @ZipCode, Race = @Race, DateOfBirth = @DateOfBirth, " +
             "        HouseholdIncomeRange = @HouseholdIncomeRange, ShirtSize = @ShirtSize, IsScholar = @IsScholar, " +
             "        AllowPhotoRelease = @AllowPhotoRelease, ContractVersion = @ContractVersion " +
             "    where Id = @Id; " +
             "end " +
             "else " +
             "begin " +
             "    insert into dbo." + tableName + " (Id, Prefix, FirstName, MiddleName, LastName, Suffix, EmailAddress, AddressLine1, AddressLine2, City, State, ZipCode, Race, DateOfBirth, HouseholdIncomeRange, ShirtSize, IsScholar, AllowPhotoRelease, ContractVersion) " +
             "    values (@Id, @Prefix, @FirstName, @MiddleName, @LastName, @Suffix, @EmailAddress, @AddressLine1, @AddressLine2, @City, @State, @ZipCode, @Race, @DateOfBirth, @HouseholdIncomeRange, @ShirtSize, @IsScholar, @AllowPhotoRelease, @ContractVersion); " +
             "end;";

        try
        {
            await _db.SaveData(sql, entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.CreateStudentError(ex);
            throw;
        }
    }

    public async Task UpdateAsync(StudentStorageContractV1 entity)
    {
        entity.UpdatedOn = DateTimeOffset.UtcNow;

        string sql = "UPDATE dbo." + tableName + " SET Prefix = @Prefix, FirstName = @FirstName, MiddleName = @MiddleName, LastName = @LastName, Suffix = @Suffix, EmailAddress = @EmailAddress, AddressLine1 = @AddressLine1, AddressLine2 = @AddressLine2, City = @City, State = @State, ZipCode = @ZipCode, Race = @Race, DateOfBirth = @DateOfBirth, HouseholdIncomeRange = @HouseholdIncomeRange, ShirtSize = @ShirtSize, IsScholar = @IsScholar, AllowPhotoRelease = @AllowPhotoRelease, ContractVersion = @ContractVersion WHERE Id = @Id;";

        try{
            await _db.SaveData(sql, entity);
        } catch (Exception ex) {
            _logger.UpdateStudentError(ex);
            throw;
        }
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
            _db.CommitTransactionAsync();
        }
        catch (Exception ex)
        {
            // Handle exception, log, or throw as needed
            _logger.DeleteStudentError(ex);

            // Rollback transaction in case of an error
            _db.RollbackTransactionAsync();
            throw;
        }
    }
}

