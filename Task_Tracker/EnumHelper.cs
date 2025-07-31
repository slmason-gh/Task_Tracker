using System;
using System.ComponentModel;
using System.Linq;

namespace Task_Tracker.Helpers
{
    public static class EnumHelper
    {
        /// <summary>
        /// List values of an enum
        /// </summary>
        /// <typeparam name="T">An enumeration to be listed</typeparam>
        public static void ListEnumValues<T>() where T : Enum
        {
            var values = Enum.GetValues(typeof(T)).Cast<T>().ToList();
            for (int i = 0; i < values.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {GetDescription((Enum)(object)values[i])}");
            }
        }

        /// <summary>
        /// Choose a value from a list of enum values
        /// </summary>
        /// <returns>Index of the selected enum value, or -1 if invalid</returns>
        /// <param name="message">Prompt message</param>
        /// <param name="allowEmpty">Whether to allow empty input</param>
        /// <typeparam name="T">An enumeration to be listed</typeparam>
        public static int? ChooseEnumValue<T>(string message = "Choose an option: ", bool allowEmpty = false) where T : Enum
        {
            ListEnumValues<T>();
            Console.Write(message);
            string input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                return allowEmpty ? (int?)null : -1;
            }
            else if (int.TryParse(input, out int choice) &&
                choice >= 1 && choice <= Enum.GetValues(typeof(T)).Length)
            {
                return choice - 1;
            }
            return -1;
        }

        /// <summary>
        /// Get the description of an enum value, if it has one. If not, returns the enum value's name.
        /// </summary>
        /// <param name="value"></param>
        /// <returns> The description of the enum value, or its name if no description is found.
        /// </returns>
        public static string GetDescription(Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attr = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
            return attr != null ? attr.Description : value.ToString();
        }
    }
}