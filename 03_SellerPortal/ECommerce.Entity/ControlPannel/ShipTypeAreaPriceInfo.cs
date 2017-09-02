﻿using ECommerce.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECommerce.Entity.ControlPannel
{
    /// <summary>
    /// 配送方式-地区-价格信息
    /// </summary>
    public class ShipTypeAreaPriceInfo : EntityBase
    {
        /// <summary>
        /// 系统编号
        /// </summary>
        public int? SysNo
        {
            get;
            set;
        }
        /// <summary>
        /// 商户
        /// </summary>
        public int? Merchant
        {
            get;
            set;

        }
        /// <summary>
        /// 配送区域
        /// </summary>
        public int? AreaSysNo
        {
            get;
            set;
        }

        /// <summary>
        /// 配送方式
        /// </summary>
        public int? ShipTypeSysNo
        {
            get;
            set;
        }
        /// <summary>
        /// 本段起始重量
        /// </summary>
        public int? BaseWeight
        {
            get;
            set;
        }
        /// <summary>
        /// 本段截至重量
        /// </summary>
        public int? TopWeight
        {
            get;
            set;
        }
        /// <summary>
        /// 重量基本单位
        /// </summary>
        public int? UnitWeight
        {
            get;
            set;
        }
        /// <summary>
        /// 价格基本单位
        /// </summary>
        public decimal? UnitPrice
        {
            get;
            set;
        }
        /// <summary>
        /// 本段最高价格
        /// </summary>
        public decimal? MaxPrice
        {
            get;
            set;
        }

        public CompanyCustomer CompanyCustomer { get; set; }


        public List<int> AreaSysNoList
        {
            get;
            set;
        }

        public string StoreCompanyCode { get; set; }

        /// <summary>
        /// 商家编号
        /// </summary>
        public int MerchantSysNo { get; set; }
    }
}
