namespace MedicSoft.Web.Models
{
    using MedicSoft.Web.Data.Entities;

    public class UserViewModels : User
    {
        public string RolID { get; set; }

        public string Rol { get; set; }

        
    }
}
