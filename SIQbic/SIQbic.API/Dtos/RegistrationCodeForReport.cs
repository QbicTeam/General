using System;
using SIQbic.API.Model;

namespace SIQbic.API.Dtos
{
    public class RegistrationCodeForReport
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Status { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DueDate { get; set; }

        public string InvitedEmail { get; set; }

        public string InvitedName { get; set; }

        public string SponsorEmail { get; set; }

        public Role Role { get; set; }        
    }
}