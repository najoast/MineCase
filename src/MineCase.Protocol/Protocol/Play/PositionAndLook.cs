using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    [Packet(0x36)]
    [GenerateSerializer]
    public sealed class ClientboundPositionAndLook : IPacket
    {
        [SerializeAs(DataType.Double)]
        public double X;

        [SerializeAs(DataType.Double)]
        public double Y;

        [SerializeAs(DataType.Double)]
        public double Z;

        [SerializeAs(DataType.Float)]
        public float Yaw;

        [SerializeAs(DataType.Float)]
        public float Pitch;

        [SerializeAs(DataType.Byte)]
        public byte Flags;

        [SerializeAs(DataType.VarInt)]
        public uint TeleportId;

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsDouble(X);
            bw.WriteAsDouble(Y);
            bw.WriteAsDouble(Z);
            bw.WriteAsFloat(Yaw);
            bw.WriteAsFloat(Pitch);
            bw.WriteAsByte(Flags);
            bw.WriteAsVarInt(TeleportId, out _);
        }

        public void Deserialize(ref SpanReader br)
        {
            X = br.ReadAsDouble();
            Y = br.ReadAsDouble();
            Z = br.ReadAsDouble();
            Yaw = br.ReadAsFloat();
            Pitch = br.ReadAsFloat();
            Flags = br.ReadAsByte();
            TeleportId = br.ReadAsVarInt(out _);
        }
    }

    [Packet(0x12)]
    [GenerateSerializer]
    public sealed class ServerboundPositionAndLook : IPacket
    {
        [SerializeAs(DataType.Double)]
        public double X;

        [SerializeAs(DataType.Double)]
        public double FeetY;

        [SerializeAs(DataType.Double)]
        public double Z;

        [SerializeAs(DataType.Float)]
        public float Yaw;

        [SerializeAs(DataType.Float)]
        public float Pitch;

        [SerializeAs(DataType.Boolean)]
        public bool OnGround;

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsDouble(X);
            bw.WriteAsDouble(FeetY);
            bw.WriteAsDouble(Z);
            bw.WriteAsFloat(Yaw);
            bw.WriteAsFloat(Pitch);
            bw.WriteAsBoolean(OnGround);
        }

        public void Deserialize(ref SpanReader br)
        {
            X = br.ReadAsDouble();
            FeetY = br.ReadAsDouble();
            Z = br.ReadAsDouble();
            Yaw = br.ReadAsFloat();
            Pitch = br.ReadAsFloat();
            OnGround = br.ReadAsBoolean();
        }
    }
}
