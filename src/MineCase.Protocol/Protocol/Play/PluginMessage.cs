using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    [Packet(0x0A)]
    [GenerateSerializer]
    public sealed class ServerboundPluginMessage : IPacket
    {
        [SerializeAs(DataType.String)]
        public string Channel;

        [SerializeAs(DataType.ByteArray)]
        public byte[] Data;

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsString(Channel);
            bw.WriteAsByteArray(Data);
        }

        public void Deserialize(ref SpanReader br)
        {
            Channel = br.ReadAsString();
            Data = br.ReadAsByteArray();
        }
    }
}
