﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECCentral.Service.EventMessage.MKT
{
    /// <summary>
    /// 优惠券审核消息体
    /// </summary>
    [Serializable]
    public class CouponAuditMessage : ECCentral.Service.Utility.EventMessage
    {
        public override string Subject
        {
            get
            {
                return "ECC_Coupon_Audited";
            }
        }

        /// <summary>
        /// 优惠券系统编号
        /// </summary>
        public int CouponSysNo { get; set; }

        /// <summary>
        /// 优惠券活动名称
        /// </summary>
        public string CouponName { get; set; }

        public int CurrentUserSysNo { get; set; }
    }
}
