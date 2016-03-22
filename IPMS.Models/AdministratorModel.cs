namespace Ipms.Models
{
    public class AdministratorModel
    {
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

        public bool IsDisabled
        {
            get;
            set;
        }
    }
}
