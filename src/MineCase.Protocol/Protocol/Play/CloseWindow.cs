using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    [Packet(0x0A)]
    [GenerateSerializer]
    public sealed class ServerboundCloseWindow : IPacket
    {
        [SerializeAs(DataType.Byte)]
        public byte WindowId;

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsByte(WindowId);
        }

        public void Deserialize(ref SpanReader br)
        {
            WindowId = br.ReadAsByte();
        }
    }

    [Packet(0x14)]
    [GenerateSerializer]
    public sealed class ClientboundCloseWindow : IPacket
    {
        [SerializeAs(DataType.Byte)]
        public byte WindowId;

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsByte(WindowId);
        }

        public void Deserialize(ref SpanReader br)
        {
            WindowId = br.ReadAsByte();
        }
    }
}
