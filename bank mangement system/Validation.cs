using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bank_mangement_system
{
    class Validation
    {
        String Message;

        public String Login_validation(String username , String Password)
        {
            if (username == "" && Password == "")
            {
                Message = "Please fill the Form !";
            }
            else if (username == "")
            {
                Message = "username is empty !";
            }
            else
            {
                Message = "password is empty !";
            }

            return Message;
        }


    }
}
