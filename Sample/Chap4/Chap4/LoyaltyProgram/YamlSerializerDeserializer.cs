using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Nancy.ModelBinding;
using Nancy.Responses.Negotiation;
using YamlDotNet.Serialization;

namespace LoyaltyProgram
{
    public class YamlSerializerDeserializer : IBodyDeserializer
    {
        /// <summary>
        /// Tells Nancy which content types this deserializer can handle
        /// </summary>
        /// <param name="mediaRange"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public bool CanDeserialize(MediaRange mediaRange, BindingContext context) =>
            mediaRange.Subtype.ToString().EndsWith("yaml");

        /// <summary>
        /// Tries to deserialize the requestbody to the type needed by the application code
        /// </summary>
        /// <param name="mediaRange"></param>
        /// <param name="bodyStream"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public object Deserialize(MediaRange mediaRange, Stream bodyStream, BindingContext context)
        {
            var yamlDeserializer = new Deserializer();
            var reader = new StreamReader(bodyStream);
            return yamlDeserializer.Deserialize(reader, context.DestinationType);
        }
    }
}
