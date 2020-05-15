using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace dynamic_configuration_parser
{
    [Serializable]
    public class DynamicConfiguration : DynamicObject, IDynamicMetaObjectProvider
    {
        public Dictionary<string, object> Properties { get; private set; } = new Dictionary<string, object>();

        public object this[string key]
        {
            get
            {
                try
                {
                    // try to get from properties collection first
                    return Properties[key];
                }
                catch (KeyNotFoundException ex)
                {
                    throw new UnknownKeyException();
                }
            }
            set
            {
                if (Properties.ContainsKey(key))
                {
                    Properties[key] = value;
                    return;
                }
                else
                {
                    Properties.Add(key, value);
                }
            }
        }


        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (!Properties.ContainsKey(binder.Name)) 
                throw new UnknownKeyException();            

            result = Properties[binder.Name];
            return true;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            Properties[binder.Name] = value;
            return true;
        }

        public override IEnumerable<string> GetDynamicMemberNames()
        {            
            return Properties.Keys.ToArray();
        }

    }
}
