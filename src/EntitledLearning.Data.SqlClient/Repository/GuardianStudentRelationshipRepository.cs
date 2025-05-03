// ----------------------------------------------------------------
// <copyright company="Tope Daramola">
//     Copyright (c) Tope Daramola. All rights reserved.
// </copyright>
// ----------------------------------------------------------------

using Microsoft.Extensions.Logging;
using EntitledLearning.Data.SqlClient;
using EntitledLearning.Data.StorageContracts;
using EntitledLearning.Data.Models;

namespace EntitledLearning.Data.Repository;

public class GuardianStudentRelationshipRepository : IDataRepository<GuardianStudentRelationshipStorageContractV1>
{
    private readonly string tableName = "GuardianStudentRelationship";
    private readonly string guardianTableName = "Guardian";
    private readonly string studentTableName = "Student";
    private readonly ISqlDataClient _db;
    private readonly ILogger<GuardianStudentRelationshipRepository> _logger;

    public GuardianStudentRelationshipRepository(ISqlDataClient db, ILogger<GuardianStudentRelationshipRepository> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<IEnumerable<GuardianStudentRelationshipStorageContractV1>> GetAllAsync()
    {
        string sql = "select * from dbo." + tableName + ";";

        try{
            var data = await _db.LoadData<GuardianStudentRelationshipStorageContractV1, dynamic>(sql, new { });
            return data;
        } catch (Exception ex) {
            _logger.GetGuardianStudentRelationshipError(ex);
            throw;
        }
    }

    public async Task<GuardianStudentRelationshipStorageContractV1> GetByIdAsync(string id)
    {
        string sql = "select * from dbo." + tableName + " where StudentId = @StudentId;";

        try{
            var data = await _db.LoadData<GuardianStudentRelationshipStorageContractV1, dynamic>(sql, new { StudentId = id });
            return data.FirstOrDefault()!;
        } catch (Exception ex) {
            _logger.GetGuardianStudentRelationshipError(ex);
            throw;
        }
    }

    public async Task<IEnumerable<StudentStorageContractV1>> GetStudentsByGuardianIdAsync(string id)
    { 
        string sql = "select " +
                        "C.Id " +
                        ",C.Prefix " +
                        ",C.FirstName " +
                        ",C.MiddleName " +
                        ",C.LastName " +
                        ",C.Suffix " +
                        ",C.EmailAddress " +
                        ",C.AddressLine1 " +
                        ",C.AddressLine2 " +
                        ",C.City " +
                        ",C.State " +
                        ",C.ZipCode " +
                        ",C.Race " +
                        ",C.DateOfBirth " +
                        ",C.HouseHoldIncomeRange " +
                        ",C.ShirtSIze " +
                        ",C.IsScholar " +
                        ",C.AllowPhotoRelease " +
                        "from dbo." + tableName + " as A " +
                        "join dbo." + guardianTableName + " as B on A.[GuardianId] = B.[Id] " +
                        "join dbo." + studentTableName + " as C on A.[StudentId] = C.[Id] " +
                        "where [GuardianId] = @GuardianId;";

        try
        {
            var data = await _db.LoadData<StudentStorageContractV1, dynamic>(sql, new { GuardianId = id });
            return data;
        }
        catch (Exception ex)
        {
            _logger.GetGuardianStudentRelationshipError(ex);
            throw;
        }
    }

    public async Task<IEnumerable<StudentGuardian>> GetByStudentIdAsync(string id)
    {
        string sql = "select * from dbo." + tableName + " as A " +
                     "join dbo." + guardianTableName + " as B on A.[GuardianId] = B.[Id] " +
                     "where [StudentId] = @StudentId;";

        try{
            var data = await _db.LoadData<StudentGuardian, dynamic>(sql, new { StudentId = id });
            return data;
        } catch (Exception ex) {
            _logger.GetGuardianStudentRelationshipError(ex);
            throw;
        }
    }


    public async Task<GuardianStudentRelationshipStorageContractV1> AddAsync(GuardianStudentRelationshipStorageContractV1 entity)
    {
        string sql = "if not exists (select 1 from dbo." + tableName + " WHERE GuardianId = @GuardianId AND StudentId = @StudentId) " +
                     "begin " +
                     "    insert into dbo." + tableName + " (GuardianId, StudentId, Relationship, IsAuthorizedPickup, IsEmergencyContact) " +
                     "    values (@GuardianId, @StudentId, @Relationship, @IsAuthorizedPickup, @IsEmergencyContact); " +
                     "end;";

        try
        {
            await _db.SaveData(sql, entity);
            return entity;
        } catch (Exception ex) {
            _logger.CreateGuardianStudentRelationshipError(ex);
            throw;
        }
    }

    public async Task<GuardianStudentRelationshipStorageContractV1> UpsertAsync(GuardianStudentRelationshipStorageContractV1 entity)
    {
        string sql = "if exists (select 1 from dbo." + tableName + " where StudentId = @StudentId and GuardianId = @GuardianId) " +
                     "begin " +
                     "    update dbo." + tableName + " " +
                     "    set StudentId = @StudentId, GuardianId = @GuardianId, Relationship = @Relationship, " +
                     "    IsEmergencyContact = @IsEmergencyContact, IsAuthorizedPickup = @IsAuthorizedPickup " +
                     "    where StudentId = @StudentId and GuardianId = @GuardianId; " +
                     "end " +
                     "else " +
                     "begin " +
                     "    insert into dbo." + tableName + " (StudentId, GuardianId, Relationship, IsEmergencyContact, IsAuthorizedPickup) " +
                     "    values (@StudentId, @GuardianId, @Relationship, @IsEmergencyContact, @IsAuthorizedPickup); " +
                     "end;";

        try
        {
            await _db.SaveData(sql, entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.CreateGuardianStudentRelationshipError(ex);
            throw;
        }
    }


    public async Task UpdateAsync(GuardianStudentRelationshipStorageContractV1 entity)
    {
        entity.UpdatedOn = DateTimeOffset.UtcNow;

        string sql = "UPDATE dbo." + tableName + " SET Relationship = @Relationship, IsEmergencyContact = @IsEmergencyContact, IsAuthorizedPickup = @IsAuthorizedPickup " +
                     "WHERE StudentId = @StudentId AND GuardianId = @GuardianId;";

        try{
            await _db.SaveData(sql, entity);
        } catch (Exception ex) {
            _logger.UpdateGuardianStudentRelationshipError(ex);
            throw;
        }
    }

    public async Task DeleteByStudentIdAsync(string id)
    {
        try{
            string sql = "delete from dbo." + tableName + " where StudentId = @StudentId;";
            await _db.SaveData(sql, new { StudentId = id });
        } catch (Exception ex) {
            _logger.DeleteGuardianStudentRelationshipError(ex);
            throw;
        }
    }

    public async Task DeleteByGuardianIdAsync(string id)
    {
        try{
            string sql = "delete from dbo." + tableName + " where GuardianId = @GuardianId;";
            await _db.SaveData(sql, new { GuardianId = id });
        } catch (Exception ex) {
            _logger.DeleteGuardianStudentRelationshipError(ex);
            throw;
        }
    }
}

