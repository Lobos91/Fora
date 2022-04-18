namespace Fora.UI.Models
{
    public class UserInterestModel
    {
        public int UserId { get; set; }
        public UserModel? User { get; set; }
        public int? InterestId { get; set; }
        public InterestModel? Interest { get; set; }
    }
}
