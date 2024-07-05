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
import * as strings from 'FacilityStandardWebPartStrings';
import FacilityStandard from './components/FacilityStandard';
import { IFacilityStandardProps } from './components/IFacilityStandardProps';

export interface IFacilityStandardWebPartProps {
  description: string;
}

export default class FacilityStandardWebPart extends BaseClientSideWebPart<IFacilityStandardWebPartProps> {

  private _isDarkTheme: boolean = false;
  private _environmentMessage: string = '';

  public render(): void {
    const element: React.ReactElement<IFacilityStandardProps> = React.createElement(
      FacilityStandard,
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
                            <h1 class="heading" tkey="TrafficLogSystemAdministration">Facility Standard</h1>
                            <div class="alert alert-info" tkey="DealerSubmitMessage" id="DealerSubmitMessage" style="display:none">Your request has been successfully submitted.</div>
                            <div class="alert alert-info" tkey="DealerFailureMessage" id="DealerFailureMessage" style="display:none">Your Submission is not successfully submitted.Please contact support team</div>
    
                            <!--<div wfd-id="9" class="filter_area">
                               
                               
                                <div style="width:100%; margin:0 auto; overflow: auto;">
                                    <table class="jsgrid-table" style="max-width: 156%; width: 156%;"><tbody>
                                        <tr class="jsgrid-header-row"><th class="jsgrid-header-cell jsgrid-align-center" style="width: 80px;">NEW UNIT SALES VOLUME</th>
                                            <th class="jsgrid-header-cell jsgrid-align-center" style="width: 90px;">Minimum Number of Vehicles In Showroom</th>
                                            <th class="jsgrid-header-cell jsgrid-align-center" style="width: 105px;">SHOWROOM (sqft)</th>
                                            <th class="jsgrid-header-cell jsgrid-align-center" style="width: 70px;">SALES OFFICE (sqft)</th>
                                            <th class="jsgrid-header-cell jsgrid-align-center" style="width: 65px;">TOTAL SALES AREA (sqft)</th>
                                            <th class="jsgrid-header-cell jsgrid-align-center" style="width: 80px;">Number OF SERVICE BAYS (Min. of 500 600 sqft par bay)</th>
                                            <th class="jsgrid-header-cell jsgrid-align-center" style="width: 90px;">Number OF NEW CAR DELIVERY BAYS (PDI, Wash)</th>
                                            <th class="jsgrid-header-cell jsgrid-align-center" style="width: 70px;">TOTAL Number OF BAYS</th>
                                            <th class="jsgrid-header-cell jsgrid-align-center" style="width: 90px;">SERVICE RECPTION </th>
                                            <th class="jsgrid-header-cell jsgrid-align-center" style="width: 100px;">CUSTOMER WAITING Area </th>
                                            <th class="jsgrid-header-cell jsgrid-align-center" style="width: auto;">TOTAL SERVICE AREA </th>
                                            <th class="jsgrid-header-cell jsgrid-align-center" style="width: auto;">PARTS DEPARTMENT (sqft)</th>
                                            <th class="jsgrid-header-cell jsgrid-align-center" style="width: auto;">PARTS DEPARTMENT (Note 5)(sqft)</th>
                                            <th class="jsgrid-header-cell jsgrid-align-center" style="width: auto;">GENERAL OFFICE & OTHER</th>
                                            <th class="jsgrid-header-cell jsgrid-align-center" style="width: auto;">TOTAL BUILDING BUIDELINE</th>
                                            <th class="jsgrid-header-cell jsgrid-align-center" style="width: auto;">TOTAL SITE REQUIREMENT (Land & Building)</th>
                                            <th class="jsgrid-header-cell jsgrid-align-center" style="width: auto;">ACRES</th></tr>
                                    <tr class="jsgrid-row"><td>100</td><td class="jsgrid-cell jsgrid-align-center" style="width: 72px;"><input type="text" value="3" title="3" id="vecShowroom_1359" required="required" class="form-control width-dynamic valid"></td><td class="jsgrid-cell jsgrid-align-center" style="width: 72px;"><input type="text" value="1,500" title="1" id="Showroomsqft_1359" required="required" class="form-control width-dynamic valid"></td><td class="jsgrid-cell jsgrid-align-center" style="width: auto;"><input type="text" value="160" title="Jan Week 1, 2024 (Week 1)" id="ReportingWeekDescriptionEN_1359" required="required" class="form-control width-dynamic valid" onchange="changevalue(this, 1359)"></td><td class="jsgrid-cell jsgrid-align-center" style="width: auto;"><input type="text" value="1,660" title="1re sem. de janv., 2024 (sem. 1)" id="ReportingWeekDescriptionFR_1359" required="required" class="form-control width-dynamic valid" onchange="changevalue(this, 1359)"></td><td class="jsgrid-cell jsgrid-align-center" style="width: auto;"><input type="text" class="form-control datewtclass width-dynamic hasDatepicker valid" value="4" title="Fri Jan 05 2024" id="ReportingWeekStartDate_1359" required="required" onchange="changevalue(this, 1359)"></td><td class="jsgrid-cell jsgrid-align-center" style="width: auto;"><input type="text" class="form-control datewtclass width-dynamic hasDatepicker valid" value="" title="Thu Jan 11 2024" id="ReportingWeekEndDate_1359" required="required" onchange="changevalue(this, 1359)"></td><td class="jsgrid-cell jsgrid-align-center" style="width: 72px;"><input type="text" value="4" title="MonthNum" id="MonthNo_1359" required="required" class="form-control width-dynamic valid"></td><td class="jsgrid-cell jsgrid-align-center" style="width: 72px;"><input type="text" value="175" title="2024" id="Bays_1359" required="required" class="form-control width-dynamic valid"></td><td class="jsgrid-cell jsgrid-align-center" style="width: auto;"><input type="text" class="form-control dateclass width-dynamic hasDatepicker valid" value="175" title="Thu Jan 11 2024 12:00" id="SubmissionWindowStartDate_1359" required="required" onchange="changevalue(this, 1359)"></td><td class="jsgrid-cell jsgrid-align-center" style="width: auto;"><input type="text" class="form-control dateclass width-dynamic hasDatepicker valid" value="2,750" title="Sun Jan 14 2024 23:59" id="SubmissionWindowEndDate_1359" required="required" onchange="changevalue(this, 1359)"></td><td class="jsgrid-cell jsgrid-align-center" style="width: auto;"><input type="text" class="form-control dateclass hasDatepicker valid" value="800" title="Fri Jan 12 2024 12:00" id="SubmissionWindowFirstDeadline_1359" required="required" onchange="changevalue(this, 1359)"></td><td class="jsgrid-cell jsgrid-align-center" style="width: auto;"><input type="text" value="640" title="January Week 1 (January 5-11, 2024)" id="SubmissionWindowDescriptionEN_1359" required="required" class="form-control width-dynamic valid" onchange="changevalue(this, 1359)"></td><td class="jsgrid-cell jsgrid-align-center" style="width: auto;"><input type="text" value="700" title="1re semaine de janvier (5-11 janvier, 2024)" id="SubmissionWindowDescriptionFR_1359" required="required" class="form-control width-dynamic valid" onchange="changevalue(this, 1359)"></td><td class="jsgrid-cell jsgrid-align-center" style="width: 72px;"><input type="text" value="5,910" title="2024" id="Bays_1359" required="required" class="form-control width-dynamic valid"></td><td class="jsgrid-cell jsgrid-align-center" style="width: 72px;"><input type="text" value="29,000" title="2024" id="Bays_1359" required="required" class="form-control width-dynamic valid"></td><td class="jsgrid-cell jsgrid-align-center" style="width: 72px;"><input type="text" value="0.67" title="2024" id="Bays_1359" required="required" class="form-control width-dynamic valid"></td></tr>
                                    <tr class="jsgrid-row"><td>150</td><td class="jsgrid-cell jsgrid-align-center" style="width: 72px;"><input type="text" value="3" title="1359" id="vecShowroom_1359" required="required" class="form-control width-dynamic valid"></td><td class="jsgrid-cell jsgrid-align-center" style="width: 72px;"><input type="text" value="1,500" title="1" id="Showroomsqft_1359" required="required" class="form-control width-dynamic valid"></td><td class="jsgrid-cell jsgrid-align-center" style="width: auto;"><input type="text" value="160" title="Jan Week 1, 2024 (Week 1)" id="ReportingWeekDescriptionEN_1359" required="required" class="form-control width-dynamic valid" onchange="changevalue(this, 1359)"></td><td class="jsgrid-cell jsgrid-align-center" style="width: auto;"><input type="text" value="1,660" title="1re sem. de janv., 2024 (sem. 1)" id="ReportingWeekDescriptionFR_1359" required="required" class="form-control width-dynamic valid" onchange="changevalue(this, 1359)"></td><td class="jsgrid-cell jsgrid-align-center" style="width: auto;"><input type="text" class="form-control datewtclass width-dynamic hasDatepicker valid" value="4" title="Fri Jan 05 2024" id="ReportingWeekStartDate_1359" required="required" onchange="changevalue(this, 1359)"></td><td class="jsgrid-cell jsgrid-align-center" style="width: auto;"><input type="text" class="form-control datewtclass width-dynamic hasDatepicker valid" value="" title="Thu Jan 11 2024" id="ReportingWeekEndDate_1359" required="required" onchange="changevalue(this, 1359)"></td><td class="jsgrid-cell jsgrid-align-center" style="width: 72px;"><input type="text" value="4" title="MonthNum" id="MonthNo_1359" required="required" class="form-control width-dynamic valid"></td><td class="jsgrid-cell jsgrid-align-center" style="width: 72px;"><input type="text" value="175" title="2024" id="Bays_1359" required="required" class="form-control width-dynamic valid"></td><td class="jsgrid-cell jsgrid-align-center" style="width: auto;"><input type="text" class="form-control dateclass width-dynamic hasDatepicker valid" value="175" title="Thu Jan 11 2024 12:00" id="SubmissionWindowStartDate_1359" required="required" onchange="changevalue(this, 1359)"></td><td class="jsgrid-cell jsgrid-align-center" style="width: auto;"><input type="text" class="form-control dateclass width-dynamic hasDatepicker valid" value="2,750" title="Sun Jan 14 2024 23:59" id="SubmissionWindowEndDate_1359" required="required" onchange="changevalue(this, 1359)"></td><td class="jsgrid-cell jsgrid-align-center" style="width: auto;"><input type="text" class="form-control dateclass hasDatepicker valid" value="800" title="Fri Jan 12 2024 12:00" id="SubmissionWindowFirstDeadline_1359" required="required" onchange="changevalue(this, 1359)"></td><td class="jsgrid-cell jsgrid-align-center" style="width: auto;"><input type="text" value="640" title="January Week 1 (January 5-11, 2024)" id="SubmissionWindowDescriptionEN_1359" required="required" class="form-control width-dynamic valid" onchange="changevalue(this, 1359)"></td><td class="jsgrid-cell jsgrid-align-center" style="width: auto;"><input type="text" value="700" title="1re semaine de janvier (5-11 janvier, 2024)" id="SubmissionWindowDescriptionFR_1359" required="required" class="form-control width-dynamic valid" onchange="changevalue(this, 1359)"></td><td class="jsgrid-cell jsgrid-align-center" style="width: 72px;"><input type="text" value="5,910" title="2024" id="Bays_1359" required="required" class="form-control width-dynamic valid"></td><td class="jsgrid-cell jsgrid-align-center" style="width: 72px;"><input type="text" value="29,000" title="2024" id="Bays_1359" required="required" class="form-control width-dynamic valid"></td><td class="jsgrid-cell jsgrid-align-center" style="width: 72px;"><input type="text" value="0.67" title="2024" id="Bays_1359" required="required" class="form-control width-dynamic valid"></td></tr>
                                    <tr class="jsgrid-row"><td>200</td><td class="jsgrid-cell jsgrid-align-center" style="width: 72px;"><input type="text" value="4" title="1359" id="vecShowroom_1359" required="required" class="form-control width-dynamic valid"></td><td class="jsgrid-cell jsgrid-align-center" style="width: 72px;"><input type="text" value="2000" title="1" id="Showroomsqft_1359" required="required" class="form-control width-dynamic valid"></td><td class="jsgrid-cell jsgrid-align-center" style="width: auto;"><input type="text" value="240" title="Jan Week 1, 2024 (Week 1)" id="ReportingWeekDescriptionEN_1359" required="required" class="form-control width-dynamic valid" onchange="changevalue(this, 1359)"></td><td class="jsgrid-cell jsgrid-align-center" style="width: auto;"><input type="text" value="2,240" title="1re sem. de janv., 2024 (sem. 1)" id="ReportingWeekDescriptionFR_1359" required="required" class="form-control width-dynamic valid" onchange="changevalue(this, 1359)"></td><td class="jsgrid-cell jsgrid-align-center" style="width: auto;"><input type="text" class="form-control datewtclass width-dynamic hasDatepicker valid" value="4" title="Fri Jan 05 2024" id="ReportingWeekStartDate_1359" required="required" onchange="changevalue(this, 1359)"></td><td class="jsgrid-cell jsgrid-align-center" style="width: auto;"><input type="text" class="form-control datewtclass width-dynamic hasDatepicker valid" value="1" title="Thu Jan 11 2024" id="ReportingWeekEndDate_1359" required="required" onchange="changevalue(this, 1359)"></td><td class="jsgrid-cell jsgrid-align-center" style="width: 72px;"><input type="text" value="5" title="MonthNum" id="MonthNo_1359" required="required" class="form-control width-dynamic valid"></td><td class="jsgrid-cell jsgrid-align-center" style="width: 72px;"><input type="text" value="200" title="2024" id="Bays_1359" required="required" class="form-control width-dynamic valid"></td><td class="jsgrid-cell jsgrid-align-center" style="width: auto;"><input type="text" class="form-control dateclass width-dynamic hasDatepicker valid" value="200" title="Thu Jan 11 2024 12:00" id="SubmissionWindowStartDate_1359" required="required" onchange="changevalue(this, 1359)"></td><td class="jsgrid-cell jsgrid-align-center" style="width: auto;"><input type="text" class="form-control dateclass width-dynamic hasDatepicker valid" value="3,400" title="Sun Jan 14 2024 23:59" id="SubmissionWindowEndDate_1359" required="required" onchange="changevalue(this, 1359)"></td><td class="jsgrid-cell jsgrid-align-center" style="width: auto;"><input type="text" class="form-control dateclass hasDatepicker valid" value="1,200" title="Fri Jan 12 2024 12:00" id="SubmissionWindowFirstDeadline_1359" required="required" onchange="changevalue(this, 1359)"></td><td class="jsgrid-cell jsgrid-align-center" style="width: auto;"><input type="text" value="960" title="January Week 1 (January 5-11, 2024)" id="SubmissionWindowDescriptionEN_1359" required="required" class="form-control width-dynamic valid" onchange="changevalue(this, 1359)"></td><td class="jsgrid-cell jsgrid-align-center" style="width: auto;"><input type="text" value="800" title="1re semaine de janvier (5-11 janvier, 2024)" id="SubmissionWindowDescriptionFR_1359" required="required" class="form-control width-dynamic valid" onchange="changevalue(this, 1359)"></td><td class="jsgrid-cell jsgrid-align-center" style="width: 72px;"><input type="text" value="7,640" title="2024" id="Bays_1359" required="required" class="form-control width-dynamic valid"></td><td class="jsgrid-cell jsgrid-align-center" style="width: 72px;"><input type="text" value="33,000" title="2024" id="Bays_1359" required="required" class="form-control width-dynamic valid"></td><td class="jsgrid-cell jsgrid-align-center" style="width: 72px;"><input type="text" value="0.76" title="2024" id="Bays_1359" required="required" class="form-control width-dynamic valid"></td></tr>
                                    <tr class="jsgrid-row"><td>250</td><td class="jsgrid-cell jsgrid-align-center" style="width: 72px;"><input type="text" value="4" title="1359" id="vecShowroom_1359" required="required" class="form-control width-dynamic valid"></td><td class="jsgrid-cell jsgrid-align-center" style="width: 72px;"><input type="text" value="2,000" title="1" id="Showroomsqft_1359" required="required" class="form-control width-dynamic valid"></td><td class="jsgrid-cell jsgrid-align-center" style="width: auto;"><input type="text" value="320" title="Jan Week 1, 2024 (Week 1)" id="ReportingWeekDescriptionEN_1359" required="required" class="form-control width-dynamic valid" onchange="changevalue(this, 1359)"></td><td class="jsgrid-cell jsgrid-align-center" style="width: auto;"><input type="text" value="2,320" title="1re sem. de janv., 2024 (sem. 1)" id="ReportingWeekDescriptionFR_1359" required="required" class="form-control width-dynamic valid" onchange="changevalue(this, 1359)"></td><td class="jsgrid-cell jsgrid-align-center" style="width: auto;"><input type="text" class="form-control datewtclass width-dynamic hasDatepicker valid" value="4" title="Fri Jan 05 2024" id="ReportingWeekStartDate_1359" required="required" onchange="changevalue(this, 1359)"></td><td class="jsgrid-cell jsgrid-align-center" style="width: auto;"><input type="text" class="form-control datewtclass width-dynamic hasDatepicker valid" value="1" title="Thu Jan 11 2024" id="ReportingWeekEndDate_1359" required="required" onchange="changevalue(this, 1359)"></td><td class="jsgrid-cell jsgrid-align-center" style="width: 72px;"><input type="text" value="5" title="MonthNum" id="MonthNo_1359" required="required" class="form-control width-dynamic valid"></td><td class="jsgrid-cell jsgrid-align-center" style="width: 72px;"><input type="text" value="200" title="2024" id="Bays_1359" required="required" class="form-control width-dynamic valid"></td><td class="jsgrid-cell jsgrid-align-center" style="width: auto;"><input type="text" class="form-control dateclass width-dynamic hasDatepicker valid" value="200" title="Thu Jan 11 2024 12:00" id="SubmissionWindowStartDate_1359" required="required" onchange="changevalue(this, 1359)"></td><td class="jsgrid-cell jsgrid-align-center" style="width: auto;"><input type="text" class="form-control dateclass width-dynamic hasDatepicker valid" value="3,400" title="Sun Jan 14 2024 23:59" id="SubmissionWindowEndDate_1359" required="required" onchange="changevalue(this, 1359)"></td><td class="jsgrid-cell jsgrid-align-center" style="width: auto;"><input type="text" class="form-control dateclass hasDatepicker valid" value="1,200" title="Fri Jan 12 2024 12:00" id="SubmissionWindowFirstDeadline_1359" required="required" onchange="changevalue(this, 1359)"></td><td class="jsgrid-cell jsgrid-align-center" style="width: auto;"><input type="text" value="960" title="January Week 1 (January 5-11, 2024)" id="SubmissionWindowDescriptionEN_1359" required="required" class="form-control width-dynamic valid" onchange="changevalue(this, 1359)"></td><td class="jsgrid-cell jsgrid-align-center" style="width: auto;"><input type="text" value="800" title="1re semaine de janvier (5-11 janvier, 2024)" id="SubmissionWindowDescriptionFR_1359" required="required" class="form-control width-dynamic valid" onchange="changevalue(this, 1359)"></td><td class="jsgrid-cell jsgrid-align-center" style="width: 72px;"><input type="text" value="720" title="2024" id="Bays_1359" required="required" class="form-control width-dynamic valid"></td><td class="jsgrid-cell jsgrid-align-center" style="width: 72px;"><input type="text" value="35,000" title="2024" id="Bays_1359" required="required" class="form-control width-dynamic valid"></td><td class="jsgrid-cell jsgrid-align-center" style="width: 72px;"><input type="text" value="0.80" title="2024" id="Bays_1359" required="required" class="form-control width-dynamic valid"></td></tr>
                                    <tr class="jsgrid-row"><td>300</td><td class="jsgrid-cell jsgrid-align-center" style="width: 72px;"><input type="text" value="4" title="1359" id="vecShowroom_1359" required="required" class="form-control width-dynamic valid"></td><td class="jsgrid-cell jsgrid-align-center" style="width: 72px;"><input type="text" value="2,000" title="1" id="Showroomsqft_1359" required="required" class="form-control width-dynamic valid"></td><td class="jsgrid-cell jsgrid-align-center" style="width: auto;"><input type="text" value="320" title="Jan Week 1, 2024 (Week 1)" id="ReportingWeekDescriptionEN_1359" required="required" class="form-control width-dynamic valid" onchange="changevalue(this, 1359)"></td><td class="jsgrid-cell jsgrid-align-center" style="width: auto;"><input type="text" value="2,320" title="1re sem. de janv., 2024 (sem. 1)" id="ReportingWeekDescriptionFR_1359" required="required" class="form-control width-dynamic valid" onchange="changevalue(this, 1359)"></td><td class="jsgrid-cell jsgrid-align-center" style="width: auto;"><input type="text" class="form-control datewtclass width-dynamic hasDatepicker valid" value="4" title="Fri Jan 05 2024" id="ReportingWeekStartDate_1359" required="required" onchange="changevalue(this, 1359)"></td><td class="jsgrid-cell jsgrid-align-center" style="width: auto;"><input type="text" class="form-control datewtclass width-dynamic hasDatepicker valid" value="1" title="Thu Jan 11 2024" id="ReportingWeekEndDate_1359" required="required" onchange="changevalue(this, 1359)"></td><td class="jsgrid-cell jsgrid-align-center" style="width: 72px;"><input type="text" value="5" title="MonthNum" id="MonthNo_1359" required="required" class="form-control width-dynamic valid"></td><td class="jsgrid-cell jsgrid-align-center" style="width: 72px;"><input type="text" value="200" title="2024" id="Bays_1359" required="required" class="form-control width-dynamic valid"></td><td class="jsgrid-cell jsgrid-align-center" style="width: auto;"><input type="text" class="form-control dateclass width-dynamic hasDatepicker valid" value="200" title="Thu Jan 11 2024 12:00" id="SubmissionWindowStartDate_1359" required="required" onchange="changevalue(this, 1359)"></td><td class="jsgrid-cell jsgrid-align-center" style="width: auto;"><input type="text" class="form-control dateclass width-dynamic hasDatepicker valid" value="3,400" title="Sun Jan 14 2024 23:59" id="SubmissionWindowEndDate_1359" required="required" onchange="changevalue(this, 1359)"></td><td class="jsgrid-cell jsgrid-align-center" style="width: auto;"><input type="text" class="form-control dateclass hasDatepicker valid" value="1,200" title="Fri Jan 12 2024 12:00" id="SubmissionWindowFirstDeadline_1359" required="required" onchange="changevalue(this, 1359)"></td><td class="jsgrid-cell jsgrid-align-center" style="width: auto;"><input type="text" value="960" title="January Week 1 (January 5-11, 2024)" id="SubmissionWindowDescriptionEN_1359" required="required" class="form-control width-dynamic valid" onchange="changevalue(this, 1359)"></td><td class="jsgrid-cell jsgrid-align-center" style="width: auto;"><input type="text" value="800" title="1re semaine de janvier (5-11 janvier, 2024)" id="SubmissionWindowDescriptionFR_1359" required="required" class="form-control width-dynamic valid" onchange="changevalue(this, 1359)"></td><td class="jsgrid-cell jsgrid-align-center" style="width: 72px;"><input type="text" value="720" title="2024" id="Bays_1359" required="required" class="form-control width-dynamic valid"></td><td class="jsgrid-cell jsgrid-align-center" style="width: 72px;"><input type="text" value="35,000" title="2024" id="Bays_1359" required="required" class="form-control width-dynamic valid"></td><td class="jsgrid-cell jsgrid-align-center" style="width: 72px;"><input type="text" value="0.80" title="2024" id="Bays_1359" required="required" class="form-control width-dynamic valid"></td></tr>
                                    </table>
                                </div>
                                <p style="text-align:right">
                                    <br/>
                                    <button id="dealersubmitbtn" tkey="Submit" type="submit" value="Submit" class="btn btn-dark primary_btn" style="display: inline-block;">Submit</button>
                                    <button id="reset" tkey="Reset" class="btn btn-outline-secondary secondary_btn">Reset</button>
                                </p>
                            </div>-->
                            <div id="jsGridFacilityStandardData" class="jsgrid">
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
                        </div>
                    </div>
                </div>
            </div>
        </div>`;
  }

  protected onInit(): Promise<void> {
    SPComponentLoader.loadCss("https://mazdausa.sharepoint.com/sites/MCISPOTestSite/dealer360/SiteAssets/css/bootstrap.min.css");
    SPComponentLoader.loadCss("https://mazdausa.sharepoint.com/sites/MCISPOTestSite/dealer360/SiteAssets/css/bootstrap-datetimepicker.min.css");
    SPComponentLoader.loadCss("https://mazdausa.sharepoint.com/sites/MCISPOTestSite/dealer360/SiteAssets/css/bootstrap-theme.min.css");
    SPComponentLoader.loadCss("https://mazdausa.sharepoint.com/sites/MCISPOTestSite/dealer360/SiteAssets/css/corev15.css");
    SPComponentLoader.loadCss("https://mazdausa.sharepoint.com/sites/MCISPOTestSite/dealer360/SiteAssets/css/jquery.datetimepicker.min.css");
    SPComponentLoader.loadCss("https://mazdausa.sharepoint.com/sites/MCISPOTestSite/dealer360/SiteAssets/css/jquery-ui.css");
    SPComponentLoader.loadCss("https://mazdausa.sharepoint.com/sites/MCISPOTestSite/dealer360/SiteAssets/css/jsgrid.min.css");
    SPComponentLoader.loadCss("https://mazdausa.sharepoint.com/sites/MCISPOTestSite/dealer360/SiteAssets/css/jsgrid-theme.min.css");
    SPComponentLoader.loadCss("https://mazdausa.sharepoint.com/sites/MCISPOTestSite/dealer360/SiteAssets/css/pagelayouts15.css");
    SPComponentLoader.loadCss("https://mazdausa.sharepoint.com/sites/MCISPOTestSite/dealer360/SiteAssets/css/responsive.bootstrap.min.css");
    SPComponentLoader.loadCss("https://mazdausa.sharepoint.com/sites/MCISPOTestSite/dealer360/SiteAssets/css/site.css");
    SPComponentLoader.loadCss("https://mazdausa.sharepoint.com/sites/MCISPOTestSite/dealer360/SiteAssets/css/SuiteNav.css");
    
    SPComponentLoader.loadScript("https://mazdausa.sharepoint.com/sites/MCISPOTestSite/dealer360/SiteAssets/js/jquery.js",{globalExportsName:'jquery'}).then(()=>{ 
      SPComponentLoader.loadScript("https://mazdausa.sharepoint.com/sites/MCISPOTestSite/dealer360/SiteAssets/js/jquery-ui.js").then(()=>{      
        SPComponentLoader.loadScript('https://mazdausa.sharepoint.com/sites/MCISPOTestSite/dealer360/SiteAssets/js/lang.js');       
        //SPComponentLoader.loadScript('https://mazdausa.sharepoint.com/sites/MCISPOTestSite/dealer360/SiteAssets/js/PeoplePickerOnline.js');
        SPComponentLoader.loadScript('https://mazdausa.sharepoint.com/sites/MCISPOTestSite/dealer360/SiteAssets/js/jquery.datetimepicker.js');
        //SPComponentLoader.loadScript('https://mazdausa.sharepoint.com/sites/MCISPOTestSite/dealer360/SiteAssets/js/moment.min.js');
        SPComponentLoader.loadScript('https://mazdausa.sharepoint.com/sites/MCISPOTestSite/dealer360/SiteAssets/js/bootstrap.min.js');
        SPComponentLoader.loadScript('https://mazdausa.sharepoint.com/sites/MCISPOTestSite/dealer360/SiteAssets/js/jquery.validate.min.js');
        //SPComponentLoader.loadScript('https://mazdausa.sharepoint.com/sites/MCISPOTestSite/dealer360/SiteAssets/js/jquery.validate.unobtrusive.min.js');
        SPComponentLoader.loadScript("https://mazdausa.sharepoint.com/sites/MCISPOTestSite/dealer360/SiteAssets/js/jsgrid.min.js").then(()=>{
        //SPComponentLoader.loadScript('https://mazdausa.sharepoint.com/sites/MCISPOTestSite/dealer360/SiteAssets/js/Loader.js');
        //SPComponentLoader.loadScript('https://mazdausa.sharepoint.com/sites/MCISPOTestSite/dealer360/SiteAssets/js/PeoplePickerOnline.js');
        SPComponentLoader.loadScript('https://mazdausa.sharepoint.com/sites/MCISPOTestSite/dealer360/SiteAssets/js/FacilityStandard.js');            
        //SPComponentLoader.loadScript('https://mazdausa.sharepoint.com/sites/MCISPOTestSite/dealer360/SiteAssets/js/Utility.js');
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
