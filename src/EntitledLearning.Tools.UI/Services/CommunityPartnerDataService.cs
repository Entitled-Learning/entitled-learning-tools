using EntitledLearning.Data.Repository;
using EntitledLearning.Data.StorageContracts;
using EntitledLearning.Tools.UI.Models;
using Microsoft.AspNetCore.Components.Forms;
using OfficeOpenXml;
using System.Globalization;

namespace EntitledLearning.Tools.UI.Services;

public class CommunityPartnerDataService : ICommunityPartnerDataService
{
    private readonly CommunityPartnerRepository _partnerRepository;
    private readonly DataMapper _mapper;
    private readonly ILogger<CommunityPartnerDataService> _logger;
    private const int MaxAllowedFileSize = 5 * 1024 * 1024; // 5MB

    public CommunityPartnerDataService(
        CommunityPartnerRepository partnerRepository,
        DataMapper mapper,
        ILogger<CommunityPartnerDataService> logger)
    {
        _partnerRepository = partnerRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<CommunityPartnerStorageContractV1>> GetAllPartnersAsync()
    {
        try
        {
            return await _partnerRepository.GetAllAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving community partners");
            throw;
        }
    }

    public Task<IEnumerable<CommunityPartnerStorageContractV1>> FilterPartnersAsync(string searchTerm, IEnumerable<CommunityPartnerStorageContractV1> partners)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return Task.FromResult(partners);

        searchTerm = searchTerm.Trim();
        
        var filteredPartners = partners.Where(t =>
            (t.Name != null && t.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) ||
            (t.EmailAddress != null && t.EmailAddress.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) ||
            (t.AddressLine1 != null && t.AddressLine1.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) ||
            (t.AddressLine2 != null && t.AddressLine2.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) ||
            (t.City != null && t.City.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) ||
            (t.State != null && t.State.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) ||
            (t.ZipCode != null && t.ZipCode.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)));

        return Task.FromResult(filteredPartners);
    }

    public async Task<MemoryStream> GenerateExcelFileAsync(IEnumerable<CommunityPartnerStorageContractV1> partners)
    {
        try
        {
            var partnerData = partners.Select(x => x.ToDictionary()).ToList();
            
            var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Community Partners");

            // Add headers
            if (partnerData.Count > 0)
            {
                int column = 1;
                foreach (var header in partnerData.First().Keys)
                {
                    worksheet.Cells[1, column].Value = header;
                    column++;
                }

                // Add data
                int row = 2;
                foreach (var dataRow in partnerData)
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
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating Excel file for community partners");
            throw;
        }
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
            csv.Context.RegisterClassMap<CsvCommunityPartnerRecordMap>();
            var csvRecords = csv.GetRecords<CsvCommunityPartnerRecord>().ToList();

            foreach (var record in csvRecords)
            {
                try
                {
                    var partnerContract = _mapper.ToCommunityPartnerStorageContractV1(record);
                    await _partnerRepository.AddAsync(partnerContract);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing CSV record for community partner");
                    throw;
                }
            }
            
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing CSV file for community partners");
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
        var sampleCsvContent = "Name,PhoneNumber,EmailAddress,AddressLine1,AddressLine2,City,State,ZipCode\n" +
                              "Community Partner 1,123-456-7890,partner1@example.com,123 Main St,,Springfield,IL,62701\n" +
                              "Community Partner 2,987-654-3210,partner2@example.com,456 Elm St,Apt 2B,Chicago,IL,60614\n" +
                              "Community Partner 3,555-555-5555,partner3@example.com,789 Oak St,,Peoria,IL,61602";

        return Task.FromResult(sampleCsvContent);
    }
}