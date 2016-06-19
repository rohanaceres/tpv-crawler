using NSoup;
using NSoup.Nodes;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Tpv.Crawler
{
    public class TpvCrawler
    {
        internal Uri TpvDashboardUri { get; set; }

        public TpvCrawler()
        {
            this.TpvDashboardUri = new Uri(@"https://dashboards.stone.com.br:9002/viewer/login");
        }

        public bool Login (string userName, string password)
        {
            IResponse initialResponse = NSoupClient.Connect(this.TpvDashboardUri.AbsoluteUri)
                .Method(Method.Get)
                .Execute();
            Document doc = initialResponse.Parse();
            Element execution = doc.Select("input[id=antiforgery]").First;

            string afval = execution.Attributes.GetValue("value");

            IDictionary<string, string> cookies = initialResponse.Cookies();

            Debug.WriteLine(initialResponse.StatusCode());

            IResponse loginResponse = NSoupClient.Connect(this.TpvDashboardUri.AbsoluteUri)
                .Data("user", userName)
                .Data("password", password)
                .Data("__RequestVerificationToken", afval)
                .Cookies(cookies)
                .Method(Method.Post)
                .Execute();

            Debug.WriteLine(loginResponse.StatusCode());

            return true;
        }
    }
}
