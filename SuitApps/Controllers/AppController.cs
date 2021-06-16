using System.Web.Http;
using SuitApps.Models.ModelClass;
using SuitApps.Models.Repository;

namespace SuitApps.Controllers
{
    public class AppController : ApiController
    {
        #region Login
        [HttpPost]
        [ActionName("Login")]
        public LoginResponse Post([FromBody]LoginRequest loginRequest)
        {
            return LoginRepository.Login(loginRequest);
        }

        [HttpGet]
        [ActionName("GetAllCompanyNames")]
        public CompanyInfoList Get()
        {
            return LoginRepository.GetAllCompanyNames();
        }

        #endregion

        #region CompanyInfo
        [HttpGet]
        [ActionName("State")]
        public StateList StateListSelect()
        {
            return CompanyinfoRepository.GetStateList();

        }

        [HttpGet]
        [ActionName("District")]
        public DistrictList DistrictListSelect(int stateId)
        {
            return CompanyinfoRepository.GetDistrictList(stateId);

        }

        [HttpGet]
        [ActionName("Taluk")]
        public TalukList TalukListSelect(int stateId)
        {
            return CompanyinfoRepository.GetTalukList(stateId);

        }

        [HttpGet]
        [ActionName("GetStateDeatils")]
        public StateDetailsById GetStateDeatilById(int stateId)
        {
            return CompanyinfoRepository.GetStateDeatils(stateId);

        }
        #endregion


        #region Bank
        [HttpGet]
        [ActionName("GetGroupMasterPopup")]
        public BankList GetGroupMasterPopup()
        {
            return BankRepository.GetGroupMasterPopup();
        }



        [HttpGet]
        [ActionName("GetBankDetailes")]
        public BankList GetBankDetailes()
        {
            return BankRepository.GetBankDetailes();
        }


        [HttpGet]
        [ActionName("GetBankDetailesById")]
        public BankList GetBankDetailesById(int bankid)
        {
            return BankRepository.GetBankDetailesById(bankid);
        }


        [HttpPost]
        [ActionName("InsertUpdateBankAcHead")]
        public AcHead Post([FromBody]AcHead achead)
        {
            return BankRepository.InsertUpdateBankAcHead(achead);
        }


        [HttpPost]
        [ActionName("DeleteBankAcHeadAcTranc")]
        public AcHead DeleteBankAcHeadAcTranc([FromBody]AcHead achead1)
        {
            return BankRepository.DeleteBankAcHeadAcTranc(achead1);
        }
        #endregion
    }
}