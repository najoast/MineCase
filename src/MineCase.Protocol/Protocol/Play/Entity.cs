using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    // FIXME: 1.15.2 no longer has this packet
    [Packet(0x25)]
    [GenerateSerializer]
    public sealed class Entity : IPacket
    {
        [SerializeAs(DataType.VarInt)]
        public uint EID;

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsVarInt(EID, out _);
        }

        public void Deserialize(ref SpanReader br)
        {
            EID = br.ReadAsVarInt(out _);
        }
    }
}
