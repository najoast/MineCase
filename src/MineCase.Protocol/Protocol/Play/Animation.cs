using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    public enum Hand : uint
    {
        Main = 0,
        Off = 1
    }

    [Packet(0x2A)]
    [GenerateSerializer]
    public sealed class ServerboundAnimation : IPacket
    {
        [SerializeAs(DataType.VarInt)]
        public Hand Hand;

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsVarInt((uint)Hand, out _);
        }

        public void Deserialize(ref SpanReader br)
        {
            Hand = (Hand)br.ReadAsVarInt(out _);
        }
    }
}
