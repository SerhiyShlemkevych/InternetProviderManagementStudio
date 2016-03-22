namespace Ipms.Models
{
    public class TariffModel
    {
        public int Id
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public decimal Price
        {
            get;
            set;
        }
        public int UploadSpeed
        {
            get;
            set;
        }
        public int DownloadSpeed
        {
            get;
            set;
        }
        public bool IsArchive
        {
            get;
            set;
        }
    }
}
