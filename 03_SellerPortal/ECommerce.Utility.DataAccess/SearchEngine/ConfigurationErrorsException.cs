﻿using System;

namespace ECommerce.Utility.DataAccess.SearchEngine
{
    public class ConfigurationErrorsException : Exception
    {
        public ConfigurationErrorsException(string message)
            : base(message)
        {
        }

        public ConfigurationErrorsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}