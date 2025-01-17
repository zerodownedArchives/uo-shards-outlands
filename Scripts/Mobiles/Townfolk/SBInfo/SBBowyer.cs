using System;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
	public class SBBowyer : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBBowyer()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
                Add(new GenericBuyInfo("Hatchet", typeof(Hatchet), Hatchet.GetSBPurchaseValue(), 50, 0xF43, 0));
                Add(new GenericBuyInfo("Saw", typeof(Saw), Saw.GetSBPurchaseValue(), 50, 0x1034, 0));

                Add(new GenericBuyInfo("Arrow", typeof(Arrow), Arrow.GetSBPurchaseValue(), 500, 0xF3F, 0));
                Add(new GenericBuyInfo("Bolt", typeof(Bolt), Bolt.GetSBPurchaseValue(), 500, 0x1BFB, 0));

                Add(new GenericBuyInfo("Bow", typeof(Bow), Bow.GetSBPurchaseValue(), 25, 0x13B2, 0));
                Add(new GenericBuyInfo("Crossbow", typeof(Crossbow), Crossbow.GetSBPurchaseValue(), 25, 0xF50, 0));
                Add(new GenericBuyInfo("Heavy Crossbow", typeof(HeavyCrossbow), HeavyCrossbow.GetSBPurchaseValue(), 25, 0x13FD, 0));
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
                Add(typeof(Hatchet), Hatchet.GetSBSellValue());
                Add(typeof(Saw), Saw.GetSBSellValue());

                //Add(typeof(Arrow), Arrow.GetSBSellValue());
                //Add(typeof(Bolt), Bolt.GetSBSellValue());

                Add(typeof(Bow), Bow.GetSBSellValue());
                Add(typeof(Crossbow), Crossbow.GetSBSellValue());
                Add(typeof(HeavyCrossbow), HeavyCrossbow.GetSBSellValue());
			}
		}
	}
}