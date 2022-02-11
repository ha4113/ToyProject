using System;

namespace Common.Core.Util
{
    public abstract class Singleton<T>
        where T : new()
    {
#region Fields

        /// <summary>
        /// Static instance. Needs to use lambda expression
        /// to construct an instance (since constructor is private).
        /// </summary>
        private static Lazy<T> _instance = new Lazy<T>(() => new T());

#endregion

#region Properties

        /// <summary>
        /// Gets the instance of this singleton.
        /// </summary>
        public static T Instance
        {
            get => _instance.Value;
            set { _instance = new Lazy<T>(() => value); }
        }

#endregion
    }
}