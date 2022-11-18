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
        public static async Task Main(string[] args)
        {
            await BasicAuthK();
            //ObjectRigEDM();

            //Testekode test = new Testekode();
            //string testest = test.REDM_JSON_request();
            //Console.WriteLine(JsonConvert.DeserializeObject(testest));
            //await REDM_API_Request();
            //await RTCAS_API_Request();
            await API_Request("redm");
        }
        public static async Task BasicAuthK()
        {
            string url = "https://jira.nov.com/rest/servicedeskapi/servicedesk/4822/requesttype/14610/field"; //GCSET"https://support.nov.com/rest/servicedeskapi/servicedesk/11/requesttype/1112/field"; //JIRA REDM = "https://jira.nov.com/rest/servicedeskapi/servicedesk/4822/requesttype/14610/field"; 
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
        public static async Task API_Request(string choice_made)
        {
            string url = "";
            string json = "";
            if (choice_made == "redm")
            {
                url = "https://jira.nov.com/rest/servicedeskapi/request";
                json = ObjectRigEDM();
            }
            if (choice_made == "gcs")
            {
                url = "https://support.nov.com/rest/servicedeskapi/request";
                json = "";
            }
            var client = new RestClient(url);
            var request = new RestRequest();
            request.AddStringBody(json, DataFormat.Json);

            client.Authenticator = new HttpBasicAuthenticator("risinggaardsortla", "91323212Zenith");

            var restResponse = await client.ExecutePostAsync(request);

            Console.WriteLine(JsonConvert.DeserializeObject(restResponse.Content)); //DENNE MÅ VEKK PÅ ET TIDSPUNKT -  FINNE URL FRA DENNE.
        }

        public static async Task REDM_API_Request()
        {
            Console.WriteLine(ObjectRigEDM());

            string url = "https://jira.nov.com/rest/servicedeskapi/request";
            var client = new RestClient(url);
            var request = new RestRequest();
            request.AddStringBody(ObjectRigEDM(), DataFormat.Json);

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

        //BARE HA EN "API_REQUEST" OG ENDRE -JSON- og -URL- VARIABELEN AVHENGIG AV REDM OG/ELLER GCSET.
        public static async Task RTCAS_API_Request()
        {
            string json = "{ \"serviceDeskId\": \"11\",\"requestTypeId\": \"1112\", \"requestFieldValues\": { \"customfield_11869\": {\"value\": \"Other\"}, \"description\": \"Greetings from CADJIRA API TEST\"  } }";
            Console.WriteLine(json);

            string url = "https://support.nov.com/rest/servicedeskapi/request";
            var client = new RestClient(url);
            var request = new RestRequest();
            request.AddStringBody(json, DataFormat.Json);

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
        public static string ObjectRigEDM()
        {
            string jsonstring = "{" +
                "\"serviceDeskId\": \"4822\"," +
                "\"requestTypeId\": \"14610\"," +
                "\"requestFieldValues\": {" +
                "\"summary\": \"12345678-001\"," +
                "\"customfield_16671\": {\"value\": \"No Business Disruption - Workaround Available\"}," +
                "\"customfield_16665\": {\"value\": \"Impacts Me or a Single Person\"}," +
                "\"customfield_10040\": {\"value\": \"Norway\"}," +
                "\"customfield_13564\": [{\"value\": \"Other\"}]," +
                "\"customfield_12699\": {\"value\": \"Rig Systems\"}," +
                "\"customfield_14114\": {\"value\": \"Norway\"}," +
                "\"customfield_15509\": {\"value\": \"No, update does not need Global ID\"}," +
                "\"customfield_14471\": {\"value\": \"Purchased\"}," +
                "\"description\": \"This is a General description sent through JIRA API, Please Ignore this ticket.\"," +
                "\"customfield_14361\": {\"value\": \"No, Do Not Enable\"}," +
                "\"customfield_13664\": 1" +
                "}" +
                //"" +
                //"" +
                //"" +
                //"" +
                //"" +
                //"" +
                //"" +
                //"" +
                //"" +
                //"" +
                //"" +
                //"" +
                //"" +
                //"" +
                //"" +
                //"" +
                "}";
            //Console.WriteLine(jsonstring);
            return jsonstring;
        }
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