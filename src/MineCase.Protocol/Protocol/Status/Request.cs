using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Status
{
    [Packet(0x00)]
    [GenerateSerializer]
    public sealed class Request : IPacket
    {
        public static readonly Request Empty = new Request();

        public void Serialize(BinaryWriter bw)
        {
            throw new NotImplementedException();
        }

        public void Deserialize(ref SpanReader br)
        {
            throw new NotImplementedException();
        }
    }

    [Packet(0x00)]
    [GenerateSerializer]
    public sealed class Response : IPacket
    {
        [SerializeAs(DataType.String)]
        public string JsonResponse;

        public void Serialize(BinaryWriter bw)
        {
            throw new NotImplementedException();
        }

        public void Deserialize(ref SpanReader br)
        {
            JsonResponse = br.ReadAsString();
        }
    }
}
