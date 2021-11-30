using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DClean.Application.DTOs.StaticFiles
{
    public class Base64FileVM : IBase64FileVM
    {
        [Required]
        public virtual string Base64String { get; set; }

        [Required]
        public virtual string FileName { get; set; }

        public virtual string Extension { get; set; }
    }

    public interface IBase64FileVM
    {
        string Base64String { get; set; }

        string FileName { get; set; }

        string Extension { get; set; }
    }
}
