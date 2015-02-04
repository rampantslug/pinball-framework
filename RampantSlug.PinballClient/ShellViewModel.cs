using RampantSlug.Common;
using RampantSlug.PinballClient.ContractImplementations;
using MassTransit;
using RampantSlug.Contracts;
using RampantSlug.PinballClient.Model;

namespace RampantSlug.PinballClient {
    public class ShellViewModel : Caliburn.Micro.PropertyChangedBase, IShell

    {

        
        public string ResponseText 
        {
            get { return TempData.SomeText; } 
        }

        private IServiceBus _bus;


        public ShellViewModel() 
        {


            _bus = BusInitializer.CreateBus("TestSubscriber", x =>
            {
                x.Subscribe(subs =>
                {
                    subs.Consumer<SimpleMessageConsumer>().Permanent();
                });
            });
        }

        public void Exit(){ _bus.Dispose();}


    }
}