using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Multis;
using Server.Network;
using Server.Gumps;
using Server.ContextMenus;

namespace Server.Items
{
    public class RoyalGoldfish : AquariumItem
    {        
        public override string DescriptionA { get { return "A Royal Goldfish"; } }
        public override string DescriptionB { get { return ""; } }

        public override Rarity ItemRarity { get { return Rarity.Rare; } }

        public override Type ItemType { get { return Type.Fish; } }

        public override int ItemId { get { return 15107; } }
        public override int ItemHue { get { return 0; } }

        public override int MinWeight { get { return 10; } }
        public override int MaxWeight { get { return 20; } }

        [Constructable]
        public RoyalGoldfish(): base()
        {
            Name = "a royal goldfish";
        }

        public RoyalGoldfish(Serial serial): base(serial)
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
