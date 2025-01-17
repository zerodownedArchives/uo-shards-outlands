using System;
using Server;

namespace Server.Items
{
	public class RedBook : BaseBook
	{
        public static int GetSBPurchaseValue() { return 1; }
        public static int GetSBSellValue() { return Item.SBDetermineSellPrice(GetSBPurchaseValue()); }

		[Constructable]
		public RedBook() : base( 0xFF1 )
		{
            Name = "book";
		}

		[Constructable]
		public RedBook( int pageCount, bool writable ) : base( 0xFF1, pageCount, writable )
		{
            Name = "book";
		}

		[Constructable]
		public RedBook( string title, string author, int pageCount, bool writable ) : base( 0xFF1, title, author, pageCount, writable )
		{
            Name = "book";
		}

		public RedBook( bool writable ) : base( 0xFF1, writable )
		{
            Name = "book";
		}

		public RedBook( Serial serial ) : base( serial )
		{
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}
	}
}