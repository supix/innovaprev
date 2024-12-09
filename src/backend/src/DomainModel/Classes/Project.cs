namespace DomainModel.Classes
{
    public partial class Project
    {
        public required PersonalData PersonalData { get; set; }
        public required ProductData ProductData { get; set; }
        public required WindowsData[] WindowsData { get; set; }
    }
}