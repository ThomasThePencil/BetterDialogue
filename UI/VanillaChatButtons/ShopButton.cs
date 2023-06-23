using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace BetterDialogue.UI.VanillaChatButtons
{
	public class ShopButton : ChatButton
	{
		internal static List<int> ShoppableNPCs;

		internal static void ResetShoppableNPCList()
		{
			ShoppableNPCs = new List<int>()
			{
				NPCID.Merchant,
				NPCID.ArmsDealer,
				NPCID.Dryad,
				NPCID.Demolitionist,
				NPCID.Clothier,
				NPCID.GoblinTinkerer,
				NPCID.Wizard,
				NPCID.Mechanic,
				NPCID.SantaClaus,
				NPCID.Truffle,
				NPCID.Steampunker,
				NPCID.DyeTrader,
				NPCID.PartyGirl,
				NPCID.Cyborg,
				NPCID.Painter,
				NPCID.WitchDoctor,
				NPCID.Pirate,
				NPCID.Stylist,
				NPCID.TravellingMerchant,
				NPCID.SkeletonMerchant,
				NPCID.DD2Bartender,
				NPCID.Golfer,
				NPCID.BestiaryGirl,
				NPCID.Princess,
			};
		}

		internal static void UnloadShoppableNPCList() => ShoppableNPCs = null;

		public override string Text(NPC npc, Player player) => Lang.inter[28].Value;

		public override string Description(NPC npc, Player player) => "See what " + npc.GivenName + " has for sale.";

		public override bool IsActive(NPC npc, Player player) => ShoppableNPCs.Contains(npc.type);

		public override void OnClick(NPC npc, Player player)
		{
			SoundEngine.PlaySound(SoundID.MenuTick);
			Main.playerInventory = true;
			Main.stackSplit = 9999;
			Main.npcChatText = "";
			Main.SetNPCShopIndex(1);
			Main.instance.shop[Main.npcShop].SetupShop(NPCShopDatabase.GetShopName(npc.type, "Shop"), npc);
		}
	}
}
