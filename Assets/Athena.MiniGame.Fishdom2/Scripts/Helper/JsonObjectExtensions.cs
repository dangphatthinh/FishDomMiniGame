using UnityEngine;
using System;
using System.Collections.Generic;

namespace Athena.MiniGame.Fishdom2.Helper
{
    public static class JsonObjectExtensions
    {
        public static string parseJsonString(this object jsonObj, string fieldName)
        {
            object value = jsonObj.parseJsonKey(fieldName);
            return value == null ? "" : value.ToString();
        }

        public static int parseJsonInt(this object jsonObj, string fieldName)
        {
            object value = jsonObj.parseJsonKey(fieldName);
            if (value == null)
                return 0;
            return MathfExtensions.parseInt(value.ToString());
        }

        public static bool parseJsonBool(this object jsonObj, string fieldName)
        {
            object value = jsonObj.parseJsonKey(fieldName);
            return MathfExtensions.parseBool(value);
        }

        public static float parseJsonFloat(this object jsonObj, string fieldName)
        {
            object key = jsonObj.parseJsonKey(fieldName);
            return MathfExtensions.parseFloat(key.ToString());
        }

        public static object parseJsonKey(this object jsonObj, string key)
        {
            Dictionary<string, object> jsonDict = jsonObj as Dictionary<string, object>;
            if (jsonDict == null) return null;
            object fieldObj;
            jsonDict.TryGetValue(key, out fieldObj);
            return fieldObj;
        }

        public static string tryParseJsonString(this object jsonObj, string fieldName)
        {
            object key = jsonObj.parseJsonKey(fieldName);
            return key == null ? "" : key.ToString();
        }

        public static int tryParseJsonInt(this object jsonObj, string fieldName, int defaultVal = -1)
        {
            object fieldObj = jsonObj.parseJsonKey(fieldName);
            if (fieldObj == null) return defaultVal;
            return MathfExtensions.parseInt(fieldObj.ToString());
        }

        public static bool tryParseJsonBool(this object jsonObj, string fieldName)
        {
            object key = jsonObj.parseJsonKey(fieldName);
            return MathfExtensions.parseBool(key);
        }

        public static List<T> parseJsonObjList<T>(this object jsonObj, string fieldName, Func<object, T> convertFunc)
        {
            object jsonObjList = jsonObj.parseJsonKey(fieldName);
            List<object> objList = jsonObjList as List<object>;
            if (objList == null) return null;

            return objList.convertJsonObjList<T>(convertFunc);
        }

        public static T parseJsonObj<T>(this object jsonObj, string key, Func<object, T> convertFunc)
        {
            object jsonAtKey = jsonObj.parseJsonKey(key);
            return jsonAtKey.convertJsonObj(convertFunc);
        }

        public static List<T> convertJsonObjList<T>(this List<object> jsonObj, Func<object, T> convertFunc)
        {
            List<T> result = new List<T>();
            for (int i = 0; i < jsonObj.Count; i++)
            {
                result.Add(convertFunc(jsonObj[i]));
            }
            return result;
        }

        public static T convertJsonObj<T>(this object jsonObj, Func<object, T> convertFunc)
        {
            return convertFunc(jsonObj);
        }
    }

}
