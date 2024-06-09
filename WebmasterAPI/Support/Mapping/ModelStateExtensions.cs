using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebmasterAPI.Support.Mapping
{
    public static class ModelStateExtensions
    {
        public static List<string> GetErrorMessages(this ModelStateDictionary dictionary)
        {
            return dictionary.SelectMany(m => m.Value.Errors)
                             .Select(e => e.ErrorMessage)
                             .ToList();
        }
    }
}
