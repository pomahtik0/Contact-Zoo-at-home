namespace Contact_zoo_at_home.Core.Enums
{
    /// <summary>
    /// supported languages
    /// To add new suporting language just add it to this enum, all the other changes must be done at UI-part.
    /// If database, already exists, adding is allowed only in the end of enum.
    /// </summary>
    public enum Language
    {
        NoLanguage,
        English,
        Ukrainian
    }
}
