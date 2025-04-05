// ----------------------------------------------------------------
// <copyright company="Tope Daramola">
//     Copyright (c) Tope Daramola. All rights reserved.
// </copyright>
// ----------------------------------------------------------------

using Microsoft.Extensions.Logging;
using EntitledLearning.Data.SqlClient;
using EntitledLearning.Data.StorageContracts;

namespace EntitledLearning.Data.Repository;

public class EnrollmentRepository : RepositoryBase, IDataRepository<EnrollmentStorageContractV1>
{
    private readonly string tableName = "Enrollment";
    private readonly string termTableName = "AcademicTerms";
    private readonly ISqlDataClient _db;
    private readonly ILogger<EnrollmentRepository> _logger;

    public EnrollmentRepository(ISqlDataClient db, ILogger<EnrollmentRepository> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<IEnumerable<EnrollmentStorageContractV1>> GetAllAsync()
    {
        string sql = "select * from dbo." + tableName + ";";

        try
        {
            var data = await _db.LoadData<EnrollmentStorageContractV1, dynamic>(sql, new { });
            return data;
        }
        catch (Exception ex)
        {
            _logger.GetStudentError(ex);
            throw;
        }
    }

    public async Task<EnrollmentStorageContractV1> GetByIdAsync(string id)
    {
        string sql = "select * from dbo." + tableName + " where EnrollmentId = @Id;";

        try{
            var data = await _db.LoadData<EnrollmentStorageContractV1, dynamic>(sql, new { EnrollmentId = id });
            return data.FirstOrDefault()!;
        } catch (Exception ex) {
            _logger.GetStudentError(ex);
            throw;
        }
    }

    public async Task<EnrollmentStorageContractV1?> GetActiveEnrollmentByStudentIdAsync(string id)
    {

        string sql = "select * from dbo." + tableName + " e " +
            "join dbo." + termTableName + " a on a.TermId = e.TermId" +
            " where StudentId = @StudentId and a.IsActive = @IsActive;";

        try
        {
            var data = await _db.LoadData<EnrollmentStorageContractV1, dynamic>(sql, new { StudentId = id,  IsActive = true });
            return data.FirstOrDefault();
        }
        catch (Exception ex)
        {
            _logger.GetStudentError(ex);
            throw;
        }
    }

    public async Task<EnrollmentStorageContractV1> AddAsync(EnrollmentStorageContractV1 entity)
    {
        string sql = "if not exists (select 1 from dbo." + tableName + " WHERE StudentId = @StudentId and TermId = @TermId) " +
            "begin " +
            "    insert into dbo." + tableName + " (StudentId, TermId, EnrollmentDate, EnrollmentStatus, Notes) " +
            "    values (@StudentId, @TermId, @EnrollmentDate, @EnrollmentStatus, @Notes); " +
            "end;";

        try{
            await _db.SaveData(sql, entity);
            return entity;
        } catch (Exception ex) {
            _logger.CreateStudentError(ex);
            throw;
        }
    }

    public async Task<EnrollmentStorageContractV1> UpsertAsync(EnrollmentStorageContractV1 entity)
    {
        string sql = "if exists (select 1 from dbo." + tableName + " where StudentId = @StudentId and TermId = @TermId) " +
             "begin " +
             "    update dbo." + tableName + " " +
             "    set StudentId = @StudentId, TermId = @TermId, EnrollmentDate= @EnrollmentDate, EnrollmentStatus = @EnrollmentStatus, Notes = @Notes " +
             "    where StudentId = @StudentId and TermId = @TermId; " +
             "end " +
             "else " +
             "begin " +
             "    insert into dbo." + tableName + " (StudentId, TermId, EnrollmentDate, EnrollmentStatus, Notes) " +
             "    values (@StudentId, @TermId, @EnrollmentDate, @EnrollmentStatus, @Notes) " +
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

    public async Task UpdateAsync(EnrollmentStorageContractV1 entity)
    {
        entity.UpdatedOn = DateTimeOffset.UtcNow;

        string sql = "update dbo." + tableName + " " +
                     "    set StudentId = @StudentId, TermId = @TermId, EnrollmentDate= @EnrollmentDate, EnrollmentStatus = @EnrollmentStatus, Notes = @Notes " +
                     "    where StudentId = @StudentId and TermId = @TermId;";

        try{
            await _db.SaveData(sql, entity);
        } catch (Exception ex) {
            _logger.UpdateStudentError(ex);
            throw;
        }
    }

    public async Task DeleteAsync(string id)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }
}

