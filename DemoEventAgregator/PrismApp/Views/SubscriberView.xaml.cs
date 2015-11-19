using System.Windows.Controls;
using Microsoft.Practices.Unity;
using PrismApp.ViewModels;

namespace PrismApp.Views
{
    public partial class SubscriberView : UserControl
    {
        public SubscriberView()
        {
            InitializeComponent();
        }

        [Dependency]
        public SubscriberViewModel ViewModel
        {
            get { return DataContext as SubscriberViewModel; }
            set { DataContext = value; }
        }
    }
}
