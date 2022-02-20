using System;

namespace Controllers
{
    public class SelectEventArgs<TModel> : EventArgs
    {
        public string RequestInfo { get; set; }

        public TModel Selected { get; set; }
    }
}