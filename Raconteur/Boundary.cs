﻿using System;

namespace Raconteur
{
    public class Boundary
    {
        public int Start { get; set; }
        public int End { get; set; }
        public int Length { get { return End - Start + 1;  } }
    }
}
