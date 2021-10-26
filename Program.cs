using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




using System.Net;
using System.ServiceModel.Description;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Tooling.Connector;
using Microsoft.Xrm.Sdk.Query;

namespace Contection_with_Microsoft_dynamics_365
{
    class Program
    {
        private static CrmServiceClient svc;
        private static Entity updatedContact ;

        static void Main(string[] args)
        {
            #region connection

            

            string authType = "OAuth";
            string userName = "shahzaib@SHAHZAIBSAFDAR1.onmicrosoft.com";
            string password = "safdar786ALI!";
            string     url = "https://org666f01ac.crm.dynamics.com";
            string appId = "51f81489-12ee-4a9e-aaae-a2591f45987d";
            string reDirectURI = "app://58145B91-0C36-4500-8554-080854F2AC97";
            string loginPrompt = "Auto";
            string ConnectionString = string.Format("AuthType = {0};Username = {1};Password = {2}; Url = {3}; AppId={4}; RedirectUri={5};LoginPrompt={6}",authType,userName, password, url, appId, reDirectURI, loginPrompt);



           svc = new CrmServiceClient(ConnectionString);

            if (svc.IsReady)
            {
                Console.WriteLine("Connection Successful!...");
                Console.WriteLine("Please enter key Options");
                Console.WriteLine("C....Create...1");
                Console.WriteLine("R...Retrive....2");
                Console.WriteLine("R...Retrive Multiple....3");
                Console.WriteLine("U...Update........4");
                Console.WriteLine("D.....Delete.....5");
                string a = Console.ReadLine();
                int aa = Convert.ToInt32(a);
                switch (aa)
                {
                    case 1:
                        C();
                        break;
                    case 2:
                        RS();
                        break;
                    case 3:
                        RM();
                        break;
                    case 4:
                        U();
                        break;
                    case 5:
                        D();
                        break;

                    default:
                   
                        break;
                }
               



                #region sir code
                /// retrieve all record
                //var myemail = "test@test.com";
                //var fetchXml = @"<?xml version='1.0'?>
                //<fetch distinct='false' mapping='logical' output-format='xml-platform' version='1.0'>
                // <entity name='contact'>
                // <attribute name='fullname'/>
                //     <attribute name='telephone1'/><attribute name='contactid'/><order descending='false' attribute='fullname'/>
                //     <filter type='and'>
                //           <condition attribute='emailaddress1' value='test@test.com' operator='eq'/>
                //           </filter></entity></fetch>";
                //fetchXml = String.Format(fetchXml, myemail);
                //EntityCollection contacts = svc.RetrieveMultiple(new FetchExpression(fetchXml));
                //Console.WriteLine("Total record: " + contacts.Entities.Count);
                //foreach (var con in contacts.Entities)
                //{
                //    Console.WriteLine("Id" + con.Id);
                //    if (con.Contains("fullname") && con["fullname"] != null)
                //    {

                //        Console.WriteLine("con Name: " + con["fullname"]);
                //    }
                //}

                //Entity retrievedContact = svc.Retrieve(contact.LogicalName, contactId, new ColumnSet(true));
                //Console.WriteLine("Record retrieved {0}", retrievedContact.Id.ToString());
                #endregion


            }
            else
            {
                Console.WriteLine("Failed to Established Connection!!!");
            }

            #region PREVIOUS CODE

            

            //IOrganizationService _service = null;

            //try
            //{
            //    ClientCredentials clientCredentials = new ClientCredentials();
            //    clientCredentials.UserName.UserName = "shahzaib@SHAHZAIBSAFDAR1.onmicrosoft.com";
            //    clientCredentials.UserName.Password = "safdar786ALI!";

            //    // Copy and Paste Organization Service Endpoint Address URL
            //    _service = (IOrganizationService)new OrganizationServiceProxy(new Uri("https://org666f01ac.api.crm.dynamics.com/XRMServices/2011/Organization.svc"),
            //     null, clientCredentials, null);
            //    if (_service != null)
            //    {
            //        Guid userid = ((WhoAmIResponse)_service.Execute(new WhoAmIRequest())).UserId;
            //        if (userid != Guid.Empty)
            //        {
            //            Console.WriteLine("Connection Successful!...");
            //        }
            //    }
            //    else
            //    {
            //        Console.WriteLine("Failed to Established Connection!!!");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Exception caught - " + ex.Message);
            //}
            #endregion
            Console.ReadKey();
#endregion

        }

        private static void D()
        {
           Entity contact = new Entity("contact");
            // Delete a record using Id
            Console.WriteLine("Enter Contact Id:");
            // Update record using Id, retrieve all attributes
            string stringGuid = Console.ReadLine();
            Guid contactId = Guid.Parse(stringGuid);
            
          
             svc.Delete(contact.LogicalName, contactId);
            Console.WriteLine("Deleted");
            Console.ReadLine();

        }

        private static void U()
        {
            Console.WriteLine("Enter Contact Id:");
            // Update record using Id, retrieve all attributes
            string contactId = Console.ReadLine();
            Entity updatedContact = new Entity("contact");
            QueryExpression qe = new QueryExpression("contact");
            qe.ColumnSet = new ColumnSet("contactId");
            EntityCollection ec = svc.RetrieveMultiple(qe);
            for (int i = 0; i < ec.Entities.Count; i++)
            {
                if (ec.Entities[i].Attributes.ContainsKey("contactId"))
                {
                   // updatedContact = svc.Update(updatedContact.LogicalName, contactId, new ColumnSet(true));
                    updatedContact["jobtitle"] = "CEO";
                    updatedContact["emailaddress1"] = "test@test.com";
                    svc.Update(updatedContact);
                    Console.WriteLine("Updated contact");
                }

            }
        }

        private static void RM()
        {
            QueryExpression qe = new QueryExpression("contact");
            qe.ColumnSet = new ColumnSet("firstname", "lastname");
            EntityCollection ec = svc.RetrieveMultiple(qe);
            for (int i = 0; i < ec.Entities.Count; i++)
            {
                if (ec.Entities[i].Attributes.ContainsKey("firstname"))
                {
                    Console.WriteLine("Firstname={0}",ec.Entities[i].Attributes["firstname"] + "\n");
                    Console.WriteLine("Lastname={0}", ec.Entities[i].Attributes["lastname"]);
                  

                }
            }
        }

        private static void RS()
        {
            // Retrieve specific fields using ColumnSet
            ColumnSet attributes = new ColumnSet(new string[] { "username", "lastname","jobtitle", "emailaddress1" });
            Entity contact = new Entity("contact");
          
            Console.WriteLine("Enter Contact Id:");
          
            string stringGuid = Console.ReadLine();
            Guid contactId = Guid.Parse(stringGuid);
           //// EntityCollection ec = svc.Retrieve(contact.LogicalName, contactId, attributes);
           // for (int i = 0; i < ec.Entities.Count; i++)
           // {
           //     if (ec.Entities[i].Attributes.ContainsKey("firstname"))
           //     {
           //         Console.WriteLine(ec.Entities[i].Attributes["firstname"] + "\n");
           //         Console.WriteLine(ec.Entities[i].Attributes["lastname"] + "\n");
           //         Console.WriteLine(ec.Entities[i].Attributes["jobtitle"] + "\n");
           //         Console.WriteLine(ec.Entities[i].Attributes["emailaddress1"] + "\n");

           //     }
           // }
            
           // foreach (var a in retrievedContact.Attributes)
           // {
           //     Console.WriteLine("Retrieved contact field {0} - {1}", a.Key, a.Value);
           // }
        }

        private static void C()
        {
            /// create new record

            Entity contact = new Entity("contact");
            Console.WriteLine("Enter username:");
           string userName = Console.ReadLine();
            Console.WriteLine("Enter lastname:");
           string lastname = Console.ReadLine();
            Console.WriteLine("Enter jobtitle:");
            string jobtitle = Console.ReadLine();
           Console.WriteLine("Enter emailaddress1:");
            string emailaddress1 = Console.ReadLine();
           contact["firstname"] = userName;
            contact["lastname"] = lastname;
            contact["jobtitle"] = jobtitle;
            contact["emailaddress1"] = emailaddress1;
            Guid contactId = svc.Create(contact);
            Console.WriteLine("New contact id: {0} .", contactId.ToString());
        }
    }
}
