﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ECCentral.QueryFilter.Invoice;

namespace ECCentral.Service.Invoice.IDataAccess.NoBizQuery
{
    public interface ICanBePayOrderQueryDA
    {
        DataTable Query(CanBePayOrderQueryFilter filter, out int totalCount);
    }
}