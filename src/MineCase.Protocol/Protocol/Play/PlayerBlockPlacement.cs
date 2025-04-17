using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    [Packet(0x2C)]
    [GenerateSerializer]
    public sealed class PlayerBlockPlacement : IPacket
    {
        [SerializeAs(DataType.Position)]
        public Position Location;

        [SerializeAs(DataType.VarInt)]
        public PlayerDiggingFace Face;

        [SerializeAs(DataType.VarInt)]
        public Hand Hand;

        [SerializeAs(DataType.Float)]
        public float CursorPositionX;

        [SerializeAs(DataType.Float)]
        public float CursorPositionY;

        [SerializeAs(DataType.Float)]
        public float CursorPositionZ;

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsPosition(Location);
            bw.WriteAsVarInt((byte)Face, out _);
            bw.WriteAsVarInt((uint)Hand, out _);
            bw.WriteAsFloat(CursorPositionX);
            bw.WriteAsFloat(CursorPositionY);
            bw.WriteAsFloat(CursorPositionZ);
        }

        public void Deserialize(ref SpanReader br)
        {
            Location = br.ReadAsPosition();
            Face = (PlayerDiggingFace)br.ReadAsVarInt(out _);
            Hand = (Hand)br.ReadAsVarInt(out _);
            CursorPositionX = br.ReadAsFloat();
            CursorPositionY = br.ReadAsFloat();
            CursorPositionZ = br.ReadAsFloat();
        }
    }
}
