using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    [Packet(0x49)]
    [GenerateSerializer]
    public sealed class UpdateHealth : IPacket
    {
        [SerializeAs(DataType.Float)]
        public float Health;

        [SerializeAs(DataType.VarInt)]
        public uint Food;

        [SerializeAs(DataType.Float)]
        public float FoodSaturation;

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsFloat(Health);
            bw.WriteAsVarInt(Food, out _);
            bw.WriteAsFloat(FoodSaturation);
        }

        public void Deserialize(ref SpanReader br)
        {
            Health = br.ReadAsFloat();
            Food = br.ReadAsVarInt(out _);
            FoodSaturation = br.ReadAsFloat();
        }
    }
}
