using System.IO;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    [Packet(0x09)]
    [GenerateSerializer]
    public sealed class BlockBreakAnimation : IPacket
    {
        [SerializeAs(DataType.VarInt)]
        public uint EntityID;

        [SerializeAs(DataType.Position)]
        public Position BlockPosition;

        [SerializeAs(DataType.Byte)]
        public byte DestoryStage;

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsVarInt(EntityID, out _);
            bw.WriteAsPosition(BlockPosition);
            bw.WriteAsByte(DestoryStage);
        }

        public void Deserialize(ref SpanReader br)
        {
            EntityID = br.ReadAsVarInt(out _);
            BlockPosition = br.ReadAsPosition();
            DestoryStage = br.ReadAsByte();
        }
    }
}
