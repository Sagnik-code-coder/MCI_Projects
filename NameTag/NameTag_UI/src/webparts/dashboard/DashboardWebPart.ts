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

import * as strings from 'DashboardWebPartStrings';
import Dashboard from './components/Dashboard';
import { IDashboardProps } from './components/IDashboardProps';
/* tslint:disable:no-unused-variable */
//let moment: any = require('https://mazdausa.sharepoint.com/sites/MCISPOTestSite/SiteAssets/NameTag/js/moment.min.js');

export interface IDashboardWebPartProps {
  description: string;
}

export default class DashboardWebPart extends BaseClientSideWebPart<IDashboardWebPartProps> {

  private _isDarkTheme: boolean = false;
  private _environmentMessage: string = '';

  public render(): void {        
    const element: React.ReactElement<IDashboardProps> = React.createElement(
      Dashboard,
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
        <div class="row">
            <div id="divHeader">
                <label id="dashboardHeaderTitle" for="dashboardHeaderTitle" tkey="dashboardHeaderTitle">Name Tags order History</label>
            </div>
            <div id="divCreateOrdreBtn" class="form-group col-sm-3">
                <button id="btnCreateOrder" type="button" tkey="btnCreateOrder" class="btn btn-dark primary_btn">Create Order</button>
            </div>
        </div>

        <!--<div class="row">
            <div class="form-group col-sm-3">
            </div>
            <div class="form-group col-sm-3">
            </div>
            <div class="form-group col-sm-5">
                
            </div>
        </div>-->                
        <!--<div class="tab" id="tabsDashboard">
        </div>-->
        <div id="jsGridNameTagOrders" class="jsgrid">
            <!--<div class="jsgrid-pager-container" style="display: none;"></div><div class="jsgrid-load-shader"></div><div class="jsgrid-load-panel" style="display: none; position: absolute; top: 50%; left: 50%; z-index: 1000;">Please, wait...</div>-->
        </div>
        <div id="divPages">
            <div style="display:flex">
                <label id="pageSize" for="pageSize" tkey="pageSize">Records per pages</label>&nbsp;

                <select class="jsgrid-pager-nav-button" id="pages">
                    <option>5</option>
                    <option>10</option>
                    <option>15</option>
                    <option>20</option>
                    <option>25</option>
                    <option>30</option>
                </select>
            </div>
        </div>
        <div id="externalPager">

        </div>
        <!--<div id="divTotalItems">
            <label id="totalItems" for="totalItems" tkey="totalItems">Total Records: </label><label id="totalItemsCount" for="totalItemsCount" tkey="totalItemsCount">{t}</label>
        </div>-->
        <!--<div id="CustomPager">
            <div class="jsgrid-pager">                        
                <span id="pageFirst" class="jsgrid-pager-nav-button "><a href="javascript:void(0);" tkey="first">First</a></span>
                <span id="pagePrev" class="jsgrid-pager-nav-button "><a href="javascript:void(0);" tkey="prev">Prev</a></span>
                <span id="pageNext" class="jsgrid-pager-nav-button"><a href="javascript:void(0);" tkey="next">Next</a></span>
                <span id="pageLast" class="jsgrid-pager-nav-button"><a href="javascript:void(0);" tkey="last">Last</a></span>
            </div>
        </div>-->
        <br />  
        <br />
        <div class="row">
            <div id="divOrderApprovalBtns">
                <button id="btnReject" type="button" tkey="btnReject" class="btn btn-dark primary_btn">Reject</button>
                <button id="btnApprove" type="button" tkey="btnApprove" class="btn btn-dark primary_btn">Approve</button>
            </div>
        </div>              
    </div>

    

    <div id="nameTagModalUpdateInfo" class="modal">
        <div class="modal-content" id="nameTagModalUpdateSuccess">
            <h4 id="updateSuccess" tkey="updateSuccess">Name Tag order(s) updated successfully.</h4>
            <button id="btnCloseUpdatesuccess" type="button" tkey="updateinfoClose" class="btn btn-dark primary_btn">Close</button>
        </div>

    </div>
    <div id="nameTagModalProcessedInfo" class="modal">
        <div class="modal-content" id="nameTagModalProcessedInfo">
            <h4 id="processedinfo" tkey="processedinfo">This order is already processed, please select confirmed orders to approve or reject.</h4>
            <button id="btnprocessedinfo" type="button" tkey="updateinfoClose" class="btn btn-dark primary_btn">Close</button>
        </div>

    </div>
    <div id="modalApproveConfirm" class="modal">
        <div class="modal-content" id="modalApproveConfirmCont">
            <h4 id="lblApvConfirm" tkey="lblApvConfirm">Are you sure you want to approve the selected orders?</h4>
            <button id="btnApvConfirmNo" type="button" tkey="btnConfirmSubmitNo" class="btn btn-dark primary_btn">No</button>
            <button id="btnApvConfirmYes" type="button" tkey="btnConfirmSubmitYes" class="btn btn-dark primary_btn">Yes</button>
        </div>
    </div>
    <div id="modalRejtConfirm" class="modal">
        <div class="modal-content" id="modalRejtConfirmCont">
            <h4 id="lblRejtConfirm" tkey="lblRejtConfirm">Are you sure you want to reject the selected orders?</h4>
            <button id="btnRejtConfirmNo" type="button" tkey="btnConfirmSubmitNo" class="btn btn-dark primary_btn">No</button>
            <button id="btnRejtConfirmYes" type="button" tkey="btnConfirmSubmitYes" class="btn btn-dark primary_btn">Yes</button>
        </div>
    </div>
</div>`;
  }

  protected onInit(): Promise<void> {
    SPComponentLoader.loadScript("https://mazdausa.sharepoint.com/sites/MCISPOTestSite/SiteAssets/NameTag/js/jquery.js",{globalExportsName:'jquery'}).then(()=>{      
    //SPComponentLoader.loadScript("https://mazdausa.sharepoint.com/sites/MCISPOTestSite/SiteAssets/NameTag/js/jquery-ui.js",{globalExportsName:'jquery'}).then(()=>{   
      SPComponentLoader.loadScript('https://mazdausa.sharepoint.com/sites/MCISPOTestSite/SiteAssets/NameTag/js/jquery-ui.js');   
      SPComponentLoader.loadScript('https://mazdausa.sharepoint.com/sites/MCISPOTestSite/SiteAssets/NameTag/js/lang.js');                     
        SPComponentLoader.loadScript('https://mazdausa.sharepoint.com/sites/MCISPOTestSite/SiteAssets/NameTag/js/jquery.datetimepicker.js');
      //});
      //SPComponentLoader.loadScript('https://mazdausa.sharepoint.com/sites/MCISPOTestSite/SiteAssets/NameTag/js/moment.min.js');
      SPComponentLoader.loadScript('https://mazdausa.sharepoint.com/sites/MCISPOTestSite/SiteAssets/NameTag/js/bootstrap.min.js');
      SPComponentLoader.loadScript('https://mazdausa.sharepoint.com/sites/MCISPOTestSite/SiteAssets/NameTag/js/jquery.validate.min.js');
      //SPComponentLoader.loadScript('https://mazdausa.sharepoint.com/sites/MCISPOTestSite/SiteAssets/NameTag/js/jquery.validate.unobtrusive.min.js');
      SPComponentLoader.loadScript('https://mazdausa.sharepoint.com/sites/MCISPOTestSite/SiteAssets/NameTag/js/jsgrid.min.js',{globalExportsName:'jsGrid'}).then(()=>{
      SPComponentLoader.loadScript('https://mazdausa.sharepoint.com/sites/MCISPOTestSite/SiteAssets/NameTag/js/Loader.js'); 
      SPComponentLoader.loadScript('https://mazdausa.sharepoint.com/sites/MCISPOTestSite/SiteAssets/NameTag/js/Utility.js',{globalExportsName:'CommonUtility'}).then(()=>{ 
      //SPComponentLoader.loadScript('https://mazdausa.sharepoint.com/sites/MCISPOTestSite/SiteAssets/NameTag/js/Utility.js');
      SPComponentLoader.loadScript('https://mazdausa.sharepoint.com/sites/MCISPOTestSite/SiteAssets/NameTag/js/Dashboard.js');                  
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
