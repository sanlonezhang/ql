﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECCentral.Service.Utility;

namespace ECCentral.Service.EventMessage.RMA
{
    /// <summary>
    /// RMA - 单件结案成功发送Message
    /// </summary>
    public class RMARegisterCaseClosedMessage : ECCentral.Service.Utility.EventMessage
    {
        /// <summary>
        /// 单件编号
        /// </summary>
        public int RegsiterSysNo { get; set; }
    }
}
