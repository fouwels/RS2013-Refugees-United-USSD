using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RS2013.RefugeesUnited.Data;
using RS2013.RefugeesUnited.Model;

namespace RS2013.RefugeesUnited.Services.Impl
{
    public class RefUnitedApiConnect : IRefUnitedApiConnect
    {
        private readonly string _apiServerHost;
        private readonly string _apiServerUsername;
        private readonly string _apiServerPassword;

        public RefUnitedApiConnect(string apiServerHostIn, string apiServerUsernameIn, string apiServerPasswordIn)
        {
            //DEBUG
            //_apiServerHost = "http://api.ru.istykker.dk/";
            //_apiServerUsername = "hackathon";
            //_apiServerPassword = "179d50c6eb31188925926a5d1872e8117dc58572";

            //ACTUAL
            _apiServerHost = apiServerHostIn;
            _apiServerUsername = apiServerUsernameIn;
            _apiServerPassword = apiServerPasswordIn;

        }
        public void ProfileGet(User userIn)
        {
            //should not be void, return obj w/ profile
            //Get info from API
        }
        public void ProfileCreate(User userIn)
        {
            
        }
        public void ProfileUpdate(User userIn)
        {
            
        }
        public void ProfileDelete(User userIn)
        {
            
        }
    }
}
