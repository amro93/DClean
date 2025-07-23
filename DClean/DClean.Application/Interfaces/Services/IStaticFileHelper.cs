using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DClean.Application.DTOs.StaticFiles;

namespace DClean.Application.Interfaces.Services
{
    public interface IStaticFileHelper
    {
        //Task<FileDto> SaveFileAsync(IFormFile file, string rootFolder = null, long? userId = null);
        Task<FileDto> SaveFileAsync(string base64File, string fileName, string rootFolder = null, long? userId = null);
        Task<FileDto> SaveFileAsync(IBase64FileVM base64FileVM, string rootFolder = null, long? userId = null);
        Task<bool> DeleteFileAsync(string path);
        bool DeleteFile(string path);
    }
}
