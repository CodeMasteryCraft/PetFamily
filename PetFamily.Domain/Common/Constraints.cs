namespace PetFamily.Domain.Common
{
    public readonly struct Constraints
    {
        public const int SHORT_TITLE_LENGTH = 70;
        public const int MEDIUM_TITLE_LENGTH = 170;
        public const int LONG_TITLE_LENGTH = 255;

        public const int MINIMUM_TITLE_LENGTH = 1;
        public const int MAXIMUM_TITLE_LENGTH = 32768;

        public const int INDEX_TITLE_LENGTH = 6;

        public const string RUSSIAN_PHONE_REGEX = @"^(
                            (8|\+374|\+994|\+995|\+375|\+7|\+380|\+38|\+996|\+998|\+993)[\- ]?)?\
                            (?\d{3,5}\)?[\- ]?\d{1}[\- ]?\d{1}[\- ]?\d{1}[\- ]?\d{1}[\- ]?\d{1}(([\- ]?\d{1})?[\- ]?\d{1})?$";

        public const int YEAR1900 = 1900;
    }
}
