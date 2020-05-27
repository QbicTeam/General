using System;

namespace SIQbic.API.Model
{
    public class User
    {
        public int Id { get; set; }

        public string UserName { get; set; }
                
        public string DisplayName { get; set; }
        
        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreationDate { get; set; }
        
        public DateTime? LastModificationDate { get; set; }            
    }
}