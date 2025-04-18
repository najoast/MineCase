﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    [Packet(0x48)]
    [GenerateSerializer]
    public sealed class SetExperience : IPacket
    {
        [SerializeAs(DataType.Float)]
        public float ExperienceBar;

        [SerializeAs(DataType.VarInt)]
        public uint Level;

        [SerializeAs(DataType.VarInt)]
        public uint TotalExperience;

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsFloat(ExperienceBar);
            bw.WriteAsVarInt(Level, out _);
            bw.WriteAsVarInt(TotalExperience, out _);
        }

        public void Deserialize(ref SpanReader br)
        {
            ExperienceBar = br.ReadAsFloat();
            Level = br.ReadAsVarInt(out _);
            TotalExperience = br.ReadAsVarInt(out _);
        }
    }
}
