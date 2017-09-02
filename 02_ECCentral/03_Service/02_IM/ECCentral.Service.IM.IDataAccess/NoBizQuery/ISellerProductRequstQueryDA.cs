﻿//************************************************************************
// 用户名				泰隆优选
// 系统名				商家商品管理
// 子系统名		        商家商品管理NoBizQuery查询接口
// 作成者				Kevin
// 改版日				2012.6.8
// 改版内容				新建
//************************************************************************


using System.Collections.Generic;
using System.Data;
using ECCentral.QueryFilter.IM;

namespace ECCentral.Service.IM.IDataAccess.NoBizQuery
{
    public interface ISellerProductRequestQueryDA
    {
        DataTable QuerySellerProductRequest(SellerProductRequestQueryFilter queryCriteria, out int totalCount);
    }
}
