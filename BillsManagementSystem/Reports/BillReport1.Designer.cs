//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BillsManagementSystem.Reports {
    
    public partial class BillReport1 : DevExpress.XtraReports.UI.XtraReport {
        private void InitializeComponent() {
            DevExpress.XtraReports.ReportInitializer reportInitializer = new DevExpress.XtraReports.ReportInitializer(this, "BillsManagementSystem.Reports.BillReport1.vsrepx");

            // Controls
            this.TopMargin = reportInitializer.GetControl<DevExpress.XtraReports.UI.TopMarginBand>("TopMargin");
            this.BottomMargin = reportInitializer.GetControl<DevExpress.XtraReports.UI.BottomMarginBand>("BottomMargin");
            this.Detail = reportInitializer.GetControl<DevExpress.XtraReports.UI.DetailBand>("Detail");
            this.label1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("label1");

            // Parameters
            this.BillID = reportInitializer.GetParameter("BillID");

            // Data Sources
            this.sqlDataSource1 = reportInitializer.GetDataSource<DevExpress.DataAccess.Sql.SqlDataSource>("sqlDataSource1");
        }
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.DataAccess.Sql.SqlDataSource sqlDataSource1;
        private DevExpress.XtraReports.Parameters.Parameter BillID;
        private DevExpress.XtraReports.UI.XRLabel label1;
    }
}
