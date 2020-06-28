namespace SIQbic.API.Dtos
{
    public class InviteForCreationDTO
    {
        public int RoleId { get; set; }

        public string InvitedEmail { get; set; }

        public string InvitedName { get; set; }

        public string SponsorEmail { get; set; }
    }
}