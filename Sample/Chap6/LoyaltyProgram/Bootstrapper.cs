using System;
using Nancy;
using Nancy.Bootstrapper;

namespace LoyaltyProgram
{
    public class Bootstrapper: DefaultNancyBootstrapper
    {
        //Remove all default status-code handlers so they don’t alter the responses.
        protected override Func<ITypeCatalog, NancyInternalConfiguration> InternalConfiguration => NancyInternalConfiguration.WithOverrides(builder => builder.StatusCodeHandlers.Clear());
    }
}
