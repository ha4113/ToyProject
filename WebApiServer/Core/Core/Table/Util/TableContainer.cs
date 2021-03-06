using System;
using System.Collections.Generic;

namespace Common.Core.Table.Util
{
    public interface IReadOnlyTable
    {
        
    }
    
    public interface ITable : IReadOnlyTable
    { 
        void PostProcess(); 
        void Valid();
    }

    public interface ITableContainer
    {
        static string PostProcessName => nameof(PostProcess);
        static string ValidCheckName => nameof(ValidCheck); 
        void PostProcess(); 
        void ValidCheck();
    }

    public class TableContainer<TValue> : ITableContainer
        where TValue : class, ITable
    {
        private static readonly Dictionary<long, TValue> _dic = new();

        public static void Add(long key, TValue def)
        {
            lock (_dic)
            {
                if (_dic.ContainsKey(key))
                {
                    throw new ArgumentException($"SameKey : {key}");
                }

                _dic.Add(key, def);
            }
        }

        public static bool TryGet(long key, out TValue def)
        {
            lock (_dic)
            {
                return _dic.TryGetValue(key, out def);
            }
        }

        public static TValue Get(long key) { return TryGet(key, out var def) ? def : null; }

        private static void PostProcess()
        {
            lock (_dic)
            {
                foreach (var (_, def) in _dic)
                {
                    def.PostProcess();
                }
            }
        }

        private static void ValidCheck()
        {
            lock (_dic)
            {
                foreach (var (_, def) in _dic)
                {
                    try
                    {
                        def.Valid();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }
            }
        }

        void ITableContainer.PostProcess() => PostProcess();
        void ITableContainer.ValidCheck() => ValidCheck();
    }
}