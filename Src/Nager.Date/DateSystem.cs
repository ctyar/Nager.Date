using Nager.Date.Contract;
using Nager.Date.Model;
using Nager.Date.PublicHolidays;
using Nager.Date.Weekends;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nager.Date
{
    public static class DateSystem
    {
        private static readonly NoHolidaysProvider _noHolidaysProvider = new NoHolidaysProvider();
        private static readonly ICatholicProvider _catholicProvider = new CatholicProvider();
        private static readonly IOrthodoxProvider _orthodoxProvider = new OrthodoxProvider();

        private static readonly Dictionary<CountryCode, IPublicHolidayProvider> _publicHolidaysProviders =
            new Dictionary<CountryCode, IPublicHolidayProvider>
            {
                { CountryCode.AD, new AndorraProvider() },
                { CountryCode.AR, new ArgentinaProvider(_catholicProvider) },
                { CountryCode.AT, new AustriaProvider(_catholicProvider) },
                { CountryCode.AU, new AustraliaProvider(_catholicProvider) },
                { CountryCode.AX, new AlandProvider(_catholicProvider) },
                { CountryCode.BB, new BarbadosProvider(_catholicProvider) },
                { CountryCode.BE, new BelgiumProvider(_catholicProvider) },
                { CountryCode.BG, new BulgariaProvider(_orthodoxProvider) },
                { CountryCode.BO, new BoliviaProvider(_catholicProvider) },
                { CountryCode.BR, new BrazilProvider(_catholicProvider) },
                { CountryCode.BS, new BahamasProvider(_catholicProvider) },
                { CountryCode.BW, new BotswanaProvider(_catholicProvider) },
                { CountryCode.BY, new BelarusProvider(_orthodoxProvider) },
                { CountryCode.BZ, new BelizeProvider(_catholicProvider) },
                { CountryCode.CA, new CanadaProvider(_catholicProvider) },
                { CountryCode.CH, new SwitzerlandProvider(_catholicProvider) },
                { CountryCode.CL, new ChileProvider(_catholicProvider) },
                { CountryCode.CN, new ChinaProvider() },
                { CountryCode.CO, new ColombiaProvider(_catholicProvider) },
                { CountryCode.CR, new CostaRicaProvider(_catholicProvider) },
                { CountryCode.CU, new CubaProvider(_catholicProvider) },
                { CountryCode.CY, new CyprusProvider(_catholicProvider) },
                { CountryCode.CZ, new CzechRepublicProvider(_catholicProvider) },
                { CountryCode.DE, new GermanyProvider(_catholicProvider) },
                { CountryCode.DK, new DenmarkProvider(_catholicProvider) },
                { CountryCode.DO, new DominicanRepublicProvider(_catholicProvider) },
                { CountryCode.EC, new EcuadorProvider(_catholicProvider) },
                { CountryCode.EE, new EstoniaProvider(_catholicProvider) },
                { CountryCode.EG, new EgyptProvider() },
                { CountryCode.ES, new SpainProvider(_catholicProvider) },
                { CountryCode.FI, new FinlandProvider(_catholicProvider) },
                { CountryCode.FO, new FaroeIslandsProvider(_catholicProvider) },
                { CountryCode.FR, new FranceProvider(_catholicProvider) },
                { CountryCode.GA, new GabonProvider(_catholicProvider) },
                { CountryCode.GB, new UnitedKingdomProvider(_catholicProvider) },
                { CountryCode.GD, new GrenadaProvider(_catholicProvider) },
                { CountryCode.GL, new GreenlandProvider(_catholicProvider) },
                { CountryCode.GR, new GreeceProvider(_orthodoxProvider) },
                { CountryCode.GT, new GuatemalaProvider(_catholicProvider) },
                { CountryCode.GY, new GuyanaProvider(_catholicProvider) },
                { CountryCode.HN, new HondurasProvider(_catholicProvider) },
                { CountryCode.HR, new CroatiaProvider(_catholicProvider) },
                { CountryCode.HT, new HaitiProvider(_catholicProvider) },
                { CountryCode.HU, new HungaryProvider(_catholicProvider) },
                { CountryCode.IE, new IrelandProvider(_catholicProvider) },
                { CountryCode.ID, new IndonesiaProvider(_catholicProvider) },
                { CountryCode.IM, new IsleOfManProvider(_catholicProvider) },
                { CountryCode.IS, new IcelandProvider(_catholicProvider) },
                { CountryCode.IT, new ItalyProvider(_catholicProvider) },
                { CountryCode.LI, new LiechtensteinProvider(_catholicProvider) },
                { CountryCode.LS, new LesothoProvider(_catholicProvider) },
                { CountryCode.LT, new LithuaniaProvider(_catholicProvider) },
                { CountryCode.LU, new LuxembourgProvider(_catholicProvider) },
                { CountryCode.LV, new LatviaProvider(_catholicProvider) },
                { CountryCode.JE, new JerseyProvider(_catholicProvider) },
                { CountryCode.JM, new JamaicaProvider(_catholicProvider) },
                { CountryCode.JP, new JapanProvider() },
                { CountryCode.MA, new MoroccoProvider() },
                { CountryCode.MC, new MonacoProvider(_catholicProvider) },
                { CountryCode.MD, new MoldovaProvider(_orthodoxProvider) },
                { CountryCode.MG, new MadagascarProvider(_catholicProvider) },
                { CountryCode.MK, new MacedoniaProvider(_orthodoxProvider) },
                { CountryCode.MN, new MongoliaProvider() },
                { CountryCode.MT, new MaltaProvider(_catholicProvider) },
                { CountryCode.MX, new MexicoProvider(_catholicProvider) },
                { CountryCode.MZ, new MozambiqueProvider() },
                { CountryCode.NA, new NamibiaProvider(_catholicProvider) },
                { CountryCode.NI, new NicaraguaProvider(_catholicProvider) },
                { CountryCode.NL, new NetherlandsProvider(_catholicProvider) },
                { CountryCode.NO, new NorwayProvider(_catholicProvider) },
                { CountryCode.NZ, new NewZealandProvider(_catholicProvider) },
                { CountryCode.PA, new PanamaProvider(_catholicProvider) },
                { CountryCode.PE, new PeruProvider(_catholicProvider) },
                { CountryCode.PL, new PolandProvider(_catholicProvider) },
                { CountryCode.PR, new PuertoRicoProvider(_catholicProvider) },
                { CountryCode.PT, new PortugalProvider(_catholicProvider) },
                { CountryCode.PY, new ParaguayProvider(_catholicProvider) },
                { CountryCode.RO, new RomaniaProvider(_orthodoxProvider) },
                { CountryCode.RS, new SerbiaProvider(_orthodoxProvider) },
                { CountryCode.RU, new RussiaProvider() },
                { CountryCode.SE, new SwedenProvider(_catholicProvider) },
                { CountryCode.SI, new SloveniaProvider(_catholicProvider) },
                { CountryCode.SJ, new SvalbardAndJanMayenProvider(_catholicProvider) },
                { CountryCode.SK, new SlovakiaProvider(_catholicProvider) },
                { CountryCode.SM, new SanMarinoProvider(_catholicProvider) },
                { CountryCode.SR, new SurinameProvider(_catholicProvider) },
                { CountryCode.SV, new ElSalvadorProvider(_catholicProvider) },
                { CountryCode.TN, new TunisiaProvider() },
                { CountryCode.TR, new TurkeyProvider() },
                { CountryCode.UA, new UkraineProvider(_orthodoxProvider) },
                { CountryCode.US, new UnitedStatesProvider() },
                { CountryCode.UY, new UruguayProvider(_catholicProvider) },
                { CountryCode.VA, new VaticanCityProvider(_catholicProvider) },
                { CountryCode.VE, new VenezuelaProvider(_catholicProvider) },
                { CountryCode.VN, new VietnamProvider() },
                { CountryCode.ZA, new SouthAfricaProvider(_catholicProvider) }
            };

        private static readonly Dictionary<CountryCode, IWeekendProvider> _nonUniversalWeekendProviders =
            new Dictionary<CountryCode, IWeekendProvider>
            {
                // https://en.wikipedia.org/wiki/Workweek_and_weekend
                { CountryCode.AE, WeekendProvider.SemiUniversal }, // since 2006 // TODO handle launch dates in weekends
                { CountryCode.AF, WeekendProvider.SemiUniversal },
                { CountryCode.BD, WeekendProvider.SemiUniversal },
                { CountryCode.BH, WeekendProvider.SemiUniversal },
                { CountryCode.BN, WeekendProvider.FridaySunday },
                // { CountryCode.CO, WeekendProvider.SundayOnly }, // No information on in which case it occurs
                { CountryCode.DJ, WeekendProvider.FridayOnly },
                { CountryCode.DZ, WeekendProvider.SemiUniversal },
                { CountryCode.EG, WeekendProvider.SemiUniversal },
                { CountryCode.GQ, WeekendProvider.SundayOnly },
                { CountryCode.HK, WeekendProvider.SundayOnly },
                { CountryCode.IL, WeekendProvider.SemiUniversal },
                // { CountryCode.IN, WeekendProvider.SundayOnly }, // Except for Government offices and IT industry
                { CountryCode.IQ, WeekendProvider.SemiUniversal },
                { CountryCode.IR, WeekendProvider.SemiUniversal }, // Or friday only (no information on in which case it occurs)
                { CountryCode.JO, WeekendProvider.SemiUniversal },
                { CountryCode.KW, WeekendProvider.SemiUniversal },
                { CountryCode.LY, WeekendProvider.SemiUniversal },
                { CountryCode.MV, WeekendProvider.SemiUniversal },
                { CountryCode.MX, WeekendProvider.SundayOnly },
                // { CountryCode.MY, WeekendProvider.SemiUniversal }, // except in some counties // TODO Add county in weekend handling
                { CountryCode.NP, WeekendProvider.SaturdayOnly },
                { CountryCode.OM, WeekendProvider.SemiUniversal },
                { CountryCode.PH, WeekendProvider.SundayOnly },
                // { CountryCode.PK, WeekendProvider.SemiUniversal }, // only partially, often universal
                { CountryCode.PS, WeekendProvider.SemiUniversal },
                { CountryCode.QA, WeekendProvider.SemiUniversal },
                { CountryCode.SA, WeekendProvider.SemiUniversal },
                { CountryCode.SD, WeekendProvider.SemiUniversal },
                { CountryCode.SO, WeekendProvider.FridayOnly },
                { CountryCode.SY, WeekendProvider.SemiUniversal },
                { CountryCode.UG, WeekendProvider.SundayOnly },
                { CountryCode.YE, WeekendProvider.SemiUniversal },
            };

        [Obsolete("Please use GetPublicHolidayProvider instead")]
        public static IPublicHolidayProvider GetProvider(CountryCode countryCode)
        {
            return GetPublicHolidayProvider(countryCode);
        }

        public static IPublicHolidayProvider GetPublicHolidayProvider(CountryCode countryCode)
        {
            _publicHolidaysProviders.TryGetValue(countryCode, out IPublicHolidayProvider provider);
            return provider ?? _noHolidaysProvider;
        }

        public static IWeekendProvider GetWeekendProvider(CountryCode countryCode)
        {
            _nonUniversalWeekendProviders.TryGetValue(countryCode, out IWeekendProvider provider);
            return provider ?? WeekendProvider.Universal;
        }

        #region Public Holidays for a given year

        /// <summary>
        /// Get Public Holidays of given year
        /// </summary>
        /// <param name="countryCode">ISO 3166-1 ALPHA-2</param>
        /// <param name="year"></param>
        /// <returns></returns>
        public static IEnumerable<PublicHoliday> GetPublicHoliday(string countryCode, int year)
        {
            if (!Enum.TryParse(countryCode, true, out CountryCode parsedCountryCode))
            {
                return null;
            }

            return GetPublicHoliday(parsedCountryCode, year);
        }

        /// <summary>
        /// Get Public Holidays of given year
        /// </summary>
        /// <param name="countryCode">ISO 3166-1 ALPHA-2</param>
        /// <param name="year"></param>
        /// <returns></returns>
        public static IEnumerable<PublicHoliday> GetPublicHoliday(CountryCode countryCode, int year)
        {
            var provider = GetPublicHolidayProvider(countryCode);
            return provider.Get(year);
        }

        #endregion

        #region Public Holidays for a date range

        public static IEnumerable<PublicHoliday> GetPublicHoliday(string countryCode, DateTime startDate, DateTime endDate)
        {
            if (!Enum.TryParse(countryCode, true, out CountryCode parsedCountryCode))
            {
                return null;
            }

            return GetPublicHoliday(parsedCountryCode, startDate, endDate);
        }

        public static IEnumerable<PublicHoliday> GetPublicHoliday(CountryCode countryCode, DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
            {
                throw new ArgumentException("startDate is before endDate");
            }

            var currentYear = startDate.Year;
            var endYear = endDate.Year;

            while (currentYear <= endYear)
            {
                var items = GetPublicHoliday(countryCode, currentYear);
                foreach (var item in items)
                {
                    if (item.Date >= startDate && item.Date <= endDate)
                    {
                        yield return item;
                    }
                }
                currentYear++;
            }
        }

        #endregion

        #region Check if a date is a Public Holiday

        public static bool IsPublicHoliday(DateTime date, CountryCode countryCode)
        {
            var items = GetPublicHoliday(countryCode, date.Year);
            return items.Any(o => o.Date.Date == date.Date);
        }

        public static bool IsOfficialPublicHolidayByCounty(DateTime date, CountryCode countryCode, string countyCode)
        {
            var items = GetPublicHoliday(countryCode, date.Year);
            return items.Any(o => o.Date.Date == date.Date && (o.Counties == null || o.Counties.Contains(countyCode)) && o.CountyOfficialHoliday);
        }

        public static bool IsAdministrationPublicHolidayByCounty(DateTime date, CountryCode countryCode, string countyCode)
        {
            var items = GetPublicHoliday(countryCode, date.Year);
            return items.Any(o => o.Date.Date == date.Date && (o.Counties == null || o.Counties.Contains(countyCode)) && o.CountyAdministrationHoliday);
        }

        #endregion

        #region Check a date is a Public Holiday

        public static bool IsWeekend(DateTime date, CountryCode countryCode)
        {
            var provider = GetWeekendProvider(countryCode);
            return provider.IsWeekend(date);
        }

        #endregion

        #region Day Finding

        /// <summary>
        /// Find the latest weekday for example monday in a month
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public static DateTime FindLastDay(int year, int month, DayOfWeek day)
        {
            var resultedDay = FindDay(year, month, day, 5);
            if (resultedDay == DateTime.MinValue)
            {
                resultedDay = FindDay(year, month, day, 4);
            }

            return resultedDay;
        }

        /// <summary>
        /// Find the next weekday for example monday from a specific date
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="dayOfWeek"></param>
        /// <returns></returns>
        public static DateTime FindDay(int year, int month, int day, DayOfWeek dayOfWeek)
        {
            var calculationDay = new DateTime(year, month, day);

            if ((int)dayOfWeek >= (int)calculationDay.DayOfWeek)
            {
                var daysNeeded = (int)dayOfWeek - (int)calculationDay.DayOfWeek;
                return calculationDay.AddDays(daysNeeded);
            }
            else
            {
                var daysNeeded = (int)dayOfWeek - (int)calculationDay.DayOfWeek;
                return calculationDay.AddDays(daysNeeded + 7);
            }
        }

        /// <summary>
        /// Find the next weekday for example monday from a specific date
        /// </summary>
        /// <param name="date"></param>
        /// <param name="dayOfWeek"></param>
        /// <returns></returns>
        public static DateTime FindDay(DateTime date, DayOfWeek dayOfWeek)
        {
            return FindDay(date.Year, date.Month, date.Day, dayOfWeek);
        }

        /// <summary>
        /// Find the next weekday for example monday before a specific date
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="dayOfWeek"></param>
        /// <returns></returns>
        public static DateTime FindDayBefore(int year, int month, int day, DayOfWeek dayOfWeek)
        {
            var calculationDay = new DateTime(year, month, day);

            if ((int)dayOfWeek < (int)calculationDay.DayOfWeek)
            {
                var daysSubtract = (int)calculationDay.DayOfWeek - (int)dayOfWeek;
                return calculationDay.AddDays(-daysSubtract);
            }
            else
            {
                var daysSubtract = (int)dayOfWeek - (int)calculationDay.DayOfWeek;
                return calculationDay.AddDays(daysSubtract - 7);
            }
        }

        /// <summary>
        /// Find the next weekday for example monday before a specific date
        /// </summary>
        /// <param name="date"></param>
        /// <param name="dayOfWeek"></param>
        /// <returns></returns>
        public static DateTime FindDayBefore(DateTime date, DayOfWeek dayOfWeek)
        {
            return FindDayBefore(date.Year, date.Month, date.Day, dayOfWeek);
        }

        /// <summary>
        /// Find for example the 3th monday in a month
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="occurrence"></param>
        /// <returns></returns>
        public static DateTime FindDay(int year, int month, DayOfWeek day, int occurrence)
        {
            if (occurrence == 0 || occurrence > 5)
            {
                throw new Exception("Occurance is invalid");
            }

            var firstDayOfMonth = new DateTime(year, month, 1);

            //Substract first day of the month with the required day of the week
            var daysNeeded = (int)day - (int)firstDayOfMonth.DayOfWeek;

            //if it is less than zero we need to get the next week day (add 7 days)
            if (daysNeeded < 0)
            {
                daysNeeded = daysNeeded + 7;
            }

            //DayOfWeek is zero index based; multiply by the Occurance to get the day
            var resultedDay = (daysNeeded + 1) + (7 * (occurrence - 1));

            if (resultedDay > DateTime.DaysInMonth(year, month))
            {
                return DateTime.MinValue;
            }

            return new DateTime(year, month, resultedDay);
        }

        #endregion

        #region Birthday

        public static int GetAge(DateTime birthdate)
        {
            var today = DateTime.UtcNow;
            var age = today.Year - birthdate.Year;
            if (birthdate > today.AddYears(-age)) age--;

            return age;
        }

        #endregion

        #region Long Weekends

        public static IEnumerable<LongWeekend> GetLongWeekend(CountryCode countryCode, int year)
        {
            LongWeekend item;

            var items = new List<LongWeekend>();

            var publicHolidays = GetPublicHoliday(countryCode, year);
            foreach (var publicHoliday in publicHolidays)
            {
                item = null;

                switch (publicHoliday.Date.DayOfWeek)
                {
                    case DayOfWeek.Thursday:
                        item = new LongWeekend();
                        item.StartDate = publicHoliday.Date;
                        item.EndDate = publicHoliday.Date.AddDays(3);
                        item.Bridge = true;
                        break;
                    case DayOfWeek.Friday:
                        item = new LongWeekend();
                        item.StartDate = publicHoliday.Date;
                        item.EndDate = publicHoliday.Date.AddDays(2);
                        item.Bridge = false;
                        break;
                    case DayOfWeek.Monday:
                        item = new LongWeekend();
                        item.StartDate = publicHoliday.Date.AddDays(-2);
                        item.EndDate = publicHoliday.Date;
                        item.Bridge = false;
                        break;
                    case DayOfWeek.Tuesday:
                        item = new LongWeekend();
                        item.StartDate = publicHoliday.Date.AddDays(-3);
                        item.EndDate = publicHoliday.Date;
                        item.Bridge = true;
                        break;
                }

                if (item == null)
                {
                    continue;
                }

                //Other LongWeekend on the same date, update the other
                var otherItem = items.Where(o => o.StartDate.Equals(item.StartDate)).FirstOrDefault();
                if (otherItem != null)
                {
                    otherItem.EndDate = item.EndDate;
                    continue;
                }

                items.Add(item);
            }

            return items;
        }

        #endregion
    }
}
