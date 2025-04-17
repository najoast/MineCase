using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    [Packet(0x03)]
    [GenerateSerializer]
    public sealed class SpawnMob : IPacket
    {
        [SerializeAs(DataType.VarInt)]
        public uint EID;

        [SerializeAs(DataType.UUID)]
        public Guid EntityUUID;

        [SerializeAs(DataType.Byte)]
        public byte Type;

        [SerializeAs(DataType.Double)]
        public double X;

        [SerializeAs(DataType.Double)]
        public double Y;

        [SerializeAs(DataType.Double)]
        public double Z;

        [SerializeAs(DataType.Angle)]
        public Angle Pitch;

        [SerializeAs(DataType.Angle)]
        public Angle Yaw;

        [SerializeAs(DataType.Angle)]
        public Angle HeadPitch;

        [SerializeAs(DataType.Short)]
        public short VelocityX;

        [SerializeAs(DataType.Short)]
        public short VelocityY;

        [SerializeAs(DataType.Short)]
        public short VelocityZ;

        [SerializeAs(DataType.ByteArray)]
        public byte[] Metadata;

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsVarInt(EID, out _);
            bw.WriteAsUUID(EntityUUID);
            bw.WriteAsByte(Type);
            bw.WriteAsDouble(X);
            bw.WriteAsDouble(Y);
            bw.WriteAsDouble(Z);
            bw.WriteAsAngle(Pitch);
            bw.WriteAsAngle(Yaw);
            bw.WriteAsAngle(HeadPitch);
            bw.WriteAsShort(VelocityX);
            bw.WriteAsShort(VelocityY);
            bw.WriteAsShort(VelocityZ);
            bw.WriteAsByteArray(Metadata);
        }

        public void Deserialize(ref SpanReader br)
        {
            EID = br.ReadAsVarInt(out _);
            EntityUUID = br.ReadAsUUID();
            Type = br.ReadAsByte();
            X = br.ReadAsDouble();
            Y = br.ReadAsDouble();
            Z = br.ReadAsDouble();
            Pitch = br.ReadAsAngle();
            Yaw = br.ReadAsAngle();
            HeadPitch = br.ReadAsAngle();
            VelocityX = br.ReadAsShort();
            VelocityY = br.ReadAsShort();
            VelocityZ = br.ReadAsShort();
            Metadata = br.ReadAsByteArray();
        }
    }
}
