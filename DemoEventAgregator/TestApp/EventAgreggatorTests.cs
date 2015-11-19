using System;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Prism.Events;
using PrismApp.Events;
using PrismApp.ViewModels;

namespace TestApp
{
    [TestClass]
    public class EventAgreggatorTests
    {
        private const string ValueTest = "0123456789";
        
        private Action<CustomEventArgs> _eventAction;

        // Mocks
        private Mock<CustomEvent> _mockEvent;
        private Mock<IEventAggregator> _mockEventAggregator;
     

        private IUnityContainer SetupContainer()
        {
             _mockEvent = new Mock<CustomEvent>();
            _mockEventAggregator = new Mock<IEventAggregator>();

            // GetEvent -> returns mock object
            _mockEventAggregator.Setup(e => e.GetEvent<CustomEvent>()).Returns(_mockEvent.Object);


            // Mock Subscribe action -> Assign an action to _eventAction
            _mockEvent.Setup(p => p.Subscribe(It.IsAny<Action<CustomEventArgs>>(),
                                                   It.IsAny<ThreadOption>(),
                                                   It.IsAny<bool>(),
                                                   It.IsAny<Predicate<CustomEventArgs>>()))
                   .Callback<Action<CustomEventArgs>, ThreadOption, bool, Predicate<CustomEventArgs>>((e, t, b, a) => _eventAction = e);

            // Mock Publish action -> Call _eventAction
            _mockEvent.Setup(p => p.Publish(It.IsAny<CustomEventArgs>()))
                .Callback<CustomEventArgs>(args => _eventAction(args));

            // Setup container
            IEventAggregator eventAggregator = _mockEventAggregator.Object;

            UnityContainer container = new UnityContainer();
            container.RegisterInstance(eventAggregator);
            container.RegisterType<PublisherViewModel>();
            container.RegisterType<SubscriberViewModel>();
            
            return container;
        }

        [TestMethod]
        public void TestSubscribeEvent()
        {
            IUnityContainer container = SetupContainer();

            IEventAggregator eventAggregator = container.Resolve<IEventAggregator>();
            SubscriberViewModel viewModel = container.Resolve<SubscriberViewModel>();

            eventAggregator.GetEvent<CustomEvent>().Publish(new CustomEventArgs(ValueTest));
            Assert.AreEqual(ValueTest, viewModel.Value);
        }

        [TestMethod]
        public void TestPublishEvent()
        {
            IUnityContainer container = SetupContainer();

            
            PublisherViewModel publisherViewModel = container.Resolve<PublisherViewModel>();
            SubscriberViewModel viewModel = container.Resolve<SubscriberViewModel>();

            publisherViewModel.Value = ValueTest;
            publisherViewModel.PublishCommand.Execute(null);
            
            Assert.AreEqual(ValueTest, viewModel.Value);
        }
    }
}
