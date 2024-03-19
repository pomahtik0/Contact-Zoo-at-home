using Contact_zoo_at_home.Infrastructure.Data;

namespace WebUI.Models
{
    public static class ValidationConstants
    {
        // Username
        public const int UserNameLengthMax = ConstantsForEFCore.Sizes.UserNameLength;
        public const int UserNameLengthMin = 5;

        // Everething from Names to any short data values 
        public const int ShortTitlesLengthMax = ConstantsForEFCore.Sizes.ShortTitlesLength;
        public const int ShortTitlesLengthMin = 1;

        // All ShortDescriptions properties, either null, or with this size:
        public const int ShortDescriptionLengthMax = ConstantsForEFCore.Sizes.ShortDescriptionLength;
        public const int ShortDescriptionLengthMin = 10;

        // All Description properties, either null, or with this size:
        public const int DescriptionLengthMax = ConstantsForEFCore.Sizes.DescriptionLength;
        public const int DescriptionLengthMin = 10;

        // Phone number
        public const int PhoneNumberLengthMax = ConstantsForEFCore.Sizes.PhoneNumberLength;
        public const int PhoneNumberLengthMin = 6;

        // Comments
        public const int CommentLengthMax = ConstantsForEFCore.Sizes.CommentMaxLength;
        public const int CommentLengthMin = 10;

        // Images
        public const int ImageSizeMax = 6 * 1024 * 1024; // 6 mb
    }
}
