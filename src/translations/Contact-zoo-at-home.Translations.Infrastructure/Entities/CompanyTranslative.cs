using Contact_zoo_at_home.Shared.Basics.Enums;

namespace Contact_zoo_at_home.Translations.Infrastructure.Entities
{
    public class CompanyTranslative
    {
        public int Id { get; set; }
        public Language Language { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
