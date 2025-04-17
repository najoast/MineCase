using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    [Packet(0x2D)]
    [GenerateSerializer]
    public sealed class UseItem : IPacket
    {
        [SerializeAs(DataType.VarInt)]
        public Hand Hand;

        public void Serialize(BinaryWriter bw)
        {
            throw new NotImplementedException();
        }

        public void Deserialize(ref SpanReader br)
        {
            Hand = (Hand)br.ReadAsVarInt(out _);
        }
    }
}
