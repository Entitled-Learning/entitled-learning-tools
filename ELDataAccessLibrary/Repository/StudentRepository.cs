using System;
using ELDataAccessLibrary.StorageContracts;

namespace ELDataAccessLibrary.Repository;

public class StudentRepository : IDataRepository<StudentStorageContractV1>
{
    private readonly ISqlDataAccess _db;

    public StudentRepository(ISqlDataAccess db)
    {
       _db = db;
    }

    public async Task<IEnumerable<StudentStorageContractV1>> GetAllAsync()
    {
        string sql = "select * from dbo.Students;";
        var data = await _db.LoadData<StudentStorageContractV1, dynamic>(sql, new { });

        return data;
    }

    public async Task<StudentStorageContractV1> GetByIdAsync(int id)
    {
        string sql = "select * from dbo.Students where Id = @Id;";
        var data = await _db.LoadData<StudentStorageContractV1, dynamic>(sql, new { });

        return data.FirstOrDefault()!;
    }

    public async Task AddAsync(StudentStorageContractV1 entity)
    {
        string sql = "insert into dbo.Students (FirstName, LastName, CellPhoneNumber, EmailAddress, AddressLine1, AddressLine2, City, State, ZipCode) values (@FirstName, @LastName, @CellPhoneNumber, @EmailAddress, @AddressLine1, @AddressLine2, @City, @State, @ZipCode);";

        await _db.SaveData(sql, entity);
    }

    public async Task UpdateAsync(StudentStorageContractV1 entity)
    {
        await Task.Delay(1000);
    }

    public async Task DeleteAsync(int id)
    {
        await Task.Delay(1000);
    }
}

