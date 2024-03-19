using Contact_zoo_at_home.Core.Enums;

namespace Contact_zoo_at_home.Core.Entities.Pets
{
    /// <summary>
    /// Dates when pet is busy.
    /// </summary>
    public class PetBlockedDate
    {
        public int Id { get; set; }
        public DateTime BlockedDate { get; set; }
        public BlockedDateReason Reason { get; set; }
    }
}
