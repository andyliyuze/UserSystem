﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace UserSystem.Infrastructure
{
    public class WebApiResponseHelper
    {
        public async Task<HttpResponseMessage> CreateHttpResponse<T>(HttpRequestMessage request, Func<Task<T>> function)
        {
            HttpResponseMessage response = null;

            try
            {
                var result = await function.Invoke();
                var resp = new WebApiResponse<T>() { Result = result, ErrCode = WebApiStateCode.Success, Msg = "OK" };
                response = request.CreateResponse<WebApiResponse<T>>(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                var resp = new WebApiResponse<string>() { Result = "", ErrCode = WebApiStateCode.Failed, Msg = JsonConvert.SerializeObject(ex) };
                response = request.CreateResponse<WebApiResponse<string>>(HttpStatusCode.OK, resp);
            }

            return response;
        }
    }
    
    public enum WebApiStateCode
    {
        Success,
        Failed
    }
}
