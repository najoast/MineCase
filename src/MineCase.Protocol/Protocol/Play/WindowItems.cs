using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    [Packet(0x15)]
    [GenerateSerializer]
    public sealed class WindowItems : IPacket
    {
        [SerializeAs(DataType.Byte)]
        public byte WindowId;

        [SerializeAs(DataType.Short)]
        public short Count;

        [SerializeAs(DataType.SlotArray, ArrayLengthMember = nameof(Count))]
        public Slot[] Slots;

        public void Serialize(BinaryWriter bw)
        {
            throw new NotImplementedException();
        }

        public void Deserialize(ref SpanReader br)
        {
            WindowId = br.ReadAsByte();
            Count = br.ReadAsShort();
            Slots = br.ReadAsSlotArray(Count);
        }
    }
}
