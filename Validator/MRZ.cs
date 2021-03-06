﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validator
{
    /// <summary>
    /// Class to holds the MRZ data, and also performs the validation
    /// </summary>
    public class MRZ
    {
        public string PassportNum { get; set; }
        public int CheckDigit1 { get; set; }
        public string Nationality { get; set; }
        public string DOB { get; set; }
        public int CheckDigit2 { get; set; }
        public string Sex { get; set; }
        public string PassportExpDate { get; set; }
        public int CheckDigit3 { get; set; }
        public string PersonalNum { get; set; }
        public int CheckDigit4 { get; set; }
        public int FinalCheckDigit { get; set; }

        public MRZ()
        {
        }

        /// <summary>
        /// The MRZ Line 2
        /// </summary>
        public string Line
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(PassportNum).Append(CheckDigit1)
                    .Append(Nationality)
                    .Append(DOB).Append(CheckDigit2)
                    .Append(Sex)
                    .Append(PassportExpDate).Append(CheckDigit3)
                    .Append(PersonalNum).Append(CheckDigit4).Append(FinalCheckDigit);
                return sb.ToString();
            }
        }

        /// <summary>
        /// Method to set the Passport No.
        /// </summary>
        /// <param name="x"></param>
        public void SetPassportNum(string x)
        {
            int n = 9;
            if (string.IsNullOrEmpty(x))
                PassportNum = "".PadRight(n, '<');

            else
                PassportNum = x.ToUpper().PadRight(n, '<');

            CheckDigit1 = GetCheckDigit(PassportNum);
        }

        /// <summary>
        /// Method to set the Nationality
        /// </summary>
        /// <param name="x"></param>
        public void SetNationality(string x)
        {
            int n = 3;
            if (string.IsNullOrEmpty(x))
                Nationality = "".PadRight(n, '<');

            else
                Nationality = x.ToUpper().PadRight(n, '<');
        }

        /// <summary>
        /// Method to set the DOB
        /// </summary>
        /// <param name="x"></param>
        public void SetDOB(string x)
        {
            if (string.IsNullOrEmpty(x))
                return;

            if (IsValidateDate(x))
            {
                DOB = x;
                CheckDigit2 = GetCheckDigit(DOB);
            }
        }

        /// <summary>
        /// Method to set the Gender
        /// </summary>
        /// <param name="x"></param>
        public void SetSex(string x)
        {
            if (string.IsNullOrEmpty(x))
                Sex = "<";

            else
            {
                string g = x.ToUpper();
                if (g == "M" || g == "F")
                    Sex = g;

                else
                    Sex = "<";
            }
        }

        /// <summary>
        /// Method to set the Passport Expiry Date
        /// </summary>
        /// <param name="x"></param>
        public void SetPassportExpDate(string x)
        {
            if (string.IsNullOrEmpty(x))
                return;

            if (IsValidateDate(x))
            {
                PassportExpDate = x;
                CheckDigit3 = GetCheckDigit(PassportExpDate);
            }
        }

        /// <summary>
        /// Method to set the Personal Number
        /// </summary>
        /// <param name="x"></param>
        public void SetPersonalNum(string x = "")
        {
            int n = 14;
            if (string.IsNullOrEmpty(x))
                PersonalNum = "".PadRight(n, '<');

            else
                PersonalNum = x.ToUpper().PadRight(n, '<');

            CheckDigit4 = GetCheckDigit(PersonalNum);
        }

        /// <summary>
        /// Method to set the final check digit
        /// </summary>
        public void SetFinalCheckDigit()
        {
            FinalCheckDigit = 0;

            StringBuilder sb = new StringBuilder();
            sb.Append(PassportNum).Append(CheckDigit1)
                .Append(Nationality)
                .Append(DOB).Append(CheckDigit2)
                .Append(Sex)
                .Append(PassportExpDate).Append(CheckDigit3)
                .Append(PersonalNum).Append(CheckDigit4);
            string s = sb.ToString();

            if (s.Length != 43)
                return;

            FinalCheckDigit = GetCheckDigit(s);
        }

        /// <summary>
        /// Property to show whether Passport No. check digit is valid
        /// </summary>
        public bool IsValidCheckDigit1
        {
            get
            {
                bool b = false;
                if (string.IsNullOrEmpty(PassportNum))
                    return b;

                b = PassportNum.All(k => char.IsLetterOrDigit(k) || k == '<');
                return b;
            }
        }

        /// <summary>
        /// Property to show whether DOB check digit is valid
        /// </summary>
        public bool IsValidCheckDigit2
        {
            get
            {
                bool b = false;
                if (string.IsNullOrEmpty(DOB))
                    return b;

                if (!DOB.All(char.IsDigit))
                    return b;

                b = IsValidateDate(DOB);
                return b;
            }
        }

        /// <summary>
        /// Property to show whether Passport Expiry Date check digit is valid
        /// </summary>
        public bool IsValidCheckDigit3
        {
            get
            {
                bool b = false;
                if (string.IsNullOrEmpty(PassportExpDate))
                    return b;

                if (!PassportExpDate.All(char.IsDigit))
                    return b;

                b = IsValidateDate(PassportExpDate);
                return b;
            }
        }

        /// <summary>
        /// Property to show whether Personal Number check digit is valid
        /// </summary>
        public bool IsValidCheckDigit4
        {
            get
            {
                bool b = false;
                if (string.IsNullOrEmpty(PersonalNum))
                    return b;

                b = PersonalNum.All(k => char.IsLetterOrDigit(k) || k == '<');
                return b;
            }
        }

        /// <summary>
        /// Property to show whether Final check digit is valid
        /// </summary>
        public bool IsValidFinalDigit
        {
            get
            {
                return IsValidCheckDigit1 & IsValidCheckDigit2 & IsValidCheckDigit3 & IsValidCheckDigit4;
            }
        }

        /// <summary>
        /// Property to show whether Gender is valid
        /// </summary>
        public bool IsValidSex
        {
            get
            {
                return Sex == "M" || Sex == "F" || Sex == "<";
            }
        }

        /// <summary>
        /// Property to show whether DOB is valid
        /// </summary>
        public bool IsValidDOB
        {
            get
            {
                return IsValidCheckDigit2;
            }
        }

        /// <summary>
        /// Property to show whether Passport Expiry Date is valid
        /// </summary>
        public bool IsValidPassportExpDate
        {
            get
            {
                return IsValidCheckDigit3;
            }
        }

        /// <summary>
        /// Property to show whether Nationality is valid
        /// </summary>
        public bool IsValidNationality
        {
            get
            {
                if (string.IsNullOrEmpty(Nationality))
                    return false;

                return Nationality.All(k => char.IsLetter(k) || k == '<');
            }
        }

        /// <summary>
        /// Property to show whether Passport No. is valid
        /// </summary>
        public bool IsValidPassportNum
        {
            get
            {
                return IsValidCheckDigit1;
            }
        }

        /// <summary>
        /// Method to validate a date in YYMMDD format
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private bool IsValidateDate(string x)
        {
            bool b = false;
            DateTime dt;

            try
            {
                b = DateTime.TryParseExact(x, "yyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
            }

            catch
            {

            }

            return b;
        }

        /// <summary>
        /// Method to calculates a check digit based on the input string
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private int GetCheckDigit(string x)
        {
            int cd = 0;

            string ch = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            int[] weight = new int[] { 7, 3, 1 };
            int sum = 0;

            for (int i = 0, j = 0; i < x.Length; i++)
            {
                char c = x[i];
                if (char.IsDigit(c))
                    sum += Convert.ToInt32(c.ToString()) * weight[j];

                else if (char.IsLetter(c))
                {
                    int v = ch.IndexOf(c) + 10;
                    sum += v * weight[j];
                }

                if (j == 2)
                    j = 0;

                else
                    ++j;
            }

            cd = sum % 10;
            return cd;
        }
    }

    /// <summary>
    /// Class to holds the MRZ data submitted from client to the web api
    /// </summary>
    public class MRZForm
    {
        public string PassportNum { get; set; }
        public string Nationality { get; set; }
        public string DOB { get; set; }
        public string Sex { get; set; }
        public string Expiry { get; set; }
    }
}
