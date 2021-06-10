using System;
using System.Collections.Generic;
using SuitApps.Models.ModelClass;
using System.Data.SqlClient;
using System.ServiceModel.Web;
using System.Net.Http;
using System.Net;


namespace SuitApps.Models.Repository
{
    public class LoginRepository
    {

        #region GetAllCompanyNames
        public static CompanyInfoList GetAllCompanyNames()
        {
            SqlConnection con;
            SqlDataReader reader;
            WebOperationContext context = WebOperationContext.Current;
            CompanyInfoList companyNameList = new CompanyInfoList();
            List<CompanyNameList> companyNameLists = new List<CompanyNameList>(); 
            try
            {
                con = DBconnect.getDataBaseConnection();

                //Create the SqlCommand object
                SqlCommand cmd = new SqlCommand("GetWebCompanyInfo", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                // con.Open();
                reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        CompanyNameList companyInfo = new CompanyNameList();
                        companyInfo.CompanyId = reader["CompanyId"].ToString();
                        companyInfo.CompanyName = reader["CompanyName"].ToString();
                        /*companyInfo.CompanyCode = reader["CompanyCode"].ToString();
                        companyInfo.Address = Regex.Replace(reader["Address"].ToString(), @"\t|\n|\r", ""); ;
                        companyInfo.TelephoneNo = reader["TelephoneNo"].ToString();
                        companyInfo.MobileNo = reader["MobileNO"].ToString();
                        companyInfo.City = reader["Citty"].ToString();
                        companyInfo.State = reader["State"].ToString();
                        companyInfo.District = reader["District"].ToString();
                        companyInfo.GSTinNo = reader["GSTinNo"].ToString();*/
                        companyNameLists.Add(companyInfo);
                    }
                }

                companyNameList.companyLists = companyNameLists;
                reader.Close();
                con.Close();
            }
            catch (Exception e)
            {
                context.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return companyNameList;
            }
            return companyNameList;
        }
        #endregion

        #region Login
        public static LoginResponse Login(LoginRequest loginRequest)
        {
            SqlConnection con;
            SqlDataReader reader;
            HttpResponseMessage httpresponse;
            LoginResponse loginResponse = new LoginResponse();

            try
            {
                con = DBconnect.getDataBaseConnection();
                if (loginRequest.CompanyId == null || loginRequest.UserName == null || loginRequest.Password == null)
                {
                    httpresponse =  new HttpResponseMessage(HttpStatusCode.BadRequest);
                    loginResponse.Message = "Invalid request..!";
                    return loginResponse;
                }

                //Create the SqlCommand object
                SqlCommand cmd = new SqlCommand("WebLogin", con);

                //Specify that the SqlCommand is a stored procedure
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                //Add the input parameters to the command object
                cmd.Parameters.AddWithValue("@CompanyId", loginRequest.CompanyId);
                cmd.Parameters.AddWithValue("@Username", loginRequest.UserName);
                cmd.Parameters.AddWithValue("@Password", loginRequest.Password);
                    reader = cmd.ExecuteReader();
                    if (reader.HasRows == true)
                    {
                        while (reader.Read())
                        {
                            loginResponse.UserId = reader["UserId"].ToString();
                            loginResponse.Name = reader["Name"].ToString();
                            loginResponse.CompanyId = reader["CompanyID"].ToString();
                            loginResponse.UserRoleId = reader["UserRoleId"].ToString();
                            loginResponse.Username = reader["UserName"].ToString();

                            if (reader["Image"] != DBNull.Value)
                            {
                                loginResponse.Image = (byte[])reader["Image"] as byte[];
                            }
                            else
                            {
                                loginResponse.Image = null;
                            }
                        }
                        loginResponse.Message = "User login successfull";
                        reader.Close();
                    }

                    else
                    {
                        httpresponse = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                        loginResponse = new LoginResponse();
                        loginResponse.Message = "Invalid user credentials..!";
                    }
            }

            catch (Exception e)
            {
                httpresponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                loginResponse.Message = e.Message.ToString();
                return loginResponse;
            }
            con.Close();
            return loginResponse;
        } 
        #endregion


    }
}