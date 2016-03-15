using System;
using System.Collections.Generic;
using System.Linq;

namespace Couverts.Example.Web.Helpers {
    public static class TranslationHelper {
        private static readonly Dictionary<string, string> Translations = new Dictionary<string, string> {
            {"Gender", "Geslacht"},
            {"FirstName", "Voornaam"},
            {"LastName", "Achternaam"},
            {"BirthDate", "Geboortedatum"},
            {"Email", "Email"},
            {"PhoneNumber", "Telefoonnummer"},
            {"PostalCode", "Postcode"},
            {"Comments", "Commentaar"},
        };


        /// <summary>
        /// Gets the translation for the requested word.
        /// This method works to both ways.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <returns>The translation.</returns>
        /// <exception cref="System.Exception">Exception when no translation has been found.</exception>
        public static string GetTranslationFor(string word) {
            if (Translations.ContainsKey(word)) {
                return Translations[word];
            }

            if (Translations.ContainsValue(word)) {
                return Translations.Single(kvp => kvp.Value == word).Key;
            }

            throw new Exception($"No translation found for: {word}");
        }
    }
}