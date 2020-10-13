using System.Collections.Generic;
using Xamarin.Forms.Internals;

namespace Core.Extensions
{
    public static class ListExtensions
    {
        public static void Add<T>(this List<T> list, IEnumerable<T> enumerable)
            => list.AddRange(enumerable);
        
        public static void Add<T>(this IList<T> list, IEnumerable<T> enumerable)
            => enumerable.ForEach(e => list.Add(e));
    }
}