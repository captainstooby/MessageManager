using CG.Pluralization;

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
            //var pluralizationService = CG.Pluralization.PluralizationService.CreateService();

            if (counter == 1)
            {
                return _pluralizationService.Singularize(word);
            }

            return _pluralizationService.Pluralize(word);
        }
    }
}
