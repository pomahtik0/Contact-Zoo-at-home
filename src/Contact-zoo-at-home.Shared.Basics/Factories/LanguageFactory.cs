using Contact_zoo_at_home.Shared.Basics.Enums;

namespace Contact_zoo_at_home.Shared.Basics.Factories
{
    public static class LanguageFactory
    {
        public static Language GetLanguage(string name)
        {
            name = name.ToLower();
            switch (name)
            {
                case "en":
                    return Language.English;
                case "uk":
                    return Language.Українська;
                default:
                    throw new Exception("Not supported language");
            }
        }
    }
}
