using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Login
{
    [Packet(Protocol.SetCompressionPacketId)]
    [GenerateSerializer]
    public sealed class SetCompression : IPacket
    {
        [SerializeAs(DataType.VarInt)]
        public uint Threshold;

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsVarInt(Threshold, out _);
        }

        public void Deserialize(ref SpanReader br)
        {
            Threshold = br.ReadAsVarInt(out _);
        }
    }
}
