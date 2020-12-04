using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day4
{
    public class Passport
    {
        public readonly int? BirthYear;
        public readonly int? IssueYear;
        public readonly int? ExpirationYear;
        public readonly int? Height;
        public readonly string HeightUnits;
        public readonly string HairColor;
        public readonly string EyeColor;
        public readonly string PassportId;
        public readonly string CountryId;

        public Passport(Dictionary<string, string> passportValues)
        {
            try
            {
                BirthYear = passportValues.ContainsKey("byr") ? Convert.ToInt32(passportValues["byr"]) : null;
                IssueYear = passportValues.ContainsKey("iyr") ? Convert.ToInt32(passportValues["iyr"]) : null;
                ExpirationYear = passportValues.ContainsKey("eyr") ? Convert.ToInt32(passportValues["eyr"]) : null;
                if (passportValues.ContainsKey("hgt"))
                {
                    var heightWithUnits = passportValues["hgt"];
                    Height = Convert.ToInt32(heightWithUnits.Substring(0, heightWithUnits.Length - 2));
                    HeightUnits = heightWithUnits.Substring(heightWithUnits.Length - 2);
                }
                HairColor = passportValues.ContainsKey("hcl") ? passportValues["hcl"] : string.Empty;
                EyeColor = passportValues.ContainsKey("ecl") ? passportValues["ecl"] : string.Empty;
                PassportId = passportValues.ContainsKey("pid") ? passportValues["pid"] : string.Empty;
                CountryId = passportValues.ContainsKey("cid") ? passportValues["cid"] : string.Empty;
            }
            catch (Exception e)
            {
                // Swallow the error, it's expected to have bad input data (Maybe try using something other than Convert.ToInt32)
                Console.WriteLine("Failed to parse PassportValues");
                Console.WriteLine(e.Message);
            }
        }

        public bool DoRequiredFieldsExist()
        {
            return BirthYear != null && IssueYear != null && ExpirationYear != null && Height != null &&
                !string.IsNullOrEmpty(HairColor) && !string.IsNullOrEmpty(EyeColor) && !string.IsNullOrEmpty(PassportId);
        }

        public bool IsPassportValid()
        {
            return IsBirthYearValid() && IsIssueYearValid() && IsExpirationYearValid() && IsHeightValid() && IsHairColorValid() && IsEyeColorValid() && IsPassportIdValid() && IsCountryIdValid();
        }

        private bool IsBirthYearValid()
        {
            return BirthYear >= 1920 && BirthYear <= 2002;
        }

        private bool IsIssueYearValid()
        {
            return IssueYear >= 2010 && IssueYear <= 2020;
        }

        private bool IsExpirationYearValid()
        {
            return ExpirationYear >= 2020 && ExpirationYear <= 2030;
        }

        private bool IsHeightValid()
        {
            if (string.Equals(HeightUnits, "cm"))
            {
                return Height >= 150 && Height <= 193;
            }
            else if (string.Equals(HeightUnits, "in"))
            {
                return Height >= 59 && Height <= 76;
            }
            else
            {
                return false;
            }
        }

        private bool IsHairColorValid()
        {
            var regex = new Regex("#[a-f0-9]*");
            return HairColor.Length == 7 && regex.IsMatch(HairColor);
        }

        private bool IsEyeColorValid()
        {
            var validEyeColors = new List<string>() { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
            return validEyeColors.Contains(EyeColor);
        }

        private bool IsPassportIdValid()
        {
            var regex = new Regex("\\d*");
            return PassportId.Length == 9 && regex.IsMatch(PassportId);
        }

        private bool IsCountryIdValid()
        {
            return true;
        }
    }
}