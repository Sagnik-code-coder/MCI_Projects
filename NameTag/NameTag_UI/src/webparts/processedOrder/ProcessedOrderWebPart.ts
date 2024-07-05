import * as React from 'react';
import * as ReactDom from 'react-dom';
import { Version } from '@microsoft/sp-core-library';
import {
  type IPropertyPaneConfiguration,
  PropertyPaneTextField
} from '@microsoft/sp-property-pane';
import { BaseClientSideWebPart } from '@microsoft/sp-webpart-base';
import { IReadonlyTheme } from '@microsoft/sp-component-base';
//import { SPComponentLoader } from '@microsoft/sp-loader';

import * as strings from 'ProcessedOrderWebPartStrings';
import ProcessedOrder from './components/ProcessedOrder';
import { IProcessedOrderProps } from './components/IProcessedOrderProps';

export interface IProcessedOrderWebPartProps {
  description: string;
}

export default class ProcessedOrderWebPart extends BaseClientSideWebPart<IProcessedOrderWebPartProps> {

  private _isDarkTheme: boolean = false;
  private _environmentMessage: string = '';

  public render(): void {
    const element: React.ReactElement<IProcessedOrderProps> = React.createElement(
      ProcessedOrder,
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
                    <label id="viewPrevDownloadTitle" for="viewPrevDownloadTitle" tkey="viewPrevDownloadTitle">Processed Order</label>
                </div>
            </div>
        </div>
        <!--<div class="tab" id="tabsDashboard">
        </div>-->
        <div id="jsGridBatches" class="jsgrid" style="position: relative; height: auto; width: 100%;"><div class="jsgrid-grid-header jsgrid-header-scrollbar"><table class="jsgrid-table"><tr class="jsgrid-header-row"><th class="jsgrid-header-cell hide jsgrid-header-sortable" style="width: 0px;">RowID</th><th class="jsgrid-header-cell jsgrid-header-sortable" style="width: 50px;"></th><th class="jsgrid-header-cell hide jsgrid-header-sortable" style="width: 100px;">Batch Id</th><th class="jsgrid-header-cell jsgrid-header-sortable" style="width: 100px;">Processed Date Time</th><th class="jsgrid-header-cell jsgrid-header-sortable" style="width: 100px;">Processed By</th></tr><tr class="jsgrid-filter-row"><td class="jsgrid-cell hide" style="width: 0px;"><input type="text"></td><td class="jsgrid-cell" style="width: 50px;"></td><td class="jsgrid-cell hide" style="width: 100px;"><input type="text"></td><td class="jsgrid-cell" style="width: 100px;"><input type="text" id="dateFilterBatchDt"></td><td class="jsgrid-cell" style="width: 100px;"><input type="text"></td></tr><tr class="jsgrid-insert-row" style="display: none;"><td class="jsgrid-cell hide" style="width: 0px;"><input type="text"></td><td class="jsgrid-cell" style="width: 50px;"><input type="text"></td><td class="jsgrid-cell hide" style="width: 100px;"><input type="text"></td><td class="jsgrid-cell" style="width: 100px;"></td><td class="jsgrid-cell" style="width: 100px;"><input type="text"></td></tr></table></div><div class="jsgrid-grid-body"><table class="jsgrid-table"><tbody><tr class="jsgrid-row"><td class="jsgrid-cell hide" style="width: 0px;"></td><td class="jsgrid-cell" style="width: 50px;"><input type="radio" name="cellRadio"></td><td class="jsgrid-cell hide" style="width: 100px;">100043</td><td class="jsgrid-cell" style="width: 100px;">05/25/2023 6:54:34 AM</td><td class="jsgrid-cell" style="width: 100px;">Suresh Chandra Awasthi  (MCI-InterraIT)</td></tr><tr class="jsgrid-alt-row"><td class="jsgrid-cell hide" style="width: 0px;"></td><td class="jsgrid-cell" style="width: 50px;"><input type="radio" name="cellRadio"></td><td class="jsgrid-cell hide" style="width: 100px;">100042</td><td class="jsgrid-cell" style="width: 100px;">05/23/2023 4:37:54 AM</td><td class="jsgrid-cell" style="width: 100px;">Suman Kalyan Chakraborty (MCI-InterraIT)</td></tr><tr class="jsgrid-row"><td class="jsgrid-cell hide" style="width: 0px;"></td><td class="jsgrid-cell" style="width: 50px;"><input type="radio" name="cellRadio"></td><td class="jsgrid-cell hide" style="width: 100px;">100041</td><td class="jsgrid-cell" style="width: 100px;">05/23/2023 4:04:12 AM</td><td class="jsgrid-cell" style="width: 100px;">Suresh Chandra Awasthi  (MCI-InterraIT)</td></tr><tr class="jsgrid-alt-row"><td class="jsgrid-cell hide" style="width: 0px;"></td><td class="jsgrid-cell" style="width: 50px;"><input type="radio" name="cellRadio"></td><td class="jsgrid-cell hide" style="width: 100px;">100040</td><td class="jsgrid-cell" style="width: 100px;">05/23/2023 3:41:36 AM</td><td class="jsgrid-cell" style="width: 100px;">Suresh Chandra Awasthi  (MCI-InterraIT)</td></tr><tr class="jsgrid-row"><td class="jsgrid-cell hide" style="width: 0px;"></td><td class="jsgrid-cell" style="width: 50px;"><input type="radio" name="cellRadio"></td><td class="jsgrid-cell hide" style="width: 100px;">100039</td><td class="jsgrid-cell" style="width: 100px;">05/22/2023 5:54:23 AM</td><td class="jsgrid-cell" style="width: 100px;">Suman Kalyan Chakraborty (MCI-InterraIT)</td></tr></tbody></table></div><div class="jsgrid-load-shader" style="display: none; position: absolute; inset: 0px; z-index: 1000;"></div><div class="jsgrid-load-panel" style="display: none; position: absolute; top: 50%; left: 50%; z-index: 1000;">Please, wait...</div></div>
        <div id="divPages">
            <div style="display:flex">
                <label id="pageSize" for="pageSize" tkey="pageSize">Page Size</label>&nbsp;

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
        <div id="externalPager" class="jsgrid-pager-container" style=""><div class="jsgrid-pager">Current page: 1 &nbsp;&nbsp; <span class="jsgrid-pager-nav-button jsgrid-pager-nav-inactive-button"><a href="javascript:void(0);"><span id="pageFirst" class="jsgrid-pager-nav-button"><a href="javascript:void(0);" tkey="first">First</a></span></a></span> <span class="jsgrid-pager-nav-button jsgrid-pager-nav-inactive-button"><a href="javascript:void(0);"><span id="pagePrev" class="jsgrid-pager-nav-button"><a href="javascript:void(0);" tkey="prev">Prev</a></span></a></span> <span class="jsgrid-pager-page jsgrid-pager-current-page">1</span><span class="jsgrid-pager-page"><a href="javascript:void(0);">2</a></span><span class="jsgrid-pager-page"><a href="javascript:void(0);">3</a></span><span class="jsgrid-pager-page"><a href="javascript:void(0);">4</a></span><span class="jsgrid-pager-page"><a href="javascript:void(0);">5</a></span><span class="jsgrid-pager-nav-button"><a href="javascript:void(0);">…</a></span> <span class="jsgrid-pager-nav-button"><a href="javascript:void(0);"><span id="pageNext" class="jsgrid-pager-nav-button"><a href="javascript:void(0);" tkey="next">Next</a></span></a></span> <span class="jsgrid-pager-nav-button"><a href="javascript:void(0);"><span id="pageLast" class="jsgrid-pager-nav-button"><a href="javascript:void(0);" tkey="last">Last</a></span></a></span> &nbsp;&nbsp; Total pages: 9 Total items: 43 </div></div>
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
        <div class="row">
            <div class="colWidth-footer-btn" id="divDownloadOrderBtns" style="display: block;">
                <button id="btnDownloadFinanceFile" type="button" tkey="btnDownloadFinanceFile" class="btn btn-dark primary_btn" disabled="">Download Finance File</button>                    
                <button id="btnDownloadDealerOrder" type="button" tkey="btnDownloadDealerOrder" class="btn btn-dark primary_btn" disabled="">Download Dealer Order</button>
                <button id="btnDownloadCorpOrder" type="button" tkey="btnDownloadCorpOrder" class="btn btn-dark primary_btn" disabled="">Download Corporate Order</button>
            </div>
        </div>          
    </div>

</div>`;
  }

  protected onInit(): Promise<void> {
    
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
