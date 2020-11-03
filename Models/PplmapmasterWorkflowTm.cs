using System;
using System.Collections.Generic;

namespace DigiSign.Models
{
    public partial class PplmapmasterWorkflowTm
    {
        public string Emplid { get; set; }
        public string NameDisplay { get; set; }
        public DateTime? Effdt { get; set; }
        public string NameLogin { get; set; }
        public string EmailAddr { get; set; }
        public string Jobcode { get; set; }
        public string JobcodeDescr { get; set; }
        public string UnionCd { get; set; }
        public string Deptid { get; set; }
        public string NdmDeptidDescr { get; set; }
        public string DivId { get; set; }
        public string DivDescr { get; set; }
        public string GroupId { get; set; }
        public string GroupDescr { get; set; }
        public string CBenefitCostCen { get; set; }
        public string CCostCentre { get; set; }
        public string NdmCstcenDescr { get; set; }
        public string Location { get; set; }
        public string NdmLocatiDescr { get; set; }
        public string PositionNbr { get; set; }
        public string ManagerPosn { get; set; }
        public string ManagerDescr { get; set; }
        public string ManagerId { get; set; }
        public string ManagerName { get; set; }
        public string ReportsTo { get; set; }
        public string ReportsToDescr { get; set; }
        public string ReportsToId { get; set; }
        public string ReportsToName { get; set; }
        public byte? ReportsToLoop { get; set; }
        public string MaManagerPosn { get; set; }
        public string MaManagerDescr { get; set; }
        public string MaManagerId { get; set; }
        public string MaManagerName { get; set; }
        public string ReportDottedLine { get; set; }
        public string ReportDottedLineDescr { get; set; }
        public string ReportDottedLineId { get; set; }
        public string ReportDottedLineName { get; set; }
        public string ApprovalDottedLine { get; set; }
        public string ApprovalDottedLineDescr { get; set; }
        public string ApprovalDottedLineId { get; set; }
        public string ApprovalDottedLineName { get; set; }
        public short? MaxEmplRcd { get; set; }
        public string NdmGroupDescr { get; set; }
        public string NdmSubgroupDescr { get; set; }
    }
}
