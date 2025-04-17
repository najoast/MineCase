using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    [Packet(0x06)]
    [GenerateSerializer]
    public sealed class ServerboundConfirmTransaction : IPacket
    {
        [SerializeAs(DataType.Byte)]
        public byte WindowId;

        [SerializeAs(DataType.Short)]
        public short ActionNumber;

        [SerializeAs(DataType.Boolean)]
        public bool Accepted;

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsByte(WindowId);
            bw.WriteAsShort(ActionNumber);
            bw.WriteAsBoolean(Accepted);
        }

        public void Deserialize(ref SpanReader br)
        {
            WindowId = br.ReadAsByte();
            ActionNumber = br.ReadAsShort();
            Accepted = br.ReadAsBoolean();
        }
    }

    [Packet(0x11)]
    [GenerateSerializer]
    public sealed class ClientboundConfirmTransaction : IPacket
    {
        [SerializeAs(DataType.Byte)]
        public byte WindowId;

        [SerializeAs(DataType.Short)]
        public short ActionNumber;

        [SerializeAs(DataType.Boolean)]
        public bool Accepted;

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsByte(WindowId);
            bw.WriteAsShort(ActionNumber);
            bw.WriteAsBoolean(Accepted);
        }

        public void Deserialize(ref SpanReader br)
        {
            WindowId = br.ReadAsByte();
            ActionNumber = br.ReadAsShort();
            Accepted = br.ReadAsBoolean();
        }
    }
}
