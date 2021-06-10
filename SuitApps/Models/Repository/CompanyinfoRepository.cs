using SuitApps.Models.ModelClass;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel.Web;
using System.Web;

namespace SuitApps.Models.Repository
{
    public class CompanyinfoRepository
    {
        #region statelist
        public static StateList GetStateList()
        {
            SqlConnection con;
            SqlDataReader reader;
            WebOperationContext context = WebOperationContext.Current;
            StateList stateList = new StateList(); 
            MesssageModel results = new MesssageModel();
            List<State> states = new List<State>();
            try
            {
                con = DBconnect.getDataBaseConnection();

                //Create the SqlCommand object
                SqlCommand cmd = new SqlCommand("WebGetState", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        State state = new State();
                        state.StateId = reader["stateid"].ToString();
                        state.StateName = reader["StateName"].ToString();
                        states.Add(state);
                    }
                }

                stateList.States = states;

                results.status = true;
                results.Message = "Success.";
                stateList.messsages = results;

                reader.Close();
                con.Close();

                return stateList;
            }
            catch (Exception ex)
            {
                context.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                
                results.status = false;
                results.Message = ex.Message;
                stateList.messsages = results;

                return stateList;
            }            
        }
        #endregion

        #region districtlist
        public static DistrictList GetDistrictList(int stateId)
        {
            SqlConnection con;
            SqlDataReader reader;
            WebOperationContext context = WebOperationContext.Current;
            DistrictList districtList = new DistrictList();
            MesssageModel results = new MesssageModel();
            List<District> districts = new List<District>();
            try
            {
                con = DBconnect.getDataBaseConnection();

                //Create the SqlCommand object
                SqlCommand cmd = new SqlCommand("WebGetDistrictByStateID", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StateID", stateId);
                reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        District district = new District();
                        district.DistrictId = reader["stateid"].ToString();
                        district.DistrictName = reader["StateName"].ToString();
                        districts.Add(district);
                    }
                }

                districtList.Districts = districts;

                results.status = true;
                results.Message = "Success.";
                districtList.messsages = results;

                reader.Close();
                con.Close();

                return districtList;
            }
            catch (Exception ex)
            {
                context.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;

                results.status = false;
                results.Message = ex.Message;
                districtList.messsages = results;

                return districtList;
            }
            
        }
        #endregion

        #region taluklist
        public static TalukList GetTalukList(int stateId)
        {
            SqlConnection con;
            SqlDataReader reader;
            WebOperationContext context = WebOperationContext.Current;
            TalukList talukList = new TalukList();
            MesssageModel results = new MesssageModel();
            List<Taluk> taluks = new List<Taluk>();
            try
            {
                con = DBconnect.getDataBaseConnection();

                //Create the SqlCommand object
                SqlCommand cmd = new SqlCommand("WebGetTalkuByStateID", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StateID", stateId);
                reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        Taluk taluk = new Taluk();
                        taluk.TalukId = reader["stateid"].ToString();
                        taluk.TalukName = reader["StateName"].ToString();
                        taluks.Add(taluk);
                    }
                }

                talukList.Taluks = taluks;

                results.status = true;
                results.Message = "Success.";
                talukList.messsages = results;

                reader.Close();
                con.Close();

                return talukList;
            }
            catch (Exception ex)
            {
                context.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;

                results.status = false;
                results.Message = ex.Message;
                talukList.messsages = results;

                return talukList;
            }

        }
        #endregion

        #region statelist
        public static StateDetailsById GetStateDeatils(int stateId)
        {
            SqlConnection con;
            SqlDataReader reader;
            WebOperationContext context = WebOperationContext.Current;
            StateDetailsById stateDetail = new StateDetailsById();
            MesssageModel results = new MesssageModel();
            try
            {
                con = DBconnect.getDataBaseConnection();
                StateDetails details = new StateDetails();
                //Create the SqlCommand object
                SqlCommand cmd = new SqlCommand("WebGetDetailsByStateID", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StateID", stateId);
                reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {                       
                        details.StateID = reader["StateID"].ToString();
                        details.StateName = reader["StateName"].ToString();
                        details.StateCode = reader["StateCode"].ToString();
                        details.StateType = reader["StateType"].ToString();
                        details.StateParent = reader["StateParent"].ToString();
                        details.level = reader["level"].ToString();
                        details.Scategory = reader["Scategory"].ToString();
                    }
                }

                stateDetail.State = details;

                results.status = true;
                results.Message = "Success.";
                stateDetail.messsages = results;

                reader.Close();
                con.Close();

                return stateDetail;
            }
            catch (Exception ex)
            {
                context.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;

                results.status = false;
                results.Message = ex.Message;
                stateDetail.messsages = results;

                return stateDetail;
            }
        }
        #endregion



    }
}