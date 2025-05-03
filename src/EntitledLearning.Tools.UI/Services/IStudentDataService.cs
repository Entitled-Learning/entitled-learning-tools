using EntitledLearning.Data.StorageContracts;
using EntitledLearning.Tools.UI.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace EntitledLearning.Tools.UI.Services;

public interface IStudentDataService
{
    Task<IEnumerable<StudentStorageContractV1>> GetAllStudentsAsync();
    Task<IEnumerable<StudentStorageContractV1>> FilterStudentsAsync(string searchTerm, IEnumerable<StudentStorageContractV1> students);
    Task<MemoryStream> GenerateExcelFileAsync(IEnumerable<StudentStorageContractV1> students);
    Task<StudentStorageContractV1> ProcessStudentRecordAsync(CsvStudentGuardianRecord record);
    Task<bool> ProcessCsvFileAsync(IBrowserFile file);
    bool ValidateCsvFile(IBrowserFile file);
    Task<string> GetSampleCsvContentAsync();
    Task<string> GetEnrollmentReportSampleAsync();
}