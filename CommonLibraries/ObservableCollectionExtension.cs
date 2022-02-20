using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Collections.Custom
{
    public static class ObservableCollectionExtension
    {
        public static ObservableCollection<T> ToObservableCollection<T>(
            this IEnumerable<T> source)
        {
            return
                new ObservableCollection<T>(source);
        }
    }
}
