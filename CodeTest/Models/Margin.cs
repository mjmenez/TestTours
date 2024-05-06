using Quote.Contracts;
using PruebaIngreso.Contract;

namespace PruebaIngreso.Models
{
    public abstract class Margin : IMarginProvider
    {
        private readonly IClientProvider client;
        protected IMarginProvider _marginProvider;
        public Margin(IClientProvider clientProvider, IMarginProvider marginProvider)
        {
            this.client = clientProvider;
            _marginProvider = marginProvider;

        }
        public virtual decimal GetMargin(string code)
        {
            var tour = this.client.GetTour(code);
            
            return 0;
        }
    }
}