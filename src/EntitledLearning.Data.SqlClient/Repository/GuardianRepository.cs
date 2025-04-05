// ----------------------------------------------------------------
// <copyright company="Tope Daramola">
//     Copyright (c) Tope Daramola. All rights reserved.
// </copyright>
// ----------------------------------------------------------------

using Microsoft.Extensions.Logging;
using EntitledLearning.Data.SqlClient;
using EntitledLearning.Data.StorageContracts;

namespace EntitledLearning.Data.Repository;

public class GuardianRepository : RepositoryBase, IDataRepository<GuardianStorageContractV1>
{
    private readonly string tableName = "Guardian";
    private readonly ISqlDataClient _db;
    private readonly ILogger<GuardianRepository> _logger;

    public GuardianRepository(ISqlDataClient db, ILogger<GuardianRepository> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<IEnumerable<GuardianStorageContractV1>> GetAllAsync()
    {
        string sql = "select * from dbo." + tableName + ";";

        try{
            var data = await _db.LoadData<GuardianStorageContractV1, dynamic>(sql, new { });
            return data;
        } catch (Exception ex) {
            _logger.GetGuardianError(ex);
            throw;
        }
    }

    public async Task<GuardianStorageContractV1> GetByIdAsync(string id)
    {
        string sql = "select * from dbo." + tableName + " where Id = @Id;";

        try{
            var data = await _db.LoadData<GuardianStorageContractV1, dynamic>(sql, new { Id = id });
            return data.FirstOrDefault()!;
        } catch (Exception ex) {
            _logger.GetGuardianError(ex);
            throw;
        }
    }

    public async Task<GuardianStorageContractV1> GetByEmailAsync(string value)
    {
        string sql = "select * from dbo." + tableName + " where EmailAddress = @EmailAddress;";

        try
        {
            var data = await _db.LoadData<GuardianStorageContractV1, dynamic>(sql, new { EmailAddress = value });
            return data.FirstOrDefault()!;
        }
        catch (Exception ex)
        {
            _logger.GetGuardianError(ex);
            throw;
        }
    }

    public async Task<GuardianStorageContractV1> AddAsync(GuardianStorageContractV1 entity)
    {
        entity.Id = GenerateId(entity.FirstName, entity.LastName);

        string sql = "if not exists (select 1 from dbo." + tableName + " WHERE Id = @Id) " +
            "begin " +
            "    insert into dbo." + tableName + " (Id, FirstName, LastName, EmailAddress, CellPhoneNumber) " +
            "    values (@Id, @FirstName, @LastName, @EmailAddress, @CellPhoneNumber); " +
            "end;";

        try
        {
            await _db.SaveData(sql, entity);
            return entity;
        } catch (Exception ex) {
            _logger.CreateGuardianError(ex);
            throw;
        }
    }

    public async Task<GuardianStorageContractV1> UpsertAsync(GuardianStorageContractV1 entity)
    {
        entity.Id = entity.Id is null ? GenerateId(entity.FirstName, entity.LastName) : entity.Id;

        string sql = "if exists (select 1 from dbo." + tableName + " where id = @id) " +
                     "begin " +
                     "    update dbo." + tableName + " " +
                     "    set Prefix = @Prefix, Firstname = @FirstName, MiddleName = @MiddleName, LastName = @LastName, Suffix = @Suffix, " +
                     "        CellPhoneNumber = @CellPhoneNumber,EmailAddress = @EmailAddress, AddressLine1 = @AddressLine1, AddressLine2 = @AddressLine2, City = @City, " +
                     "        State = @State, Zipcode = @ZipCode, ReceiveUpdates = @ReceiveUpdates, Contractversion = @ContractVersion " +
                     "    where Id = @Id; " +
                     "end " +
                     "else " +
                     "begin " +
                     "    insert into dbo." + tableName + " (Id, Prefix, FirstName, MiddleName, LastName, Suffix, CellPhoneNumber, EmailAddress, AddressLine1, AddressLine2, City, State, ZipCode, ReceiveUpdates, ContractVersion) " +
                     "    values (@Id, @Prefix, @FirstName, @MiddleName, @LastName, @Suffix, @CellPhoneNumber, @EmailAddress, @AddressLine1, @AddressLine2, @City, @State, @ZipCode, @ReceiveUpdates, @ContractVersion); " +
                     "end;";

        try
        {
            await _db.SaveData(sql, entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.CreateGuardianError(ex);
            throw;
        }
    }


    public async Task UpdateAsync(GuardianStorageContractV1 entity)
    {
        entity.UpdatedOn = DateTimeOffset.UtcNow;

        string sql = "UPDATE dbo." + tableName + " SET Prefix = @Prefix, FirstName = @FirstName, MiddleName = @MiddleName, LastName = @LastName, Suffix = @Suffix, " +
            "CellPhoneNumber = @CellPhoneNumber, EmailAddress = @EmailAddress, AddressLine1 = @AddressLine1, AddressLine2 = @AddressLine2, City = @City, State = @State, " +
            "ZipCode = @ZipCode, ReceiveUpdates = @ReceiveUpdates, ContractVersion = @ContractVersion WHERE Id = @Id;";

        try{
            await _db.SaveData(sql, entity);
        } catch (Exception ex) {
            _logger.UpdateGuardianError(ex);
            throw;
        }
    }

    public async Task DeleteAsync(string id)
    {
        try{
            string guardianIdParameter = id;
            string sql = "delete from dbo." + tableName + " where Id = @guardianIdParameter;";
            await _db.SaveData(sql, new { guardianIdParameter });
        } catch (Exception ex) {
            _logger.DeleteGuardianError(ex);
            throw;
        }
    }
}

