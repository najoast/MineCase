using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    [Packet(0x0C)]
    [GenerateSerializer]
    public sealed class BlockChange : IPacket
    {
        [SerializeAs(DataType.Position)]
        public Position Location;

        [SerializeAs(DataType.VarInt)]
        public uint BlockId;

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsPosition(Location);
            bw.WriteAsVarInt(BlockId, out _);
        }

        public void Deserialize(ref SpanReader br)
        {
            Location = br.ReadAsPosition();
            BlockId = br.ReadAsVarInt(out _);
        }
    }
}
