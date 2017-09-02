﻿using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Newegg.Oversea.Silverlight.ControlPanel.Core.Base;

namespace ECCentral.Portal.UI.Customer.Models
{
    public class ThirdPartyUserVM : ModelBase
    {
        public int CustomerSysNo { get; set; }

        public string CustomerID { get; set; }

        public string SubSource { get; set; }

        public string SourceName { get; set; }

        public string UserSource { get; set; }
    }
}
