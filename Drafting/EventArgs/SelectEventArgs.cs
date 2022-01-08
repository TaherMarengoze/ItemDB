using System;

namespace Controllers
{
    public class SelectEventArgs<TModel> : EventArgs
    {
        public TModel Selected { get; set; }
    }
}