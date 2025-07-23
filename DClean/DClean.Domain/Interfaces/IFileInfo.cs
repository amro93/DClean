

using System;
using DClean.Domain.Enums;

namespace DClean.Domain.Interfaces
{
    public interface IFileInfo : IFileInfo<Guid>
    {

    }
    public interface IFileInfo<TPK> where TPK : IEquatable<TPK>
    {
        public TPK Id { get; set; }
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
        //public TUserPK? UserId { get; set; }
        public StaticFileProvider StaticFileProvider { get; set; }
    }
}
