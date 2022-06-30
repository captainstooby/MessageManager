using System.Data.Entity.Design.PluralizationServices;

namespace MessageManager.Helpers
{
    public class LanguageHelper : ILanguageHelper
    {
        private readonly PluralizationService _pluralizationService;

        public LanguageHelper(PluralizationService pluralizationService)
        {
            _pluralizationService = pluralizationService;
        }

        public string NumberizeText(string word, int counter)
        {
            return (counter == 1)
                    ? _pluralizationService.Singularize(word)
                    : _pluralizationService.Pluralize(word);
        }
    }
}
