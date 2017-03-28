﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tc.Crm.Service.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeXrmEasy;
using Tc.Crm.Service.Services;
using Tc.Crm.ServiceTests;
using System.Net;
using System.Web.Http.Hosting;
using System.Web.Http;
using Tc.Crm.Service.Models;

namespace Tc.Crm.ServiceTests.Controllers
{
    [TestClass()]
    public class SurveyControllerTests
    {

        XrmFakedContext context;
        ISurveyService surveyService;
        SurveyController controller;
        ICrmService crmService;
        IList<SurveyResponse> survey;

        [TestInitialize()]
        public void TestSetup()
        {
            context = new XrmFakedContext();
            surveyService = new SurveyService();
            crmService = new TestCrmService(context);
            controller = new SurveyController(surveyService, crmService);
            controller.Request = new System.Net.Http.HttpRequestMessage();
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            survey = new List<SurveyResponse>();
            survey.Add(new SurveyResponse() { Id = 123, CookieUID = "123", Mode = "WEB" });
        }

        [TestMethod()]
        public void SurveyIsNull()
        {
            var response = controller.Create(null);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
            Assert.AreEqual(((System.Net.Http.ObjectContent)response.Content).Value, Service.Constants.Messages.SurveyDataPassedIsNullOrCouldNotBeParsed);
        }

        [TestMethod()]
        public void SurveyResponseCountIsZero()
        {
            var response = controller.Create(new List<SurveyResponse>());
            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
            Assert.AreEqual(((System.Net.Http.ObjectContent)response.Content).Value, Service.Constants.Messages.SurveyDataPassedIsNullOrCouldNotBeParsed);
        }

        
        [TestMethod()]
        public void SurveyCreated()
        {
           
            TestCrmService service = new TestCrmService(context);
            service.Switch = DataSwitch.Created;
            controller = new SurveyController(surveyService, service);
            controller.Request = new System.Net.Http.HttpRequestMessage();
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            var response = controller.Create(survey);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
        }

        [TestMethod()]
        public void ErrorInMSCrm()
        {
            TestCrmService service = new TestCrmService(context);
            service.Switch = DataSwitch.Response_Failed;
            controller = new SurveyController(surveyService, service);
            controller.Request = new System.Net.Http.HttpRequestMessage();
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            var response = controller.Create(survey);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.GatewayTimeout);
        }

        [TestMethod()]
        public void ActionResponseIsNull()
        {
            TestCrmService service = new TestCrmService(context);
            service.Switch = DataSwitch.Return_NULL;
            controller = new SurveyController(surveyService, service);
            controller.Request = new System.Net.Http.HttpRequestMessage();
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            var response = controller.Create(survey);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.GatewayTimeout);
        }

        
        [TestMethod()]
        public void ActionThrowsException()
        {
            TestCrmService service = new TestCrmService(context);
            service.Switch = DataSwitch.ActionThrowsError;
            controller = new SurveyController(surveyService, service);
            controller.Request = new System.Net.Http.HttpRequestMessage();
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            var response = controller.Create(survey);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.GatewayTimeout);
        }

        [TestMethod()]
        public void ServiceLayerThrowsException()
        {
            TestCrmService service = new TestCrmService(context);
            service.Switch = DataSwitch.Created;
            controller = new SurveyController(surveyService, null);
            controller.Request = new System.Net.Http.HttpRequestMessage();
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            survey = new List<SurveyResponse>();
            survey.Add(new SurveyResponse() { Id = 123, CookieUID = "123", Mode = "WEB" });
            var response = controller.Create(survey);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.InternalServerError);
        }



    }
}
