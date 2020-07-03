﻿namespace CommeTuVeux2.Infrastructure
{
    using System;
    using System.IO;
    using Newtonsoft.Json;

    public class JSonSerializer
    {
        [Obsolete]
        public T Deserialize<T>(Type objectType, string data)
            => (T)GetSerializer().Deserialize(new StringReader(data), objectType);

        public string Serialize(object obj)
        {
            var textWriter = new StringWriter();
            new JsonSerializer().Serialize(textWriter, obj);

            return textWriter.ToString();
        }

        [Obsolete]
        public object? Deserialize(Type objectType, string data) => GetSerializer().Deserialize(new StringReader(data), objectType);

        [Obsolete]
        private JsonSerializer GetSerializer()
        {
            var settings = new JsonSerializerSettings();
            settings.ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor;
            var contractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
            contractResolver.DefaultMembersSearchFlags = contractResolver.DefaultMembersSearchFlags | System.Reflection.BindingFlags.NonPublic;
            settings.ContractResolver = contractResolver;

            return JsonSerializer.Create(settings);
        }
    }

}
