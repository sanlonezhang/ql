﻿using System;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;

using Newegg.Oversea.Silverlight.ControlPanel.Core.Base;
using Newegg.Oversea.Silverlight.Utilities.Validation;

using ECCentral.BizEntity.Common;
using ECCentral.BizEntity.Inventory;
using ECCentral.Portal.Basic.Utilities;
using ECCentral.Portal.Basic.Components.Models;
using Newegg.Oversea.Silverlight.ControlPanel.Core;
using ECCentral.Portal.UI.Inventory.Resources;
using ECCentral.QueryFilter.Common;
using ECCentral.Portal.UI.Inventory.Models;

namespace ECCentral.Portal.UI.Inventory.Models
{
    public class WarehouseOwnerInfoVM : ModelBase
    {
        public WarehouseOwnerInfoVM()
        {
            this.OwnerType = WarehouseOwnerType.Self;
            this.OwnerStatus = ValidStatus.Valid;
        }
        private int? sysNo;

        public int? SysNo
        {
            get { return sysNo; }
            set { base.SetValue("SysNo", ref sysNo, value); }
        }

        private string companyCode;

        public string CompanyCode
        {
            get { return companyCode; }
            set { base.SetValue("CompanyCode", ref companyCode, value); }
        }

        private WebChannelVM webChannel;

        public WebChannelVM WebChannel
        {
            get { return webChannel; }
            set { base.SetValue("WebChannel", ref webChannel, value); }
        }

        /// <summary>
        /// 所有者编号
        /// </summary>
        private string ownerID;

        public string OwnerID
        {
            get { return ownerID; }
            set { base.SetValue("OwnerID", ref ownerID, value); }
        }

        /// <summary>
        /// 所有者名称
        /// </summary>
        private string ownerName;

        public string OwnerName
        {
            get { return ownerName; }
            set { base.SetValue("OwnerName", ref ownerName, value); }
        }

        /// <summary>
        /// 所有者名称
        /// </summary>
        private string ownerMemo;

        public string OwnerMemo
        {
            get { return ownerMemo; }
            set { base.SetValue("OwnerMemo", ref ownerMemo, value); }
        }

        /// <summary>
        /// 所有者类型
        /// </summary>
        private WarehouseOwnerType ownerType;

        public WarehouseOwnerType OwnerType
        {
            get { return ownerType; }
            set { base.SetValue("OwnerType", ref ownerType, value); }
        }

        /// <summary>
        /// 所有者状态
        /// </summary>
        private ValidStatus ownerStatus;

        public ValidStatus OwnerStatus
        {
            get { return ownerStatus; }
            set { base.SetValue("OwnerStatus", ref ownerStatus, value); }
        }

        /// <summary>
        /// 创建人
        /// </summary>
        private int? createUserSysNo;

        public int? CreateUserSysNo
        {
            get { return createUserSysNo; }
            set { base.SetValue("CreateUserSysNo", ref createUserSysNo, value); }
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        private DateTime? createDate;

        public DateTime? CreateDate
        {
            get { return createDate; }
            set { base.SetValue("CreateDate", ref createDate, value); }
        }

        /// <summary>
        /// 编辑人
        /// </summary>
        private int? editUserSysNo;

        public int? EditUserSysNo
        {
            get { return editUserSysNo; }
            set { base.SetValue("EditUserSysNo", ref editUserSysNo, value); }
        }

        /// <summary>
        /// 编辑时间
        /// </summary>
        private DateTime? editDate;

        public DateTime? EditDate
        {
            get { return editDate; }
            set { base.SetValue("EditDate", ref editDate, value); }
        }

        private List<KeyValuePair<ValidStatus?, string>> ownerStatusList;
        public List<KeyValuePair<ValidStatus?, string>> OwnerStatusList
        {

            get
            {
                ownerStatusList = ownerStatusList ?? EnumConverter.GetKeyValuePairs<ValidStatus>();
                return ownerStatusList;
            }
        }

        private List<KeyValuePair<WarehouseOwnerType?, string>> ownerTypeList;
        public List<KeyValuePair<WarehouseOwnerType?, string>> OwnerTypeList
        {

            get
            {
                ownerTypeList = ownerTypeList ?? EnumConverter.GetKeyValuePairs<WarehouseOwnerType>();
                return ownerTypeList;
            }
        }

        #region UI Model
        public bool IsCreateMode
        {
            get
            {
                return !this.SysNo.HasValue || this.SysNo <= 0;
            }
        }
        #endregion UI Model
    }
}
