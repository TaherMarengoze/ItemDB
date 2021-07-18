using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserInterface.Types;

namespace UserInterface.Factory
{
    public static class Delegators
    {
        public delegate object ReturnCallback();

        public static void FieldActionCallback(FieldType field,
            Action sizeCallback, Action brandCallback, Action endsCallback)
        {
            switch (field)
            {
                case FieldType.SIZE:
                    sizeCallback();
                    break;
                case FieldType.BRAND:
                    brandCallback();
                    break;
                case FieldType.ENDS:
                    endsCallback();
                    break;
            }
        }

        public static object FieldFunctionCallback(FieldType field,
            Func<object> sizeCallback, Func<object> brandCallback, ReturnCallback endsCallback)
        {
            switch (field)
            {
                case FieldType.SIZE:
                    return sizeCallback.Invoke();

                case FieldType.BRAND:
                    return brandCallback.Invoke();

                case FieldType.ENDS:
                    return endsCallback.Invoke();

                default:
                    break;
            }
            return null;
        }
    }
}
