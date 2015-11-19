using System.Windows.Input;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using PrismApp.Events;

namespace PrismApp.ViewModels
{
    public class PublisherViewModel : BindableBase
    {
        private string _value;

        public string Value
        {
            get { return _value; }
            set { SetProperty(ref _value, value); }
        }

        public ICommand PublishCommand { get; private set; }

        private readonly IEventAggregator _eventAggregator;

        public PublisherViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            PublishCommand = new DelegateCommand(PublishValue, CanPublish);
        }

        private void PublishValue()
        {
            _eventAggregator?.GetEvent<CustomEvent>().Publish(new CustomEventArgs(Value));
        }

        private bool CanPublish()
        {
            return true;
        }
    }
}
