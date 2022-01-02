using System;

namespace Client
{
    public static class EventsExtension
    {
        public static void CheckedInvoke<T>(this EventHandler<T> handler, T val, bool check)
        {
            if (check)
                handler?.Invoke(handler.Target, val);
        }
    }
}