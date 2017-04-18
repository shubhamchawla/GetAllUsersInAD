using Facebook;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace GetAllUsersInAD
{
    class Program
    {
        static void Main(string[] args)
        {
            GetAllUsersFromAD();
            //DataSet ds = GetAllUserFromDataBase();
            //foreach (DataRow dr in ds.Tables[0].Rows)
            //{
            //    GetUserDetailFromFacebook(Convert.ToInt32(dr["UserID"].ToString()) ,dr["FirstName"].ToString(), dr["LastName"].ToString()).Wait();
            //}

            //Console.ReadLine();
        }

        static string URL = "https://graph.facebook.com/search";
        private static async Task GetUserDetailFromFacebook(int userID, string fname, string lname)
        {
            var fb = new FacebookClient();
            fb.AccessToken = "AccessToken";

            
            //string fname = "shubham";
            //string lname = "chawla";
            string fields = "id,name,updated_time,verified";
            string urlParameters = "?q="+fname +"+" + lname +""  +"&type=user&fields="+ fields + "&access_token=" + fb.AccessToken;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = await client.GetAsync(urlParameters);  // Blocking call!

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            //var obj = JsonConvert.DeserializeObject<JArray>(responseBody);
            dynamic parsed = JsonConvert.DeserializeObject<JObject>(responseBody);

            JArray data = parsed.data;
            foreach (dynamic jobj in data)
            {
                string id = (string)jobj.id;
                string url = GetPictureURL(id).Result; //(string)jobj.picture.data.url;

                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringName"].ToString());
                conn.Open();
                SqlCommand cmd = new SqlCommand("usp_InsertUserPhotoURL", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", userID);
                cmd.Parameters.AddWithValue("@URL", url);
                cmd.ExecuteNonQuery();
                conn.Close();

            }

        }


        public static async Task<string> GetPictureURL(string fbProfileID)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string urlParameters = "/"+fbProfileID+ "/picture?redirect=false&type=large";
            // List data response.
            HttpResponseMessage response = await client.GetAsync(urlParameters);  // Blocking call!

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            //var obj = JsonConvert.DeserializeObject<JArray>(responseBody);
            dynamic parsed = JsonConvert.DeserializeObject<JObject>(responseBody);

            string url = (string)parsed.data.url;

            return url;
        }
        public static void GetAllUsersFromAD()
        {
            using (var context = new PrincipalContext(ContextType.Domain, "xyzDomain"))
            {
                using (var searcher = new PrincipalSearcher(new UserPrincipal(context)))
                {
                    foreach (var result in searcher.FindAll())
                    {
                        DirectoryEntry de = result.GetUnderlyingObject() as DirectoryEntry;
                        if (de.Properties["info"].Value != null)
                        {
                            //Console.WriteLine("First Name: " + de.Properties["givenName"].Value);
                            //Console.WriteLine("Last Name : " +  de.Properties["sn"].Value);
                            //Console.WriteLine("Location : " +   de.Properties["l"].Value);
                            //Console.WriteLine("title : " +      de.Properties["title"].Value);
                            //Console.WriteLine("postalCode : " + de.Properties["postalCode"].Value);
                            //Console.WriteLine("telephoneNumber : " + de.Properties["telephoneNumber"].Value);
                            //Console.WriteLine("whenChanged : " + de.Properties["whenChanged"].Value);
                            //Console.WriteLine("streetAddress : " + de.Properties["streetAddress"].Value);
                            //Console.WriteLine("mobile : " + de.Properties["mobile"].Value);
                            //Console.WriteLine("msExchWhenMailboxCreated : " + de.Properties["msExchWhenMailboxCreated"].Value);
                            //Console.WriteLine("msExchExtensionAttribute19 : " + de.Properties["msExchExtensionAttribute19"].Value);
                            //Console.WriteLine("SAPID : " + de.Properties["physicalDeliveryOfficeName"].Value);
                            //Console.WriteLine("Gender : " + de.Properties["info"].Value.ToString().Substring(de.Properties["info"].Value.ToString().IndexOf("Gender : ") + 9));
                            //Console.WriteLine("Seat Code : " + de.Properties["info"].Value.ToString().Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None)[1].ToString().Substring("Seat Code : ".Length));
                            //Console.WriteLine("SAM account name   : " + de.Properties["samAccountName"].Value);
                            //Console.WriteLine("User principal name: " + de.Properties["userPrincipalName"].Value);
                            //Console.WriteLine();

                            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringName"].ToString());
                            conn.Open();
                            SqlCommand cmd = new SqlCommand("InsertActiveUser ", conn);
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@FirstName", de.Properties["givenName"].Value);
                            cmd.Parameters.AddWithValue("@LastName", de.Properties["sn"].Value);
                            cmd.Parameters.AddWithValue("@Location" , de.Properties["l"].Value);
                            cmd.Parameters.AddWithValue("@Title", de.Properties["title"].Value);
                            cmd.Parameters.AddWithValue("@postalCode" , de.Properties["postalCode"].Value);
                            cmd.Parameters.AddWithValue("@telephoneNumber" , de.Properties["telephoneNumber"].Value);
                            cmd.Parameters.AddWithValue("@whenChanged" , de.Properties["whenChanged"].Value);
                            cmd.Parameters.AddWithValue("@streetAddress" , de.Properties["streetAddress"].Value);
                            cmd.Parameters.AddWithValue("@mobile" , de.Properties["mobile"].Value);
                            cmd.Parameters.AddWithValue("@msExchWhenMailboxCreated" , de.Properties["msExchWhenMailboxCreated"].Value);
                            cmd.Parameters.AddWithValue("@msExchExtensionAttribute19", de.Properties["msExchExtensionAttribute19"].Value);
                            cmd.Parameters.AddWithValue("@SAPID" , de.Properties["physicalDeliveryOfficeName"].Value);
                            try
                            {
                                cmd.Parameters.AddWithValue("@Gender", de.Properties["info"].Value.ToString().Substring(de.Properties["info"].Value.ToString().IndexOf("Gender : ") + 9));
                            
                            cmd.Parameters.AddWithValue("@SeatCode" , de.Properties["info"].Value.ToString().Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None)[1].ToString().Substring("Seat Code : ".Length));
                            }
                            catch { }
                            cmd.Parameters.AddWithValue("@SAMAccountName" , de.Properties["samAccountName"].Value);
                            cmd.Parameters.AddWithValue("@UserPrincipalName" , de.Properties["userPrincipalName"].Value);

                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }
                    }
                }
            }
            Console.ReadLine();
        }

        public static DataSet GetAllUserFromDataBase()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringName"].ToString());
            DataSet ds = new DataSet();
            conn.Open();
            //SqlCommand cmd = new SqlCommand(, conn);
            string cmd = "select firstname +' '+ lastname , * from ActiveUser where gender like '%Female%' and  streetAddress like 'Noida SEZ-INF Twr-I (U-2)-All Flrs' and UserID not in ( select distinct userid from UserPhotoURL ) order by title ";
            SqlDataAdapter da = new SqlDataAdapter(cmd, conn);
            da.Fill(ds);
            conn.Close();
            return ds;
        }
    }
}
