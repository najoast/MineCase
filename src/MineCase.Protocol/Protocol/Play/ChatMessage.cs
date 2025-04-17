using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    [Packet(0x03)]
    [GenerateSerializer]
    public sealed class ServerboundChatMessage : IPacket
    {
        [SerializeAs(DataType.String)]
        public string Message;

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsString(Message);
        }

        public void Deserialize(ref SpanReader br)
        {
            Message = br.ReadAsString();
        }
    }

    // TODO
    [Packet(0x0F)]
    [GenerateSerializer]
    public sealed class ClientboundChatMessage : IPacket
    {
        [SerializeAs(DataType.Chat)]
        public Chat JSONData;

        [SerializeAs(DataType.Byte)]
        public byte Position; // 0: chat (chat box), 1: system message (chat box), 2: game info (above hotbar).

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsChat(JSONData);
            bw.WriteAsByte(Position);
        }

        public void Deserialize(ref SpanReader br)
        {
            JSONData = br.ReadAsChat();
            Position = br.ReadAsByte();
        }
    }
}
