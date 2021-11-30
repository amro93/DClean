using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DClean.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using DClean.Application.Interfaces.Identity;
using System.IO;
using DClean.Application.DTOs.StaticFiles;

namespace DClean.Infrastructure.Shared.Services.StaticFiles
{
    public class StaticFileHelper : IStaticFileHelper
    {
        public const string StaticFilesDirectory = "StaticFiles";
        private readonly IWebHostEnvironment environment;
        private readonly ILogger<StaticFileHelper> logger;
        private readonly ICurrentTenant _currentTenant;
        private string filesRootPath = StaticFilesDirectory;

        public StaticFileHelper(IWebHostEnvironment environment,
            ILogger<StaticFileHelper> logger,
            ICurrentTenant currentTenant
            )
        {
            this.environment = environment;
            this.logger = logger;
            _currentTenant = currentTenant;
            // TODO: Validation
            //filesRootPath = Path.Combine(StaticFilesDirectory, "org-" + userResolverService.GetOrganizationId()?.ToString());
        }

        private string GetTenantStaticFilesPath()
        {
            return Path.Combine(StaticFilesDirectory, _currentTenant.TenantId?.ToString() ?? "DefaultTenant");
        }
        public bool DeleteFile(string path)
        {
            if (_currentTenant.TenantId.HasValue)
            {
                filesRootPath = GetTenantStaticFilesPath();
            }

            if (string.IsNullOrWhiteSpace(path)) return false;
            try
            {
                var filePath = Path.Combine(environment.ContentRootPath, path);
                File.Delete(filePath);
                return true;
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, $"cant delete image at path: { path?.Take(1000) ?? "null"}");
                return false;
            }
        }

        // TODO: Validate Delete, MoreDynamic
        public async Task<bool> DeleteFileAsync(string path)
        {
            if (_currentTenant.TenantId.HasValue)
            {
                filesRootPath = GetTenantStaticFilesPath();
            }
            if (string.IsNullOrWhiteSpace(path)) return false;
            try
            {
                var filePath = Path.Combine(environment.ContentRootPath, path);
                File.Delete(filePath);
                return true;
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, $"cant delete image at path: { path?.Take(1000) ?? "null"}");
                return false;
            }
        }

        public async Task<FileDto> SaveFileAsync(IFormFile file, string rootFolder = null, long? userId = null)
        {
            if (file == null) return null;
            if (_currentTenant.TenantId.HasValue)
            {
                filesRootPath = GetTenantStaticFilesPath();
            }

            FileInfo fileInfo = new FileInfo(file.FileName);
            var extension = fileInfo.Extension;
            var newFileName = Guid.NewGuid().ToString() + extension;
            var folderPath = Path.Combine(filesRootPath, rootFolder);
            var filePath = Path.Combine(folderPath, newFileName);
            var fileInfoDto = new FileDto()
            {
                FileName = file.FileName,
                FolderPath = folderPath,
                TempFileName = newFileName,
                Extension = extension
            };
            var folderRootPath = Path.Combine(environment.ContentRootPath, filePath);
            Directory.CreateDirectory(Path.GetDirectoryName(folderRootPath));
            using (var fileStream = new FileStream(folderRootPath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return fileInfoDto;
        }

        public async Task<FileDto> SaveFileAsync(string base64File, string fileName, string rootFolder = null, long? userId = null)
        {
            var dataImage = FileData.TryParse(base64File);
            var extension = dataImage?.MimeType;
            if (extension != null) extension = extension.StartsWith(".") ? extension : "." + extension;
            //else extension = ".jpg";
            var newFileName = Guid.NewGuid().ToString() + extension;
            if (_currentTenant.TenantId.HasValue)
            {
                filesRootPath = GetTenantStaticFilesPath();
            }
            var folderPath = Path.Combine(filesRootPath, rootFolder);
            var filePath = Path.Combine(folderPath, newFileName);
            var fileInfoDto = new FileDto()
            {
                FileName = fileName,
                FolderPath = folderPath,
                TempFileName = newFileName,
                Extension = extension
            };
            var folderRootPath = Path.Combine(environment.ContentRootPath, filePath);
            var directory = Directory.CreateDirectory(Path.GetDirectoryName(folderRootPath));
            await File.WriteAllBytesAsync(fileInfoDto.TempFilePath, Convert.FromBase64String(base64File));
            return fileInfoDto;
        }

        public async Task<FileDto> SaveFileAsync(IBase64FileVM base64FileVM, string rootFolder = null, long? userId = null)
        {
            if (base64FileVM == null) return null;
            var extension = base64FileVM.Extension;
            if (extension != null) extension = extension.StartsWith(".") ? extension : "." + extension;
            var fileId = Guid.NewGuid();
            var newFileName = fileId.ToString() + extension;
            if (_currentTenant.TenantId.HasValue)
            {
                filesRootPath = GetTenantStaticFilesPath();
            }
            var folderPath = Path.Combine(filesRootPath, rootFolder ?? string.Empty);
            var filePath = Path.Combine(folderPath, newFileName);
            var fileInfoDto = new FileDto()
            {
                FileName = base64FileVM.FileName,
                FolderPath = folderPath,
                TempFileName = newFileName,
                Extension = extension,
                Id = fileId
            };

            var folderRootPath = Path.Combine(environment.ContentRootPath, filePath);
            var directory = Directory.CreateDirectory(Path.GetDirectoryName(folderRootPath));
            await File.WriteAllBytesAsync(fileInfoDto.TempFilePath, Convert.FromBase64String(base64FileVM.Base64String));
            return fileInfoDto;
        }
    }
}
