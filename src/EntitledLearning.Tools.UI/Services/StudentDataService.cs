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

            // Detect the CSV format
            var format = await DetectCsvFormatAsync(memoryStream);
            memoryStream.Position = 0;

            if (format == CsvFormat.EnrollmentReport)
            {
                return await ProcessEnrollmentReportCsvAsync(memoryStream);
            }
            else
            {
                return await ProcessStandardCsvAsync(memoryStream);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing CSV file");
            return false;
        }
    }

    private async Task<bool> ProcessStandardCsvAsync(MemoryStream memoryStream)
    {
        try
        {
            using var reader = new StreamReader(memoryStream);
            
            // Create a configuration that trims headers
            var csvConfig = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture) 
            {
                PrepareHeaderForMatch = args => args.Header.Trim(),
                TrimOptions = CsvHelper.Configuration.TrimOptions.Trim
            };
            
            using var csv = new CsvHelper.CsvReader(reader, csvConfig);
                
            // Register the mapping for standard format
            csv.Context.RegisterClassMap<CsvStudentGuardianRecordMap>();
            var csvRecords = csv.GetRecords<CsvStudentGuardianRecord>().ToList();

            foreach (var record in csvRecords)
            {
                try
                {
                    // Process student
                    var studentContract = _mapper.ToStudentStorageContractV1(record);
                    
                    // Check if student already exists
                    StudentStorageContractV1? existingStudent = null;
                    if (!string.IsNullOrEmpty(studentContract.Id))
                    {
                        existingStudent = await _studentRepository.GetByIdAsync(studentContract.Id);
                    }
                    
                    // Use UpsertAsync to create or update the student record
                    var storedStudent = await _studentRepository.UpsertAsync(studentContract);
                    
                    // If student already existed, remove all existing guardian relationships
                    if (existingStudent != null)
                    {
                        // Get all student's guardian relationships
                        var existingRelationships = await _relationshipRepository.GetByStudentIdAsync(storedStudent.Id!);
                        
                        // Delete all existing relationships for this student
                        foreach (var existingRelationship in existingRelationships)
                        {
                            await _relationshipRepository.DeleteByGuardianIdAsync(existingRelationship.GuardianId!);
                        }
                    }
                    
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
                    _logger.LogError(ex, "Error processing standard CSV record");
                    throw;
                }
            }
            
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing standard CSV format");
            throw;
        }
    }

    private async Task<bool> ProcessEnrollmentReportCsvAsync(MemoryStream memoryStream)
    {
        try
        {
            using var reader = new StreamReader(memoryStream);
            
            // Create a configuration that trims headers
            var csvConfig = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture) 
            {
                PrepareHeaderForMatch = args => args.Header.Trim(),
                TrimOptions = CsvHelper.Configuration.TrimOptions.Trim
            };
            
            using var csv = new CsvHelper.CsvReader(reader, csvConfig);

            // Register the mapping for enrollment report format
            csv.Context.RegisterClassMap<EnrollmentReportRecordMap>();
            
            // Skip header rows in enrollment report (since it has multiple header rows)
            csv.Read();
            csv.Read();
            csv.Read();
            csv.Read();
            
            var csvRecords = csv.GetRecords<EnrollmentReportRecord>().ToList();

            foreach (var record in csvRecords)
            {
                try
                {
                    // Skip empty rows
                    if (string.IsNullOrWhiteSpace(record.ScholarName) || 
                        string.IsNullOrWhiteSpace(record.ParentName))
                    {
                        continue;
                    }
                    
                    // Process student
                    var studentContract = _mapper.ToStudentStorageContractV1(record);
                    
                    // Check if student already exists
                    StudentStorageContractV1? existingStudent = null;
                    if (!string.IsNullOrEmpty(studentContract.Id))
                    {
                        existingStudent = await _studentRepository.GetByIdAsync(studentContract.Id);
                    }
                    
                    // Use UpsertAsync to create or update the student record
                    var storedStudent = await _studentRepository.UpsertAsync(studentContract);
                    
                    // If student already existed, remove all existing guardian relationships
                    if (existingStudent != null)
                    {
                        // Get all student's guardian relationships
                        var existingRelationships = await _relationshipRepository.GetByStudentIdAsync(storedStudent.Id!);
                        
                        // Delete all existing relationships for this student
                        foreach (var existingRelationship in existingRelationships)
                        {
                            await _relationshipRepository.DeleteByGuardianIdAsync(existingRelationship.GuardianId!);
                            await _guardianRepository.DeleteAsync(existingRelationship.GuardianId!);
                        }
                    }

                    // Process primary guardian
                    var primaryGuardianContract = _mapper.ToPrimaryGuardianStorageContractV1(record);
                    var storedPrimaryGuardian = await _guardianRepository.AddAsync(primaryGuardianContract);

                    // Create relationship between student and primary guardian
                    var primaryRelationship = new GuardianStudentRelationshipStorageContractV1
                    {
                        StudentId = storedStudent.Id,
                        GuardianId = storedPrimaryGuardian.Id,
                        Relationship = "Primary Guardian",
                        IsAuthorizedPickup = true, // Assuming primary guardian is always authorized pickup
                        IsEmergencyContact = true  // Assuming primary guardian is always emergency contact
                    };
                    await _relationshipRepository.AddAsync(primaryRelationship);
                    
                    // Process caregiver if exists
                    var caregiverGuardianContract = _mapper.ToCaregiverGuardianStorageContractV1(record);
                    if (caregiverGuardianContract != null)
                    {
                        var storedCaregiverGuardian = await _guardianRepository.AddAsync(caregiverGuardianContract);
                        var caregiverRelationship = new GuardianStudentRelationshipStorageContractV1
                        {
                            StudentId = storedStudent.Id,
                            GuardianId = storedCaregiverGuardian.Id,
                            Relationship = "Caregiver"
                        };
                        await _relationshipRepository.AddAsync(caregiverRelationship);
                    }
                    
                    // Process secondary contact if exists
                    var secondaryContactGuardianContract = _mapper.ToSecondaryContactGuardianStorageContractV1(record);
                    if (secondaryContactGuardianContract != null)
                    {
                        var storedSecondaryContactGuardian = await _guardianRepository.AddAsync(secondaryContactGuardianContract);
                        var secondaryContactRelationship = new GuardianStudentRelationshipStorageContractV1
                        {
                            StudentId = storedStudent.Id,
                            GuardianId = storedSecondaryContactGuardian.Id,
                            Relationship = "Secondary Contact",
                            IsEmergencyContact = true // Assuming secondary contact is always emergency contact
                        };
                        await _relationshipRepository.AddAsync(secondaryContactRelationship);
                    }
                    
                    // Process authorized pickup if exists
                    // var authorizedPickupGuardianContract = _mapper.ToAuthorizedPickupGuardianStorageContractV1(record);
                    // if (authorizedPickupGuardianContract != null)
                    // {
                    //     var storedAuthorizedPickupGuardian = await _guardianRepository.AddAsync(authorizedPickupGuardianContract);
                    //     var authorizedPickupRelationship = new GuardianStudentRelationshipStorageContractV1
                    //     {
                    //         StudentId = storedStudent.Id,
                    //         GuardianId = storedAuthorizedPickupGuardian.Id
                    //     };
                    //     await _relationshipRepository.AddAsync(authorizedPickupRelationship);
                    // }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing enrollment report record");
                    throw;
                }
            }
            
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing enrollment report CSV format");
            throw;
        }
    }

    private async Task<CsvFormat> DetectCsvFormatAsync(MemoryStream memoryStream)
    {
        try
        {
            using var reader = new StreamReader(memoryStream, leaveOpen: true);
            
            // Read the first line to get header
            string? headerLine = await reader.ReadLineAsync();
            
            if (string.IsNullOrEmpty(headerLine))
                return CsvFormat.Standard;

            // Check if it looks like the enrollment report format
            if (headerLine.Contains("Scholar ID") || 
                headerLine.Contains("Scholar Name") || 
                headerLine.Contains("Parent Name") ||
                headerLine.StartsWith(",Activity,Day,") ||  // First line of enrollment report
                headerLine.Contains("Parent Address"))
            {
                return CsvFormat.EnrollmentReport;
            }

            return CsvFormat.Standard;
        }
        catch
        {
            // Default to standard format if detection fails
            return CsvFormat.Standard;
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
        // Standard format sample
        var standardFormatSample = "FirstName,LastName,Email,AddressLine1,AddressLine2,City,State,ZipCode,GuardianFirstName,GuardianLastName,GuardianEmail\n" +
                              "John,Doe,john.doe@example.com,123 Main St,,Springfield,IL,62701,Jane,Doe,jane.doe@example.com\n" +
                              "Alice,Smith,alice.smith@example.com,456 Elm St,Apt 2B,Chicago,IL,60614,Bob,Smith,bob.smith@example.com\n" +
                              "Charlie,Brown,charlie.brown@example.com,789 Oak St,,Peoria,IL,61602,Susan,Brown,susan.brown@example.com";

        return Task.FromResult(standardFormatSample);
    }
    
    public Task<string> GetEnrollmentReportSampleAsync()
    {
        // Enrollment report format sample (simplified)
        var enrollmentSample = "Scholar ID,Scholar Name,Age,Birthday,Allergies,School,Medication Instructions,Gender,Race,Grade Level Completed,Learning Accommodations,Parent Name,Parent Email,Parent Phone,Parent Address Street 1,Parent Address Street 2,Parent City,Parent State,Parent Zipcode\n" +
                        "1001,John Doe,10 yrs,5/15/14,None,Lincoln Elementary,None,Male,Black or African American,4th,None,Jane Doe,jane.doe@example.com,555-123-4567,123 Main St,,Springfield,IL,62701\n" +
                        "1002,Alice Smith,8 yrs,9/22/16,Peanuts,Washington Elementary,Epipen as needed,Female,Black or African American,2nd,None,Bob Smith,bob.smith@example.com,555-987-6543,456 Elm St,Apt 2B,Chicago,IL,60614\n" +
                        "1003,Charlie Brown Jr.,12 yrs,3/30/12,None,Jefferson Middle School,Inhaler twice daily,Male,Black or African American,6th,Extra time for tests,Susan Brown,susan.brown@example.com,555-456-7890,789 Oak St,,Peoria,IL,61602";

        return Task.FromResult(enrollmentSample);
    }
}

public enum CsvFormat
{
    Standard,
    EnrollmentReport
}