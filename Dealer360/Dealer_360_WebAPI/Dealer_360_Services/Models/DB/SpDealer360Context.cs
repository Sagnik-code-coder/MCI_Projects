using System;
using System.Collections.Generic;
using Dealer_360_Services.Models.View_Models;
using Microsoft.EntityFrameworkCore;

namespace Dealer_360_Services.Models.DB;

public partial class SpDealer360Context : DbContext
{
    public SpDealer360Context()
    {
    }

    public SpDealer360Context(DbContextOptions<SpDealer360Context> options)
        : base(options)
    {
    }

    public virtual DbSet<AwardMaster> AwardMasters { get; set; }

    public virtual DbSet<Dealer> Dealers { get; set; }

    public virtual DbSet<DealerAward> DealerAwards { get; set; }

    public virtual DbSet<Deficiency> Deficiencies { get; set; }

    public virtual DbSet<Dssadatum> Dssadata { get; set; }

    public virtual DbSet<ExternalManufacturer> ExternalManufacturers { get; set; }

    public virtual DbSet<FacilityStandard> FacilityStandards { get; set; }

    public virtual DbSet<OtherDealership> OtherDealerships { get; set; }

    public virtual DbSet<OwnerDetail> OwnerDetails { get; set; }

    public virtual DbSet<StaffingStandard> StaffingStandards { get; set; }
    //Add property for Store procedure
    public virtual DbSet<GetDealerAwardsDetails> GetDealerAwardsDetail { get; set; }

    public virtual DbSet<GetFacilityStandardVM> GetFacilityStandards { get; set; }
    public virtual DbSet<GetStaffingStandardVM> GetStaffingStandards { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AwardMaster>(entity =>
        {
            entity.HasKey(e => e.AwardId);

            entity.ToTable("AwardMaster");

            entity.Property(e => e.AwardId).HasColumnName("AwardID");
            entity.Property(e => e.AwardName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
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

        modelBuilder.Entity<DealerAward>(entity =>
        {
            entity.HasKey(e => e.DealerAwardsId);

            entity.Property(e => e.DealerAwardsId).HasColumnName("DealerAwardsID");
            entity.Property(e => e.AwardId).HasColumnName("AwardID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

            entity.HasOne(d => d.Award).WithMany(p => p.DealerAwards)
                .HasForeignKey(d => d.AwardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DealerAwards_Award_3");
        });

        modelBuilder.Entity<Deficiency>(entity =>
        {
            entity.Property(e => e.DeficiencyId).HasColumnName("DeficiencyID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Deficiency1)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Deficiency");
            entity.Property(e => e.DeficiencyCommitment)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.DfcncyCrrDueDate).HasColumnType("datetime");
            entity.Property(e => e.Dssaid).HasColumnName("DSSAID");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

            entity.HasOne(d => d.Dssa).WithMany(p => p.Deficiencies)
                .HasForeignKey(d => d.Dssaid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Deficiencies_DSSA_3");
        });

        modelBuilder.Entity<Dssadatum>(entity =>
        {
            entity.HasKey(e => e.Dssaid);

            entity.ToTable("DSSAData");

            entity.Property(e => e.Dssaid).HasColumnName("DSSAID");
            entity.Property(e => e.AdditionalProvisions)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.CurrentAgreementTerm).HasColumnType("datetime");
            entity.Property(e => e.ImportantCommnets)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.MarketType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            entity.Property(e => e.OffSiteLocation)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.OffSiteLocationUse)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Representation)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ExternalManufacturer>(entity =>
        {
            entity.HasKey(e => e.ManufacturerId);

            entity.ToTable("ExternalManufacturer");

            entity.Property(e => e.ManufacturerId).HasColumnName("ManufacturerID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.ManufacturerName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<FacilityStandard>(entity =>
        {
            entity.ToTable("FacilityStandard");

            entity.Property(e => e.FacilityStandardId).HasColumnName("FacilityStandardID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.MinAcreage).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<OtherDealership>(entity =>
        {
            entity.ToTable("OtherDealership");

            entity.Property(e => e.OtherDealerShipId).HasColumnName("OtherDealerShipID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            entity.Property(e => e.OtherManufacturerId).HasColumnName("OtherManufacturerID");

            entity.HasOne(d => d.OtherManufacturer).WithMany(p => p.OtherDealerships)
                .HasForeignKey(d => d.OtherManufacturerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OtherDealership_OtherManufact_3");
        });

        modelBuilder.Entity<OwnerDetail>(entity =>
        {
            entity.HasKey(e => e.OwnerId);

            entity.Property(e => e.OwnerId).HasColumnName("OwnerID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Dssaid).HasColumnName("DSSAID");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PercentOwned).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Title)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.Dssa).WithMany(p => p.OwnerDetails)
                .HasForeignKey(d => d.Dssaid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OwnerDetails_DSSA_3");
        });

        modelBuilder.Entity<StaffingStandard>(entity =>
        {
            entity.HasKey(e => e.StaffingStandardId).HasName("PK_StaffigStandard");

            entity.ToTable("StaffingStandard");

            entity.Property(e => e.StaffingStandardId).HasColumnName("StaffingStandardID");
            entity.Property(e => e.Accountant)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Apprentices)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ClericalStaff)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.CustomRelCood)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.FinServiceManager)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.FleetLeasingManager)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.GeneralManagerOwner)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.JobClassification)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            entity.Property(e => e.OfficeStaff)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Other)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.PartsAdvisor)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.PartsClerk)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.PartsManager)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.PreownedManager)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.SalerManager)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.SalesRepresentative)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ServiceClericalStaff)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ServiceManager)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ServicedAdvisor)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ShopForeperson)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Technicians)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
