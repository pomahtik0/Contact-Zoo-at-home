namespace Contact_zoo_at_home.Core.Enums
{
    /// <summary>
    /// What is the status of the pet? is it awailble for contracts? if no, why?
    /// </summary>
    public enum PetStatus
    {
        Active,
        Frozen,
        FrozenByModerator,
        InvalidData,
        Archived
    }
}
