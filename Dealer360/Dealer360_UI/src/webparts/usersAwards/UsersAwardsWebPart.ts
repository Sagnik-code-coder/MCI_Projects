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
import * as strings from 'UsersAwardsWebPartStrings';
import UsersAwards from './components/UsersAwards';
import { IUsersAwardsProps } from './components/IUsersAwardsProps';

export interface IUsersAwardsWebPartProps {
  description: string;
}

export default class UsersAwardsWebPart extends BaseClientSideWebPart<IUsersAwardsWebPartProps> {

  private _isDarkTheme: boolean = false;
  private _environmentMessage: string = '';

  public render(): void {
    const element: React.ReactElement<IUsersAwardsProps> = React.createElement(
      UsersAwards,
      {
        description: this.properties.description,
        isDarkTheme: this._isDarkTheme,
        environmentMessage: this._environmentMessage,
        hasTeamsContext: !!this.context.sdks.microsoftTeams,
        userDisplayName: this.context.pageContext.user.displayName
      }
    );

    ReactDom.render(element, this.domElement);
    this.domElement.innerHTML = `<div id="loader" class="centerloader" style="visibility: visible; display: none;"></div>
    <div class="body-content" style="visibility: visible;">
            <div class="container-fluid">
                
                <div>
                    <div class="alert alert-info" tkey="UserInaccessibility" id="UserInaccessibilityAlert" style="display:none">The current user is not allowed to visit this page.</div>
                    <div id="grids">
                        <div id="dealersubmit">
                            <h1 class="heading" tkey="TrafficLogSystemAdministration">User's Awards</h1>
                            <div class="alert alert-info" tkey="DealerSubmitMessage" id="DealerSubmitMessage" style="display:none">Your request has been successfully submitted.</div>
                            <div class="alert alert-info" tkey="DealerFailureMessage" id="DealerFailureMessage" style="display:none">Your Submission is not successfully submitted.Please contact support team</div>
    
                            <!--<div wfd-id="9" class="filter_area">
                               
                               
                            <div style="width:100%; margin:0 auto;">
                            <table class="jsgrid-table"><tbody style="height:100px;">
                                <tr class="jsgrid-header-row">
                                    <th class="jsgrid-header-cell jsgrid-align-center">Dealer Code</th>
                                    <th class="jsgrid-header-cell jsgrid-align-center">Award Year</th>
                                    <th class="jsgrid-header-cell jsgrid-align-center">President Club Tier 1</th>
                                    <th class="jsgrid-header-cell jsgrid-align-center">President Club Tier 2</th>
                                    <th class="jsgrid-header-cell jsgrid-align-center" style="width: auto;">Grand Performers </th>
                                    <th class="jsgrid-header-cell jsgrid-align-center" style="width: auto;">Dealer of Distinction</th></tr>
                                
                                        
                                </table>
                        </div>
                                
                            </div>-->
                            <div id="jsGridAwardData" class="jsgrid">
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
              <p style="text-align:right">
                                    <br/>
                                    <button id="dealersubmitbtn"  data-toggle="modal" data-target="#exampleModal" tkey="Submit" type="button" value="Add" class="btn btn-dark primary_btn" style="display: inline-block;">Add</button>
                                    <button id="dealersubmitbtn"  data-toggle="modal" data-target="#exampleModal" tkey="Submit" type="button" value="View" class="btn btn-dark primary_btn" style="display: inline-block;">View</button>
    
                                </p>

                                <!-- Modal -->
  <div class="modal fade" id="exampleModal" data-backdrop="static" data-keyboard='false'>   <!--tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true"-->
    <div class="modal-dialog" role="document" style="max-width: 1211px!important;left: 0%;width: 1211px;">
      <div class="modal-content">
       
        <div class="modal-body">
            <div style="width: 100%; margin:0 auto;">
                <h1 class="heading" tkey="TrafficLogSystemAdministration">Award Data</h1>
                <div id="basicTab" class="tabcontent" style="display: block;">
                    <div class="row"><br/></div>
                    
                    <div class="row">
                        <div class="col-4" id="divTicketNumber">
                            <label for="ticketNumber" tkey="ticketNumber" id="ticketNumber">Dealer Code &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>
                            <select class="form-control" id="selTicketStatus"><option value="0">Select  </option><option value="111142">111142  </option><option value="111143">111143  </option><option value="111144">111144  </option><option value="111145">111145  </option><option value="111146">111146  </option></select>
                        </div>
                        <div class="col-4" id="divTicketNumber">
                            <label for="ticketNumber" tkey="ticketNumber" id="ticketNumber">Award Date &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>
                            <select class="form-control" id="selTicketStatus"><option value="0">Select  </option><option value="2020">2020  </option><option value="2021">2021  </option><option value="2022">2022  </option><option value="2023">2023  </option><option value="2024">2024  </option></select>
                        </div>
                        <div class="col-3" id="divDealerCode">
                            <label for="dealerCode" tkey="dealerCode" id="dealerCode">President Club Tier 1</label>
                            <select class="form-control" id="selTicketStatus"><option value="0">Select  </option><option value="1">Yes  </option><option value="0">No  </option></select>
                        </div>
                        
                    
                    </div>
                    <div class="row">
                    <br/>
                    
                    </div>
                    <div class="row">
                        <div class="col-4" id="divTicketStatus">
                            <label for="ticketStatus" tkey="ticketStatus" id="ticketStatus">President Club Tier 2</label>
                            <select class="form-control" id="selTicketStatus"><option value="0">Select  </option><option value="1">Yes  </option><option value="0">No  </option></select>
                        </div>
                        <div class="col-4" id="divTicketNumber">
                            <label for="ticketNumber" tkey="ticketNumber" id="ticketNumber">Grand Performers </label>
                            <select class="form-control" id="selTicketStatus"><option value="0">Select  </option><option value="1">Yes  </option><option value="0">No  </option></select>
                        </div>
                        <div class="col-3" id="divTicketNumber">
                            <label for="ticketNumber" tkey="ticketNumber" id="ticketNumber">Dealer of Distinction </label>
                            <select class="form-control" id="selTicketStatus"><option value="0">Select  </option><option value="1">Yes  </option><option value="0">No  </option></select>
                        </div>
                        
                        
                    
                    </div>
                    <div class="row">
                    
                    <p style="text-align:right; width:100%;">
                        <br>
                        <button id="dealersubmitbtn" tkey="Submit" type="submit" value="Submit" class="btn btn-dark primary_btn" style="display: inline-block;">Submit</button>
                        <button id="reset" tkey="Reset" class="btn btn-outline-secondary secondary_btn">Reset</button>
                    </p>
                    
                </div>
                    
                    
                    
                    // <script>
                    //     $(document).ready(function () {
                    //         if (ticketSystemIDGlobal == 3 && culture == 'en-US') {         //Added for Phase2 change
                    //             $("#subjectCount").removeAttr("tkey");
                    //             $("#subjectCount").text('100');
                    //         }
                    //         else if (ticketSystemIDGlobal == 3 && culture == 'fr-FR') {
                    //             $("#subjectCount").removeAttr("tkey");
                    //             $("#subjectCount").text('100');
                    //         }
                    //     });
                    // </script>
                    
                    
                    </div>
            </div>
        </div>
        <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
          </div>
      </div>
    </div>
  </div>
              </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>`;
  }

  protected onInit(): Promise<void> {
    SPComponentLoader.loadCss("https://mazdausa.sharepoint.com/sites/MCIOneMazdaDev/dealer360/SiteAssets/css/bootstrap.css");
    SPComponentLoader.loadCss("https://mazdausa.sharepoint.com/sites/MCIOneMazdaDev/dealer360/SiteAssets/css/bootstrap.min.css");
    SPComponentLoader.loadCss("https://mazdausa.sharepoint.com/sites/MCIOneMazdaDev/dealer360/SiteAssets/css/bootstrap-datetimepicker.min.css");
    SPComponentLoader.loadCss("https://mazdausa.sharepoint.com/sites/MCIOneMazdaDev/dealer360/SiteAssets/css/bootstrap-theme.min.css");
    SPComponentLoader.loadCss("https://mazdausa.sharepoint.com/sites/MCIOneMazdaDev/dealer360/SiteAssets/css/corev15.css");
    SPComponentLoader.loadCss("https://mazdausa.sharepoint.com/sites/MCIOneMazdaDev/dealer360/SiteAssets/css/jquery.datetimepicker.min.css");
    SPComponentLoader.loadCss("https://mazdausa.sharepoint.com/sites/MCIOneMazdaDev/dealer360/SiteAssets/css/jquery-ui.css");
    SPComponentLoader.loadCss("https://mazdausa.sharepoint.com/sites/MCIOneMazdaDev/dealer360/SiteAssets/css/jsgrid.min.css");
    SPComponentLoader.loadCss("https://mazdausa.sharepoint.com/sites/MCIOneMazdaDev/dealer360/SiteAssets/css/jsgrid-theme.min.css");
    SPComponentLoader.loadCss("https://mazdausa.sharepoint.com/sites/MCIOneMazdaDev/dealer360/SiteAssets/css/pagelayouts15.css");
    SPComponentLoader.loadCss("https://mazdausa.sharepoint.com/sites/MCIOneMazdaDev/dealer360/SiteAssets/css/responsive.bootstrap.min.css");
    SPComponentLoader.loadCss("https://mazdausa.sharepoint.com/sites/MCIOneMazdaDev/dealer360/SiteAssets/css/site.css");
    SPComponentLoader.loadCss("https://mazdausa.sharepoint.com/sites/MCIOneMazdaDev/dealer360/SiteAssets/css/SuiteNav.css");
    
    SPComponentLoader.loadScript("https://mazdausa.sharepoint.com/sites/MCIOneMazdaDev/dealer360/SiteAssets/js/jquery.js",{globalExportsName:'jquery'}).then(()=>{ 
      SPComponentLoader.loadScript("https://mazdausa.sharepoint.com/sites/MCIOneMazdaDev/dealer360/SiteAssets/js/jquery-ui.js").then(()=>{      
        SPComponentLoader.loadScript('https://mazdausa.sharepoint.com/sites/MCIOneMazdaDev/dealer360/SiteAssets/js/lang.js');       
        //SPComponentLoader.loadScript('https://mazdausa.sharepoint.com/sites/MCIOneMazdaDev/dealer360/SiteAssets/js/PeoplePickerOnline.js');
        SPComponentLoader.loadScript('https://mazdausa.sharepoint.com/sites/MCIOneMazdaDev/dealer360/SiteAssets/js/jquery.datetimepicker.js');
        //SPComponentLoader.loadScript('https://mazdausa.sharepoint.com/sites/MCIOneMazdaDev/dealer360/SiteAssets/js/moment.min.js');
        SPComponentLoader.loadScript('https://mazdausa.sharepoint.com/sites/MCIOneMazdaDev/dealer360/SiteAssets/js/bootstrap.min.js');
        SPComponentLoader.loadScript('https://mazdausa.sharepoint.com/sites/MCIOneMazdaDev/dealer360/SiteAssets/js/jquery.validate.min.js');
        //SPComponentLoader.loadScript('https://mazdausa.sharepoint.com/sites/MCIOneMazdaDev/dealer360/SiteAssets/js/jquery.validate.unobtrusive.min.js');
        SPComponentLoader.loadScript("https://mazdausa.sharepoint.com/sites/MCIOneMazdaDev/dealer360/SiteAssets/js/jsgrid.min.js").then(()=>{
        //SPComponentLoader.loadScript('https://mazdausa.sharepoint.com/sites/MCIOneMazdaDev/dealer360/SiteAssets/js/Loader.js');
        //SPComponentLoader.loadScript('https://mazdausa.sharepoint.com/sites/MCIOneMazdaDev/dealer360/SiteAssets/js/PeoplePickerOnline.js');
        SPComponentLoader.loadScript('https://mazdausa.sharepoint.com/sites/MCIOneMazdaDev/dealer360/SiteAssets/js/UsersAwards.js');            
        //SPComponentLoader.loadScript('https://mazdausa.sharepoint.com/sites/MCIOneMazdaDev/dealer360/SiteAssets/js/Utility.js');
      });
   });
  });
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
