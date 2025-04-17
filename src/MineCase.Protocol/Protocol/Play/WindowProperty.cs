using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    [Packet(0x15)]
    [GenerateSerializer]
    public sealed class WindowProperty : IPacket
    {
        [SerializeAs(DataType.Byte)]
        public byte WindowId;

        [SerializeAs(DataType.Short)]
        public short Property;

        [SerializeAs(DataType.Short)]
        public short Value;

        public void Serialize(BinaryWriter bw)
        {
            throw new NotImplementedException();
        }

        public void Deserialize(ref SpanReader br)
        {
            WindowId = br.ReadAsByte();
            Property = br.ReadAsShort();
            Value = br.ReadAsShort();
        }
    }
}
