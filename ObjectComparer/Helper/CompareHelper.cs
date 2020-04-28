using System;
using System.Collections;
using System.Reflection;
using System.Linq;

namespace ObjectComparer.Helper
{
    public class CompareHelper
    {
        public static bool CompareObjects(object inputObjectA, object inputObjectB)
        {
            bool areObjectsEqual = true;
            //check if both objects are not null before starting comparing children
            if (inputObjectA != null && inputObjectB != null)
            {
                //create variables to store object values
                object value1, value2;

                PropertyInfo[] properties = inputObjectA.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

                //get all public properties of the object using reflection  
                foreach (PropertyInfo propertyInfo in properties)
                {

                    //get the property values of both the objects
                    value1 = propertyInfo.GetValue(inputObjectA, null);
                    value2 = propertyInfo.GetValue(inputObjectB, null);

                    // if the objects are primitive types such as (integer, string etc)
                    //that implement IComparable, we can just directly try and compare the value     
                    if (TypeHelper.IsAssignableFrom(propertyInfo.PropertyType) 
                        || TypeHelper.IsPrimitiveType(propertyInfo.PropertyType) 
                        || TypeHelper.IsValueType(propertyInfo.PropertyType))
                    {
                        //compare the values
                        if (!CompareValues(value1, value2))
                        {
                            areObjectsEqual = false;
                            return areObjectsEqual;
                        }
                    }

                    //if the property is a collection (or something that implements IEnumerable)
                    //we have to iterate through all items and compare values
                    else if (TypeHelper.IsEnumerableType(propertyInfo.PropertyType))
                    {
                        areObjectsEqual = CompareEnumerations(value1 as IEnumerable, value2 as IEnumerable);

                        if (!areObjectsEqual)
                        {
                            return areObjectsEqual;
                        }
                    }
                    //if it is a class object, call the same function recursively again
                    else if (propertyInfo.PropertyType.IsClass)
                    {
                        if (!CompareObjects(propertyInfo.GetValue(inputObjectA, null), propertyInfo.GetValue(inputObjectB, null)))
                        {
                            areObjectsEqual = false;

                            return areObjectsEqual;
                        }
                    }
                    else
                    {
                        areObjectsEqual = false;
                    }
                }
            }
            else if(inputObjectA == null && inputObjectB == null)
            {
                areObjectsEqual = true;
            }
            else
                areObjectsEqual = false;

            return areObjectsEqual;
        }

        

        /// <summary>
        /// Compares two values and returns if they are the same.
        /// </summary>       
        private static bool CompareValues(object value1, object value2)
        {
            bool areValuesEqual = true;
            IComparable selfValueComparer = value1 as IComparable;

            // one of the values is null            
            if (value1 == null && value2 != null || value1 != null && value2 == null)
                areValuesEqual = false;
            else if (selfValueComparer != null && selfValueComparer.CompareTo(value2) != 0)
                areValuesEqual = false;
            else if (!object.Equals(value1, value2))
                areValuesEqual = false;

            return areValuesEqual;
        }

        private static bool CompareEnumerations(IEnumerable value1, IEnumerable value2)
        {
            // if one of the values is null, no need to proceed return false;
            if (value1 == null && value2 != null || value1 != null && value2 == null)
                return false;


            var enumValue1 = value1.Cast<object>().ToList();
            var enumValue2 = value2.Cast<object>().ToList();
            //var item = enumValue1.FirstOrDefault();
            //var isComparable = IsAssignableFrom(item.GetType());

            if (!(value1 is IDictionary))
            {
                enumValue1 = value1.Cast<object>().OrderBy(x => x).ToList();
                enumValue2 = value2.Cast<object>().OrderBy(x => x).ToList();


            }

            // if the items count are different return false
            if (enumValue1.Count() != enumValue2.Count())
                return false;
            // if the count is same, compare individual item
            else
            {
                object firstObjectValue, secondObjectValue;
                Type objectValueType;
                for (int itemIndex = 0; itemIndex < enumValue1.Count(); itemIndex++)
                {
                    firstObjectValue = enumValue1.ElementAt(itemIndex);
                    secondObjectValue = enumValue2.ElementAt(itemIndex);

                    objectValueType = firstObjectValue.GetType();

                    if (TypeHelper.IsAssignableFrom(objectValueType) 
                        || TypeHelper.IsPrimitiveType(objectValueType) 
                        || TypeHelper.IsValueType(objectValueType))
                    {
                        if (!CompareValues(firstObjectValue, secondObjectValue))
                            return false;
                    }
                    else if (!CompareObjects(firstObjectValue, secondObjectValue))
                        return false;
                }
            }
            return true;
        }
    }
}
