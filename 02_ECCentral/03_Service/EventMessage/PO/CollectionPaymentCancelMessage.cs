﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECCentral.Service.EventMessage.PO
{
    /// <summary>
    /// 代收代付结算单取消审核
    /// </summary>
    public class CollectionPaymentCancelMessage : ECCentral.Service.Utility.EventMessage
    {
        public override string Subject
        {
            get
            {
                return "ECC_COLLECTIONPAYMENT_CANCELED";
            }
        }
        /// <summary>
        /// 当前用户编号
        /// </summary>
        public int CurrentUserSysNo { get; set; }
        /// <summary>
        /// 代收代付结算单编号
        /// </summary>
        public int SysNo { get; set; }
    }
}
