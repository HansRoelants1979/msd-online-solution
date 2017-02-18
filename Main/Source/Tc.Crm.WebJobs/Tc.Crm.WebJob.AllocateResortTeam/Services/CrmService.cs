﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Tc.Crm.WebJob.AllocateResortTeam.Models;
using Tc.Crm.WebJob.AllocateResortTeam.Services;
using Microsoft.Xrm.Tooling.Connector;
using System.Configuration;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Messages;

namespace Tc.Crm.WebJob.AllocateResortTeam
{
    public class CrmService : ICrmService
    {
        IOrganizationService organizationService;
        IConfigurationService configurationService;
        ILogger logger;
        public CrmService(IConfigurationService configurationService, ILogger logger)
        {
            this.configurationService = configurationService;
            this.organizationService = GetOrganizationService();
            this.logger = logger;
        }

        public IList<BookingAllocationResponse> GetBookingAllocations(BookingAllocationRequest bookingRequest)
        {            

            string query = @"<fetch distinct='true' version='1.0' output-format='xml-platform' mapping='logical'>
                             <entity name='tc_booking'>
                                <attribute name='tc_bookingid'/>
                                <attribute name='tc_name'/>
                                <attribute name='ownerid'/>
                                <order attribute='tc_name' descending='false'/>
                                <filter type='and'>
                                    <filter type='and'>
                                            <filter type='or'>
                                            <condition attribute='tc_departuredate' operator='next-x-days' value='9'/>
                                                <filter type='and'>
                                                    <condition attribute='tc_departuredate' operator='on-or-after' value='2017-02-18'/>
                                                    <condition attribute='tc_returndate' operator='on-or-before' value='2017-02-18'/>
                                                </filter>
                                            </filter>
                                        <condition attribute='tc_destinationgatewayid' operator='in'>
                                        <value uiname='CDG' uitype='tc_gateway'>{54218CAD-37ED-E611-80FE-3863BB354FF0}</value>
                                        <value uiname='MEX' uitype='tc_gateway'>{FF1B2B35-D7EE-E611-8106-3863BB34FB48}</value>
                                        <value uiname='OPO' uitype='tc_gateway'>{F0D11EF9-D9EE-E611-8106-3863BB34FB48}</value>
                                        </condition>
                                    </filter>
                                </filter>
                                <link-entity name='tc_bookingaccommodation' alias='ad' from='tc_bookingid' to='tc_bookingid'>
                                    <attribute name='tc_startdateandtime'/>
                                    <attribute name='tc_enddateandtime'/>
                                        <link-entity name='tc_hotel' alias='ae' from='tc_hotelid' to='tc_hotelid'>
                                            <attribute name='ownerid'/>
                                        </link-entity>
                                </link-entity>
                                <link-entity name='tc_customerbookingrole' alias='af' from='tc_bookingid' to='tc_bookingid'>
                                    <attribute name='tc_customer'/>
                                </link-entity>
                             </entity>
                             </fetch>";

           EntityCollection bookings = RetrieveMultipleRecordsFetchXml(query);           

           return PrepareBookingAllocation(bookings);

        }

        public IList<BookingAllocationResponse> PrepareBookingAllocation(EntityCollection bookingCollection)
        {
            IList <BookingAllocationResponse> bookingAllocationResponse = null;
            if (bookingCollection != null && bookingCollection.Entities.Count > 0)
            {
                bookingAllocationResponse = new List<BookingAllocationResponse>();
                foreach (Entity booking in bookingCollection.Entities)
                {
                    var response = new BookingAllocationResponse();
                    if (booking.Attributes.Contains(Attributes.Booking.BookingId) && booking.Attributes[Attributes.Booking.BookingId] != null)
                        response.BookingId = Guid.Parse(booking.Attributes[Attributes.Booking.BookingId].ToString());
                    if (booking.Attributes.Contains(Attributes.BookingAccommodation.StartDateandTime) && booking.Attributes[Attributes.BookingAccommodation.StartDateandTime] != null)
                        response.AccommodationStartDate = DateTime.Parse(booking.Attributes[Attributes.BookingAccommodation.StartDateandTime].ToString());
                    if (booking.Attributes.Contains(Attributes.BookingAccommodation.EndDateandTime) && booking.Attributes[Attributes.BookingAccommodation.EndDateandTime] != null)
                        response.AccommodationEndDate = DateTime.Parse(booking.Attributes[Attributes.BookingAccommodation.EndDateandTime].ToString());
                    if (booking.Attributes.Contains(Attributes.Hotel.Owner) && booking.Attributes[Attributes.Hotel.Owner] != null)
                    {
                        EntityReference owner = (EntityReference)booking.Attributes[Attributes.Hotel.Owner];
                        OwnerType ownerType;

                        if (owner.LogicalName == EntityName.User)
                            ownerType = OwnerType.User;
                        else
                            ownerType = OwnerType.Team;

                        response.OwnerId = new Owner() { Id = owner.Id,Name=owner.Name,OwnerType= ownerType};
                    }

                    bookingAllocationResponse.Add(response);
                }
            }

            return bookingAllocationResponse;
        }

        public EntityCollection RetrieveMultipleRecords(string entityName, string[] columns, string[] filterKeys, string[] filterValues)
        {
            var query = new QueryExpression(entityName);
            query.ColumnSet = new ColumnSet(columns);


            for (int i = 0; i < filterKeys.Length; i++)
            {
                var condExpr = new ConditionExpression();
                condExpr.AttributeName = filterKeys[i];
                condExpr.Operator = ConditionOperator.Equal;
                condExpr.Values.Add(filterValues[i]);

                var fltrExpr = new FilterExpression(LogicalOperator.And);
                fltrExpr.AddCondition(condExpr);

                query.Criteria.AddFilter(fltrExpr);
            }
            return GetRecordsUsingQuery(query);

        }

        public EntityCollection RetrieveMultipleRecordsFetchXml(string query)
        {
            //paging has to be implimented 
            FetchExpression fetch = new FetchExpression(query);
            return organizationService.RetrieveMultiple(fetch);

        }
        public EntityCollection GetRecordsUsingQuery(QueryExpression queryExpr)
        {
            int pageNumber = 1;
            int recordCount = 1;
            queryExpr.PageInfo = new PagingInfo();
            queryExpr.PageInfo.PageNumber = pageNumber;
            queryExpr.PageInfo.Count = recordCount;
            queryExpr.PageInfo.PagingCookie = null;
            EntityCollection entityCollection = null;
            while (true)
            {
                entityCollection = organizationService.RetrieveMultiple(queryExpr);

                // Check for more records, if it returns true.
                if (entityCollection.MoreRecords)
                {
                    // Increment the page number to retrieve the next page.
                    queryExpr.PageInfo.PageNumber++;

                    // Set the paging cookie to the paging cookie returned from current results.
                    queryExpr.PageInfo.PagingCookie = entityCollection.PagingCookie;
                }
                else
                {
                    // If no more records are in the result nodes, exit the loop.
                    break;
                }

            }

            return entityCollection;
        }


        public ExecuteMultipleResponse BulkUpdate(EntityCollection entityCollection)
        {
            ExecuteMultipleRequest request = new ExecuteMultipleRequest()
            {
                Settings = new ExecuteMultipleSettings()
                {
                    ContinueOnError = true,
                    ReturnResponses = true
                },
                Requests = new OrganizationRequestCollection()
            };

            for (int i = 0; i < entityCollection.Entities.Count; i++)
            {
                request.Requests.Add(new UpdateRequest() { Target = entityCollection[i] });
            }

            return (ExecuteMultipleResponse)organizationService.Execute(request);
        }



        public IOrganizationService GetOrganizationService()
        {
            if (organizationService != null) return organizationService;

            var connectionString = configurationService.ConnectionString;
            CrmServiceClient client = new CrmServiceClient(connectionString);
            return (IOrganizationService)client;
        }




        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;
            if (disposing)
            {
                DisposeObject(organizationService);
                DisposeObject(configurationService);
                DisposeObject(logger);
            }

            disposed = true;
        }

        void DisposeObject(Object obj)
        {
            if (obj != null)
            {
                if (obj is IDisposable)
                    ((IDisposable)obj).Dispose();
                else
                    obj = null;
            }

        }
    }

    public static class EntityName
    {

        public const string Booking = "tc_booking";
        public const string BookingAccommodation = "tc_bookingaccommodation";
        public const string CustomerBookingRole = "tc_customerbookingrole";
        public const string Hotel = "tc_hotel";
        public const string Team = "team";
        public const string User = "systemuser";

    }

    public static class Attributes
    {
        public class Booking
        {
            public const string BookingId = "tc_bookingid";
            public const string Owner = "ownerid";
            public const string Name = "tc_name";
            public const string DepartureDate = "tc_departuredate";
            public const string DestinationId = "tc_destinationid";
            public const string ReturnDate = "tc_returndate";
        }

        public class BookingAccommodation
        {
            public const string BookingAccommodationid = "tc_bookingaccommodationid";
            public const string BookingId = "tc_bookingid";
            public const string EndDateandTime = "tc_enddateandtime";
            public const string HotelId = "tc_hotelid";
            public const string StartDateandTime = "tc_startdateandtime";
            public const string Owner = "ownerid";

        }

        public class CustomerBookingRole
        {
            public const string BookingId = "tc_bookingid";
            public const string Customer = "tc_customer";
            public const string Role = "tc_customerbookingrole";
            public const string CustomerBookingRoleId = "tc_customerbookingroleid";
            public const string Name = "tc_name";
        }

        public class Hotel
        {
           
            public const string HotelId = "tc_hotelid";
            public const string LocationId = "tc_locationid";
            public const string MasterHotelID = "tc_masterhotelid";
            public const string Name = "tc_name";
            public const string SourceMarketHotelID = "tc_sourcemarkethotelid";
            public const string ResortTeam = "tc_teamid";
            public const string Owner = "ownerid";

        }
        public class Team
            {
            public const string TeamId = "teamid";
            public const string Name = "name";
            }
    }
}

