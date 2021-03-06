﻿using System.Collections.Generic;
using System.Linq;


namespace ECCentral.Portal.Basic.Components.UserControls.AreaPicker
{
    public class AreaVM_Old
    {
        private AreaInfoVM_Old m_CurrentAreaInfo;
        /// <summary>
        /// 表示当前地区信息
        /// </summary>
        public AreaInfoVM_Old CurrentArea
        {
            get
            {
                if (m_CurrentAreaInfo == null)
                {
                    m_CurrentAreaInfo = new AreaInfoVM_Old();
                }
                return m_CurrentAreaInfo;
            }
            set
            {
                m_CurrentAreaInfo = value;
            }
        }

        private List<AreaInfoVM_Old> m_areaList;
        public List<AreaInfoVM_Old> AllArea
        {
            get
            {
                if (m_areaList == null)
                {
                    m_areaList = new List<AreaInfoVM_Old>();
                }
                return m_areaList;
            }
            set
            {
                m_areaList = value;
            }
        }

        /// <summary>
        /// 省份列表
        /// </summary>
        public List<KeyValuePair<string, string>> ProvinceAreaList
        {
            get
            {
                var list = AllArea.Where(w => w.ProvinceSysNo == null && w.CitySysNo == null);
                return LoadUIAreas(list);
            }
        }

        /// <summary>
        /// 市列表
        /// </summary>
        public List<KeyValuePair<string, string>> CityeAreaList
        {
            get
            {
                var list = AllArea.Where(w => w.ProvinceSysNo != null && w.CitySysNo == null && w.ProvinceSysNumber == CurrentArea.ProvinceSysNumber);
                return LoadUIAreas(list);
            }
        }

        /// <summary>
        /// 地区列表
        /// </summary>
        public List<KeyValuePair<string, string>> DistrictAreaList
        {
            get
            {
                var list = AllArea.Where(w => w.ProvinceSysNo != null && w.CitySysNo != null && w.CitySysNumber == CurrentArea.CitySysNumber);
                return LoadUIAreas(list);
            }
        }

        List<KeyValuePair<string, string>> LoadUIAreas(IEnumerable<AreaInfoVM_Old> whereList)
        {
            var result = whereList.Select(s => new KeyValuePair<string, string>(s.SysNo.ToString(), s.Status == -1 ? "(*)" + s.AreaName : s.AreaName)).ToList();
            result.Insert(0, new KeyValuePair<string, string>(null, ResLinkableDataPicker.ComboBox_PleaseSelect));
            return result;
        }
    }
}
