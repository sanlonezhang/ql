﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoClose.Model;

namespace AutoClose.Biz
{
    public class LeaseProductComparer : IEqualityComparer<LeaseProduct>
    {
        public bool Equals(LeaseProduct x, LeaseProduct y)
        {
            return x.ProductSysNo == y.ProductSysNo;
        }

        public int GetHashCode(LeaseProduct obj)
        {
            return obj.GetHashCode();
        }
    }
}
