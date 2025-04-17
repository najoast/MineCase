using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Login
{
    [Packet(0x00)]
    [GenerateSerializer]
    public sealed class LoginStart : IPacket
    {
        [SerializeAs(DataType.String)]
        public string Name;

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsString(Name);
        }

        public void Deserialize(ref SpanReader br)
        {
            Name = br.ReadAsString();
        }
    }

    [Packet(0x00)]
    [GenerateSerializer]
    public sealed class LoginDisconnect : IPacket
    {
        [SerializeAs(DataType.String)]
        public string Reason;

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsString(Reason);
        }

        public void Deserialize(ref SpanReader br)
        {
            Reason = br.ReadAsString();
        }
    }

    [Packet(0x02)]
    [GenerateSerializer]
    public sealed class LoginSuccess : IPacket
    {
        [SerializeAs(DataType.String)]
        public string UUID;

        [SerializeAs(DataType.String)]
        public string Username;

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsString(UUID);
            bw.WriteAsString(Username);
        }

        public void Deserialize(ref SpanReader br)
        {
            UUID = br.ReadAsString();
            Username = br.ReadAsString();
        }
    }
}
