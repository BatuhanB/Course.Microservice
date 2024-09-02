using Course.Invoice.Application.Abstractions.Services;

namespace Course.Invoice.Infrastructure.Services;
public class FileService : IFileService
{
    private readonly string _env;

    public FileService(string webHostEnvironment)
    {
        _env = webHostEnvironment;
    }

    public async Task<string> SaveInvoicePdf(Domain.Invoice.Invoice invoice, byte[] pdfBytes, string scheme, string host)
    {
        var fileName = $"Invoice_{invoice.Id}_{invoice.CreatedDate:F}.pdf";

        var filePath = Path.Combine(_env, "invoices", fileName);

        Directory.CreateDirectory(Path.GetDirectoryName(filePath));

        await File.WriteAllBytesAsync(filePath, pdfBytes);

        // Generate the URL for accessing the file
        var fileUrl = $"{scheme}://{host}/invoices/{fileName}";

        return fileUrl;
    }

}
