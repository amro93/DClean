using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DClean.Domain.Enums;
using DClean.Domain.Interfaces;

namespace DClean.Infrastructure.Common.SharedEntities
{
    public class StaticFileInfo : ICreateAuditedEntity<Guid?>, IEntity<Guid>, IFileInfo
    {
        public Guid Id { get; set; }
        /// <summary>
        /// The real name of the attached file.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// The file type extension
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        /// The path to the folder which contains the file
        /// </summary>
        public string FolderPath { get; set; }

        /// <summary>
        /// the user who must be elligible to acces this file
        /// </summary>
        //public Guid? UserId { get; set; }
        public StaticFileProvider StaticFileProvider { get; set; }
        public Guid? CreatedById { get; set; }
        public DateTime? CreatedAt { get; set; }
    }   
}
