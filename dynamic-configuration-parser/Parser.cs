using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Dynamic;
using System.Linq;
using System.Data;
using System.Collections;
using System.Runtime.Serialization;

namespace dynamic_configuration_parser
{
    public class Parser : IParser
    {
        public dynamic Parse(string configuration)
        {
            if (configuration.Trim().Length == 0) throw new ArgumentException();


            dynamic d = new DynamicConfiguration();
            var lines = configuration.Split(';');

            Regex rx = new Regex(@"^[a-zA-Z]\w*(\.@?[a-zA-Z]\w*)*$");  // Valid C# Property Name

            foreach(var line in lines)
            {
                if (line.Trim().Length == 0) continue;    //empty line

                var kvp = line.Split(':');
                if (kvp.Length < 2)
                    throw new Exception("One of configuration enty has Key or Value is missing.");

                var k = kvp[0].Trim();
                var v = kvp[1].Trim();

                if (k.Equals("")) throw new EmptyKeyException();

                if(!rx.Match(k).Success) throw new InvalidKeyException();


                if (d.Properties.ContainsKey(k)) throw new DuplicatedKeyException();

                bool bResult;
                int iResult;

                if (Boolean.TryParse(v, out bResult))
                    d[k] = bResult;
                else if (int.TryParse(v, out iResult))
                    d[k] = iResult;
                else
                    d[k] = v;
            }
            return d;
        }
    }
}
