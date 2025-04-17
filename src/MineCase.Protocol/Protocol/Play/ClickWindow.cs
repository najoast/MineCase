using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    [Packet(0x09)]
    [GenerateSerializer]
    public sealed class ClickWindow : IPacket
    {
        [SerializeAs(DataType.Byte)]
        public byte WindowId;

        [SerializeAs(DataType.Short)]
        public short Slot;

        [SerializeAs(DataType.Byte)]
        public byte Button;

        [SerializeAs(DataType.Short)]
        public short ActionNumber;

        [SerializeAs(DataType.VarInt)]
        public uint Mode;

        [SerializeAs(DataType.Slot)]
        public Slot ClickedItem;

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsByte(WindowId);
            bw.WriteAsShort(Slot);
            bw.WriteAsByte(Button);
            bw.WriteAsShort(ActionNumber);
            bw.WriteAsVarInt(Mode, out _);
            bw.WriteAsSlot(ClickedItem);
        }

        public void Deserialize(ref SpanReader br)
        {
            WindowId = br.ReadAsByte();
            Slot = br.ReadAsShort();
            Button = br.ReadAsByte();
            ActionNumber = br.ReadAsShort();
            Mode = br.ReadAsVarInt(out _);
            ClickedItem = br.ReadAsSlot();
        }
    }
}
