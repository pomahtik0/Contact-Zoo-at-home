namespace Contact_zoo_at_home.Infrastructure.Data
{
    /// <summary>
    /// Static class to save all constant sizes used in database, is public to be able to make checks of validation in other parts of App.
    /// </summary>
    public static class Sizes
    {
        public const int ProfileImageMax = 2 * 1024 * 1024; // 2 mb, actually does not work

        public const int EmailLenght = 256;

        public const int PhoneNumberLength = 20;

        public const int ShortTitlesLength = 50;

        public const int ShortDescriptionLength = 400;

        public const int DescriptionLength = 4096; // does not work as well

        public const int CommentMaxLength = 1024;

        public const string RatingType = "decimal(8, 5)"; // supports marks 1 to 5, or 1 to 100, depends on application
    }
}
