﻿using Course.Invoice.Application.Abstractions.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Course.Invoice.Infrastructure.Services;
public class FileService : IFileService
{
    private readonly string _env;
    private readonly IConfiguration _configuration;
    private readonly ILogger<FileService> _logger;

    public FileService(
        string webHostEnvironment, 
        IConfiguration configuration, 
        ILogger<FileService> logger)
    {
        _configuration = configuration;
        _env = webHostEnvironment;
        _logger = logger;
    }

    public async Task<string> SaveInvoicePdf(Domain.Invoice.Invoice invoice, byte[] pdfBytes)
    {
        var fileName = $"Invoice_{invoice.Id}_{invoice.CreatedDate:yyyyMMdd_HHmmss}";

        var fileExtension = ".pdf";

        var fileDirectory = Path.Combine(_env, "invoices");

        if (!Directory.Exists(fileDirectory))
        {
            Directory.CreateDirectory(fileDirectory);
        }

        var filePath = UniqueFilePath(fileName, fileExtension, fileDirectory);

        await File.WriteAllBytesAsync(filePath, pdfBytes);

        var fileUrl = $"{_configuration["HostScheme"]}/invoices/{fileName}";

        _logger.LogInformation($"{fileUrl} has been created!");

        return fileUrl;
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