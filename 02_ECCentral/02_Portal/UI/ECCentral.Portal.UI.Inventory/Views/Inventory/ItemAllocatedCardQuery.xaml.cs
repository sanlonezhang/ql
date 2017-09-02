﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Navigation;

using Newegg.Oversea.Silverlight.Controls;
using Newegg.Oversea.Silverlight.ControlPanel.Core.Base;
using Newegg.Oversea.Silverlight.Controls.Data;

using ECCentral.QueryFilter.Inventory;
using ECCentral.Portal.UI.Inventory.Models;
using ECCentral.Portal.UI.Inventory.Facades;
using ECCentral.Portal.Basic.Utilities;
using ECCentral.Portal.Basic;
using ECCentral.Portal.UI.Inventory.Resources;

namespace ECCentral.Portal.UI.Inventory.Views
{
    [View(IsSingleton = true, NeedAccess = false, SingletonType = SingletonTypes.Url)]
    public partial class ItemAllocatedCardQuery : PageBase
    {
        public ItemAllocatedCardQueryFacade QueryFacade;
        public InventoryAllocatedCardQueryView PageView;
        public ItemAllocatedCardQuery()
        {
            InitializeComponent();
        }

        public override void OnPageLoad(object sender, EventArgs e)
        {
            base.OnPageLoad(sender, e);
            PageView = new InventoryAllocatedCardQueryView();
            QueryFacade = new ItemAllocatedCardQueryFacade(this);
            string getParam = this.Request.Param;
            if (!string.IsNullOrEmpty(getParam))
            {
                if (getParam.Contains(','))
                {
                    string[] ParamArray = getParam.Split(',');
                    PageView.QueryInfo.ProductSysNo = Convert.ToInt32(ParamArray[0]);
                    if (ParamArray[1] != "-999")//存在分仓(-999表示总仓)
                    {
                        PageView.QueryInfo.StockSysNo = Convert.ToInt32(ParamArray[1]);
                    }
                }
                else
                {
                    PageView.QueryInfo.ProductSysNo = Convert.ToInt32(getParam);
                }
            }
            expanderCondition.DataContext = PageView.QueryInfo;
            dgInventoryQueryResult.DataContext = PageView;
            dgItemCardQueryResult.DataContext = PageView;
            if (!string.IsNullOrEmpty(getParam))
            {
                btnSearch_Click(null, null);
            }            
        }

        #region [Events]

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            //查询:
            if (!PageView.QueryInfo.ProductSysNo.HasValue)
            {
                Window.Alert("请先选择商品!");
                return;
            }
            else
            {
               string errorMessage = "对不起，您没有权限访问该商品!";
               InventoryQueryFilter  queryFilter = new InventoryQueryFilter();
               queryFilter.ProductSysNo = PageView.QueryInfo.ProductSysNo;
               queryFilter.UserName = Newegg.Oversea.Silverlight.ControlPanel.Core.CPApplication.Current.LoginUser.LoginName;
               queryFilter.UserSysNo = Newegg.Oversea.Silverlight.ControlPanel.Core.CPApplication.Current.LoginUser.UserSysNo;
               queryFilter.CompanyCode = Newegg.Oversea.Silverlight.ControlPanel.Core.CPApplication.Current.CompanyCode;
               if (!AuthMgr.HasFunctionAbsolute(AuthKeyConst.IM_SeniorPM_Query))
               {
                   new InventoryQueryFacade(this).CheckOperateRightForCurrentUser(queryFilter, (ogj, args) =>
                  {
                      if (!args.FaultsHandle())
                      {
                          if (!args.Result)
                          {
                              Window.Alert(errorMessage);
                              return;
                          }
                          else
                          {
                              this.dgInventoryQueryResult.Bind();
                          }
                      }
                  });
               }
               else
               {
                   this.dgInventoryQueryResult.Bind();
               }
            }
        }

        private void dgInventoryQueryResult_LoadingDataSource(object sender, LoadingDataEventArgs e)
        {
            PageView.QueryInfo.PagingInfo = new QueryFilter.Common.PagingInfo
            {
                PageIndex = e.PageIndex,
                PageSize = e.PageSize,
                SortBy = e.SortField
            };

            #region 获取自己能访问到的PM

            PageView.QueryInfo.UserName = Newegg.Oversea.Silverlight.ControlPanel.Core.CPApplication.Current.LoginUser.LoginName;
            PageView.QueryInfo.CompanyCode = Newegg.Oversea.Silverlight.ControlPanel.Core.CPApplication.Current.CompanyCode;
            if (AuthMgr.HasFunctionAbsolute(AuthKeyConst.IM_SeniorPM_Query))
            {
                PageView.QueryInfo.PMQueryRightType = BizEntity.Common.PMQueryType.AllValid;
            }
            else if (AuthMgr.HasFunctionAbsolute(AuthKeyConst.IM_IntermediatePM_Query))
            {
                PageView.QueryInfo.PMQueryRightType = BizEntity.Common.PMQueryType.Team;
            }
            else if (AuthMgr.HasFunctionAbsolute(AuthKeyConst.IM_JuniorPM_Query))
            {
                PageView.QueryInfo.PMQueryRightType = BizEntity.Common.PMQueryType.Self;
            }
            else
            {
                PageView.QueryInfo.PMQueryRightType = null;
            }

            #endregion

            QueryFacade.QueryItemAllocatedCardInventoryTotal(PageView.QueryInfo, (totalCount1, vmList1) =>
            {
                PageView.InventoryTotalCount = totalCount1;
                PageView.InventoryResult = vmList1;

                if (PageView.QueryInfo.StockSysNo != null)
                {
                    QueryFacade.QueryItemAllocatedCardInventoryByStock(PageView.QueryInfo, (totalCount2, vmList2) =>
                    {
                        PageView.InventoryTotalCount += totalCount2;
                        PageView.InventoryResult.AddRange(vmList2);

                        dgInventoryQueryResult.ItemsSource = PageView.InventoryResult;
                    });
                }

                if (null != PageView.InventoryResult && PageView.InventoryTotalCount > 0)
                {
                    this.txtProductName.Visibility = Visibility.Visible;
                    this.txtProductNameDesc.Visibility = Visibility.Visible;
                    this.txtProductNameDesc.Text = PageView.InventoryResult[0]["ProductName"].ToString();
                }
                else
                {
                    this.txtProductName.Visibility = Visibility.Collapsed;
                    this.txtProductNameDesc.Visibility = Visibility.Collapsed;
                }
                this.dgItemCardQueryResult.Bind();

            });
        }
        private void dgItemCardQueryResult_LoadingDataSource(object sender, LoadingDataEventArgs e)
        {
            PageView.QueryInfo.PagingInfo = new QueryFilter.Common.PagingInfo
            {
                PageIndex = e.PageIndex,
                PageSize = e.PageSize,
                SortBy = e.SortField
            };
            QueryFacade.QueryItemAllocatedCardtemOrders(PageView.QueryInfo, (totalCount, vmList) =>
            {
                PageView.ItemCardTotalCount = totalCount;
                PageView.ItemCardResult = vmList;
                dgItemCardQueryResult.ItemsSource = PageView.ItemCardResult;
            });
        }


        #endregion

        private void hplOrderCode_Click(object sender, RoutedEventArgs e)
        {
            //TODO:链接至借货单维护页面:
            HyperlinkButton btn = sender as HyperlinkButton;
            if (null != btn)
            {
                string orderURL = GetOrderURLByOrderType(btn.Tag.ToString());
                Window.Navigate(String.Format(orderURL, btn.CommandParameter), null, true);
            }
        }

        private string GetOrderURLByOrderType(string orderType)
        {
            string orderURL = string.Empty;
            switch (orderType)
            {               
                case "4":
                    orderURL = ConstValue.Inventory_LendRequestMaintainUrlFormat;
                    break;               
                case "7"://transfer
                    orderURL = ConstValue.Inventory_ConvertRequestMaintainUrlFormat;
                    break;
                case "14":
                case "15":
                case "16"://shift
                    orderURL = ConstValue.Inventory_ShiftRequestMaintainUrlFormat;
                    break;
                case "10"://adjust
                    orderURL = ConstValue.Inventory_AdjustRequestMaintainUrlFormat;
                    break;
                case "17"://rma
                    orderURL = ConstValue.RMA_RequestMaintainUrl;
                    break;
                case "20"://负PO
                    orderURL = ConstValue.PO_PurchaseOrderMaintain;
                    break;
            }
            return orderURL;
        }

        private void dgItemCardQueryResult_ExportAllClick(object sender, EventArgs e)
        {
            if (!AuthMgr.HasFunctionPoint(AuthKeyConst.Inventory_ItemAllocatedCardQuery_ExportExcellAll))
            {
                Window.Alert("对不起，你没有权限进行此操作！");
                return;
            }

            if (this.dgItemCardQueryResult == null | this.dgItemCardQueryResult.TotalCount==0)
            {
                Window.Alert("没有可供导出的数据！");
                return;
            }
            //导出全部:
            if (null != PageView.QueryInfo)
            {
                InventoryAllocatedCardQueryVM exportQueryRequest = Newegg.Oversea.Silverlight.Utilities.UtilityHelper.DeepClone<InventoryAllocatedCardQueryVM>(PageView.QueryInfo);
                exportQueryRequest.PagingInfo = new QueryFilter.Common.PagingInfo() { PageIndex = 0, PageSize = ConstValue.MaxRowCountLimit };
                ColumnSet columnSet = new ColumnSet()
                .Add("OrderNameString", ResItemAllocatedCardQuery.GridHeader_OrderName, 40)
                .Add("OrderSysNo", ResItemAllocatedCardQuery.GridHeader_OrderCode, 40)
                .Add("OrderTime", ResItemAllocatedCardQuery.GridHeader_OrderTime, 20)
                .Add("OrderQty", ResItemAllocatedCardQuery.GridHeader_OrderQty, 20)
                .Add("OrderThenQty", ResItemAllocatedCardQuery.GridHeader_OrderThenQty, 20);
                new ItemAllocatedCardQueryFacade(this).ExportExcelForItemAllocatedCardOrders(exportQueryRequest, new ColumnSet[] { columnSet });
            }
        }
    }

}
