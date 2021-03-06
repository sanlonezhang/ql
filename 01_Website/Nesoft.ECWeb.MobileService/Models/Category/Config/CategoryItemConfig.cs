﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Nesoft.ECWeb.MobileService.Models.Category.Config
{
    [Serializable]
    public class CategoryItemConfig
    {
        [XmlAttribute("id")]
        public int ID { get; set; }

        [XmlAttribute("icon")]
        public string Icon { get; set; }

        [XmlElement("subCategory", Type = typeof(SubCategoryItemConfig))]
        public List<SubCategoryItemConfig> SubCatList { get; set; }
    }
}