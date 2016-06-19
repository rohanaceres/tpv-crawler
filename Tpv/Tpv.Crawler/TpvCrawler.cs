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
            // Faz o request inicial, para pegar infos necessárias no login:
            IResponse initialResponse = NSoupClient.Connect(this.TpvDashboardUri.AbsoluteUri)
                .Method(Method.Get)
                .Execute();
            Document doc = initialResponse.Parse();
            
            // Pega o token:
            string antiforgery = doc.Select("input[id=antiforgery]").First.Attributes.GetValue("value");

            // Pega os cookies:
            IDictionary<string, string> cookies = initialResponse.Cookies();

            // Envia o post de login:
            IResponse loginResponse = NSoupClient.Connect(this.TpvDashboardUri.AbsoluteUri)
                .Data("user", userName)
                .Data("password", password)
                .Data("__RequestVerificationToken", antiforgery)
                .Cookies(cookies)
                .Method(Method.Post)
                .Execute();

            // Verifica se o login deu certo ou não:
            if (loginResponse.StatusCode() == System.Net.HttpStatusCode.OK) { return true; }

            return false;
        }
    }
}
