﻿using System;
using System.Collections.Generic;
using System.Text;

namespace dynamic_configuration_parser
{
    public interface IParser
    {
        dynamic Parse(string configuration);
    }
}
