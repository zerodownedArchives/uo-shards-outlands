using System;

namespace Server.Items
{
    public class Coconut : Food
    {
        [Constructable]
        public Coconut(): this(1)
        {
        }

        [Constructable]
        public Coconut(int amount): base(amount, 0x1726)
        {
            Weight = 1.0;
        }

        public Coconut(Serial serial): base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}