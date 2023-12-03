using System;
using ELDataAccessLibrary.StorageContracts;

namespace ELDataAccessLibrary.Repository;

public class GuardianStudentRelationshipRepository : IDataRepository<GuardianStudentRelationshipStorageContractV1>
{
    private readonly ISqlDataAccess _db;
    private readonly string tableName = "GuardianStudentRelationship";

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

    public async Task<GuardianStudentRelationshipStorageContractV1> GetByIdAsync(int id)
    {
        string sql = "select * from dbo." + tableName + " where StudentId = @Id;";
        var data = await _db.LoadData<GuardianStudentRelationshipStorageContractV1, dynamic>(sql, new { });

        return data.FirstOrDefault()!;
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
        await Task.Delay(1000); 
    }

    public async Task DeleteAsync(int id)
    {
        await Task.Delay(1000);
    }
}

