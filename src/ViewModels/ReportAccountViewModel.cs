namespace CarShowRoom.ViewModels
{
    public class ReportAccountViewModel
    {
        public string AccountName { get; set; }
        public int Lead { get; set; }
        public int Interest { get; set; }
        public int Decision { get; set; }
        public int Purchase { get; set; }
        public int Contracted { get; set; }
        public int Denied { get; set; }

        public int Total()
        {
            return Lead + Interest + Decision + Purchase + Contracted;
        }
    }
}