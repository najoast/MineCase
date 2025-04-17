using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    [Packet(0x06)]
    [GenerateSerializer]
    public sealed class ClientboundAnimation : IPacket
    {
        [SerializeAs(DataType.VarInt)]
        public uint EntityID;

        [SerializeAs(DataType.Byte)]
        public ClientboundAnimationId AnimationID;

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsVarInt(EntityID, out _);
            bw.WriteAsByte((byte)AnimationID);
        }

        public void Deserialize(ref SpanReader br)
        {
            EntityID = br.ReadAsVarInt(out _);
            AnimationID = (ClientboundAnimationId)br.ReadAsByte();
        }
    }
}
