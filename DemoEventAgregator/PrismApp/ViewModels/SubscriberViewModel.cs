using Prism.Events;
using Prism.Mvvm;
using PrismApp.Events;

namespace PrismApp.ViewModels
{
    public class SubscriberViewModel : BindableBase
    {
        private SubscriptionToken _subscriptionToken;

        private string _value;
        public string Value
        {
            get { return _value; }
            set { SetProperty(ref _value, value); }
        }

        private readonly IEventAggregator _eventAggregator;
        public SubscriberViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            Suscribe();
        }

        private void Suscribe()
        {
            
            var theEvent = _eventAggregator.GetEvent<CustomEvent>();
            _subscriptionToken = theEvent.Subscribe(OnValueReceived, ThreadOption.UIThread);
        }

        private void Unsubscribe()
        {
            if ((_eventAggregator != null) && (_subscriptionToken != null))
                _eventAggregator.GetEvent<CustomEvent>().Unsubscribe(_subscriptionToken);
        }
        
        private void OnValueReceived(CustomEventArgs e)
        {
            Value = e.Value;
        }
    }
}
