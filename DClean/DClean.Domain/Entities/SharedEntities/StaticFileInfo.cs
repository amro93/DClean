using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DClean.Domain.Common.BaseEntities;
using DClean.Domain.Enums;
using DClean.Domain.Interfaces;

namespace DClean.Infrastructure.Common.SharedEntities
{
    public class StaticFileInfo : CreateAuditedEntity<Guid>, IEntity<Guid>, IStaticFileInfo
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
        public EStaticFileProvider StaticFileProvider { get; set; }
    }   
}
