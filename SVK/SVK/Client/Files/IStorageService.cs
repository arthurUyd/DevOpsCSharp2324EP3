using Microsoft.AspNetCore.Components.Forms;

namespace SVK.Client.Files;

public interface IStorageService
{
    Task UploadImageAsync(string sas, IBrowserFile file);
}