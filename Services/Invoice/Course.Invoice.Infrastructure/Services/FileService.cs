using Course.Invoice.Application.Abstractions.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Course.Invoice.Infrastructure.Services;
public class FileService : IFileService
{
    private readonly IWebHostEnvironment _env;
    private readonly IConfiguration _configuration;
    private readonly ILogger<FileService> _logger;

    public FileService(
        IWebHostEnvironment webHostEnvironment, 
        IConfiguration configuration, 
        ILogger<FileService> logger)
    {
        _configuration = configuration;
        _env = webHostEnvironment;
        _logger = logger;
    }

    public async Task<(string FileUrlWithEnv, string FileUrlWithOutEnv)> SaveInvoicePdf(Domain.Invoice.Invoice invoice, byte[] pdfBytes)
    {
        var fileName = $"Invoice_i_{invoice.Id}_o_{invoice.OrderInformation.OrderId}_b_{invoice.OrderInformation.BuyerId}_{invoice.CreatedDate:dd_MM_yyyy_HH_mm_ss}";

        var fileExtension = ".pdf";

        var fileDirectory = Path.Combine(_env.WebRootPath, "invoices");

        if (!Directory.Exists(fileDirectory))
        {
            Directory.CreateDirectory(fileDirectory);
        }

        var filePath = UniqueFilePath(fileName, fileExtension, fileDirectory);

        await File.WriteAllBytesAsync(filePath, pdfBytes);

        var fileUrlWithEnv = $"{_configuration["HostScheme"]}/invoices/{fileName + fileExtension}";
        var fileUrlWithoutEnv = $"/invoices/{fileName + fileExtension}";

        _logger.LogInformation($"{fileUrlWithEnv} has been created!");

        return (fileUrlWithEnv, fileUrlWithoutEnv);
    }

    private static string UniqueFilePath(string fileName, string fileExtension, string directory)
    {
        var path = Path.Combine(directory, fileName + fileExtension);

        if (File.Exists(path))
        {
            fileName += $"_{Guid.NewGuid()}";
            path = Path.Combine(directory, fileName + fileExtension);
        }
        return path;
    }
}