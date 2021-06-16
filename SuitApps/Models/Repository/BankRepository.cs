using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel.Web;
using System.Web;
using SuitApps.Models.ModelClass;

namespace SuitApps.Models.Repository
{
    public class BankRepository
    {
        public static BankList GetGroupMasterPopup()
        {
            SqlConnection con;
            SqlDataReader reader;
            WebOperationContext context = WebOperationContext.Current;
            BankList banklist = new BankList();
            List<Bank> banks = new List<Bank>();

            try
            {
                con = DBconnect.getDataBaseConnection();

                //Create the SqlCommand object
                SqlCommand cmd = new SqlCommand("GetGroupMasterPopup", con);
                //Specify that the SqlCommand is a stored procedure
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        Bank bank = new Bank();
                        bank.GroupId = reader["GSlNo"].ToString();
                        bank.GroupName = reader["GName"].ToString();
                        banks.Add(bank);
                    }
                }
                banklist.bankList = banks;

                reader.Close();
                con.Close();
            }
            catch (Exception e)
            {
                context.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return banklist;
            }
            return banklist;
        }

        public static BankList GetBankDetailes()
        {
            SqlConnection con;
            SqlDataReader reader;
            WebOperationContext context = WebOperationContext.Current;
            BankList banklist = new BankList();
            List<Bank> banks = new List<Bank>();

            try
            {
                con = DBconnect.getDataBaseConnection();

                //Create the SqlCommand object
                SqlCommand cmd = new SqlCommand("GetBankDetailes", con);
                //Specify that the SqlCommand is a stored procedure
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        Bank bank = new Bank();
                        bank.Slno = reader["Slno"].ToString();
                        bank.AcheadId = reader["HeadNo"].ToString();
                        bank.BankName = reader["BankName"].ToString();
                        bank.GroupId = reader["GUnder"].ToString();
                        bank.Place = reader["place"].ToString();
                        bank.Branch = reader["Branch"].ToString();
                        bank.phoneNo = reader["PhoneNo1"].ToString();
                        bank.AccountNo = reader["AccNo"].ToString();
                        bank.Date = reader["BDate"].ToString();
                        bank.UserId = reader["UserId"].ToString();
                        bank.mailto = reader["Mailto"].ToString();
                        bank.address = reader["Address"].ToString();
                        bank.country = reader["Country"].ToString();
                        bank.state = reader["State"].ToString();
                        bank.pincode = reader["Pincode"].ToString();
                        bank.opbal = reader["OpeningBal"].ToString();
                        bank.rtgs = reader["Rtgs"].ToString();
                        bank.ifsc = reader["Ifsc"].ToString();
                        bank.micr = reader["Micr"].ToString();
                        bank.Debit = reader["Debit"].ToString();
                        bank.Credit = reader["Credit"].ToString();
                        bank.Opdate = reader["OpEntryDate"].ToString();
                        banks.Add(bank);
                    }
                }
                banklist.bankList = banks;

                reader.Close();
                con.Close();
            }
            catch (Exception e)
            {
                context.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return banklist;
            }
            return banklist;
        }


        #region InsertUpdateBankAcHead
        public static AcHead InsertUpdateBankAcHead(AcHead achead)
        {
            SqlConnection con;
            HttpResponseMessage httpresponse;
            AcHead acHead = new AcHead();
            try
            {

                //Create the SqlCommand object
                con = DBconnect.getDataBaseConnection();
                SqlCommand cmd = new SqlCommand("InsertUpdateBankAcHead", con);


                //Specify that the SqlCommand is a stored procedure
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                if (achead.AcheadId == "0" || achead.AcheadId == null)
                {
                    //Specify that the SqlCommand is a stored procedure
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    //Add the input parameters to the command object
                    cmd.Parameters.AddWithValue("@AcHeadId", "0");
                    cmd.Parameters.AddWithValue("@AcHeadName", achead.BankName);
                    cmd.Parameters.AddWithValue("@GroupId", achead.GroupId);
                    cmd.Parameters.AddWithValue("@UserId", achead.UserId);
                    cmd.Parameters.AddWithValue("@Date", DateTime.Now.ToString("M/d/yyyy"));
                    cmd.Parameters.AddWithValue("@Type", "B");
                    cmd.Parameters.AddWithValue("@Companyid", achead.CompanyId);
                    cmd.Parameters.Add("@OutId", SqlDbType.Int, 20);
                    cmd.Parameters["@OutId"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    int value = (int)cmd.Parameters["@OutId"].Value;
                    if (value != 0)
                    {
                        acHead.Message = "Inserted";
                        acHead.AcheadId = value.ToString();
                        acHead.status = true;
                        InsertUpdateBank(achead, acHead.AcheadId);

                    }
                    else
                    {
                        httpresponse = new HttpResponseMessage(HttpStatusCode.NoContent);
                        acHead.Message = "Failed";
                        acHead.status = true;
                    }
                }

                else
                {
                    //Add the input parameters to the command object
                    cmd.Parameters.AddWithValue("@AcHeadId", achead.AcheadId);
                    cmd.Parameters.AddWithValue("@AcHeadName", achead.BankName);
                    cmd.Parameters.AddWithValue("@GroupId", achead.GroupId);
                    cmd.Parameters.AddWithValue("@UserId", achead.UserId);
                    cmd.Parameters.AddWithValue("@Date", DateTime.Now.ToString("M/d/yyyy"));
                    cmd.Parameters.AddWithValue("@Type", "B");
                    cmd.Parameters.AddWithValue("@Companyid", achead.CompanyId);
                    cmd.Parameters.Add("@OutId", SqlDbType.Int, 20);
                    cmd.Parameters["@OutId"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    int value = (int)cmd.Parameters["@OutId"].Value;
                    if (value != 0)
                    {
                        acHead.Message = "Updated";
                        acHead.AcheadId = value.ToString();
                        acHead.status = true;
                        InsertUpdateBank(achead, acHead.AcheadId);
                    }
                    else
                    {
                        httpresponse = new HttpResponseMessage(HttpStatusCode.NoContent);
                        acHead.Message = "Failed";
                        acHead.status = true;
                    }
                }
            }

            catch (Exception e)
            {
                httpresponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return acHead;
            }
            con.Close();
            return acHead;
        }
        #endregion


        #region InsertUpdateBank
        public static AcHead InsertUpdateBank(AcHead bank, String acheadid)
        {
            SqlConnection con;
            HttpResponseMessage httpresponse;
            AcHead bnk = new AcHead();
            try
            {

                con = DBconnect.getDataBaseConnection();

                //Create the SqlCommand object
                SqlCommand cmd = new SqlCommand("InsertUpdateBank", con);

                if (bank.Slno == "0" || bank.Slno == null)
                {
                    //Specify that the SqlCommand is a stored procedure
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    //Add the input parameters to the command object
                    cmd.Parameters.AddWithValue("@Bankid", "0");
                    cmd.Parameters.AddWithValue("@HeadId", acheadid);
                    cmd.Parameters.AddWithValue("@BankName", bank.BankName);
                    cmd.Parameters.AddWithValue("@GUnder", bank.GroupId);
                    cmd.Parameters.AddWithValue("@Place", bank.Place);
                    cmd.Parameters.AddWithValue("@Branch", bank.Branch);
                    cmd.Parameters.AddWithValue("@PhoneNo", bank.phoneNo);
                    cmd.Parameters.AddWithValue("@AccNo", bank.AccountNo);
                    cmd.Parameters.AddWithValue("@BDate", DateTime.Now.ToString("M/d/yyyy"));
                    cmd.Parameters.AddWithValue("@UserId", bank.UserId);
                    cmd.Parameters.AddWithValue("@Mailto", bank.mailto);
                    cmd.Parameters.AddWithValue("@Address", bank.address);
                    cmd.Parameters.AddWithValue("@Country", bank.country);
                    cmd.Parameters.AddWithValue("@State", bank.state);
                    cmd.Parameters.AddWithValue("@Pincode", bank.pincode);
                    cmd.Parameters.AddWithValue("@Opbal", bank.opbal);
                    cmd.Parameters.AddWithValue("@Ifsc", bank.ifsc);
                    if (bank.micr != null)
                    {
                        cmd.Parameters.AddWithValue("@Micr", bank.micr);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Micr", "");
                    }

                    if (bank.rtgs != null)
                    {
                        cmd.Parameters.AddWithValue("@Rtgs", bank.rtgs);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Rtgs", "");
                    }

                    if (bank.drcr == "Dr")
                    {
                        cmd.Parameters.AddWithValue("@Debit", bank.opbal);
                        cmd.Parameters.AddWithValue("@Credit", "0");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Debit", "0");
                        cmd.Parameters.AddWithValue("@Credit", bank.opbal);
                    }
                    cmd.Parameters.AddWithValue("@Opdate", bank.Date);
                    cmd.Parameters.Add("@slno", SqlDbType.Int, 20);
                    cmd.Parameters["@slno"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();

                    int value = (int)cmd.Parameters["@slno"].Value;
                    if (value != 0)
                    {
                        bnk.Message = "Inserted";
                        bnk.Bankid = value.ToString();
                        bnk.status = true;
                        int slno = GetAcTrancByBillno(value);
                        InsertUpdateBankOpeningBalance(bank, acheadid, bnk.Bankid, slno);

                    }
                    else
                    {
                        httpresponse = new HttpResponseMessage(HttpStatusCode.NoContent);
                        bnk.Message = "Failed";
                        bnk.status = false;
                    }
                }

                else
                {
                    //Specify that the SqlCommand is a stored procedure
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    //Add the input parameters to the command object
                    cmd.Parameters.AddWithValue("@Bankid", bank.Slno);
                    cmd.Parameters.AddWithValue("@HeadId", acheadid);
                    cmd.Parameters.AddWithValue("@BankName", bank.BankName);
                    cmd.Parameters.AddWithValue("@GUnder", bank.GroupId);
                    cmd.Parameters.AddWithValue("@Place", bank.Place);
                    cmd.Parameters.AddWithValue("@Branch", bank.Branch);
                    cmd.Parameters.AddWithValue("@PhoneNo", bank.phoneNo);
                    cmd.Parameters.AddWithValue("@AccNo", bank.AccountNo);
                    cmd.Parameters.AddWithValue("@BDate", DateTime.Now.ToString("M/d/yyyy"));
                    cmd.Parameters.AddWithValue("@UserId", bank.UserId);
                    cmd.Parameters.AddWithValue("@Mailto", bank.mailto);
                    cmd.Parameters.AddWithValue("@Address", bank.address);
                    cmd.Parameters.AddWithValue("@Country", bank.country);
                    cmd.Parameters.AddWithValue("@State", bank.state);
                    cmd.Parameters.AddWithValue("@Pincode", bank.pincode);
                    cmd.Parameters.AddWithValue("@Opbal", bank.opbal);
                    cmd.Parameters.AddWithValue("@Ifsc", bank.ifsc);
                    if (bank.micr != null)
                    {
                        cmd.Parameters.AddWithValue("@Micr", bank.micr);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Micr", "");
                    }

                    if (bank.rtgs != null)
                    {
                        cmd.Parameters.AddWithValue("@Rtgs", bank.rtgs);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Rtgs", "");
                    }

                    if (bank.drcr == "Dr")
                    {
                        cmd.Parameters.AddWithValue("@Debit", bank.opbal);
                        cmd.Parameters.AddWithValue("@Credit", "0");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Debit", "0");
                        cmd.Parameters.AddWithValue("@Credit", bank.opbal);
                    }
                    cmd.Parameters.AddWithValue("@Opdate", bank.Date);
                    cmd.Parameters.Add("@slno", SqlDbType.Int, 20);
                    cmd.Parameters["@slno"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();

                    int value = (int)cmd.Parameters["@slno"].Value;
                    if (value != 0)
                    {
                        bnk.Message = "Updated";
                        bnk.Bankid = value.ToString();
                        bnk.status = true;
                        int slno = GetAcTrancByBillno(value);
                        InsertUpdateBankOpeningBalance(bank, acheadid, bnk.Bankid, slno);


                    }
                    else
                    {
                        httpresponse = new HttpResponseMessage(HttpStatusCode.NoContent);
                        bnk.Message = "Failed";
                        bnk.status = false;
                    }
                }
            }

            catch (Exception e)
            {
                httpresponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return bnk;
            }
            con.Close();
            return bnk;
        }
        #endregion


        #region InsertUpdateBankOpeningBalance
        public static AcHead InsertUpdateBankOpeningBalance(AcHead acTranc, String acheadid, String bankid, int slno)
        {
            SqlConnection con;
            HttpResponseMessage httpresponse;
            AcHead actran = new AcHead();
            try
            {

                con = DBconnect.getDataBaseConnection();

                //Create the SqlCommand object
                SqlCommand cmd = new SqlCommand("InsertUpdateBankOpeningBalance", con);

                //Specify that the SqlCommand is a stored procedure
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                if (slno != 0)
                {

                    //Add the input parameters to the command object
                    cmd.Parameters.AddWithValue("@slno", slno);
                    cmd.Parameters.AddWithValue("@BillNo", bankid);
                    cmd.Parameters.AddWithValue("@ADate", acTranc.Date);
                    cmd.Parameters.AddWithValue("@CDate", acTranc.Date);
                    if (acTranc.drcr == "Dr")
                    {
                        cmd.Parameters.AddWithValue("@FMode", "D");
                        cmd.Parameters.AddWithValue("@Amt", acTranc.opbal);
                        cmd.Parameters.AddWithValue("@Fcode", "0");
                        cmd.Parameters.AddWithValue("@TCode", acheadid);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@FMode", "C");
                        cmd.Parameters.AddWithValue("@Amt", acTranc.opbal);
                        cmd.Parameters.AddWithValue("@Fcode", acheadid);
                        cmd.Parameters.AddWithValue("@TCode", "0");
                    }
                    cmd.Parameters.AddWithValue("@Narration", "OpeningBalance");
                    cmd.Parameters.AddWithValue("@UserId", acTranc.UserId);
                    cmd.Parameters.AddWithValue("@Type", "B");
                    cmd.Parameters.AddWithValue("@CompanyId", acTranc.CompanyId);
                    cmd.Parameters.AddWithValue("@OtherNarration", "OpeningBalance");
                    cmd.Parameters.AddWithValue("@FYearID", "1");
                    cmd.Parameters.Add("@Opout", SqlDbType.Int, 20);
                    cmd.Parameters["@Opout"].Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();

                    int value = (int)cmd.Parameters["@Opout"].Value;
                    if (value != 0)
                    {

                        actran.Message = "Updated";
                        actran.status = true;
                        actran.Slno = value.ToString();


                    }
                    else
                    {
                        httpresponse = new HttpResponseMessage(HttpStatusCode.NoContent);
                        actran.Message = "Failed";
                        actran.status = false;
                    }
                }
                else
                {

                    //Add the input parameters to the command object
                    cmd.Parameters.AddWithValue("@slno", "0");
                    cmd.Parameters.AddWithValue("@BillNo", bankid);
                    cmd.Parameters.AddWithValue("@ADate", acTranc.Date);
                    cmd.Parameters.AddWithValue("@CDate", acTranc.Date);
                    if (acTranc.drcr == "Dr")
                    {
                        cmd.Parameters.AddWithValue("@FMode", "D");
                        cmd.Parameters.AddWithValue("@Amt", acTranc.opbal);
                        cmd.Parameters.AddWithValue("@Fcode", "0");
                        cmd.Parameters.AddWithValue("@TCode", acheadid);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@FMode", "C");
                        cmd.Parameters.AddWithValue("@Amt", acTranc.opbal);
                        cmd.Parameters.AddWithValue("@Fcode", acheadid);
                        cmd.Parameters.AddWithValue("@TCode", "0");
                    }
                    cmd.Parameters.AddWithValue("@Narration", "OpeningBalance");
                    cmd.Parameters.AddWithValue("@UserId", acTranc.UserId);
                    cmd.Parameters.AddWithValue("@Type", "B");
                    cmd.Parameters.AddWithValue("@CompanyId", acTranc.CompanyId);
                    cmd.Parameters.AddWithValue("@OtherNarration", "OpeningBalance");
                    cmd.Parameters.AddWithValue("@FYearID", "1");
                    cmd.Parameters.Add("@Opout", SqlDbType.Int, 20);
                    cmd.Parameters["@Opout"].Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();

                    int value = (int)cmd.Parameters["@Opout"].Value;
                    if (value != 0)
                    {

                        actran.Message = "Inserted";
                        actran.status = true;
                        actran.Slno = value.ToString();


                    }
                    else
                    {
                        httpresponse = new HttpResponseMessage(HttpStatusCode.NoContent);
                        actran.Message = "Failed";
                        actran.status = false;
                    }
                }


            }

            catch (Exception e)
            {
                httpresponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return actran;
            }
            con.Close();
            return actran;
        }
        #endregion


        public static BankList GetBankDetailesById(int bankid)
        {
            SqlConnection con;
            SqlDataReader reader;
            WebOperationContext context = WebOperationContext.Current;
            BankList banklist = new BankList();
            List<Bank> banks = new List<Bank>();

            try
            {
                con = DBconnect.getDataBaseConnection();

                //Create the SqlCommand object
                SqlCommand cmd = new SqlCommand("GetBankDetailesById", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BankId", bankid);
                reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        Bank bank = new Bank();
                        bank.Slno = reader["Slno"].ToString();
                        bank.AcheadId = reader["HeadNo"].ToString();
                        bank.BankName = reader["BankName"].ToString();
                        bank.GroupId = reader["GUnder"].ToString();
                        bank.Place = reader["place"].ToString();
                        bank.Branch = reader["Branch"].ToString();
                        bank.phoneNo = reader["PhoneNo1"].ToString();
                        bank.AccountNo = reader["AccNo"].ToString();
                        bank.Date = reader["BDate"].ToString();
                        bank.UserId = reader["UserId"].ToString();
                        bank.mailto = reader["Mailto"].ToString();
                        bank.address = reader["Address"].ToString();
                        bank.country = reader["Country"].ToString();
                        bank.state = reader["State"].ToString();
                        bank.pincode = reader["Pincode"].ToString();
                        bank.opbal = reader["OpeningBal"].ToString();
                        bank.rtgs = reader["Rtgs"].ToString();
                        bank.ifsc = reader["Ifsc"].ToString();
                        bank.micr = reader["Micr"].ToString();
                        bank.Credit = reader["Credit"].ToString();
                        bank.Debit = reader["Debit"].ToString();
                        bank.Opdate = reader["OpEntryDate"].ToString();
                        if (bank.Debit != "0.00")
                        {
                            bank.drcr = "Dr";
                        }
                        else
                        {
                            bank.drcr = "Cr";
                        }

                        banks.Add(bank);
                    }
                }
                banklist.bankList = banks;

                reader.Close();
                con.Close();
            }
            catch (Exception e)
            {
                context.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return banklist;
            }
            return banklist;
        }


        public static int GetAcTrancByBillno(int Bankid)
        {
            SqlConnection con;
            SqlDataReader reader;
            WebOperationContext context = WebOperationContext.Current;
            AcHead actran = new AcHead();
            int slno = 0;
            try
            {
                con = DBconnect.getDataBaseConnection();

                //Create the SqlCommand object
                SqlCommand cmd = new SqlCommand("GetAcTrancByBillno", con);
                //Specify that the SqlCommand is a stored procedure
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Bankid", Bankid);
                // con.Open();
                reader = cmd.ExecuteReader();

                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        slno = Convert.ToInt32(reader["slno"].ToString());

                    }
                }
                reader.Close();

                con.Close();
            }
            catch (Exception e)
            {
                context.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return slno;
            }
            return slno;
        }



        #region DeleteBankAcHeadAcTranc
        public static AcHead DeleteBankAcHeadAcTranc(AcHead achead)
        {
            SqlConnection con;
            HttpResponseMessage httpresponse;
            AcHead acHead = new AcHead();
            try
            {
                //Create the SqlCommand object
                con = DBconnect.getDataBaseConnection();
                SqlCommand cmd = new SqlCommand("AcHeadsDelete", con);
                //Specify that the SqlCommand is a stored procedure
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@HSlNo", achead.AcheadId);
                cmd.Parameters.AddWithValue("@HName", achead.BankName);
                cmd.ExecuteNonQuery();

                SqlCommand cmd1 = new SqlCommand("DeletebankDetailes", con);
                //Specify that the SqlCommand is a stored procedure
                cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@BankId", achead.Slno);
                cmd1.Parameters.AddWithValue("@AcHeadId", achead.AcheadId);
                cmd1.ExecuteNonQuery();

                SqlCommand cmd2 = new SqlCommand("DeleteBankOpeningBalance", con);
                //Specify that the SqlCommand is a stored procedure
                cmd2.CommandType = System.Data.CommandType.StoredProcedure;
                cmd2.Parameters.AddWithValue("@BankId", achead.Slno);
                cmd2.Parameters.AddWithValue("@AcHeadId", achead.AcheadId);
                cmd2.ExecuteNonQuery();

                achead.Message = "Deleted";

            }

            catch (Exception e)
            {
                httpresponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return acHead;
            }
            con.Close();
            return acHead;
        }
        #endregion
    }
}