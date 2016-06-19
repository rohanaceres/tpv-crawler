using NSoup;
using NSoup.Nodes;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Tpv.Crawler
{
    public class TpvCrawler
    {
        /// <summary>
        /// Uri de login da dashboard.
        /// </summary>
        internal Uri TpvDashboardUri { get; set; }

        /// <summary>
        /// Cria a propriedade <see cref="TpvDashboardUri"/>
        /// </summary>
        public TpvCrawler()
        {
            this.TpvDashboardUri = new Uri(@"https://dashboards.stone.com.br:9002/viewer/login");
        }
        
        /// <summary>
        /// Faz o login na dashboard da Stone.
        /// </summary>
        /// <param name="userName">Usuário</param>
        /// <param name="password">Senha do usuário</param>
        /// <returns>Se o login foi bem sucedido ou não.</returns>
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
