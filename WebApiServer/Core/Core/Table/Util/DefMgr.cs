using System;
using System.Collections.Generic;

namespace Common.Core.Table.Util
{
    public interface IDef
    {
    }

    public static class DefMgr<T>
        where T : class, IDef
    {
        private static readonly Dictionary<long, T> _dic = new();

        public static void Add(long key, T value)
        {
            lock (_dic)
            {
                if (_dic.ContainsKey(key))
                {
                    throw new ArgumentException($"SameKey : {key}");
                }
                _dic.Add(key, value);
            }
        }

        public static bool TryGet(long key, out T def)
        {
            lock (_dic)
            {
                return _dic.TryGetValue(key, out def);
            }
        }

        public static T Get(long key)
        {
            return TryGet(key, out var def) ? def : null;
        }
    }
}