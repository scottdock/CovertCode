using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CovertCode.Services.Secret.Contracts.DTOs
{
    public class SecretDto : BaseEntity
    {
        [Key]
        public int SecretId { get; set; }
        [Required]
        public string AccessPhrase { get; set; }
        [Required]
        public string Value { get; set; }
        [Required]
        public DateTime ExpireDate { get; set; }
        public string PassPhrase { get; set; }
        public bool Active { get; set; }
        public bool Served { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public bool DelFlag { get; set; }
    }
}
