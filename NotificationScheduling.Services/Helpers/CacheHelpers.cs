using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;

namespace NotificationScheduling.Services.Helpers
{
    public static class CacheHelpers
    {
        public static T GetOrAdd<T>(this IMemoryCache cache, string key, T data)
        {
            if (cache.TryGetValue(key, out T value))
            {
                return value;
            }

            cache.Set(key, data, DateTimeOffset.Now.AddHours(1)); // could be made customizable

            return data;
        }
    }
}
