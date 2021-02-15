namespace CustomLocalizer
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Microsoft.Extensions.Localization;

    public class StringLocalizer : IStringLocalizer
    {
        private readonly CultureInfo _culture;
        private readonly List<StringData> _stringData; 
               
        public StringLocalizer()
        {
            _stringData = new List<StringData>();
            InitializeLocalizedStrings(_stringData);
        }
        
        public StringLocalizer(CultureInfo culture) : this()
        {
            _culture = culture;
        }

        public LocalizedString this[string name]
        {
            get
            {
                var culture = _culture ?? CultureInfo.CurrentUICulture;
                var translation = _stringData.FirstOrDefault(x => x.CultureName == culture.Name && x.Name == name)?.Value;

                return new LocalizedString(name, translation ?? name, translation != null);
            }
        }

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                var culture = _culture ?? CultureInfo.CurrentUICulture;
                var translation = _stringData.FirstOrDefault(x => x.CultureName == culture.Name && x.Name == name)?.Value;

                if (translation != null)
                {
                    translation = string.Format(translation, arguments);
                }

                return new LocalizedString(name, translation ?? name, translation != null);
            }
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            return _stringData.Select(x => new LocalizedString(x.Name, x.Value, true)).ToList();
        }

        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            return new StringLocalizer(culture);
        }

        private void InitializeLocalizedStrings(List<StringData> localizedStrings)
        {
            localizedStrings.Clear();
            
            localizedStrings.Add(new StringData("it-IT", "Hello", "Ciao"));
            localizedStrings.Add(new StringData("it-IT", "Goodbye", "Arrivederci"));
            localizedStrings.Add(new StringData("it-IT", "The Current Date", "La Data Corrente"));
            localizedStrings.Add(new StringData("it-IT", "A Formatted Number", "Un Numero Formattato"));
            localizedStrings.Add(new StringData("it-IT", "A Currency Value", "Un Valore di Valuta"));
            
            localizedStrings.Add(new StringData("ja-JP", "Hello", "こんにちは"));
            localizedStrings.Add(new StringData("ja-JP", "Goodbye", "さようなら"));
            localizedStrings.Add(new StringData("ja-JP", "The Current Date", "現在の日付"));
            localizedStrings.Add(new StringData("ja-JP", "A Formatted Number", "フォーマットされた数値"));
            localizedStrings.Add(new StringData("ja-JP", "A Currency Value", "通貨の値"));
            
            localizedStrings.Add(new StringData("sv-SE", "Hello", "Hej"));
            localizedStrings.Add(new StringData("sv-SE", "Goodbye", "Hej då"));
            localizedStrings.Add(new StringData("sv-SE", "The Current Date", "Aktuellt Datum"));
            localizedStrings.Add(new StringData("sv-SE", "A Formatted Number", "En Formaterad Rad"));
            localizedStrings.Add(new StringData("sv-SE", "A Currency Value", "Ett Valutavärde"));

            localizedStrings.Add(new StringData("nl-NL", "Hello", "Hallo"));
            localizedStrings.Add(new StringData("nl-NL", "Goodbye", "Tot ziens"));
            localizedStrings.Add(new StringData("nl-NL", "The Current Date", "De Huidige Datum"));
            localizedStrings.Add(new StringData("nl-NL", "A Formatted Number", "Een Opgemaakte Nummer"));
            localizedStrings.Add(new StringData("nl-NL", "A Currency Value", "Een Valutawaarde"));

            localizedStrings.Add(new StringData("ru-RU", "Hello", "Привет"));
            localizedStrings.Add(new StringData("ru-RU", "Goodbye", "До свидания"));
            localizedStrings.Add(new StringData("ru-RU", "The Current Date", "Текущая дата"));
            localizedStrings.Add(new StringData("ru-RU", "A Formatted Number", "Отформатированный номер"));
            localizedStrings.Add(new StringData("ru-RU", "A Currency Value", "Значение валюты"));

            localizedStrings.Add(new StringData("tr-TR", "Hello", "Merhaba"));
            localizedStrings.Add(new StringData("tr-TR", "Goodbye", "Hoşçakal"));
            localizedStrings.Add(new StringData("tr-TR", "The Current Date", "Güncel Tarih"));
            localizedStrings.Add(new StringData("tr-TR", "A Formatted Number", "Biçimlendirilmiş Sayı"));
            localizedStrings.Add(new StringData("tr-TR", "A Currency Value", "Para Birimi"));
        }
        
        private class StringData
        {
            public StringData(string cultureName, string name, string value)
            {
                CultureName = cultureName;
                Name = name;
                Value = value;
            }
        
            public string CultureName { get; private set; }
            public string Name {get; private set; }
            public string Value {get; private set; }
        }
    }
}