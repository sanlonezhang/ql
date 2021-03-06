﻿using ECCentral.BizEntity.SO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECCentral.Job.SO.SFExpress
{
    public class Processor : Newegg.Oversea.Framework.JobConsole.Client.IJobAction
    {
        public void Run(Newegg.Oversea.Framework.JobConsole.Client.JobContext context)
        {
            string baseUrl = System.Configuration.ConfigurationManager.AppSettings["RestFulBaseUrl"];
            string languageCode = System.Configuration.ConfigurationManager.AppSettings["LanguageCode"];
            ECCentral.Job.Utility.RestClient client = new ECCentral.Job.Utility.RestClient(baseUrl, languageCode);
            client.Timeout = 100000;
            string companyCode = System.Configuration.ConfigurationManager.AppSettings["CompanyCode"];
            companyCode = companyCode == null ? null : (companyCode.Trim() == String.Empty ? null : companyCode.Trim());
            int Cnt = int.Parse(System.Configuration.ConfigurationManager.AppSettings["SigleCount"]);

            ECCentral.Job.Utility.RestServiceError error;

            List<string> trackingNumberList = new List<string>();
            client.Query<List<string>>("/SO/Job/GetWaitingFinishShippingList", ExpressType.SF, out trackingNumberList, out error);
            if (trackingNumberList != null && trackingNumberList.Count > 0)
            {
                trackingNumberList = trackingNumberList.FindAll(m => !string.IsNullOrWhiteSpace(m));
                for (int i = 0; i < trackingNumberList.Count; i = i + Cnt)
                {
                    List<string> currHandleList = new List<string>();
                    int thisCnt = i + Cnt;
                    for (int j = i; j < thisCnt; j++)
                    {
                        if (j >= trackingNumberList.Count)
                            break;
                        currHandleList.Add(trackingNumberList[j]);
                    }
                    object obj = new object();
                    client.Query("/SO/Job/QueryTracking", new ExpressQueryEntity() { Type = ExpressType.SF, TrackingNumberList = trackingNumberList }, out obj, out error);
                }
            }
        }
    }
}
