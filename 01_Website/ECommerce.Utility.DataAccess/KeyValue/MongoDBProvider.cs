﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECommerce.Utility.DataAccess.KeyValue
{
    public class MongoDBProvider : IKeyValueDataProvider
    {
        public T GetKeyValueData<T>(string dataCategory, string key) where T : class, new()
        {
            throw new NotImplementedException();
        }


        public List<T> GetKeyValueData<T>(string dataCategory) where T : class, new()
        {
            throw new NotImplementedException();
        }
    }
}
