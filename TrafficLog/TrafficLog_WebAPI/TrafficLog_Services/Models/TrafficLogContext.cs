using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TrafficLog_Services.Models;

public partial class TrafficLogContext : DbContext
{
    public TrafficLogContext()
    {
    }

    public TrafficLogContext(DbContextOptions<TrafficLogContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CarLine> CarLines { get; set; }

    public virtual DbSet<CarLineView> CarLineViews { get; set; }

    public virtual DbSet<CarLineViewCx90p> CarLineViewCx90ps { get; set; }

    public virtual DbSet<CarlineCxpsView> CarlineCxpsViews { get; set; }

    public virtual DbSet<Dealer> Dealers { get; set; }

    public virtual DbSet<DealerTimeZone> DealerTimeZones { get; set; }

    public virtual DbSet<DealerTimeZone20nov23> DealerTimeZone20nov23s { get; set; }

    public virtual DbSet<DealerView> DealerViews { get; set; }

    public virtual DbSet<HistoricalSubmissionReport> HistoricalSubmissionReports { get; set; }

    public virtual DbSet<MonthlySalesTarget> MonthlySalesTargets { get; set; }

    public virtual DbSet<MonthlySalesTargetView> MonthlySalesTargetViews { get; set; }

    public virtual DbSet<MssqlTemporalHistoryFor805577908> MssqlTemporalHistoryFor805577908s { get; set; }

    public virtual DbSet<NcmDataExportCarLineOrder> NcmDataExportCarLineOrders { get; set; }

    public virtual DbSet<OldDealerCode> OldDealerCodes { get; set; }

    public virtual DbSet<ReportingWeekCalendar> ReportingWeekCalendars { get; set; }

    public virtual DbSet<ReportingWeekCalendarBfUpdt> ReportingWeekCalendarBfUpdts { get; set; }

    public virtual DbSet<ReportingWeekCalenderView> ReportingWeekCalenderViews { get; set; }

    public virtual DbSet<Reportingweek> Reportingweeks { get; set; }

    public virtual DbSet<SubmissionReportLoadError> SubmissionReportLoadErrors { get; set; }

    public virtual DbSet<TempHistDataLoad1> TempHistDataLoad1s { get; set; }

    public virtual DbSet<TempHistDataLoad2> TempHistDataLoad2s { get; set; }

    public virtual DbSet<TempHistDataLoad3> TempHistDataLoad3s { get; set; }

    public virtual DbSet<TrafficLog> TrafficLogs { get; set; }

    public virtual DbSet<TrafficLog19jun23> TrafficLog19jun23s { get; set; }

    public virtual DbSet<TrafficLogAdd> TrafficLogAdds { get; set; }

    public virtual DbSet<TrafficLogCx90PreSplitView> TrafficLogCx90PreSplitViews { get; set; }

    public virtual DbSet<TrafficLogHistory> TrafficLogHistories { get; set; }

    public virtual DbSet<TrafficeLogCsvsheet> TrafficeLogCsvsheets { get; set; }

    public virtual DbSet<TrafficlogHistory1> TrafficlogHistory1s { get; set; }
    public virtual DbSet<SubmissionReport_WOW> SubmissionReport_WOWs { get; set; }

    public virtual DbSet<DetailReport_MTD> DetailReport_MTDs { get; set; }

    public virtual DbSet<DetailReport_MTDTrend> DetailReport_MTDTrends { get; set; }
    public virtual DbSet<DetailReport_WOW> DetailReport_WOWs { get; set; }

    public virtual DbSet<DetailReport_MOM> DetailReport_MOMs { get; set; }

    public virtual DbSet<SummaryReport> SummaryReports { get; set; }

    public virtual DbSet<SummaryReport_MTD> SummaryReport_MTDs { get; set; }

    public virtual DbSet<SummaryReport_WOW> SummaryReport_WOWs { get; set; }

    public virtual DbSet<SummaryReport_MOM> SummaryReport_MOMs { get; set; }

    public virtual DbSet<SummaryReport_MTDTrend> SummaryReport_MTDTrends { get; set; }



    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CarLine>(entity =>
        {
            entity.HasKey(e => e.CarLineId).HasName("CarLne_PK");

            entity.ToTable("CarLine");

            entity.Property(e => e.CarLineId).HasColumnName("CarLineID");
            entity.Property(e => e.CarLineEffectiveDate).HasColumnType("date");
            entity.Property(e => e.CarLineEndDate).HasColumnType("date");
            entity.Property(e => e.CarLineName)
                .HasMaxLength(60)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CarLineView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("CarLine_View");

            entity.Property(e => e.CarLineEffectiveDate).HasColumnType("date");
            entity.Property(e => e.CarLineEndDate).HasColumnType("date");
            entity.Property(e => e.CarLineId)
                .ValueGeneratedOnAdd()
                .HasColumnName("CarLineID");
            entity.Property(e => e.CarLineName)
                .HasMaxLength(60)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CarLineViewCx90p>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("CarLine_View_CX90PS");

            entity.Property(e => e.CarLineEffectiveDate).HasColumnType("date");
            entity.Property(e => e.CarLineEndDate).HasColumnType("date");
            entity.Property(e => e.CarLineId)
                .ValueGeneratedOnAdd()
                .HasColumnName("CarLineID");
            entity.Property(e => e.CarLineName)
                .HasMaxLength(60)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CarlineCxpsView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Carline_CXPS_View");

            entity.Property(e => e.CarLineName)
                .HasMaxLength(14)
                .IsUnicode(false);
            entity.Property(e => e.CarlineId).HasColumnName("CarlineID");
        });

        modelBuilder.Entity<Dealer>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Dealer");

            entity.Property(e => e.Area)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.DlrmAbbrev)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("DLRM-ABBREV");
            entity.Property(e => e.DlrmAbstime)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("DLRM-ABSTIME");
            entity.Property(e => e.DlrmActgTermDte)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("DLRM-ACTG-TERM-DTE");
            entity.Property(e => e.DlrmAddrLn1)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("DLRM-ADDR-LN-1");
            entity.Property(e => e.DlrmAddrLn2)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("DLRM-ADDR-LN-2");
            entity.Property(e => e.DlrmApptDate)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("DLRM-APPT-DATE");
            entity.Property(e => e.DlrmBatchInd)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("DLRM-BATCH-IND");
            entity.Property(e => e.DlrmCity)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("DLRM-CITY");
            entity.Property(e => e.DlrmCopAdvAmt)
                .HasColumnType("numeric(16, 2)")
                .HasColumnName("DLRM-COP-ADV-AMT");
            entity.Property(e => e.DlrmCsiOverFlg)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("DLRM-CSI-OVER-FLG ");
            entity.Property(e => e.DlrmDlrArea)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("DLRM-DLR-AREA");
            entity.Property(e => e.DlrmDlrAsso)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("DLRM-DLR-ASSO");
            entity.Property(e => e.DlrmDlrAttriCde)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("DLRM-DLR-ATTRI-CDE");
            entity.Property(e => e.DlrmDlrCde)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("DLRM-DLR-CDE");
            entity.Property(e => e.DlrmDlrSz)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("DLRM-DLR-SZ");
            entity.Property(e => e.DlrmDlrTermDte)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("DLRM-DLR-TERM-DTE");
            entity.Property(e => e.DlrmDlrTerrCde)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("DLRM-DLR-TERR-CDE");
            entity.Property(e => e.DlrmDlyOrdLines).HasColumnName("DLRM-DLY-ORD-LINES");
            entity.Property(e => e.DlrmDsmCde)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("DLRM-DSM-CDE");
            entity.Property(e => e.DlrmFlrPlanLimit).HasColumnName("DLRM-FLR-PLAN-LIMIT");
            entity.Property(e => e.DlrmFnceCde)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("DLRM-FNCE-CDE");
            entity.Property(e => e.DlrmGstFlag)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("DLRM-GST-FLAG");
            entity.Property(e => e.DlrmLangInd)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("DLRM-LANG-IND");
            entity.Property(e => e.DlrmMarketCityN)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("DLRM-MARKET-CITY-N");
            entity.Property(e => e.DlrmMnaoDlrCde)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("DLRM-MNAO-DLR-CDE");
            entity.Property(e => e.DlrmOpName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DLRM-OP-NAME");
            entity.Property(e => e.DlrmOrderingAreaCde)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("DLRM-ORDERING-AREA-CDE");
            entity.Property(e => e.DlrmParFlg)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("DLRM-PAR-FLG");
            entity.Property(e => e.DlrmPartsCrRating)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("DLRM-PARTS-CR-RATING");
            entity.Property(e => e.DlrmPhoneNumber)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("DLRM-PHONE-NUMBER");
            entity.Property(e => e.DlrmPostCde)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("DLRM-POST-CDE");
            entity.Property(e => e.DlrmPrincipal)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("DLRM-PRINCIPAL");
            entity.Property(e => e.DlrmProv)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("DLRM-PROV");
            entity.Property(e => e.DlrmPrtsTermDte)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("DLRM-PRTS-TERM-DTE");
            entity.Property(e => e.DlrmPymtTerm2).HasColumnName("DLRM-PYMT-TERM (2)");
            entity.Property(e => e.DlrmPymtTerm4m).HasColumnName("DLRM-PYMT-TERM (4M)");
            entity.Property(e => e.DlrmPymtTerm4m1).HasColumnName("DLRM-PYMT-TERM (4M)_1");
            entity.Property(e => e.DlrmPymtTermMc).HasColumnName("DLRM-PYMT-TERM (MC)");
            entity.Property(e => e.DlrmRegName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DLRM-REG-NAME");
            entity.Property(e => e.DlrmRegion)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("DLRM-REGION");
            entity.Property(e => e.DlrmReglCoopAmt)
                .HasColumnType("numeric(16, 2)")
                .HasColumnName("DLRM-REGL-COOP-AMT");
            entity.Property(e => e.DlrmRgnHold)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("DLRM-RGN-HOLD");
            entity.Property(e => e.DlrmRsvdForStpt)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("DLRM-RSVD-FOR-STPT");
            entity.Property(e => e.DlrmSalesDist)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("DLRM-SALES-DIST");
            entity.Property(e => e.DlrmSelfAuthCde)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("DLRM-SELF-AUTH-CDE");
            entity.Property(e => e.DlrmServAttr1Flg)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("DLRM-SERV-ATTR1-FLG");
            entity.Property(e => e.DlrmServTermDte)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("DLRM-SERV-TERM-DTE");
            entity.Property(e => e.DlrmShipAddrLn1)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("DLRM-SHIP-ADDR-LN1");
            entity.Property(e => e.DlrmShipAddrLn2)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("DLRM-SHIP-ADDR-LN2");
            entity.Property(e => e.DlrmShipCity)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("DLRM-SHIP-CITY");
            entity.Property(e => e.DlrmShipPostCde)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("DLRM-SHIP-POST-CDE");
            entity.Property(e => e.DlrmShipProv)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("DLRM-SHIP-PROV");
            entity.Property(e => e.DlrmSimDlrCde)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("DLRM-SIM-DLR-CDE");
            entity.Property(e => e.DlrmSrvcngDepot)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("DLRM-SRVCNG-DEPOT");
            entity.Property(e => e.DlrmStkOrdFlag)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("DLRM-STK-ORD-FLAG");
            entity.Property(e => e.DlrmStkOrdLimit).HasColumnName("DLRM-STK-ORD-LIMIT");
            entity.Property(e => e.DlrmStkShipVia)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("DLRM-STK-SHIP-VIA");
            entity.Property(e => e.DlrmStockInd)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("DLRM-STOCK-IND");
            entity.Property(e => e.DlrmStpt2)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("DLRM-STPT (2)");
            entity.Property(e => e.DlrmStpt4)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("DLRM-STPT (4) ");
            entity.Property(e => e.DlrmStpt4m)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("DLRM-STPT (4M)    ");
            entity.Property(e => e.DlrmStptMc)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("DLRM-STPT (MC)");
            entity.Property(e => e.DlrmTaxExmptCde)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("DLRM-TAX-EXMPT-CDE");
            entity.Property(e => e.DlrmVehCrRating)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("DLRM-VEH-CR-RATING");
            entity.Property(e => e.DlrmVehTermDte)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("DLRM-VEH-TERM-DTE");
            entity.Property(e => e.DlrmVodFlg)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("DLRM-VOD-FLG");
            entity.Property(e => e.DlrmVolGrp)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("DLRM-VOL-GRP");
            entity.Property(e => e.DlrmVorShipVia)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("DLRM-VOR-SHIP-VIA");
            entity.Property(e => e.DlrmWebsite)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DLRM-WEBSITE");
            entity.Property(e => e.DlrmWtyDlrFlg)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("DLRM-WTY-DLR-FLG");
            entity.Property(e => e.DlrmWtyLrEffDte)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("DLRM-WTY-LR-EFF-DTE");
            entity.Property(e => e.DlrmWtyLrRte)
                .HasColumnType("numeric(16, 2)")
                .HasColumnName("DLRM-WTY-LR-RTE");
            entity.Property(e => e.DlrmWtyPrevLrRte)
                .HasColumnType("numeric(16, 2)")
                .HasColumnName("DLRM-WTY-PREV-LR-RTE");
            entity.Property(e => e.DlrxOemAccyLrRte)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DLRX-OEM-ACCY-LR-RTE");
            entity.Property(e => e.DlrxOemMaintLrRte)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DLRX-OEM-MAINT-LR-RTE");
            entity.Property(e => e.DlrxPostedLrEffDte)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DLRX-POSTED-LR-EFF-DTE");
            entity.Property(e => e.DlrxPostedLrRte)
                .HasColumnType("numeric(16, 2)")
                .HasColumnName("DLRX-POSTED-LR-RTE");
            entity.Property(e => e.DlrxPostedPrevLrRte)
                .HasColumnType("numeric(16, 2)")
                .HasColumnName("DLRX-POSTED-PREV-LR-RTE");
            entity.Property(e => e.PdlrDdsLoadSeq)
                .HasMaxLength(9)
                .IsUnicode(false)
                .HasColumnName("PDLR-DDS-LOAD-SEQ");
            entity.Property(e => e.PdlrDlrPrcplEmail)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("PDLR-DLR-PRCPL-EMAIL");
            entity.Property(e => e.PdlrDlrPrcplFax)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("PDLR-DLR-PRCPL-FAX");
            entity.Property(e => e.PdlrDlrPrcplPhn)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("PDLR-DLR-PRCPL-PHN");
            entity.Property(e => e.PdlrDmsSys)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("PDLR-DMS-SYS");
            entity.Property(e => e.PdlrEmailStndrd)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("PDLR-EMAIL-STNDRD");
            entity.Property(e => e.PdlrOldBuySell)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("PDLR-OLD-BUY-SELL");
            entity.Property(e => e.PdlrPrtsMgrEmail)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("PDLR-PRTS-MGR-EMAIL");
            entity.Property(e => e.PdlrPrtsMgrFax)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("PDLR-PRTS-MGR-FAX");
            entity.Property(e => e.PdlrPrtsMgrPhn)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("PDLR-PRTS-MGR-PHN");
            entity.Property(e => e.PdlrToteCage)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("PDLR-TOTE-CAGE");
            entity.Property(e => e.Region)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.ServiceTier)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("SERVICE-TIER");
        });

        modelBuilder.Entity<DealerTimeZone>(entity =>
        {
            entity.HasKey(e => e.DealerTimeZoneId).HasName("DealerTimeZone_PK");

            entity.ToTable("DealerTimeZone");

            entity.Property(e => e.DealerTimeZoneId).HasColumnName("DealerTimeZoneID");
            entity.Property(e => e.DealerCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.TimeZone).HasColumnType("decimal(5, 2)");
        });

        modelBuilder.Entity<DealerTimeZone20nov23>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("DealerTimeZone_20nov23");

            entity.Property(e => e.DealerCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.DealerTimeZoneId)
                .ValueGeneratedOnAdd()
                .HasColumnName("DealerTimeZoneID");
            entity.Property(e => e.TimeZone).HasColumnType("decimal(5, 2)");
        });

        modelBuilder.Entity<DealerView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Dealer_View");

            entity.Property(e => e.Area)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.City)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.DealerCode)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.District)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.Metro)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Prov)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.Region)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.Volumn)
                .HasMaxLength(1)
                .IsUnicode(false);
        });

        modelBuilder.Entity<HistoricalSubmissionReport>(entity =>
        {
            entity.HasKey(e => e.CreatedDateTime)
               .HasName("CreatedDateTime_PK");
                //.HasNoKey()
                entity.ToTable("HistoricalSubmissionReport");

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");
            entity.Property(e => e.DealerCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.DistrictCode)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.RegionCode)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.ReportingWeekId).HasColumnName("ReportingWeekID");
            entity.Property(e => e.SubmissionStatus)
                .HasMaxLength(60)
                .IsUnicode(false);

            entity.HasOne(d => d.ReportingWeek).WithMany()
                .HasForeignKey(d => d.ReportingWeekId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("HistoricalSubmissionReport_ReportingWeekCalendar_FK");
        });

        modelBuilder.Entity<MonthlySalesTarget>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("MonthlySalesTarget");

            entity.Property(e => e.DrtcCarline)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("DRTC-CARLINE");
            entity.Property(e => e.DrtcDealer)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("DRTC-DEALER");
            entity.Property(e => e.DrtcDistrict)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("DRTC-DISTRICT");
            entity.Property(e => e.DrtcQty1).HasColumnName("DRTC-QTY(1)");
            entity.Property(e => e.DrtcQty10).HasColumnName("DRTC-QTY(10)");
            entity.Property(e => e.DrtcQty11).HasColumnName("DRTC-QTY(11)");
            entity.Property(e => e.DrtcQty12).HasColumnName("DRTC-QTY(12)");
            entity.Property(e => e.DrtcQty2).HasColumnName("DRTC-QTY(2)");
            entity.Property(e => e.DrtcQty3).HasColumnName("DRTC-QTY(3)");
            entity.Property(e => e.DrtcQty4).HasColumnName("DRTC-QTY(4)");
            entity.Property(e => e.DrtcQty5).HasColumnName("DRTC-QTY(5)");
            entity.Property(e => e.DrtcQty6).HasColumnName("DRTC-QTY(6)");
            entity.Property(e => e.DrtcQty7).HasColumnName("DRTC-QTY(7)");
            entity.Property(e => e.DrtcQty8).HasColumnName("DRTC-QTY(8)");
            entity.Property(e => e.DrtcQty9).HasColumnName("DRTC-QTY(9)");
            entity.Property(e => e.DrtcTotalQty).HasColumnName("DRTC-TOTAL-QTY");
            entity.Property(e => e.DrtcYear).HasColumnName("DRTC-YEAR");
        });

        modelBuilder.Entity<MonthlySalesTargetView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("MonthlySalesTarget_View");

            entity.Property(e => e.Carline)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("CARLINE");
            entity.Property(e => e.Dealer)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("DEALER");
            entity.Property(e => e.District)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("DISTRICT");
            entity.Property(e => e.Dryear).HasColumnName("DRYEAR");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Monthid)
                .HasMaxLength(128)
                .HasColumnName("MONTHID");
            entity.Property(e => e.Qty).HasColumnName("QTY");
        });

        modelBuilder.Entity<MssqlTemporalHistoryFor805577908>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("MSSQL_TemporalHistoryFor_805577908");

            entity.Property(e => e.CarLineId).HasColumnName("CarLineID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");
            entity.Property(e => e.DealerArea)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.DealerCity)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.DealerCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.DealerDistrict)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.DealerMetro)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.DealerName)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.DealerProvince)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.DealerRegion)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDateTime).HasColumnType("datetime");
            entity.Property(e => e.ReportingWeekId).HasColumnName("ReportingWeekID");
            entity.Property(e => e.TrafficLogId).HasColumnName("TrafficLogID");
            entity.Property(e => e.UserName)
                .HasMaxLength(60)
                .IsUnicode(false);
        });

        modelBuilder.Entity<NcmDataExportCarLineOrder>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("NCM_Data_Export_CarLineOrder");

            entity.Property(e => e.CarLineId).HasColumnName("CarLineID");
        });

        modelBuilder.Entity<OldDealerCode>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("OldDealerCode");

            entity.Property(e => e.OldDealerCode1)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("OldDealerCode");
        });

        modelBuilder.Entity<ReportingWeekCalendar>(entity =>
        {
            entity.HasKey(e => e.ReportingWeekId).HasName("ReportingWeekCalendar_PK");

            entity.ToTable("ReportingWeekCalendar");

            entity.Property(e => e.ReportingWeekId).HasColumnName("ReportingWeekID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDatetime).HasColumnType("datetime");
            entity.Property(e => e.ExcelFileNameLegacyProcess)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDateTime).HasColumnType("datetime");
            entity.Property(e => e.ReportingWeekDescriptionEn)
                .HasMaxLength(250)
                .HasColumnName("ReportingWeekDescriptionEN");
            entity.Property(e => e.ReportingWeekDescriptionFr)
                .HasMaxLength(250)
                .HasColumnName("ReportingWeekDescriptionFR");
            entity.Property(e => e.ReportingWeekEndDate).HasColumnType("date");
            entity.Property(e => e.ReportingWeekStartDate).HasColumnType("date");
            entity.Property(e => e.SubmissionWindowDescriptionEn)
                .HasMaxLength(250)
                .HasColumnName("SubmissionWindowDescriptionEN");
            entity.Property(e => e.SubmissionWindowDescriptionFr)
                .HasMaxLength(250)
                .HasColumnName("SubmissionWindowDescriptionFR");
            entity.Property(e => e.SubmissionWindowEndDate).HasColumnType("datetime");
            entity.Property(e => e.SubmissionWindowFirstDeadline).HasColumnType("datetime");
            entity.Property(e => e.SubmissionWindowStartDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<ReportingWeekCalendarBfUpdt>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ReportingWeekCalendar_BfUpdt");

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDatetime).HasColumnType("datetime");
            entity.Property(e => e.ExcelFileNameLegacyProcess)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDateTime).HasColumnType("datetime");
            entity.Property(e => e.ReportingWeekDescriptionEn)
                .HasMaxLength(250)
                .HasColumnName("ReportingWeekDescriptionEN");
            entity.Property(e => e.ReportingWeekDescriptionFr)
                .HasMaxLength(250)
                .HasColumnName("ReportingWeekDescriptionFR");
            entity.Property(e => e.ReportingWeekEndDate).HasColumnType("date");
            entity.Property(e => e.ReportingWeekId)
                .ValueGeneratedOnAdd()
                .HasColumnName("ReportingWeekID");
            entity.Property(e => e.ReportingWeekStartDate).HasColumnType("date");
            entity.Property(e => e.SubmissionWindowDescriptionEn)
                .HasMaxLength(250)
                .HasColumnName("SubmissionWindowDescriptionEN");
            entity.Property(e => e.SubmissionWindowDescriptionFr)
                .HasMaxLength(250)
                .HasColumnName("SubmissionWindowDescriptionFR");
            entity.Property(e => e.SubmissionWindowEndDate).HasColumnType("datetime");
            entity.Property(e => e.SubmissionWindowFirstDeadline).HasColumnType("datetime");
            entity.Property(e => e.SubmissionWindowStartDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<ReportingWeekCalenderView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ReportingWeekCalender_View");

            entity.Property(e => e.ReportingWeekDescriptionEn)
                .HasMaxLength(250)
                .HasColumnName("ReportingWeekDescriptionEN");
            entity.Property(e => e.ReportingWeekDescriptionFr)
                .HasMaxLength(250)
                .HasColumnName("ReportingWeekDescriptionFR");
            entity.Property(e => e.ReportingWeekEndDate).HasColumnType("date");
            entity.Property(e => e.ReportingWeekId)
                .ValueGeneratedOnAdd()
                .HasColumnName("ReportingWeekID");
            entity.Property(e => e.ReportingWeekStartDate).HasColumnType("date");
            entity.Property(e => e.SubmissionWindowDescriptionEn)
                .HasMaxLength(250)
                .HasColumnName("SubmissionWindowDescriptionEN");
            entity.Property(e => e.SubmissionWindowDescriptionFr)
                .HasMaxLength(250)
                .HasColumnName("SubmissionWindowDescriptionFR");
            entity.Property(e => e.SubmissionWindowEndDate).HasColumnType("datetime");
            entity.Property(e => e.SubmissionWindowFirstDeadline).HasColumnType("datetime");
            entity.Property(e => e.SubmissionWindowStartDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Reportingweek>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("reportingweek$");

            entity.Property(e => e.F12).HasMaxLength(255);
            entity.Property(e => e.F7).HasMaxLength(255);
            entity.Property(e => e.ReportinEndDate)
                .HasColumnType("datetime")
                .HasColumnName("Reportin End Date");
            entity.Property(e => e.ReportinStartDate)
                .HasColumnType("datetime")
                .HasColumnName("Reportin Start Date");
            entity.Property(e => e.SubmissionWindowDescriptionEnCurrent)
                .HasMaxLength(255)
                .HasColumnName("SubmissionWindowDescriptionEN - Current");
            entity.Property(e => e.SubmissionWindowDescriptionEnUpdated)
                .HasMaxLength(255)
                .HasColumnName("SubmissionWindowDescriptionEN - Updated");
            entity.Property(e => e.SubmissionWindowDescriptionEnUpdatedFrw)
                .HasMaxLength(255)
                .HasColumnName("SubmissionWindowDescriptionEN - Updated FRW");
            entity.Property(e => e.SubmissionWindowDescriptionFrCurrent)
                .HasMaxLength(255)
                .HasColumnName("SubmissionWindowDescriptionFR - Current");
            entity.Property(e => e.SubmissionWindowDescriptionFrUpdated)
                .HasMaxLength(255)
                .HasColumnName("SubmissionWindowDescriptionFR - Updated");
            entity.Property(e => e.SubmissionWindowDescriptionFrUpdatedFrw)
                .HasMaxLength(255)
                .HasColumnName("SubmissionWindowDescriptionFR - Updated FRW");
            entity.Property(e => e.SubmissionWindowEndDateRevised2021)
                .HasMaxLength(255)
                .HasColumnName("SubmissionWindowEndDate  (REVISED 2021)");
            entity.Property(e => e.SubmissionWindowFirstDeadlineRevised2021)
                .HasMaxLength(255)
                .HasColumnName("SubmissionWindowFirstDeadline (REVISED 2021)");
            entity.Property(e => e.SubmissionWindowStartDateRevised2021)
                .HasMaxLength(255)
                .HasColumnName("SubmissionWindowStartDate  (REVISED 2021)");
            entity.Property(e => e.WeekDescriptionEnFrw)
                .HasMaxLength(255)
                .HasColumnName("Week Description EN FRW");
            entity.Property(e => e.WeekDescriptionEnFrwRevised2020)
                .HasMaxLength(255)
                .HasColumnName("Week Description EN FRW (REVISED 2020)");
            entity.Property(e => e.WeekDescriptionEnFrwRevised2021)
                .HasMaxLength(255)
                .HasColumnName("Week Description EN FRW (REVISED 2021)");
            entity.Property(e => e.WeekDescriptionFrFrw)
                .HasMaxLength(255)
                .HasColumnName("Week Description FR FRW");
            entity.Property(e => e.WeekDescriptionFrFrwRevised2020)
                .HasMaxLength(255)
                .HasColumnName("Week Description FR FRW (REVISED 2020)");
            entity.Property(e => e.WeekDescriptionFrFrwRevised2021)
                .HasMaxLength(255)
                .HasColumnName("Week Description FR FRW (REVISED 2021)");
        });

        modelBuilder.Entity<SubmissionReportLoadError>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("SubmissionReportLoadError");

            entity.Property(e => e.ReportingWeekId).HasColumnName("ReportingWeekID");
            entity.Property(e => e.SubmissionStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TempHistDataLoad1>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tempHistDataLoad1");

            entity.Property(e => e.ReportingWeekId).HasColumnName("ReportingWeekID");
            entity.Property(e => e.SubmissionStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TempHistDataLoad2>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tempHistDataLoad2");

            entity.Property(e => e.F1).HasMaxLength(255);
            entity.Property(e => e.F2).HasMaxLength(255);
            entity.Property(e => e.F3).HasMaxLength(255);
            entity.Property(e => e.ReportingWeekId).HasColumnName("ReportingWeekID");
            entity.Property(e => e.SubmissionStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TempHistDataLoad3>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tempHistDataLoad3");

            entity.Property(e => e.ReportingWeekId).HasColumnName("ReportingWeekID");
            entity.Property(e => e.SubmissionStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TrafficLog>(entity =>
        {
            entity.HasKey(e => e.TrafficLogId).HasName("Trafficlog_PK");

            entity.ToTable("TrafficLog");

            entity.Property(e => e.TrafficLogId).HasColumnName("TrafficLogID");
            entity.Property(e => e.CarLineId).HasColumnName("CarLineID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");
            entity.Property(e => e.DealerArea)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.DealerCity)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.DealerCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.DealerDistrict)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.DealerMetro)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.DealerName)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.DealerProvince)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.DealerRegion)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDateTime).HasColumnType("datetime");
            entity.Property(e => e.ReportingWeekId).HasColumnName("ReportingWeekID");
            entity.Property(e => e.UserName)
                .HasMaxLength(60)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TrafficLog19jun23>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("TrafficLog_19jun23");

            entity.Property(e => e.CarLineId).HasColumnName("CarLineID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");
            entity.Property(e => e.DealerArea)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.DealerCity)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.DealerCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.DealerDistrict)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.DealerMetro)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.DealerName)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.DealerProvince)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.DealerRegion)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDateTime).HasColumnType("datetime");
            entity.Property(e => e.ReportingWeekId).HasColumnName("ReportingWeekID");
            entity.Property(e => e.TrafficLogId)
                .ValueGeneratedOnAdd()
                .HasColumnName("TrafficLogID");
            entity.Property(e => e.UserName)
                .HasMaxLength(60)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TrafficLogAdd>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("trafficLog_Add");

            entity.Property(e => e.CarLineId).HasColumnName("CarLineID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");
            entity.Property(e => e.DealerArea)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.DealerCity)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.DealerCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.DealerDistrict)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.DealerMetro)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.DealerName)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.DealerProvince)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.DealerRegion)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDateTime).HasColumnType("datetime");
            entity.Property(e => e.ReportingWeekId).HasColumnName("ReportingWeekID");
            entity.Property(e => e.TrafficLogId)
                .ValueGeneratedOnAdd()
                .HasColumnName("TrafficLogID");
            entity.Property(e => e.UserName)
                .HasMaxLength(60)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TrafficLogCx90PreSplitView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("TrafficLogCX90_PreSplit_View");

            entity.Property(e => e.Carline).HasColumnName("carline");
            entity.Property(e => e.DealerCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.MonthlySalesForecast).HasColumnType("numeric(36, 1)");
            entity.Property(e => e.MonthlyTarget).HasColumnType("numeric(36, 1)");
            entity.Property(e => e.ReportingWeekId).HasColumnName("ReportingWeekID");
            entity.Property(e => e.Traffic).HasColumnType("numeric(36, 1)");
            entity.Property(e => e.Writes).HasColumnType("numeric(36, 1)");
        });

        modelBuilder.Entity<TrafficLogHistory>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("TrafficLogHistory");

            entity.Property(e => e.CarLineId).HasColumnName("CarLineID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");
            entity.Property(e => e.DealerArea)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.DealerCity)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.DealerCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.DealerDistrict)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.DealerMetro)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.DealerName)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.DealerProvince)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.DealerRegion)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDateTime).HasColumnType("datetime");
            entity.Property(e => e.ReportingWeekId).HasColumnName("ReportingWeekID");
            entity.Property(e => e.TrafficLogId).HasColumnName("TrafficLogID");
            entity.Property(e => e.UserName)
                .HasMaxLength(60)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TrafficeLogCsvsheet>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("TrafficeLogCSVSheet");

            entity.Property(e => e.Month).HasMaxLength(50);
            entity.Property(e => e.SubmissinFirstDeadLine).HasColumnName("Submissin_First_DeadLine");
            entity.Property(e => e.SubmissionEndDate).HasColumnName("Submission_End_Date");
            entity.Property(e => e.SubmissionStateDate).HasColumnName("Submission_State_Date");
            entity.Property(e => e.TrafficEndDate).HasColumnName("Traffic_End_Date");
            entity.Property(e => e.TrafficStartDate).HasColumnName("Traffic_Start_Date");
            entity.Property(e => e.WeekNumber)
                .HasMaxLength(50)
                .HasColumnName("Week_Number");
        });

        modelBuilder.Entity<TrafficlogHistory1>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("trafficlogHistory1");

            entity.Property(e => e.CarLineId).HasColumnName("CarLineID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");
            entity.Property(e => e.DealerArea)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.DealerCity)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.DealerCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.DealerDistrict)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.DealerMetro)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.DealerName)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.DealerProvince)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.DealerRegion)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDateTime).HasColumnType("datetime");
            entity.Property(e => e.ReportingWeekId).HasColumnName("ReportingWeekID");
            entity.Property(e => e.TrafficLogId).HasColumnName("TrafficLogID");
            entity.Property(e => e.UserName)
                .HasMaxLength(60)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SubmissionReport_WOW>(entity =>
        {
            entity
                .HasNoKey();
        });
        modelBuilder.Entity<DetailReport_MTD>(entity =>
        {
            entity
                .HasNoKey();
        });
        modelBuilder.Entity<DetailReport_MTDTrend>(entity =>
        {
            entity
                .HasNoKey();
        });
        modelBuilder.Entity<DetailReport_WOW>(entity =>
        {
            entity
                .HasNoKey();
        });
        modelBuilder.Entity<DetailReport_MOM>(entity =>
        {
            entity
                .HasNoKey();
        });
        modelBuilder.Entity<SummaryReport>(entity =>
        {
            entity
                .HasNoKey();
                //.ToTable("ReportingWeekCalendar");
        });
        modelBuilder.Entity<SummaryReport_MTD>(entity =>
        {
            entity
                .HasNoKey();
        });
        modelBuilder.Entity<SummaryReport_WOW>(entity =>
        {
            entity
                .HasNoKey();
        });
        modelBuilder.Entity<SummaryReport_MOM>(entity =>
        {
            entity
                .HasNoKey();
        });
        modelBuilder.Entity<SummaryReport_MTDTrend>(entity =>
        {
            entity
                .HasNoKey();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
