using System;
using System.Collections.Generic;

namespace TrafficLog_Services.Models;

public partial class Dealer
{
    public string? DlrmDlrCde { get; set; }

    public string? DlrmAbstime { get; set; }

    public string? DlrmRegName { get; set; }

    public string? DlrmOpName { get; set; }

    public string? DlrmAbbrev { get; set; }

    public string? DlrmPrincipal { get; set; }

    public string? DlrmAddrLn1 { get; set; }

    public string? DlrmAddrLn2 { get; set; }

    public string? DlrmCity { get; set; }

    public string? DlrmProv { get; set; }

    public string? DlrmPostCde { get; set; }

    public string? DlrmPhoneNumber { get; set; }

    public string? DlrmSalesDist { get; set; }

    public string? DlrmRegion { get; set; }

    public string? DlrmMarketCityN { get; set; }

    public string? DlrmDlrSz { get; set; }

    public int? DlrmFlrPlanLimit { get; set; }

    public string? DlrmVolGrp { get; set; }

    public string? DlrmSimDlrCde { get; set; }

    public string? DlrmDlrAttriCde { get; set; }

    public string? DlrmLangInd { get; set; }

    public string? DlrmVehCrRating { get; set; }

    public string? DlrmPartsCrRating { get; set; }

    public string? DlrmRgnHold { get; set; }

    public string? DlrmSrvcngDepot { get; set; }

    public string? DlrmStockInd { get; set; }

    public string? DlrmShipAddrLn1 { get; set; }

    public string? DlrmShipAddrLn2 { get; set; }

    public string? DlrmShipCity { get; set; }

    public string? DlrmShipProv { get; set; }

    public string? DlrmShipPostCde { get; set; }

    public string? DlrmServAttr1Flg { get; set; }

    public string? DlrmSelfAuthCde { get; set; }

    public decimal? DlrmReglCoopAmt { get; set; }

    public string? DlrmDlrAsso { get; set; }

    public string? DlrmTaxExmptCde { get; set; }

    public decimal? DlrmCopAdvAmt { get; set; }

    public string? DlrmApptDate { get; set; }

    public string? DlrmDlrTermDte { get; set; }

    public string? DlrmVehTermDte { get; set; }

    public string? DlrmPrtsTermDte { get; set; }

    public string? DlrmServTermDte { get; set; }

    public string? DlrmActgTermDte { get; set; }

    public string? DlrmFnceCde { get; set; }

    public decimal? DlrmWtyLrRte { get; set; }

    public string? DlrmWtyLrEffDte { get; set; }

    public decimal? DlrmWtyPrevLrRte { get; set; }

    public string? DlrmCsiOverFlg { get; set; }

    public string? DlrmVodFlg { get; set; }

    public string? DlrmParFlg { get; set; }

    public string? DlrmWtyDlrFlg { get; set; }

    public string? DlrmWebsite { get; set; }

    public string? DlrmDlrArea { get; set; }

    public string? DlrmMnaoDlrCde { get; set; }

    public string? DlrmBatchInd { get; set; }

    public string? DlrmStkOrdFlag { get; set; }

    public string? DlrmGstFlag { get; set; }

    public string? DlrmDsmCde { get; set; }

    public string? DlrmStptMc { get; set; }

    public string? DlrmStpt2 { get; set; }

    public string? DlrmStpt4m { get; set; }

    public string? DlrmStpt4 { get; set; }

    public string? DlrmRsvdForStpt { get; set; }

    public int? DlrmStkOrdLimit { get; set; }

    public int? DlrmPymtTermMc { get; set; }

    public int? DlrmPymtTerm2 { get; set; }

    public int? DlrmPymtTerm4m { get; set; }

    public int? DlrmPymtTerm4m1 { get; set; }

    public string? DlrmOrderingAreaCde { get; set; }

    public string? DlrmDlrTerrCde { get; set; }

    public string? DlrmVorShipVia { get; set; }

    public string? DlrmStkShipVia { get; set; }

    public int? DlrmDlyOrdLines { get; set; }

    public string? PdlrDlrPrcplPhn { get; set; }

    public string? PdlrDlrPrcplFax { get; set; }

    public string? PdlrPrtsMgrPhn { get; set; }

    public string? PdlrPrtsMgrFax { get; set; }

    public string? PdlrEmailStndrd { get; set; }

    public string? PdlrPrtsMgrEmail { get; set; }

    public string? PdlrDlrPrcplEmail { get; set; }

    public string? PdlrDmsSys { get; set; }

    public string? PdlrOldBuySell { get; set; }

    public string? PdlrToteCage { get; set; }

    public string? PdlrDdsLoadSeq { get; set; }

    public string? Area { get; set; }

    public string? Region { get; set; }

    public decimal? DlrxPostedLrRte { get; set; }

    public string? DlrxPostedLrEffDte { get; set; }

    public decimal? DlrxPostedPrevLrRte { get; set; }

    public string? DlrxOemAccyLrRte { get; set; }

    public string? DlrxOemMaintLrRte { get; set; }

    public string? ServiceTier { get; set; }
}
