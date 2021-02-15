namespace CustomLocalizer
{
    using System;
    using Microsoft.Extensions.Localization;

    public class StringLocalizerFactory : IStringLocalizerFactory
    {
        public IStringLocalizer Create(Type resourceSource)
        {
            return new StringLocalizer();
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            return new StringLocalizer();
        }
    }
}