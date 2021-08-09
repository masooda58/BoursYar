using System;

using System.Data;
using System.Data.SqlClient;
using DAL.Services;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols;



namespace DAL
{
    public class UnitOfWorkDapper:IDisposable,IDapperGenric

    {
      


        private static string connectionString = InitialSettingData.GetConnectionString("WDbContext");
           
          //  ConfigurationManager<>.ConnectionStrings["WDbContext"].ConnectionString;

       IDbConnection db= new SqlConnection(connectionString);

      
        private GenericDapperRepository<Codal> _codaldapperrepository;
        private GenericDapperRepository<BourseIndex> _bourseDapperRepository;
        private GenericDapperRepository<FavNamad> _favdDapperRepository;
        private GenericDapperRepository<IndNamad> _inDapperRepository;
        private GenericDapperRepository<IndusteryIndex> _industeryDapperRepository;
        private GenericDapperRepository<AllNamadInfo> _allnamaDapperRepository;
        private GenericDapperRepository<AllNamadInfo_Daily> _allnamaddailydDapperRepository;
        private GenericDapperRepository<AllNamadOption> _allnamadoptionDapperRepository;
        private GenericDapperRepository<PayamNazer> _payamnazeRepository;
        private GenericDapperRepository<Khodro> _khodrodDapperRepository;
        private GenericDapperRepository<Crypto> _cryptoDapperRepository;
        private GenericDapperRepository<Arz> _arzdDapperRepository;
        private GenericDapperRepository<CallWebServiceSetting> _callwebservicesettingDapperRepository;
        private GenericDapperRepository<Logger> _loggerdDapperRepository;

    

        public GenericDapperRepository<Codal> CodalDapperRepository
        {
            get
            {
                if (_codaldapperrepository == null)
                {
                    _codaldapperrepository = new GenericDapperRepository<Codal>(db);
                }
                    return _codaldapperrepository;
            }

        }

        public GenericDapperRepository<PayamNazer> PayamnazeRepository
        {

            get
            {
                if (_payamnazeRepository == null)
                {
                    _payamnazeRepository = new GenericDapperRepository<PayamNazer>(db);
                }
                return _payamnazeRepository;
            }
        }

        public GenericDapperRepository<AllNamadOption> AllnamadOptionDapperRepository
        {
            get
            {
                if (_allnamadoptionDapperRepository == null)
                {
                    _allnamadoptionDapperRepository = new GenericDapperRepository<AllNamadOption>(db);
                }
                return _allnamadoptionDapperRepository;
            }
        }

        public GenericDapperRepository<AllNamadInfo_Daily> AllnamadDailydDapperRepository
        {
            get
            {
                if (_allnamaddailydDapperRepository == null)
                {
                    _allnamaddailydDapperRepository = new GenericDapperRepository<AllNamadInfo_Daily>(db);
                }
                return _allnamaddailydDapperRepository;
            }
        }

        public GenericDapperRepository<AllNamadInfo> AllnamadDapperRepository
        {
            get
            {
                if (_allnamaDapperRepository == null)
                {
                    _allnamaDapperRepository = new GenericDapperRepository<AllNamadInfo>(db);
                }
                return _allnamaDapperRepository;
            }
        }

        public GenericDapperRepository<IndusteryIndex> IndusteryDapperRepository
        {
            get
            {
                if (_industeryDapperRepository == null)
                {
                    _industeryDapperRepository = new GenericDapperRepository<IndusteryIndex>(db);
                }
                return _industeryDapperRepository;
            }
        }

        public GenericDapperRepository<IndNamad> InDapperRepository
        {
            get
            {
                if (_inDapperRepository == null)
                {
                    _inDapperRepository = new GenericDapperRepository<IndNamad>(db);
                }
                return _inDapperRepository;
            }
        }

        public GenericDapperRepository<FavNamad> FavdDapperRepository
        {
            get
            {
                if (_favdDapperRepository == null)
                {
                    _favdDapperRepository = new GenericDapperRepository<FavNamad>(db);
                }
                return _favdDapperRepository;
            }
        }

        public GenericDapperRepository<BourseIndex> BourseDapperRepository
        {
            get
            {
                if (_bourseDapperRepository == null)
                {
                    _bourseDapperRepository = new GenericDapperRepository<BourseIndex>(db);
                }
                return _bourseDapperRepository;
            }
        }

        public GenericDapperRepository<Khodro> KhodrodDapperRepository
        {
            get
            {
                if (_khodrodDapperRepository == null)
                {
                    _khodrodDapperRepository=new GenericDapperRepository<Khodro>(db);
                }
                return _khodrodDapperRepository;
            }
        }

        public GenericDapperRepository<Crypto> CryptoDapperRepository
        {
            get
            {
                if (_cryptoDapperRepository == null)
                {
                    _cryptoDapperRepository= new GenericDapperRepository<Crypto>(db);
                }
                return _cryptoDapperRepository;
            }
        }

        public GenericDapperRepository<Arz> ArzdDapperRepository
        {
            get
            {
                if (_arzdDapperRepository == null)
                {
                    _arzdDapperRepository= new GenericDapperRepository<Arz>(db);
                }
                return _arzdDapperRepository;
            }
        }

        public GenericDapperRepository<CallWebServiceSetting> CallwebservicesettingDapperRepository
        {
            get
            {
                if (_callwebservicesettingDapperRepository == null)
                {
                    _callwebservicesettingDapperRepository=new GenericDapperRepository<CallWebServiceSetting>(db);
                }
                return _callwebservicesettingDapperRepository;
            }
        }

        public GenericDapperRepository<Logger> LoggerdDapperRepository
        {
            get
            {
                if (_loggerdDapperRepository == null)
                {
                    _loggerdDapperRepository= new GenericDapperRepository<Logger>(db);
                }
                return _loggerdDapperRepository;
            }
        }

        public void  Dispose ()
        {
            db.Dispose();
        }
    }
}
