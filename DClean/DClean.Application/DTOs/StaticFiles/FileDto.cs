using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DClean.Domain.Enums;
using DClean.Domain.Interfaces;
using DClean.Infrastructure.Common.SharedEntities;

namespace DClean.Application.DTOs.StaticFiles
{
    public class FileDto : IFileInfo
    {
        /// <summary>
        /// temp file relative path
        /// </summary>
        public virtual string TempFilePath { get => GetTempFilePath(); }
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string TempFileName { get; set; }
        public string Extension { get; set; }
        public string FolderPath { get; set; }
        public StaticFileProvider StaticFileProvider { get; set; }

        public FileDto()
        {

        }

        public FileDto(StaticFileInfo staticFile)
        {
            if (staticFile == null) return;
            Id = staticFile.Id;
            FileName = staticFile.FileName;
            Extension = staticFile.Extension;
            FolderPath = staticFile.FolderPath;
            TempFileName = staticFile.Id.ToString();
        }
        /// <summary>
        /// temp file relative path
        /// </summary>
        public string GetTempFilePath()
        {
            if (FolderPath == null || TempFileName == null) return null;
            return Path.Combine(FolderPath, TempFileName);
        }

        public StaticFileInfo GetFileInfo()
        {
            return new StaticFileInfo
            {
                Id = Id,
                FileName = FileName,
                Extension = Extension,
                FolderPath = FolderPath,
                StaticFileProvider = StaticFileProvider.Local
            };
        }
    }
}
