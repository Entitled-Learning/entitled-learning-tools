using EntitledLearning.Data.Repository;
using EntitledLearning.Data.StorageContracts;
using EntitledLearning.Tools.UI.Models;
using Microsoft.AspNetCore.Components.Forms;
using OfficeOpenXml;
using System.Globalization;

namespace EntitledLearning.Tools.UI.Services;

public class StudentDataService : IStudentDataService
{
    private readonly StudentRepository _studentRepository;
    private readonly GuardianRepository _guardianRepository;
    private readonly GuardianStudentRelationshipRepository _relationshipRepository;
    private readonly DataMapper _mapper;
    private readonly ILogger<StudentDataService> _logger;
    private const int MaxAllowedFileSize = 5 * 1024 * 1024; // 5MB

    public StudentDataService(
        StudentRepository studentRepository, 
        GuardianRepository guardianRepository, 
        GuardianStudentRelationshipRepository relationshipRepository,
        DataMapper mapper,
        ILogger<StudentDataService> logger)
    {
        _studentRepository = studentRepository;
        _guardianRepository = guardianRepository;
        _relationshipRepository = relationshipRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<StudentStorageContractV1>> GetAllStudentsAsync()
    {
        try
        {
            var students = await _studentRepository.GetAllAsync();
            return students;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving students");
            throw;
        }
    }

    public async Task<IEnumerable<StudentStorageContractV1>> FilterStudentsAsync(string searchTerm, IEnumerable<StudentStorageContractV1> students)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return students;

        searchTerm = searchTerm.Trim();
        
        return students.Where(t =>
            (t.FirstName != null && t.FirstName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) ||
            (t.LastName != null && t.LastName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) ||
            (t.Race != null && t.Race.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) ||
            (t.AddressLine1 != null && t.AddressLine1.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) ||
            (t.AddressLine2 != null && t.AddressLine2.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) ||
            (t.City != null && t.City.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) ||
            (t.State != null && t.State.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) ||
            (t.ZipCode != null && t.ZipCode.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)));
    }

    public async Task<MemoryStream> GenerateExcelFileAsync(IEnumerable<StudentStorageContractV1> students)
    {
        var studentData = new List<Dictionary<string, object>>();
        
        foreach (var student in students)
        {
            var dict = student.ToDictionary();
            
            // Fetch Guardian data for each student
            var guardians = await _relationshipRepository.GetByStudentIdAsync(student.Id!);
            dict["Guardians"] = string.Join(", ", guardians.Select(g => $"{g.FirstName} {g.LastName}"));
            
            studentData.Add(dict);
        }

        var package = new ExcelPackage();
        var worksheet = package.Workbook.Worksheets.Add("Students");

        // Add headers
        if (studentData.Count > 0)
        {
            int column = 1;
            foreach (var header in studentData.First().Keys)
            {
                worksheet.Cells[1, column].Value = header;
                column++;
            }

            // Add data
            int row = 2;
            foreach (var dataRow in studentData)
            {
                column = 1;
                foreach (var value in dataRow.Values)
                {
                    worksheet.Cells[row, column].Value = value;
                    column++;
                }
                row++;
            }
        }

        var memoryStream = new MemoryStream();
        await package.SaveAsAsync(memoryStream);
        memoryStream.Position = 0;
        
        return memoryStream;
    }

    public async Task<StudentStorageContractV1> ProcessStudentRecordAsync(CsvStudentGuardianRecord record)
    {
        var studentContract = _mapper.ToStudentStorageContractV1(record);
        return await _studentRepository.AddAsync(studentContract);
    }

    public async Task<bool> ProcessCsvFileAsync(IBrowserFile file)
    {
        if (!ValidateCsvFile(file))
            return false;

        try
        {
            using var stream = file.OpenReadStream(MaxAllowedFileSize);
            using var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            using var reader = new StreamReader(memoryStream);
            using var csv = new CsvHelper.CsvReader(reader, CultureInfo.InvariantCulture);
                
            // Register the mapping explicitly
            csv.Context.RegisterClassMap<CsvStudentGuardianRecordMap>();
            var csvRecords = csv.GetRecords<CsvStudentGuardianRecord>().ToList();

            foreach (var record in csvRecords)
            {
                try
                {
                    // Process student
                    var studentContract = _mapper.ToStudentStorageContractV1(record);
                    var storedStudent = await _studentRepository.AddAsync(studentContract);

                    // Process guardian
                    var guardianStorageContract = _mapper.ToGuardianStorageContractV1(record);
                    var storedGuardian = await _guardianRepository.AddAsync(guardianStorageContract);

                    // Create relationship
                    var relationship = new GuardianStudentRelationshipStorageContractV1
                    {
                        StudentId = storedStudent.Id,
                        GuardianId = storedGuardian.Id
                    };

                    await _relationshipRepository.AddAsync(relationship);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing CSV record");
                    throw;
                }
            }
            
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing CSV file");
            return false;
        }
    }

    public bool ValidateCsvFile(IBrowserFile file)
    {
        if (file.Size > MaxAllowedFileSize)
            return false;

        if (!file.Name.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
            return false;

        if (file.Size == 0)
            return false;

        return true;
    }

    public Task<string> GetSampleCsvContentAsync()
    {
        var sampleCsvContent = "FirstName,LastName,Email,AddressLine1,AddressLine2,City,State,ZipCode,GuardianFirstName,GuardianLastName,GuardianEmail\n" +
                              "John,Doe,john.doe@example.com,123 Main St,,Springfield,IL,62701,Jane,Doe,jane.doe@example.com\n" +
                              "Alice,Smith,alice.smith@example.com,456 Elm St,Apt 2B,Chicago,IL,60614,Bob,Smith,bob.smith@example.com\n" +
                              "Charlie,Brown,charlie.brown@example.com,789 Oak St,,Peoria,IL,61602,Susan,Brown,susan.brown@example.com";

        return Task.FromResult(sampleCsvContent);
    }
}