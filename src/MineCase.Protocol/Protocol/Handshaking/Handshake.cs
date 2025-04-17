using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Handshaking
{
    [Packet(0x00)]
    [GenerateSerializer]
    public sealed class Handshake : IPacket
    {
        [SerializeAs(DataType.VarInt)]
        public uint ProtocolVersion;

        [SerializeAs(DataType.String)]
        public string ServerAddress;

        [SerializeAs(DataType.UnsignedShort)]
        public ushort ServerPort;

        [SerializeAs(DataType.VarInt)]
        public uint NextState;

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsVarInt(ProtocolVersion, out _);
            bw.WriteAsString(ServerAddress);
            bw.WriteAsUnsignedShort(ServerPort);
            bw.WriteAsVarInt(NextState, out _);
        }

        public void Deserialize(ref SpanReader br)
        {
            ProtocolVersion = br.ReadAsVarInt(out _);
            ServerAddress = br.ReadAsString();
            ServerPort = br.ReadAsUnsignedShort();
            NextState = br.ReadAsVarInt(out _);
        }
    }
}
