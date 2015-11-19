using System.Windows.Controls;
using Microsoft.Practices.Unity;
using PrismApp.ViewModels;

namespace PrismApp.Views
{
    public partial class PublisherView : UserControl
    {
        public PublisherView()
        {
            InitializeComponent();
        }

        [Dependency]
        public PublisherViewModel ViewModel
        {
            get { return DataContext as PublisherViewModel; }
            set { DataContext = value; }
        }
    }
}
