using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Infrastructure.Data
{
    public static class ConstantsForEFCore
    {
        public static class TableNames
        {
            public const string BaseUserTableName = "Users";
            
            public const string BasePetOwnerTableName = "PetOwners";
            
            public const string CompanyTableName = "Companies";
            
            public const string IndividualPetOwnerTableName = "IndividualPetOwners";
            
            public const string CustomerUserTableName = "Customers";
            
            public const string CompanyPetRepresentativeTableName = "CompanyWorkers";

            public const string UnregisteredPetRepresentativeTableName = "CustomRepresentatives";

            public const string PetTableName = "Pets";

            public const string ExtraPetOptionTableName = "ExtraPetOptions";

            public const string BaseCommentsTableName = "Comments";
        }

        public static class Sizes
        {
            public const int UserNameLength = 32;

            public const int ProfileImageMax = 1024 * 1024; // up to one mb photo

            public const int EmailLenght = 256;

            public const int PhoneNumberLength = 20;

            public const int ShortTitlesLength = 50;

            public const int ShortDescriptionLength = 400;

            public const int DescriptionLength = 4096;

            public const int CommentMaxLength = 1024;
        }
    }
}
