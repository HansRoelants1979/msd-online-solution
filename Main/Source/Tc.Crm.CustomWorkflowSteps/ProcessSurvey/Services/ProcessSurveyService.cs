﻿using System;
using System.Collections.Generic;
using Microsoft.Xrm.Sdk;
using Tc.Crm.CustomWorkflowSteps.ProcessSurvey.Models;
using System.ServiceModel;

namespace Tc.Crm.CustomWorkflowSteps.ProcessSurvey.Services
{
    public class ProcessSurveyService
    {
        private PayloadSurvey payloadSurvey;
        private ITracingService trace;

        public ProcessSurveyService(PayloadSurvey payloadSurvey)
        {
            this.payloadSurvey = payloadSurvey;
            this.trace = payloadSurvey.Trace;
        }

        /// <summary>
        /// To process survey information
        /// </summary>
        /// <param name="json"></param>       
        /// <returns></returns>
        public string ProcessSurveyResponse()
        {
            trace.Trace("Processing Process payload - start");
            if (payloadSurvey == null) throw new InvalidPluginExecutionException("payloadSurvey is null;");
            var failedSurveys = ProcessResponses();
            trace.Trace("Processing Process payload - end");
            return JsonHelper.SerializeSurveyJson(new SurveyReturnResponse() { FailedSurveys= failedSurveys }, trace);
        }

        /// <summary>
        /// To process all survey responses
        /// </summary>
        private List<FailedSurvey> ProcessResponses()
        {
            trace.Trace("Processing ProcessResponses - start");
            if (payloadSurvey.SurveyResponse == null) throw new InvalidPluginExecutionException("SurveyResponse is null in json payload");
            if (payloadSurvey.SurveyResponse.Responses == null) throw new InvalidPluginExecutionException("Response object in payload json is null");
            List<Response> responses = payloadSurvey.SurveyResponse.Responses;
            var failedSurveys = new List<FailedSurvey>();
            for (int i = 0; i < responses.Count; i++)
            {
                try
                {
                    trace.Trace("Processing Response " + i + " - start");
                    var surveyResponse = SurveyResponseHelper.GetResponseEntityFromPayload(responses[i], trace);
                    if (responses[i].Answers != null && responses[i].Answers.Count > 0)
                    {
                        MapBookingContact(surveyResponse, responses[i]);
                        ProcessFeedback(surveyResponse, responses[i].Answers);
                    }
                    CommonXrm.CreateRecord(surveyResponse, payloadSurvey.CrmService);
                    trace.Trace("Processing Response " + i + " - end");
                }
                catch (FaultException<OrganizationServiceFault> ex)
                {
                    failedSurveys.Add(PrepareFailedSurveys(responses[i].SurveyId, ex.ToString()));
                }
                catch (TimeoutException ex)
                {
                    failedSurveys.Add(PrepareFailedSurveys(responses[i].SurveyId, ex.ToString()));
                }
                catch (Exception ex)
                {
                    failedSurveys.Add(PrepareFailedSurveys(responses[i].SurveyId, ex.ToString()));
                }
            }           
                    
            trace.Trace("Processing ProcessResponses - end");
            return failedSurveys;
        }

        

        /// <summary>
        /// To prepare failed surveys information
        /// </summary>
        /// <param name="survey"></param>
        /// <param name="failedSurveys"></param>
        /// <returns></returns>
        private FailedSurvey PrepareFailedSurveys(long? surveyId, string exception)
        {
            if (surveyId != null && surveyId.HasValue)
                return new FailedSurvey { SurveyId = surveyId.Value.ToString(), Exception = exception };
            else
                return new FailedSurvey { Exception = exception };
        }

        /// <summary>
        /// To map booking and contact records
        /// </summary>
        /// <param name="surveyResponse"></param>
        /// <param name="answers"></param>
        private void MapBookingContact(Entity surveyResponse, Response response)
        {
            trace.Trace("Processing MapBookingContact - start");
            var bookingNumber = AnswerHelper.GetBookingNumber(response.Answers,trace);
            var sourceMarket = AnswerHelper.GetSourceMarket(response.Answers, trace);
            var tourOperator = AnswerHelper.GetTourOperator(response.Answers, trace);
            var brand = AnswerHelper.GetBrand(response.Answers, trace);
            var lastName = ContactHelper.GetLastName(response.Contact,trace);           

            if (string.IsNullOrWhiteSpace(bookingNumber)) return;
            if (string.IsNullOrWhiteSpace(sourceMarket)) return;
            if (string.IsNullOrWhiteSpace(tourOperator)) return;
            if (string.IsNullOrWhiteSpace(brand)) return;

            FetchBookingContact(bookingNumber, sourceMarket, tourOperator, brand, lastName, surveyResponse);
            
            trace.Trace("Processing MapBookingContact - end");
        }


        /// <summary>
        /// To fetch booking, contact
        /// </summary>
        /// <param name="bookingNumber"></param>
        /// <param name="sourceMarket"></param>
        /// <param name="tourOperator"></param>
        /// <param name="brand"></param>
        /// <param name="lastName"></param>
        /// <param name="email"></param>
        /// <param name="surveyResponse"></param>
        private void FetchBookingContact(string bookingNumber,string sourceMarket, string tourOperator, string brand,  string lastName, Entity surveyResponse)
        {
            trace.Trace("Processing FetchBookingContact - start");
            var contactCondition = PrepareContactCondition(lastName);
            var sourceMarketCondition = PrepareSourceMarketCondition(sourceMarket);
            var tourOperatorCondition = PrepareTourOperatorCondition(tourOperator);
            var brandCondition = PrepareBrandCondition(brand);
            var query = $@"<fetch output-format='xml-platform' distinct='false' version='1.0' mapping='logical'>
                            <entity name='tc_customerbookingrole'>
                                <link-entity name='tc_booking' alias='booking' from='tc_bookingid' to='tc_bookingid'>
                                  <attribute name='tc_bookingid' />                                               
                                    <filter type = 'and'>
                                      <condition attribute='tc_name' operator='eq' value='{bookingNumber}' />
                                    </filter>
                            {sourceMarketCondition}
                            {tourOperatorCondition}
                            {brandCondition}
                                </link-entity>                           
                            {contactCondition}
                            </entity>
                           </fetch>";

            var entColBookingContact = CommonXrm.RetrieveMultipleRecordsFetchXml(query, payloadSurvey.CrmService);
            MapBooking(surveyResponse, entColBookingContact);
            MapContact(surveyResponse, entColBookingContact);
            trace.Trace("Processing FetchBookingContact - end");
        }

        /// <summary>
        ///  To prepare link entity for contact when contact last name and email is not empty
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        private string PrepareContactCondition(string lastName)
        {
            var contactCondition = string.Empty;
            if (!string.IsNullOrWhiteSpace(lastName))
            {
                contactCondition = $@"<link-entity name='contact' alias='contact' from='contactid' to='tc_customer' link-type='outer'>
                                      <attribute name='contactid'/>
                                        <filter type='and'>
                                            <condition attribute='lastname' operator='eq' value='{lastName}' />
                                         </filter>
                                      </link-entity>";
            }
            return contactCondition;
        }

        /// <summary>
        /// To prepare link entity for Source Market
        /// </summary>
        /// <param name="sourceMarket"></param>
        /// <returns></returns>
        private string PrepareSourceMarketCondition(string sourceMarket)
        {
            var sourceMarketCondition = string.Empty;
            if (!string.IsNullOrWhiteSpace(sourceMarket))
            {
                sourceMarketCondition = $@"<link-entity name='tc_country' alias='aa' from='tc_countryid' to='tc_sourcemarketid'>
                                           <filter type='and'>
                                            <condition attribute='tc_iso2code' operator='eq' value='{sourceMarket}' />
                                           </filter>
                                           </link-entity>";
            }
            return sourceMarketCondition;
        }

        /// <summary>
        /// To prepare link entity for brand 
        /// </summary>
        /// <param name="brand"></param>
        /// <returns></returns>
        private string PrepareBrandCondition(string brand)
        {
            var brandCondition = string.Empty;
            if (!string.IsNullOrWhiteSpace(brand))
            {
                brandCondition = $@"<link-entity name='tc_brand' alias='ab' from='tc_brandid' to='tc_brandid'>
                                    <filter type='and'>
                                     <condition attribute='tc_brandcode' operator='eq' value='{brand}' />
                                    </filter>
                                    </link-entity>";
            }
            return brandCondition;
        }

        /// <summary>
        /// To prepare link entity for Tour operator
        /// </summary>
        /// <param name="tourOperatorCode"></param>
        /// <returns></returns>
        private string PrepareTourOperatorCondition(string tourOperatorCode)
        {
            var tourOperatorCondition = string.Empty;
            if (!string.IsNullOrWhiteSpace(tourOperatorCode))
            {
                tourOperatorCondition = $@"<link-entity name='tc_touroperator' alias='ac' from='tc_touroperatorid' to='tc_touroperatorid'>
                                           <filter type='and'>
                                            <condition attribute='tc_touroperatorcode' operator= 'eq' value = '{tourOperatorCode}' />
                                           </filter>
                                           </link-entity>";
            }
            return tourOperatorCondition;
        }


        /// <summary>
        /// To map booking to survey response
        /// </summary>
        /// <param name="surveyResponse"></param>
        /// <param name="entColBookingContact"></param>
        private void MapBooking(Entity surveyResponse, EntityCollection entColBookingContact)
        {
            trace.Trace("Processing MapBooking - start");
            if (entColBookingContact != null && entColBookingContact.Entities.Count > 0)
            {
                var fieldBooking = AliasName.Booking + Attributes.Booking.BookingId;
                var entity = entColBookingContact.Entities[0];
                if (entity != null && entity.Attributes.Contains(fieldBooking) && entity.Attributes[fieldBooking] != null)
                {
                    var booking = (AliasedValue)entity.Attributes[fieldBooking];
                    surveyResponse[Attributes.SurveyResponse.BookingId] = new EntityReference(booking.EntityLogicalName, Guid.Parse(booking.Value.ToString()));
                }
            }
            trace.Trace("Processing MapBooking - end");
        }

        /// <summary>
        /// To map contact to survey response (when only one contact found)
        /// </summary>
        /// <param name="surveyResponse"></param>
        /// <param name="entColBookingContact"></param>
        private void MapContact(Entity surveyResponse, EntityCollection entColBookingContact)
        {
            trace.Trace("Processing MapContact - start");
            if (entColBookingContact != null && entColBookingContact.Entities.Count > 0)
            {
                var fieldContact = AliasName.Contact + Attributes.Contact.ContactId;
                var previousContactId = Guid.Empty;
                var currentContactId = Guid.Empty;
                AliasedValue contact = null;
                for (int i = 0; i < entColBookingContact.Entities.Count; i++)
                {
                    var entity = entColBookingContact.Entities[i];
                    if (entity != null && entity.Attributes.Contains(fieldContact) && entity.Attributes[fieldContact] != null)
                    {
                        contact = (AliasedValue)entity.Attributes[fieldContact];
                        currentContactId = Guid.Parse(contact.Value.ToString());

                        if(previousContactId == Guid.Empty)
                            previousContactId = Guid.Parse(contact.Value.ToString());

                        if (previousContactId != currentContactId)
                            return;
                    }
                }
                var customer = GetPartyList(contact);
                if (customer != null && customer.Entities.Count > 0)
                    surveyResponse[Attributes.SurveyResponse.CustomerId] = customer;
            }
            trace.Trace("Processing MapContact - end");
        }


        /// <summary>
        /// To prepare partylist for contact
        /// </summary>
        /// <param name="aliasValue"></param>
        /// <returns></returns>
        private EntityCollection GetPartyList(AliasedValue aliasValue)
        {
            trace.Trace("Processing GetPartyList - start");
            var customers = new EntityCollection();
            if (aliasValue != null)
            {   
                var party = new Entity(EntityName.ActivityParty);
                party[Attributes.ActivityParty.PartyId] = new EntityReference() { LogicalName = aliasValue.EntityLogicalName, Id = Guid.Parse(aliasValue.Value.ToString()) };
                customers.Entities.Add(party);
            }
            trace.Trace("Processing GetPartyList - end");
            return customers;
        }

        /// <summary>
        /// To process feedback information
        /// </summary>
        /// <param name="surveyResponse"></param>
        /// <param name="answers"></param>
        private void ProcessFeedback(Entity surveyResponse, List<Answer> answers)
        {
            trace.Trace("Processing ProcessFeedback - start");
            var feedbackCollection = ProcessAnswers(answers);
            if (feedbackCollection != null && feedbackCollection.Entities.Count > 0)
                surveyResponse.RelatedEntities.Add(new Relationship(Relationships.SurveyResponseFeedback), feedbackCollection);
            trace.Trace("Processing ProcessFeedback - end");
        }

        /// <summary>
        /// To process answer records
        /// </summary>
        /// <param name="answers"></param>
        /// <returns></returns>
        private EntityCollection ProcessAnswers(List<Answer> answers)
        {
            trace.Trace("Processing ProcessAnswers - start");
            var feedbackCollection = new EntityCollection();
            if (answers != null)
            {
                for (int i = 0; i < answers.Count; i++)
                {
                    trace.Trace("Processing Answer " + i + " - start");
                    var feedback = AnswerHelper.GetFeedbackEntityFromPayload(answers[i],trace);                    
                    feedbackCollection.Entities.Add(feedback);
                    trace.Trace("Processing Answer " + i + " - end");
                }                
            }
            trace.Trace("Processing ProcessAnswers - end");
            return feedbackCollection;
        }

    }
}
