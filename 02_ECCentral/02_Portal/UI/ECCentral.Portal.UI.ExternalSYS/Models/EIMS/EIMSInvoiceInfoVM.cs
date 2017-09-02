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
using System.Collections.Generic;

namespace ECCentral.Portal.UI.ExternalSYS.Models
{
    public class EIMSInvoiceInfoVM : ModelBase
    {
        public List<EIMSInvoiceEntryVM> InvoiceInfoList { get; set; }

        public List<EIMSInvoiceInfoEntityVM> EIMSList { get; set; }

        public List<EIMSInvoiceInfoEntityVM> OldEIMSList { get; set; }
    }
}
