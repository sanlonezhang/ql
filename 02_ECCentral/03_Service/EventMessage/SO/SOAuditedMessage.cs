﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECCentral.Service.Utility;
using System.Xml.Serialization;

namespace ECCentral.Service.EventMessage.SO
{
    [Serializable]
    public class SOAuditedMessage : ECCentral.Service.Utility.EventMessage
    {
        public override string Subject
        {
            get
            {
                return "ECC_SO_Audited";
            }
        }
        /// <summary>
        /// 订单编号
        /// </summary>
        public int SOSysNo
        {
            get;
            set;
        }

        /// <summary>
        /// 主订单编号，如果作废的是子订单，则此值才有效：表示子订单（SOSysNo）对应的主订单编号
        /// </summary> 
        public int? MasterSOSysNo { get; set; }

        /// <summary>
        /// 商家系统编号
        /// </summary>
        public int MerchantSysNo { get; set; }

        /// <summary>
        /// 客户编号
        /// </summary>
        public int CustomerSysNo { get; set; }

        /// <summary>
        /// 订单创建时间
        /// </summary>
        public DateTime OrderTime { get; set; }

        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal OrderAmount { get; set; }

        /// <summary>
        /// 订单审核用户编号
        /// </summary>
        public int AuditedUserSysNo { get; set; }

        /// <summary>
        /// 订单审核用户名
        /// </summary>
        public string AuditedUserName { get; set; }

    }
}
