﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    // FIXME : 1.15.2 does not have this packet
    [Packet(0x27)]
    [GenerateSerializer]
    public sealed class EntityLookAndRelativeMove : IPacket
    {
        [SerializeAs(DataType.VarInt)]
        public uint EID;

        [SerializeAs(DataType.Short)]
        public short DeltaX;

        [SerializeAs(DataType.Short)]
        public short DeltaY;

        [SerializeAs(DataType.Short)]
        public short DeltaZ;

        [SerializeAs(DataType.Angle)]
        public Angle Yaw;

        [SerializeAs(DataType.Angle)]
        public Angle Pitch;

        [SerializeAs(DataType.Boolean)]
        public bool OnGround;

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsVarInt(EID, out _);
            bw.WriteAsShort(DeltaX);
            bw.WriteAsShort(DeltaY);
            bw.WriteAsShort(DeltaZ);
            bw.WriteAsAngle(Yaw);
            bw.WriteAsAngle(Pitch);
            bw.WriteAsBoolean(OnGround);
        }

        public void Deserialize(ref SpanReader br)
        {
            EID = br.ReadAsVarInt(out _);
            DeltaX = br.ReadAsShort();
            DeltaY = br.ReadAsShort();
            DeltaZ = br.ReadAsShort();
            Yaw = br.ReadAsAngle();
            Pitch = br.ReadAsAngle();
            OnGround = br.ReadAsBoolean();
        }
    }
}
