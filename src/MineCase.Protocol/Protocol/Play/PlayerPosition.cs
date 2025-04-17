using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    [Packet(0x11)]
    [GenerateSerializer]
    public sealed class PlayerPosition : IPacket
    {
        [SerializeAs(DataType.Double)]
        public double X;

        [SerializeAs(DataType.Double)]
        public double FeetY;

        [SerializeAs(DataType.Double)]
        public double Z;

        [SerializeAs(DataType.Boolean)]
        public bool OnGround;

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsDouble(X);
            bw.WriteAsDouble(FeetY);
            bw.WriteAsDouble(Z);
            bw.WriteAsBoolean(OnGround);
        }

        public void Deserialize(ref SpanReader br)
        {
            X = br.ReadAsDouble();
            FeetY = br.ReadAsDouble();
            Z = br.ReadAsDouble();
            OnGround = br.ReadAsBoolean();
        }
    }
}
