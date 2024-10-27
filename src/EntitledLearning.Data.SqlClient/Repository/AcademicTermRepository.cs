// ----------------------------------------------------------------
// <copyright company="Tope Daramola">
//     Copyright (c) Tope Daramola. All rights reserved.
// </copyright>
// ----------------------------------------------------------------

using Microsoft.Extensions.Logging;
using EntitledLearning.Data.SqlClient;
using EntitledLearning.Data.StorageContracts;

namespace EntitledLearning.Data.Repository;

public class AcademicTermRepository : RepositoryBase, IDataRepository<AcademicTermStorageContractV1>
{
    private readonly string tableName = "AcademicTerms";
    private readonly ISqlDataClient _db;
    private readonly ILogger<AcademicTermRepository> _logger;

    public AcademicTermRepository(ISqlDataClient db, ILogger<AcademicTermRepository> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<IEnumerable<AcademicTermStorageContractV1>> GetAllAsync()
    {
        string sql = "select * from dbo." + tableName + ";";

        try
        {
            var data = await _db.LoadData<AcademicTermStorageContractV1, dynamic>(sql, new { });
            return data;
        }
        catch (Exception ex)
        {
            _logger.GetStudentError(ex);
            throw;
        }
    }

    public async Task<AcademicTermStorageContractV1> GetByIdAsync(string id)
    {
        string sql = "select * from dbo." + tableName + " where EnrollmentId = @Id;";

        try{
            var data = await _db.LoadData<AcademicTermStorageContractV1, dynamic>(sql, new { Id = id });
            return data.FirstOrDefault()!;
        } catch (Exception ex) {
            _logger.GetStudentError(ex);
            throw;
        }
    }

    public async Task<AcademicTermStorageContractV1?> GetActiveTerm()
    {
        string sql = "select * from dbo." + tableName + " where IsActive = @IsActive;";

        try
        {
            var data = await _db.LoadData<AcademicTermStorageContractV1, dynamic>(sql, new { IsActive = true });

            if(data.Count() > 1)
            {
                throw new InvalidOperationException("Found multiple active terms");
            }
            else
            {
                return data.FirstOrDefault();
            }
        }
        catch (Exception ex)
        {
            _logger.GetStudentError(ex);
            throw;
        }
    }

    public async Task<AcademicTermStorageContractV1> GetByNameAsync(string name)
    {
        string sql = "select * from dbo." + tableName + " where TermName = @TermName;";

        try
        {
            var data = await _db.LoadData<AcademicTermStorageContractV1, dynamic>(sql, new { TermName = name });
            return data.FirstOrDefault()!;
        }
        catch (Exception ex)
        {
            _logger.GetStudentError(ex);
            throw;
        }
    }

    public async Task<AcademicTermStorageContractV1> AddAsync(AcademicTermStorageContractV1 entity)
    {
        string sql = "if not exists (select 1 from dbo." + tableName + " WHERE TermName = @TermName) " +
            "begin" +
            "insert into dbo." + tableName + " (TermName, StartDate, EndDate, IsActive) " +
            "values (@TermName, @StartDate, @EndDate, @IsActive) " +
            "end;";

        try{
            await _db.SaveData(sql, entity);
            return entity;
        } catch (Exception ex) {
            _logger.CreateStudentError(ex);
            throw;
        }
    }

    public async Task<AcademicTermStorageContractV1> UpsertAsync(AcademicTermStorageContractV1 entity)
    {
        string sql = "if exists (select 1 from dbo." + tableName + " WHERE TermName = @TermName) " +
             "begin " +
             "    update dbo." + tableName + " " +
             "    set TermName = @TermName, StartDate = @StartDate, EndDate= @EndDate, IsActive = @IsActive " +
             "    where TermName = @TermName; " +
             "end "   +
             "else "  +
             "begin " +
             "    insert into dbo." + tableName + " (TermName, StartDate, EndDate, IsActive) " +
             "    values (@TermName, @StartDate, @EndDate, @IsActive) " +
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

    public async Task UpdateAsync(AcademicTermStorageContractV1 entity)
    {
        entity.UpdatedOn = DateTimeOffset.UtcNow;

        string sql = "update dbo." + tableName + " " +
                     "    set TermName = @TermName, StartDate = @StartDate, EndDate= @EndDate, IsActive = @IsActive " +
                     "    where TermName = @TermName;";

        try
        {
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

