using System;


namespace CoreLibrary.Factory
{
    using Enums;

    public static class Delegators
    {
        
        public delegate void CallbackArg3<T>(T arg0, T arg1, T arg2);
        public delegate object ReturnCallback();

        #region Actions
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

        public static void FieldActionCallback<T>(FieldType field,
            Action<T> sizeCallback, Action<T> brandCallback, Action<T> endsCallback,
            T arg)
        {
            switch (field)
            {
                case FieldType.SIZE:
                    sizeCallback(arg);
                    break;
                case FieldType.BRAND:
                    brandCallback(arg);
                    break;
                case FieldType.ENDS:
                    endsCallback(arg);
                    break;
            }
        }

        public static void FieldActionCallback<T>(FieldType field,
            Action<T, T> sizeCallback, Action<T, T> brandCallback, Action<T, T> endsCallback,
            T arg0, T arg1)
        {
            switch (field)
            {
                case FieldType.SIZE:
                    sizeCallback(arg0, arg1);
                    break;
                case FieldType.BRAND:
                    brandCallback(arg0, arg1);
                    break;
                case FieldType.ENDS:
                    endsCallback(arg0, arg1);
                    break;
            }
        }

        public static void FieldActionCallback<T>(FieldType field,
            CallbackArg3<T> sizeCallback, CallbackArg3<T> brandCallback, CallbackArg3<T> endsCallback,
            T arg0, T arg1, T arg2)
        {
            switch (field)
            {
                case FieldType.SIZE:
                    sizeCallback(arg0, arg1, arg2);
                    break;
                case FieldType.BRAND:
                    brandCallback(arg0, arg1, arg2);
                    break;
                case FieldType.ENDS:
                    endsCallback(arg0, arg1, arg2);
                    break;
            }
        }
        #endregion

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

        public static object FieldFunctionCallback(FieldType field,
            Func<string, object> sizeCallback, Func<string, object> brandCallback, Func<string, object> endsCallback, string arg)
        {
            switch (field)
            {
                case FieldType.SIZE:
                    return sizeCallback.Invoke(arg);

                case FieldType.BRAND:
                    return brandCallback.Invoke(arg);

                case FieldType.ENDS:
                    return endsCallback.Invoke(arg);

                default:
                    break;
            }
            return null;
        }

        public static object FieldFunctionCallback(FieldType field,
            Func<string, string, object> sizeCallback, Func<string, string, object> brandCallback, Func<string, string, object> endsCallback,
            string arg0, string arg1)
        {
            switch (field)
            {
                case FieldType.SIZE:
                    return sizeCallback.Invoke(arg0, arg1);

                case FieldType.BRAND:
                    return brandCallback.Invoke(arg0, arg1);

                case FieldType.ENDS:
                    return endsCallback.Invoke(arg0, arg1);

                default:
                    break;
            }
            return null;
        }

    }
}
