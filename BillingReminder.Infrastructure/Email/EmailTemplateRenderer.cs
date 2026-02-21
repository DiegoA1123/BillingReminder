using System.Text;
using System.IO;

namespace BillingReminder.Infrastructure.Email;

public static class EmailTemplateRenderer
{
    public static async Task<string> Render(string templateFileName, Dictionary<string, string> values, CancellationToken ct = default)
    {
        var basePath = AppContext.BaseDirectory;
        var fullPath = Path.Combine(basePath, "Email", "Templates", templateFileName);

        var html = await File.ReadAllTextAsync(fullPath, Encoding.UTF8, ct);

        foreach (var kv in values)
        {
            html = html.Replace(kv.Key, kv.Value);
        }

        return html;
    }
}