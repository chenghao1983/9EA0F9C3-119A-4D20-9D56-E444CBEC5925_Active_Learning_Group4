using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ActiveLearning.Business.Common
{
    public class Util
    {
        public static void CopyNonNullProperty(ref object from, ref object to)
        {
            if (from == null || to == null) return;
            PropertyInfo[] allProps = from.GetType().GetProperties();
            PropertyInfo toProp;
            foreach (PropertyInfo fromProp in allProps)
            {
                //find property on "to" with same name

                toProp = to.GetType().GetProperty(fromProp.Name);
                if (toProp == null) continue; //not here
                if (!toProp.CanWrite) continue; //only if writeable
                                                //set the property value from "from" to "to"
                                                // Debug.WriteLine(toProp.Name + " = from." + fromProp.Name + ";");
                if (fromProp.GetValue(from, null) == null) continue;
                toProp.SetValue(to, fromProp.GetValue(from, null), null);
            }
        }

        public static void CopyNonNullProperty(object from, object to)
        {
            if (from == null || to == null) return;
            PropertyInfo[] allProps = from.GetType().GetProperties();
            PropertyInfo toProp;
            foreach (PropertyInfo fromProp in allProps)
            {
                //find property on "to" with same name

                toProp = to.GetType().GetProperty(fromProp.Name);
                if (toProp == null) continue; //not here
                if (!toProp.CanWrite) continue; //only if writeable
                                                //set the property value from "from" to "to"
                                                // Debug.WriteLine(toProp.Name + " = from." + fromProp.Name + ";");
                if (fromProp.GetValue(from, null) == null) continue;
                toProp.SetValue(to, fromProp.GetValue(from, null), null);
            }
        }
    }
}
