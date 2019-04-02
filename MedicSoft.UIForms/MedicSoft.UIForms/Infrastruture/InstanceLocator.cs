using MedicSoft.UIForms.ViewModels;

namespace MedicSoft.UIForms.Infrastruture
{
    public class InstanceLocator
    {
        public MainViewModel Main { get; set; }

        public InstanceLocator()
        {
            this.Main = new MainViewModel();
        }
    }
}
