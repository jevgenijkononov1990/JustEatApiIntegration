using System;
using System.ComponentModel;
using System.Reflection;


namespace JustEatDemo.Common.Integrations.Enums
{
    public static class EnumExtensions
    {
        public static string GetDescription<T>(this T enumerationValue) where T : struct
        {
            try
            {
                Type type = enumerationValue.GetType();
                if (!type.IsEnum)
                {
                  
                    return string.Empty;
                }
                
                if (!System.Enum.IsDefined(type, enumerationValue))
                {
                    return string.Empty;
                }

                //Tries to find a DescriptionAttribute for a potential friendly name
                //for the enum
                MemberInfo[] memberInfo = type.GetMember(enumerationValue.ToString());
                if (memberInfo != null && memberInfo.Length > 0)
                {
                    object[] attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                    if (attrs != null && attrs.Length > 0)
                    {
                        //Pull out the description value
                        return ((DescriptionAttribute)attrs[0]).Description;
                    }
                }
                //If we have no description attribute, just return the ToString of the enum
                return enumerationValue.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return string.Empty;
        }

    }
}
