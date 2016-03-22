using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipms.UI.Models
{
    public class Administrator
    {
        private static Administrator _current;

        private Administrator()
        {

        }

        public static Administrator Current
        {
            get
            {
                if(_current == null)
                {
                    lock(App.Current)
                    {
                        if (_current == null)
                        {
                            _current = new Administrator();
                        }
                    }
                }

                return _current;
            }
        }

        public int Id
        {
            get;
            set;
        }

        public string Login
        {
            get;
            set;
        }

        public string Forename
        {
            get;
            set;
        }

        public string Surname
        {
            get;
            set;
        }
    }
}
