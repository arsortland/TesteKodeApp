using System;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System.Text.Json;
using System.Text.Json.Serialization; 
using System.Threading.Tasks;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Collections;
using System.Xml.Linq;
using System.Diagnostics;
using Atlassian.Jira;
using System.Security.Cryptography;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;

namespace TesteKodeApp
{
    class Testekode
    {
        //static async Task Main(string[] args)
        //{
        //    var tasks = new List<Task> { TestTask() };
        //    await Task.WhenAll(tasks);
        //}
        public static async Task Main(string[] args)
        {
            await BasicAuthK();
            //Testekode test = new Testekode();
            //string testest = test.REDM_JSON_request();
            //Console.WriteLine(JsonConvert.DeserializeObject(testest));
            //await REDM_API_Request();
            await RTCAS_API_Request();
        }

        public static async Task BasicAuthK()
        {
            string url = "https://support.nov.com/rest/servicedeskapi/servicedesk/11/requesttype/312/field"; //JIRA REDM = "https://jira.nov.com/rest/servicedeskapi/servicedesk/4822/requesttype/14610/field"; 
            var client = new RestClient(url);
            var request = new RestRequest();
            request.AddHeader("Accept", "application/json");

            client.Authenticator = new HttpBasicAuthenticator("risinggaardsortla", "91323212Zenith");
            var restResponse = await client.ExecuteAsync(request);

            //if (restResponse.IsSuccessful)
            //{
                Console.WriteLine(restResponse.StatusCode);
                //Console.WriteLine(restResponse.Content);
                Console.WriteLine(JsonConvert.DeserializeObject(restResponse.Content));
            //}
        }

        public static async Task REDM_API_Request()
        {
            Testekode redmReq = new Testekode();
            string jsonredm = redmReq.REDM_JSON_request();

           
            string url = "https://jira.nov.com/rest/servicedeskapi/request";
            var client = new RestClient(url);
            var request = new RestRequest();
            //request.AddHeader("Content-type", "application/json");
            request.AddStringBody(jsonredm, DataFormat.Json);
            //request.AddJsonBody(jsonredm);

            client.Authenticator = new HttpBasicAuthenticator("risinggaardsortla", "91323212Zenith");
            var restResponse = await client.ExecutePostAsync(request);

            //if (restResponse.IsSuccessful)
            //{
            //Console.WriteLine(restResponse.Content);  ///HVA BLIR FAKTISK SENDT.
            //Console.WriteLine(restResponse.StatusCode);
            //Console.WriteLine(restResponse.ErrorException);
            //Console.WriteLine(request.ToString());
            Console.WriteLine(JsonConvert.DeserializeObject(restResponse.Content));
            //}

        }

        public static async Task RTCAS_API_Request()
        {


            string url = "https://jira.nov.com/rest/servicedeskapi/request";
            var client = new RestClient(url);
            var request = new RestRequest();

            client.Authenticator = new HttpBasicAuthenticator("risinggaardsortla", "91323212Zenith");
            var restResponse = await client.ExecutePostAsync(request);

            //if (restResponse.IsSuccessful)
            //{
            //Console.WriteLine(restResponse.Content);  ///HVA BLIR FAKTISK SENDT.
            //Console.WriteLine(restResponse.StatusCode);
            //Console.WriteLine(restResponse.ErrorException);
            Console.WriteLine(JsonConvert.DeserializeObject(restResponse.Content));
            //}

        }

        public string REDM_JSON_request()
        {

            REDM redm = new REDM()
            {
                summary = "12345678-001",
                customfield_16671 = "[{\"value\": \"45893\"}]", //Legge til hele arrayet som en validValue? PÅ alle punktene? Prøve å lage en god gammaldags string for dette tror jeg.
                customfield_16665 = "[{\"value\": \"40143\"}]",
                customfield_10040 = "10122",
                customfield_13564 = "44068",
                customfield_12699 = "[44010]",
                customfield_14114 = "16524",
                customfield_15509 = "24911",
                customfield_14471 = "18285",
                description = "Dette er en test av CADJIRA",
                customfield_14361 = "17343",
                customfield_10361 = "44501",
                customfield_14385 = "[{\"value\": \"1750\"}]",
                customfield_13664 = 1,
            };

            string stringredm = JsonConvert.SerializeObject(redm);
            string fullJsonreq = $"{{\"serviceDeskId\":\"4822\",\"requestTypeId\":\"14610\",\"requestFieldValues\":{stringredm}}}";
            return fullJsonreq;
        }
    }


    class REDM
    {

        public string summary { get; set; } //PN
        public string customfield_16671 { get; set; } //Urgency
        public string customfield_16665 { get; set; } //Impact

        public string customfield_10040 { get; set; } //NOV Facility
        public string customfield_13564 { get; set; } //Type of Change
        public string customfield_12699 { get; set; } //Business segment
        public string customfield_14114 { get; set; } // NOV mfg location
        public string customfield_15509 { get; set; } //Requires Global ID
        public string customfield_14471 { get; set; } //Item Type
        public string description { get; set; } //Description
        public string customfield_14361 { get; set; } //Enable part in inventory Org
        public string customfield_10361 { get; set; } //Oracle Inventory
        //Skipping CAD DAtaset
        public string customfield_14385 { get; set; } //Part of workflow?
        //Skipping productline
        //Skipping replication
        //Skipping ERP
        //Skipping documentatiion
        public int customfield_13664 { get; set; } //Number of line items.
        //Skipping include ppl in ticket.

        //CANRAISEBEHALFOF?? USE THAT AS AUTH?

    }
}

//API TOKEN2: Dh5GTGZ56tN15TESboIY9897



//ERLING SIN AD KODE PER MAIL::

//PrincipalContext context = new PrincipalContext(ContextType.Domain, "nov.com");
//List<string> lines = await Task.Run(() => {
//    return AllReportsLines(context, user.Name, userInfo, progress);
//});



//private List<string> AllReportsLines(PrincipalContext context, string identity, bool userInfo, IProgress<string> progress)
//{
//    UserPrincipal userPrincipal = UserPrincipal.FindByIdentity(context, identity);
//    List<string> lines = new List<string>();
//    if (userPrincipal?.Enabled == true && userPrincipal.EmailAddress != null)
//    {
//        PropertyValueCollection directReports;
//        string title = "";
//        using (var entry = (DirectoryEntry)userPrincipal.GetUnderlyingObject())
//        {
//            directReports = entry.Properties["directReports"];
//            title = entry.Properties["Title"].Value.ToString();
//        }
//        string line = userPrincipal.EmailAddress;
//        if (userInfo)
//            line += $";{userPrincipal.SamAccountName};{userPrincipal.DisplayName};{userPrincipal.EmployeeId};{title}";
//        lines.Add(line);
//        progress.Report("1");
//        if (directReports.Count > 0)
//        {
//            foreach (object directReport in directReports)
//            {
//                lines.AddRange(AllReportsLines(context, (string)directReport, userInfo, progress));
//            }
//        }
//    }
//    return lines;
//}


//Prøve dette i CADJIRA I morgen.