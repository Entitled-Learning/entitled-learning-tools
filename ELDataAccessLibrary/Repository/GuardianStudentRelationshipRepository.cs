// ----------------------------------------------------------------
// <copyright company="Tope Daramola">
//     Copyright (c) Tope Daramola. All rights reserved.
// </copyright>
// ----------------------------------------------------------------

using Microsoft.Extensions.Logging;
using ELDataAccessLibrary.StorageContracts;
using ELDataAccessLibrary.Models;

namespace ELDataAccessLibrary.Repository;

public class GuardianStudentRelationshipRepository : IDataRepository<GuardianStudentRelationshipStorageContractV1>
{
    private readonly string tableName = "GuardianStudentRelationship";
    private readonly string guardianTableName = "Guardian";
    private readonly ISqlDataAccess _db;
    private readonly ILogger<GuardianStudentRelationshipRepository> _logger;

    public GuardianStudentRelationshipRepository(ISqlDataAccess db, ILogger<GuardianStudentRelationshipRepository> logger)
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
        string sql = "insert into dbo." + tableName + " (StudentId, GuardianId, Relationship, IsEmergencyContact, IsAuthorizedPickup) " +
        "values (@StudentId, @GuardianId, @Relationship, @IsEmergencyContact, @IsAuthorizedPickup);";
        
        try{
            await _db.SaveData(sql, entity);
            return entity;
        } catch (Exception ex) {
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

    public async Task DeleteAsync(string id)
    {
        try{
            string studentIdParameter = id;
            string sql = "delete from dbo." + tableName + " where StudentId = @StudentId;";
            await _db.SaveData(sql, new { StudentId = studentIdParameter });
        } catch (Exception ex) {
            _logger.DeleteGuardianStudentRelationshipError(ex);
            throw;
        }
    }
}

