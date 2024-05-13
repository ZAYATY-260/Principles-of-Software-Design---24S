using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bank_mangement_system
{
    class Validation
    {


        public bool Login_validation(String username , String Password)
        {
            bool done = true;
            if (username == "" || Password == "")
            {
                done = false;
            }

            return done;
        }


    }
}
