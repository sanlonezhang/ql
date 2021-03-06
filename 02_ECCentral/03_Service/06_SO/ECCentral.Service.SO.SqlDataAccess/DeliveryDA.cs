﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECCentral.BizEntity.SO;
using ECCentral.Service.Utility;
using ECCentral.Service.Utility.DataAccess;
using ECCentral.Service.SO.IDataAccess;

namespace ECCentral.Service.SO.SqlDataAccess
{
    [VersionExport(typeof(IDeliveryDA))]
    public class DeliveryDA : IDeliveryDA
    {
        /// <summary>
        ///  修改配送状态(info.Status,info.Note)，根据配送的订单编号(info.SOSysNo)、配送原因(info.Reason)和配送状态(oldStatus)
        /// </summary>
        /// <param name="info"></param>
        /// <param name="oldStatus">配送状态</param>
        public void UpdateLastOKStatus(DeliveryInfo info, DeliveryStatus oldStatus)
        {
            DataCommand command = DataCommandManager.GetDataCommand("SO_Delivery_Update_StatusBySOSysNoAndReason");
            command.SetParameterValue<DeliveryInfo>(info, true, false);
            command.SetParameterValue("@OldStatus", oldStatus);
            command.ExecuteNonQuery();
        }
        /// <summary>
        /// 取得配送信息
        /// </summary>
        /// <param name="type">配送类型</param>
        /// <param name="orderSysNo">单据编号</param>
        /// <param name="status">配送状态</param>
        /// <returns></returns>
        public DeliveryInfo GetDeliveryInfo(DeliveryType type, int orderSysNo, DeliveryStatus status)
        {
            DataCommand command = DataCommandManager.GetDataCommand("SO_Delivery_Get_DeliveryByOrderTypeAndOrderSysNo");
            command.SetParameterValue("@Type", type);
            command.SetParameterValue("@OrderSysNo", orderSysNo);
            command.SetParameterValue("@Status", status);
            return command.ExecuteEntity<DeliveryInfo>();
        }
    }
}