using System;
using Server;
using Server.Engines.Craft;

namespace Server.Items
{
	public class DrawKnife : BaseTool
	{
        public static int GetSBPurchaseValue() { return 1; }
        public static int GetSBSellValue() { return Item.SBDetermineSellPrice(GetSBPurchaseValue()); }

		public override CraftSystem CraftSystem{ get{ return DefCarpentry.CraftSystem; } }

		[Constructable]
		public DrawKnife() : base( 0x10E4 )
		{
            Name = "drawing knife";
			Weight = 1.0;
		}

		[Constructable]
		public DrawKnife( int uses ) : base( uses, 0x10E4 )
		{
            Name = "drawing knife";
			Weight = 1.0;
		}

		public DrawKnife( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}