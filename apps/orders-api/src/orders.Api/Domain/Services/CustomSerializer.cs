using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Confluent.Kafka;

namespace orders.Api.Domain.Services
{
    public class CustomSerializer<T> : ISerializer<T>, IDeserializer<T>
    {
        public T Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            return JsonSerializer.Deserialize<T>(data.ToArray());
        }

        public byte[] Serialize(T data, SerializationContext context)
        {
            return JsonSerializer.SerializeToUtf8Bytes(data);
        }
    }
}