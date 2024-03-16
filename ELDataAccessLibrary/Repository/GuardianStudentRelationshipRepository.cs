// ----------------------------------------------------------------
// <copyright company="Tope Daramola">
//     Copyright (c) Tope Daramola. All rights reserved.
// </copyright>
// ----------------------------------------------------------------

using ELDataAccessLibrary.StorageContracts;
using ELDataAccessLibrary.Models;

namespace ELDataAccessLibrary.Repository;

public class GuardianStudentRelationshipRepository : IDataRepository<GuardianStudentRelationshipStorageContractV1>
{
    private readonly ISqlDataAccess _db;
    private readonly string tableName = "GuardianStudentRelationship";
    private readonly string guardianTableName = "Guardian";

    public GuardianStudentRelationshipRepository(ISqlDataAccess db)
    {
       _db = db;
    }

    public async Task<IEnumerable<GuardianStudentRelationshipStorageContractV1>> GetAllAsync()
    {
        string sql = "select * from dbo." + tableName + ";";
        var data = await _db.LoadData<GuardianStudentRelationshipStorageContractV1, dynamic>(sql, new { });

        return data;
    }

    public async Task<GuardianStudentRelationshipStorageContractV1> GetByIdAsync(string id)
    {
        string sql = "select * from dbo." + tableName + " where StudentId = @StudentId;";
        var data = await _db.LoadData<GuardianStudentRelationshipStorageContractV1, dynamic>(sql, new { });

        return data.FirstOrDefault()!;
    }

    public async Task<IEnumerable<StudentGuardian>> GetByStudentIdAsync(string id)
    {
        string sql = "select * from dbo." + tableName + " as A " +
                     "join dbo." + guardianTableName + " as B on A.[GuardianId] = B.[Id] " +
                     "where [StudentId] = @StudentId;";

        var data = await _db.LoadData<StudentGuardian, dynamic>(sql, new { StudentId = id });

        return data;
    }


    public async Task<GuardianStudentRelationshipStorageContractV1> AddAsync(GuardianStudentRelationshipStorageContractV1 entity)
    {
        string sql = "insert into dbo." + tableName + " (StudentId, GuardianId, Relationship, IsEmergencyContact, IsAuthorizedPickup) " +
        "values (@StudentId, @GuardianId, @Relationship, @IsEmergencyContact, @IsAuthorizedPickup);";

        await _db.SaveData(sql, entity);
        
        return entity;
    }

    public async Task UpdateAsync(GuardianStudentRelationshipStorageContractV1 entity)
    {
        entity.UpdatedOn = DateTimeOffset.UtcNow;

        string sql = "UPDATE dbo." + tableName + " SET Relationship = @Relationship, IsEmergencyContact = @IsEmergencyContact, IsAuthorizedPickup = @IsAuthorizedPickup " +
                     "WHERE StudentId = @StudentId AND GuardianId = @GuardianId;";

        await _db.SaveData(sql, entity);
    }

    public async Task DeleteAsync(string id)
    {
        await Task.Delay(1000);
    }
}

