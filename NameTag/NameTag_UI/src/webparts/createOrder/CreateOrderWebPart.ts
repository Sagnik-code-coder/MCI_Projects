import * as React from 'react';
import * as ReactDom from 'react-dom';
import { Version } from '@microsoft/sp-core-library';
import {
  type IPropertyPaneConfiguration,
  PropertyPaneTextField
} from '@microsoft/sp-property-pane';
import { BaseClientSideWebPart } from '@microsoft/sp-webpart-base';
import { IReadonlyTheme } from '@microsoft/sp-component-base';
import { SPComponentLoader } from '@microsoft/sp-loader';

import * as strings from 'CreateOrderWebPartStrings';
import CreateOrder from './components/CreateOrder';
import { ICreateOrderProps } from './components/ICreateOrderProps';

export interface ICreateOrderWebPartProps {
  description: string;
}

export default class CreateOrderWebPart extends BaseClientSideWebPart<ICreateOrderWebPartProps> {

  private _isDarkTheme: boolean = false;
  private _environmentMessage: string = '';

  public render(): void {
    const element: React.ReactElement<ICreateOrderProps> = React.createElement(
      CreateOrder,
      {
        description: this.properties.description,
        isDarkTheme: this._isDarkTheme,
        environmentMessage: this._environmentMessage,
        hasTeamsContext: !!this.context.sdks.microsoftTeams,
        userDisplayName: this.context.pageContext.user.displayName
      }
    );

    ReactDom.render(element, this.domElement);
    this.domElement.innerHTML = `<div class="body-content">
    <div class="container-fluid">
        <div id="divInput" class="tabcontent">
            <div class="row">
                <div id="divHeader">
                    <label id="mainScreenHeaderTitle" for="mainScreenHeaderTitle" tkey="mainScreenHeaderTitle">Create NameTag Order</label>
                </div>
            </div>
            <div class="row" id="bulletPointsEn">
                <ul style="list-style-type:disc">
                    <li><span class="bulletPoint">•</span><span id="bullet1" tkey="bullet1">All manager-level Mazda retail employees can order name tags for their teams.</span></li>
                    <li><span class="bulletPoint">•</span><span id="bullet2" tkey="bullet2">Name tags can be ordered for anyone that has a valid One.Mazda/MDrive(?) profile.</span></li>
                    <li><span class="bulletPoint">•</span><span id="bullet3" tkey="bullet3"><b>SEARCH and SELECT employee(s), review your order, and click SUBMIT ORDER to proceed.</b></span></li>
                </ul>
            </div>
            <div class="row" id="divSearch">
                <div id="dvSearchLbl" class="form-group col-sm-3">
                    <label for="lblSearchEmployee" tkey="lblSearchEmployee" id="lblSearchEmployee">Search Employee</label>&nbsp;<span class="requiredField">*</span>
                    <input type="text" class="form-control" id="txtEmployeeName">
                </div>
                <div class="form-group col-sm-3">
                    
                </div>
                <!--<div class="form-group col-sm-3" id="divModel">
            <label id="lblSearch" for="pageSize" tkey="lblSearch">Search by name or job title</label>
        </div>
        <div class="form-group col-sm-3" id="divEngine">
            <input type="text" class="form-control" id="txtSearch" placeholder="Search">
        </div>-->
            </div>                
        </div>
        <!--<div class="tab" id="tabsDashboard">
        </div>-->
        <div id="jsGridEmployee" class="jsgrid">
            <!--<div class="jsgrid-pager-container" style="display: none;"></div><div class="jsgrid-load-shader"></div><div class="jsgrid-load-panel" style="display: none; position: absolute; top: 50%; left: 50%; z-index: 1000;">Please, wait...</div>-->
        </div>            
        <div id="externalPager">                
        </div>
        <div id="divPagesCrOrd">
            <div style="display:flex">
                <label id="pageSize" for="pageSize" tkey="pageSize">Records per pages</label>&nbsp;

                <select class="jsgrid-pager-nav-button" id="pagesCrtOrd">
                    <option>5</option>
                    <option selected>10</option>
                    <option>15</option>
                    <option>20</option>
                    <option>25</option>
                    <option>30</option>
                </select>
            </div>
        </div>
        <!--<div id="divTotalItems">
            <label id="totalItems" for="totalItems" tkey="totalItems">Total Records: </label><label id="totalItemsCount" for="totalItemsCount" tkey="totalItemsCount">{t}</label>
        </div>-->
        <div id="CustomPager">
            <div class="jsgrid-pager">

                <!--<label id="pageNumber" for="pageNumber" tkey="pageNumber">Showing: {f} to {t}</label>-->
                <!--<select class="jsgrid-pager-nav-button " id="pages">
            <option>5</option>
            <option>10</option>
            <option>15</option>
            <option>20</option>
        </select>-->
                <!--<span id="pageFirst" class="jsgrid-pager-nav-button "><a href="javascript:void(0);" tkey="first">First</a></span>
                <span id="pagePrev" class="jsgrid-pager-nav-button "><a href="javascript:void(0);" tkey="prev">Prev</a></span>
                <span id="pageNext" class="jsgrid-pager-nav-button"><a href="javascript:void(0);" tkey="next">Next</a></span>
                <span id="pageLast" class="jsgrid-pager-nav-button"><a href="javascript:void(0);" tkey="last">Last</a></span>-->
            </div>
        </div>
        <br />
        <br />
        <!--<div class="row">-->
        <div class="form-group col-sm-6" id="divLblMySelection"><label id="lblMySelection" for="lblMySelection" tkey="lblMySelection">Selected Employees for NameTag</label></div><br/><br />
        <div class="row" id="divInstruction">
            <ul style="list-style-type:disc">
                <li><span class="bulletPoint">1</span><span id="instruction1" tkey="instruction1">Click pencil icon on each employee name to fill in their details.</span></li>
                <li><span class="bulletPoint">2</span><span id="instruction2" tkey="instruction2">Click the green check to save.</span></li>
                <li><span class="bulletPoint">3</span><span id="instruction3" tkey="instruction3"><b>Click delete icon to remove selected rows.</b></span></li>
            </ul>
        </div>
        <!--</div>-->
            <div id="jsGridSelectedEmp" class="jsgrid">
                
            </div>
        <br />
        
            <div id="jsGrid" class="jsgrid">
                <div class="jsgrid-grid-header jsgrid-header-scrollbar">
                    <table class="jsgrid-table">
                        <thead>
                            <tr class="jsgrid-header-row">
                                <!--<th class="jsgrid-header-cell jsgrid-header-sortable jsgrid-header-sort jsgrid-header-sort-desc" style="width: 10%;">RowID</th>-->
                                <th class="jsgrid-header-cell" tkey="grdQty">Qty</th>
                                <th class="jsgrid-header-cell" tkey="grdCost">Cost</th>
                                <!--<th class="jsgrid-header-cell" tkey="jsGridTax">Tax</th>-->
                                <th class="jsgrid-header-cell" tkey="grdTotalCost">Total Cost</th>
                            </tr>
                        </thead>
                    </table>
                </div>
                <div class="jsgrid-grid-body">
                    <table id="tblTotalTax" class="jsgrid-table">
                        <tbody>
                            <tr class="jsgrid-row">
                                <td class="jsgrid-cell"><label for="lblQty" id="lblQty">0.00</label></td>
                                <td class="jsgrid-cell"><label for="lblCost" id="lblCost">0.00</label></td>
                                <!--<td class="jsgrid-cell"><label for="lblTax" tkey="lblTax" id="lblTax">0.00</label></td>-->
                                <td class="jsgrid-cell"><label for="lblTotalCost" id="lblTotalCost">0.00</label></td>
                            </tr>
                        </tbody>
                    </table>
                </div><div class="jsgrid-pager-container" style="display: none;"></div><div class="jsgrid-load-shader"></div><div class="jsgrid-load-panel" style="display: none; position: absolute; top: 50%; left: 50%; z-index: 1000;">Please, wait...</div>
            </div>
            <br />
            <div class="row">
                <div class="form-group col-sm-12">
                    <label for="orderDescr" tkey="orderDescr" id="orderDescr">Order Description:</label>&nbsp;<span class="requiredField">*</span>
                    <textarea type="text" class="form-control" id="txtOrderDescr" maxlength="1200" required></textarea>
                </div>
            </div>
            <!--<div class="row">
        <div class="form-group col-sm-3">
            <label for="confirmEmail" tkey="confirmEmail" id="confirmEmail">Send confirmation email to:</label>&nbsp;<span class="requiredField">*</span>
        </div>
        <div class="form-group col-sm-3">
            <input type="text" class="form-control" id="txtConfirmEmail" placeholder="xyz@mazda.ca">
        </div>
    </div>-->
            <br />
            <div class="row">
                <div class="form-group col-sm-6"><b id="lblWarningEn" tkey="lblWarning">Health &amp; Safety Warning: </b><span id="spHealthInst" tkey="spHealthInst"> Individuals with a pacemaker should refrain from the use of magnetic devices including a magnetic name tag.</span></div>
                <div class="colWidth-footer-btn" id="divSubmitOrder">
                    <button id="btnSubmitOrder" type="button" tkey="btnSubmitOrder" class="btn btn-dark primary_btn">Submit Order</button>
                    <button id="btnUpdateOrder" type="button" tkey="btnUpdateOrder" class="btn btn-dark primary_btn">Update Order</button>
                    <button id="btnDeleteOrder" type="button" tkey="btnDeleteOrder" class="btn btn-dark primary_btn">Delete Order</button>
                </div>
            </div>

            <div id="modalConfirmOrder" class="modal">
                <div id="modalConfirmOrdCont" class="modal-content">
                    <h4 id="confirmSubmitMsg" tkey="confirmSubmitMsg">Are you sure you want to submit this order? You will be charged accordingly on your monthly dealer statement.</h4><br><br>
                    <button id="btnConfirmSubmitNo" type="button" tkey="btnConfirmSubmitNo" class="btn btn-dark primary_btn">No</button>
                    <button id="btnConfirmSubmitYes" type="button" tkey="btnConfirmSubmitYes" class="btn btn-dark primary_btn">Yes</button>
                </div>
            </div>
            <div id="modalNameTagOrdValidation" class="modal">
                <div id="modalNameTagOrdValidationCont" class="modal-content">
                    <h4 id="ordValidationMsg" tkey="ordValidationMsg">Order for the selected employee is already added!</h4><br><br>
                    <button id="btnOrdValidationOk" type="button" tkey="btnOrdValidationOk" class="btn btn-dark primary_btn">Ok</button>
                </div>
            </div>
            <div id="modalNameTagSuccessMsg" class="modal">
                <div id="modalNameTagSuccessMsgCont" class="modal-content">
                    <h4 id="nameTagSuccessMsg" tkey="nameTagSuccessMsg">Order for NameTag has been submitted successfully!</h4><br><br>
                    <button id="btnNameTagSuccessMsgOk" type="button" tkey="btnOrdValidationOk" class="btn btn-dark primary_btn">Ok</button>
                </div>
            </div>
            <div id="modalNameTagUpdSuccessMsg" class="modal">
                <div id="modalNameTagUpdSuccessMsgCont" class="modal-content">
                    <h4 id="nameTagUpdSuccessMsg" tkey="nameTagUpdSuccessMsg">Order for NameTag has been updated successfully!</h4><br><br>
                    <button id="btnNameTagUpdSuccessMsgOk" type="button" tkey="btnOrdValidationOk" class="btn btn-dark primary_btn">Ok</button>
                </div>
            </div>
            <div id="modalNameTagOrdValidation2" class="modal">
                <div class="modal-content" id="modalNameTagOrdValidationCont2">
                    <h4 id="orderInfo" tkey="orderInfo">This order is already processed/confirmed, please select another employee.</h4>
                    <button id="btnOrdValid2Close" type="button" tkey="updateinfoClose" class="btn btn-dark primary_btn">Close</button>
                </div>

            </div>
            <div id="modalRemoveSelectionConfirm" class="modal">
                <div class="modal-content" id="modalRemvConfirmCont">
                    <h4 id="orderInfo" tkey="orderInfo">Are you sure, you want to remove from selection?</h4>
                    <button id="btnRemvConfirmNo" type="button" tkey="btnConfirmSubmitNo" class="btn btn-dark primary_btn">No</button>
                    <button id="btnRemvConfirmYes" type="button" tkey="btnConfirmSubmitYes" class="btn btn-dark primary_btn">Yes</button>
                </div>
            </div>
            <div id="modalRemoveOrderConfirm" class="modal">
                <div class="modal-content" id="modalRemvOrdConfirmCont">
                    <h4 id="orderInfo" tkey="orderInfo">Are you sure, you want to remove the order?</h4>
                    <button id="btnRemvOrdConfirmNo" type="button" tkey="btnConfirmSubmitNo" class="btn btn-dark primary_btn">No</button>
                    <button id="btnRemvOrdConfirmYes" type="button" tkey="btnConfirmSubmitYes" class="btn btn-dark primary_btn">Yes</button>
                </div>
            </div>            
            <div id="modalNameTagDeleteSuccessMsg" class="modal">
                <div id="modalNameTagDeleteSuccessMsgCont" class="modal-content">
                    <h4 id="nameTagDeleteSuccessMsg" tkey="nameTagDelSuccessMsg">NameTag order has been deleted successfully!</h4><br><br>
                    <button id="nameTagDeleteSuccessMsgOk" type="button" tkey="btnOrdValidationOk" class="btn btn-dark primary_btn">Ok</button>
                </div>
            </div>
            <div id="modalNameTagDelValidMsg" class="modal">
                <div id="modalNameTagDelValidMsgCont" class="modal-content">
                    <h4 id="modalNameTagDelValidMsg" tkey="modalNameTagDelValidMsg">Order Cannot be blank!</h4><br><br>
                    <button id="modalNameTagDelValidMsgOk" type="button" tkey="btnOrdValidationOk" class="btn btn-dark primary_btn">Ok</button>
                </div>
            </div>
            <div id="modalNameTagCheckUpdMsg" class="modal">
                <div id="modalNameTagCheckUpdMsgCont" class="modal-content">
                    <h4 id="modalNameTagCheckUpdMessage" tkey="modalNameTagCheckUpdMessage">Please provide Nametag Display Name and Quantity to proceed.</h4><br><br>
                    <button id="modalNameTagCheckUpdMsgOk" type="button" tkey="btnOrdValidationOk" class="btn btn-dark primary_btn">Ok</button>
                </div>
            </div>
            <div id="modalNameTagValidSelection" class="modal">
                <div id="modalNameTagValidSelectionCont" class="modal-content">
                    <h4 id="modalNameTagValidSelectionMsg" tkey="modalNameTagCheckUpdMessage">Please provide Nametag Display Name and Quantity to proceed.</h4><br><br>
                    <button id="modalNameTagValidSelectionOk" type="button" tkey="btnOrdValidationOk" class="btn btn-dark primary_btn">Ok</button>
                </div>
            </div>
            <div id="modalUserTypeValidMsg" class="modal">
                <div id="modalUserTypeValidMsgCont" class="modal-content">
                    <h4 id="lblUserTypeValidMsg" tkey="lblUserTypeValidMsg">MCI user cannot apply NameTag for Dealer!</h4><br><br>
                    <button id="btnUserTypeValidMsgOk" type="button" tkey="btnOrdValidationOk" class="btn btn-dark primary_btn">Ok</button>
                </div>
            </div>
        </div>                

  
</div>`;
  }
  

  protected onInit(): Promise<void> {
    SPComponentLoader.loadScript("https://mazdausa.sharepoint.com/sites/MCISPOTestSite/SiteAssets/NameTag/js/jquery.js",{globalExportsName:'jquery'}).then(()=>{      
    //SPComponentLoader.loadScript("https://mazdausa.sharepoint.com/sites/MCISPOTestSite/SiteAssets/NameTag/js/jquery-ui.js",{globalExportsName:'jquery'}).then(()=>{ 
      SPComponentLoader.loadScript('https://mazdausa.sharepoint.com/sites/MCISPOTestSite/SiteAssets/NameTag/js/jquery-ui.js');     
      SPComponentLoader.loadScript('https://mazdausa.sharepoint.com/sites/MCISPOTestSite/SiteAssets/NameTag/js/lang.js');
      ////SPComponentLoader.loadScript('https://mazdausa.sharepoint.com/sites/MCISPOTestSite/SiteAssets/NameTag/js/jquery-ui.js');
      ////SPComponentLoader.loadScript("https://mazdausa.sharepoint.com/sites/MCISPOTestSite/SiteAssets/NameTag/js/jquery-ui.js",{globalExportsName:'jqueryUI'}).then(()=>{        
        //SPComponentLoader.loadScript('https://mazdausa.sharepoint.com/sites/MCISPOTestSite/SiteAssets/NameTag/js/PeoplePickerOnline.js');
        //SPComponentLoader.loadScript('https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.18.1/moment.min.js');
      //});
      SPComponentLoader.loadScript('https://mazdausa.sharepoint.com/sites/MCISPOTestSite/SiteAssets/NameTag/js/jquery.datetimepicker.js');      
      SPComponentLoader.loadScript('https://mazdausa.sharepoint.com/sites/MCISPOTestSite/SiteAssets/NameTag/js/bootstrap.min.js');
      SPComponentLoader.loadScript('https://mazdausa.sharepoint.com/sites/MCISPOTestSite/SiteAssets/NameTag/js/jquery.validate.min.js');
      //SPComponentLoader.loadScript('https://mazdausa.sharepoint.com/sites/MCISPOTestSite/SiteAssets/NameTag/js/jquery.validate.unobtrusive.min.js');
      SPComponentLoader.loadScript('https://mazdausa.sharepoint.com/sites/MCISPOTestSite/SiteAssets/NameTag/js/jsgrid.min.js',{globalExportsName:'jsGrid'}).then(()=>{
      SPComponentLoader.loadScript('https://mazdausa.sharepoint.com/sites/MCISPOTestSite/SiteAssets/NameTag/js/Loader.js');
      SPComponentLoader.loadScript('https://mazdausa.sharepoint.com/sites/MCISPOTestSite/SiteAssets/NameTag/js/PeoplePickerOnline.js');
      SPComponentLoader.loadScript('https://mazdausa.sharepoint.com/sites/MCISPOTestSite/SiteAssets/NameTag/js/Utility.js',{globalExportsName:'CommonUtility'}).then(()=>{ 
      //SPComponentLoader.loadScript('https://mazdausa.sharepoint.com/sites/MCISPOTestSite/SiteAssets/NameTag/js/Utility.js');
      SPComponentLoader.loadScript('https://mazdausa.sharepoint.com/sites/MCISPOTestSite/SiteAssets/NameTag/js/CreateOrder.js');                  
    });
  });
 });

 SPComponentLoader.loadCss("https://mazdausa.sharepoint.com/sites/MCISPOTestSite/SiteAssets/NameTag/css/style.css");
    SPComponentLoader.loadCss('https://mazdausa.sharepoint.com/sites/MCISPOTestSite/SiteAssets/NameTag/css/bootstrap.min.css');
    SPComponentLoader.loadCss('https://mazdausa.sharepoint.com/sites/MCISPOTestSite/SiteAssets/NameTag/css/Create.css');
    SPComponentLoader.loadCss("https://mazdausa.sharepoint.com/sites/MCISPOTestSite/SiteAssets/NameTag/css/jsgrid.min.css");
    SPComponentLoader.loadCss("https://mazdausa.sharepoint.com/sites/MCISPOTestSite/SiteAssets/NameTag/css/jsgrid-theme.min.css");
    SPComponentLoader.loadCss("https://mazdausa.sharepoint.com/sites/MCISPOTestSite/SiteAssets/NameTag/css/font-awesome.min.css");
    SPComponentLoader.loadCss("https://mazdausa.sharepoint.com/sites/MCISPOTestSite/SiteAssets/NameTag/css/jquery-ui.css");
    SPComponentLoader.loadCss("https://mazdausa.sharepoint.com/sites/MCISPOTestSite/SiteAssets/NameTag/css/jquery.datetimepicker.min.css");
    return this._getEnvironmentMessage().then(message => {
      this._environmentMessage = message;
    });
  }



  private _getEnvironmentMessage(): Promise<string> {
    if (!!this.context.sdks.microsoftTeams) { // running in Teams, office.com or Outlook
      return this.context.sdks.microsoftTeams.teamsJs.app.getContext()
        .then(context => {
          let environmentMessage: string = '';
          switch (context.app.host.name) {
            case 'Office': // running in Office
              environmentMessage = this.context.isServedFromLocalhost ? strings.AppLocalEnvironmentOffice : strings.AppOfficeEnvironment;
              break;
            case 'Outlook': // running in Outlook
              environmentMessage = this.context.isServedFromLocalhost ? strings.AppLocalEnvironmentOutlook : strings.AppOutlookEnvironment;
              break;
            case 'Teams': // running in Teams
            case 'TeamsModern':
              environmentMessage = this.context.isServedFromLocalhost ? strings.AppLocalEnvironmentTeams : strings.AppTeamsTabEnvironment;
              break;
            default:
              environmentMessage = strings.UnknownEnvironment;
          }

          return environmentMessage;
        });
    }

    return Promise.resolve(this.context.isServedFromLocalhost ? strings.AppLocalEnvironmentSharePoint : strings.AppSharePointEnvironment);
  }

  protected onThemeChanged(currentTheme: IReadonlyTheme | undefined): void {
    if (!currentTheme) {
      return;
    }

    this._isDarkTheme = !!currentTheme.isInverted;
    const {
      semanticColors
    } = currentTheme;

    if (semanticColors) {
      this.domElement.style.setProperty('--bodyText', semanticColors.bodyText || null);
      this.domElement.style.setProperty('--link', semanticColors.link || null);
      this.domElement.style.setProperty('--linkHovered', semanticColors.linkHovered || null);
    }

  }

  protected onDispose(): void {
    ReactDom.unmountComponentAtNode(this.domElement);
  }

  protected get dataVersion(): Version {
    return Version.parse('1.0');
  }

  protected getPropertyPaneConfiguration(): IPropertyPaneConfiguration {
    return {
      pages: [
        {
          header: {
            description: strings.PropertyPaneDescription
          },
          groups: [
            {
              groupName: strings.BasicGroupName,
              groupFields: [
                PropertyPaneTextField('description', {
                  label: strings.DescriptionFieldLabel
                })
              ]
            }
          ]
        }
      ]
    };
  }
}
