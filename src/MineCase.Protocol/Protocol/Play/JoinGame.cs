﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    [Packet(0x26)]
    [GenerateSerializer]
    public sealed class JoinGame : IPacket
    {
        [SerializeAs(DataType.Int)]
        public int EID;

        [SerializeAs(DataType.Byte)]
        public byte GameMode;

        [SerializeAs(DataType.Int)]
        public int Dimension;

        [SerializeAs(DataType.Long)]
        public long HashedSeed;

        [SerializeAs(DataType.Byte)]
        public byte MaxPlayers;

        [SerializeAs(DataType.String)]
        public string LevelType;

        [SerializeAs(DataType.VarInt)]
        public uint ViewDistance;

        [SerializeAs(DataType.Boolean)]
        public bool ReducedDebugInfo;

        [SerializeAs(DataType.Boolean)]
        public bool EnableRespawnScreen;

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsInt(EID);
            bw.WriteAsByte(GameMode);
            bw.WriteAsInt(Dimension);
            bw.WriteAsLong(HashedSeed);
            bw.WriteAsByte(MaxPlayers);
            bw.WriteAsString(LevelType);
            bw.WriteAsVarInt(ViewDistance, out _);
            bw.WriteAsBoolean(ReducedDebugInfo);
            bw.WriteAsBoolean(EnableRespawnScreen);
        }

        public void Deserialize(ref SpanReader br)
        {
            EID = br.ReadAsInt();
            GameMode = br.ReadAsByte();
            Dimension = br.ReadAsInt();
            HashedSeed = br.ReadAsLong();
            MaxPlayers = br.ReadAsByte();
            LevelType = br.ReadAsString();
            ViewDistance = br.ReadAsVarInt(out _);
            ReducedDebugInfo = br.ReadAsBoolean();
            EnableRespawnScreen = br.ReadAsBoolean();
        }
    }
}
