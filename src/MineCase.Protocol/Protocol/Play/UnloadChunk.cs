using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    [Packet(0x1E)]
    [GenerateSerializer]
    public sealed class UnloadChunk : IPacket
    {
        [SerializeAs(DataType.Int)]
        public int ChunkX;

        [SerializeAs(DataType.Int)]
        public int ChunkZ;

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsInt(ChunkX);
            bw.WriteAsInt(ChunkZ);
        }

        public void Deserialize(ref SpanReader br)
        {
            ChunkX = br.ReadAsInt();
            ChunkZ = br.ReadAsInt();
        }
    }
}
