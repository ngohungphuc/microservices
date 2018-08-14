using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Nancy;
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

    public class YamlBodySerializer : IResponseProcessor
    {
        /// <summary>
        /// Tells Nancy that this processor can handle accept header values that end with “yaml”
        /// </summary>
        /// <param name="requestedMediaRange"></param>
        /// <param name="model"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public ProcessorMatch CanProcess(MediaRange requestedMediaRange, dynamic model, NancyContext context)
        {
            return requestedMediaRange.Subtype.ToString().EndsWith("yaml")
                ? new ProcessorMatch
                {
                    ModelResult = MatchResult.DontCare,
                    RequestedContentTypeResult = MatchResult.NonExactMatch
                }
                : ProcessorMatch.None;
        }

        /// <summary>
        /// Creates a new response object to use in the rest of Nancy’s pipeline
        /// </summary>
        /// <param name="requestedMediaRange"></param>
        /// <param name="model"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public Response Process(MediaRange requestedMediaRange, dynamic model, NancyContext context)
        {
            return new Response
            {
                //Sets up a function that writes the response body to a stream
                Contents = stream =>
                {
                    var yamlSerializer = new Serializer();
                    var streamWriter = new StreamWriter(stream);
                    //Writes the YAML serialized object to the stream Nancy uses for  the response body
                    yamlSerializer.Serialize(streamWriter, model);
                    streamWriter.Flush();
                },
                ContentType = "application/yaml"
            };
        }

        /// <summary>
        /// Tells Nancy which file extensions can be handled by this response processor
        /// </summary>
        public IEnumerable<Tuple<string, MediaRange>> ExtensionMappings
        {
            get
            {
                yield return new Tuple<string, MediaRange>("yaml", new MediaRange("application/yaml"));
            }
        }
    }
}
