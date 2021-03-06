﻿using Tc.Crm.Common.IntegrationLayer.Jti.Models;
using Tc.Crm.Common.IntegrationLayer.Model;

namespace Tc.Crm.Common.IntegrationLayer.Jti.Service
{
    public interface IJwtService
    {
        string CreateJwtToken<T>(string privateKey, T payloadObj) where T : JsonWebTokenPayloadBase;
        ResponseEntity SendHttpRequest(HttpMethod method, string serviceUrl, string token, string data, string correlationId);
        ResponseEntity SendHttpRequest(HttpMethod method, string serviceUrl, string token, string data);
        double GetExpiry(string expiredSeconds);
        double GetIssuedAtTime();
        double GetNotBeforeTime(string notBeforeSeconds);
    }
}