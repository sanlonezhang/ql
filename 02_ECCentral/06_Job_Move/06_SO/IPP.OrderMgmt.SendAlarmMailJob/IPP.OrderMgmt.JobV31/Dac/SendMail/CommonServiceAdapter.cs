﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IPP.OrderMgmt.JobV31.BusinessEntities.SendMail;
using Newegg.Oversea.Framework.ServiceConsole.Client;
using Newegg.Oversea.Framework.Contract;
using ECCentral.BizEntity.Common;
using ECCentral.Job.Utility;

namespace IPP.OrderMgmt.JobV31.Dac.SendMail
{
   public class CommonServiceAdapter
    {
        #region Message Contains Mail
              
        /// <summary>
        /// 发送内部邮件
        /// </summary>
        /// <param name="mail">内部邮件实体</param>
       public static bool SendMail(MailInfo mailInfor)
        {
            if (string.IsNullOrEmpty(mailInfor.ToName))
            {
                return false;
            }
            else
            {
                mailInfor.ToName = mailInfor.ToName.Trim();
            }
            string baseUrl = System.Configuration.ConfigurationManager.AppSettings["CommonRestFulBaseUrl"];
            string languageCode = System.Configuration.ConfigurationManager.AppSettings["LanguageCode"];
            string companyCode = System.Configuration.ConfigurationManager.AppSettings["CompanyCode"];
            ECCentral.Job.Utility.RestClient client = new ECCentral.Job.Utility.RestClient(baseUrl, languageCode);
            ECCentral.Job.Utility.RestServiceError error;
            var ar = client.Create("/Message/SendMail", mailInfor, out error);
            if (error != null && error.Faults != null && error.Faults.Count > 0)
            {
                string errorMsg = "";
                foreach (var errorItem in error.Faults)
                {
                    errorMsg += errorItem.ErrorDescription;
                }
                Logger.WriteLog(errorMsg, "JobConsole");
            }

            return true;
        }       
        #endregion
    }
}
