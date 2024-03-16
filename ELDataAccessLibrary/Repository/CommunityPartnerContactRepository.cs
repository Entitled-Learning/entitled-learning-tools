// ----------------------------------------------------------------
// <copyright company="Tope Daramola">
//     Copyright (c) Tope Daramola. All rights reserved.
// </copyright>
// ----------------------------------------------------------------

using Microsoft.Extensions.Logging;
using ELDataAccessLibrary.StorageContracts;

namespace ELDataAccessLibrary.Repository;

public class CommunityPartnerContactRepository : RepositoryBase, IDataRepository<CommunityPartnerContactStorageContractV1>
{
    private readonly string tableName = "CommunityPartnerContact";
    private readonly ISqlDataAccess _db;
    private readonly ILogger<StudentRepository> _logger;

    public CommunityPartnerContactRepository(ISqlDataAccess db, ILogger<StudentRepository> logger)
    {
       _db = db;
       _logger = logger;
    }

    public async Task<IEnumerable<CommunityPartnerContactStorageContractV1>> GetAllAsync()
    {
        string sql = "select * from dbo." + tableName + ";";

        try {
            var data = await _db.LoadData<CommunityPartnerContactStorageContractV1, dynamic>(sql, new { });
            return data;
        } catch (Exception ex) {
            _logger.GetCommunityPartnerContactError(ex);
            throw;
        }
    }

    public async Task<CommunityPartnerContactStorageContractV1> GetByIdAsync(string id)
    {
        string sql = "select * from dbo." + tableName + " where Id = @Id;";

        try {
            var data = await _db.LoadData<CommunityPartnerContactStorageContractV1, dynamic>(sql, new { Id = id });
            return data.FirstOrDefault()!;
        } catch (Exception ex) {
            _logger.GetCommunityPartnerContactError(ex);
            throw;
        }
    }

    public async Task<IEnumerable<CommunityPartnerContactStorageContractV1>> GetByPartnerNameAsync(string partnerName)
    {
        string sql = "select * from dbo." + tableName + " where CommunityPartnerName = @CommunityPartnerName;";

        try {
            var data = await _db.LoadData<CommunityPartnerContactStorageContractV1, dynamic>(sql, new { CommunityPartnerName = partnerName });
            return data;
        } catch (Exception ex) {
            _logger.GetCommunityPartnerContactError(ex);
            throw;
        }
    }

    public async Task<CommunityPartnerContactStorageContractV1> AddAsync(CommunityPartnerContactStorageContractV1 entity)
    {       
        entity.Id = GenerateId(entity.FirstName, entity.LastName);
        
        string sql = "insert into dbo." + tableName + " (Id, Prefix, FirstName, MiddleName, LastName, Suffix, OfficePhoneNumber, CellPhoneNumber, EmailAddress, AddressLine1, AddressLine2, City, State, ZipCode, CommunityPartnerName, ContractVersion) " +
        "values (@Id, @Prefix, @FirstName, @MiddleName, @LastName, @Suffix, @OfficePhoneNumber, @CellPhoneNumber, @EmailAddress, @AddressLine1, @AddressLine2, @City, @State, @ZipCode, @CommunityPartnerName, @ContractVersion);";

        try {
            await _db.SaveData(sql, entity);
            return entity;
        } catch (Exception ex) {
            _logger.CreateCommunityPartnerContactError(ex);
            throw;
        }
    }
    public async Task UpdateAsync(CommunityPartnerContactStorageContractV1 entity)
    {
        entity.UpdatedOn = DateTimeOffset.UtcNow;

        try{
            string sql = "update dbo." + tableName + " set Prefix = @Prefix, FirstName = @FirstName, MiddleName = @MiddleName, LastName = @LastName, Suffix = @Suffix, OfficePhoneNumber = @OfficePhoneNumber, CellPhoneNumber = @CellPhoneNumber, EmailAddress = @EmailAddress, AddressLine1 = @AddressLine1, AddressLine2 = @AddressLine2, City = @City, State = @State, ZipCode = @ZipCode, CommunityPartnerName = @CommunityPartnerName, ContractVersion = @ContractVersion, UpdatedOn = @UpdatedOn where Id = @Id;";

            await _db.SaveData(sql, entity);
        } catch (Exception ex) {
            _logger.UpdateCommunityPartnerContactError(ex);
            throw;
        }
    }

    public async Task DeleteAsync(string id)
    {
        try {
            string sql = "delete from dbo." + tableName + " where Id = @Id;";
            await _db.SaveData(sql, new { Id = id });
        } catch (Exception ex) {
            _logger.DeleteCommunityPartnerContactError(ex);
            throw;
        }
    }
}

