﻿using ECCentral.Portal.UI.MKT.Facades;
using Newegg.Oversea.Silverlight.ControlPanel.Core;
using Newegg.Oversea.Silverlight.Controls;
using Newegg.Oversea.Silverlight.Controls.Components;
using System;
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
using ECCentral.Portal.Basic.Utilities;

namespace ECCentral.Portal.UI.MKT.UserControls
{
    public partial class UCGroupBuyingSettlementDetail : UserControl
    {
        public IDialog Dialog { get; set; }

        public IPage Page
        {
            get
            {
                return CPApplication.Current.CurrentPage;
            }
        }

        public UCGroupBuyingSettlementDetail()
        {
            InitializeComponent();

            Loaded += UCGroupBuyingSettlementDetail_Loaded;
        }

        void UCGroupBuyingSettlementDetail_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded -= UCGroupBuyingSettlementDetail_Loaded;

            var d = this.DataContext as dynamic;
            int sysNo = int.Parse(d.SysNo.ToString());
            new GroupBuyingFacade(this.Page).LoadGroupBuyingSettlementItemBySettleSysNo(sysNo, (s, a) =>
            {
                if (a.FaultsHandle())
                {
                    return;
                }
                this.DataGrid.ItemsSource = a.Result.Rows;
            });
        }

        private void hylView_Click(object sender, RoutedEventArgs e)
        {
            UCGroupBuyingTicketList uc = new UCGroupBuyingTicketList();
            var link = sender as HyperlinkButton;
            var info = link.DataContext as dynamic;
            uc.DataContext = info;
            var dialog = this.Page.Context.Window.ShowDialog("团购券", uc);
            uc.Dialog = dialog;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Dialog.ResultArgs = new ResultEventArgs { DialogResult = DialogResultType.Cancel };
            this.Dialog.Close();
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            var d = this.DataContext as dynamic;
            string sysNo = d.SysNo.ToString();

            HtmlViewHelper.WebPrintPreview("MKT", "GroupConsignLeasePrint", new Dictionary<string, string>() { { "GroupSysNo", sysNo } });
        }
    }
}
