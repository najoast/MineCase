using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Status
{
    [Packet(0x01)]
    [GenerateSerializer]
    public sealed class Ping : IPacket
    {
        [SerializeAs(DataType.Long)]
        public long Payload;

        public void Serialize(BinaryWriter bw)
        {
            throw new NotImplementedException();
        }

        public void Deserialize(ref SpanReader br)
        {
            Payload = br.ReadAsLong();
        }
    }

    [Packet(0x01)]
    [GenerateSerializer]
    public sealed class Pong : IPacket
    {
        [SerializeAs(DataType.Long)]
        public long Payload;

        public void Serialize(BinaryWriter bw)
        {
            throw new NotImplementedException();
        }

        public void Deserialize(ref SpanReader br)
        {
            Payload = br.ReadAsLong();
        }
    }
}
