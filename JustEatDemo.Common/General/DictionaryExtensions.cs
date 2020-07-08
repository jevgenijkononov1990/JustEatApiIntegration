using System.Collections;
using System.Collections.Generic;

namespace JustEatDemo.Common.General
{
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Converts an unqualified IDictionary to an IDictionary&lt;string, string&gt;
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IDictionary<string, string> ToStringDictionary(this IDictionary source)
        {
            var dict = new Dictionary<string, string>();

            foreach (DictionaryEntry de in source)
            {
                if (!(de.Key is string))
                    continue;

                dict.Add((string)de.Key, de.Value == null ? null : de.Value.ToString());
            }

            return dict;
        }

        /// <summary>
        /// Extension method to retrieve a value from a dictionary or null if the key is not present.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <param name="source"></param>
        /// <param name="key"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public static TVal GetValueOrDefault<TKey, TVal>(this IDictionary<TKey, TVal> source, TKey key, TVal def = default(TVal))
        {
            TVal val;
            return source.TryGetValue(key, out val) ? val : def;
        }

        /// <summary>
        /// Extension method to retrieve a value from a read-only dictionary or null if the key is not present.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <param name="source"></param>
        /// <param name="key"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public static TVal GetValueOrDefault<TKey, TVal>(this IReadOnlyDictionary<TKey, TVal> source, TKey key, TVal def = default(TVal))
        {
            TVal val;
            return source.TryGetValue(key, out val) ? val : def;
        }
    }
}
