using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimplyBrand.SocialMonitor.BusinessTest
{

    public  class TestUtility
    {


        //Random class used when generating strings.  Using these classes will
        //guarantee that multiple calls to these functions should return 
        //random values.
        private static Random _stringRandom = new Random();
        private static Random _dateRandom = new Random();
        private static Random _numberRandom = new Random();

        private static DateTime _minDate = new DateTime(1990, 1, 1);
        private static DateTime _maxDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

        /// <summary>
        /// Private constructor for Singleton
        /// </summary>



        /// <summary>
        /// Generates a random number between 0 and int.MaxValue.
        /// </summary>
        /// <returns>Random Number</returns>
        public static int RandomNumber()
        {
            return RandomNumber(0, int.MaxValue);
        }

        /// <summary>
        /// Generates a random number between the given bounds.
        /// </summary>
        /// <param name="min">lowest bound</param>
        /// <param name="max">highest bound</param>
        /// <returns>Random Number</returns>
        public static int RandomNumber(int min, int max)
        {
            return _numberRandom.Next(min, max);
        }

        /// <summary>
        /// Generates a random string with the given length
        /// </summary>
        /// <param name="size">Size of the string</param>
        /// <param name="lowerCase">If true, generate lowercase string</param>
        /// <returns>Random string</returns>
        /// <remarks>Mahesh Chand  - http://www.c-sharpcorner.com/Code/2004/Oct/RandomNumber.asp</remarks>
        public static string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            string retVal;
            char ch;

            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(26 * _stringRandom.NextDouble() + 65));
                builder.Append(ch);
            }

            if (lowerCase)
                retVal = builder.ToString().ToLower();
            else
                retVal = builder.ToString();

            return retVal;
        }

        /// <summary>
        /// Returns a random date between the default min and max dates
        /// </summary>
        /// <returns></returns>
        public static DateTime RandomDate()
        {
            return RandomDate(_minDate, _maxDate);
        }

        /// <summary>
        /// Returns a random date between the dates you pass in
        /// </summary>
        /// <param name="minDate">Min date to return</param>
        /// <param name="maxDate">Max date to return</param>
        /// <returns></returns>
        public static DateTime RandomDate(DateTime minDate, DateTime maxDate)
        {
            //Get the total days between the 2 dates
            int totalDays = (int)((TimeSpan)maxDate.Subtract(minDate)).TotalDays;

            //Pick a random date in between
            int randomDays = _dateRandom.Next(0, totalDays);

            //Return the random day.
            return minDate.AddDays(randomDays);
        }

        /// <summary>
        /// Returns a random date and time between the default datess
        /// </summary>
        /// <returns></returns>
        public static DateTime RandomDateTime()
        {
            return RandomDateTime(_minDate, _maxDate);
        }

        /// <summary>
        /// Get a random DateTime with a random Time
        /// </summary>
        /// <param name="minDate">Min datetime to return</param>
        /// <param name="maxDate">Max datetime to return</param>
        /// <returns></returns>
        public static DateTime RandomDateTime(DateTime minDate, DateTime maxDate)
        {
            //Get the total seconds between the 2 dates
            //Careful of overflow here
            int totalSeconds = (int)((TimeSpan)maxDate.Subtract(minDate)).TotalSeconds;

            //Pick a random date in between
            int randomSeconds = _dateRandom.Next(0, totalSeconds);

            //Return the random date.
            return minDate.AddSeconds(randomSeconds);
        }

        /// <summary>
        /// Returns a random boolean value
        /// </summary>
        /// <returns>Random Boolean</returns>
        public static bool RandomBoolean()
        {
            //if the second is odd, return True
            return ((DateTime.Now.Second % 2) > 0);
        }

        /// <summary>
        /// Returns a random character
        /// </summary>
        /// <returns>Random Character</returns>
        public static char RandomChar()
        {
            return Convert.ToChar(Convert.ToInt32(26 * _stringRandom.NextDouble() + 65));
        }

        /// <summary>
        /// Return a random byte between 0 and byte.MaxValue;
        /// </summary>
        /// <returns></returns>
        public static byte RandomByte()
        {
            return RandomByte(0, byte.MaxValue);
        }

        /// <summary>
        /// Return a random byte between the values specified
        /// </summary>
        /// <param name="min">Min value</param>
        /// <param name="max">Max value</param>
        /// <returns>Random Byte</returns>
        public static byte RandomByte(byte min, byte max)
        {
            return (byte)RandomNumber((int)min, (int)max);
        }

        /// <summary>
        /// Return a random short between 0 and byte.MaxValue;
        /// </summary>
        /// <returns></returns>
        public static short RandomShort()
        {
            return RandomShort(0, short.MaxValue);
        }

        /// <summary>
        /// Return a random short between the values specified
        /// </summary>
        /// <param name="min">Min value</param>
        /// <param name="max">Max value</param>
        /// <returns>Random short</returns>
        public static short RandomShort(short min, short max)
        {
            return (short)RandomNumber((int)min, (int)max);
        }
    }
}
