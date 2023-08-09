using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Achievements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace BetterDialogue.UI.VanillaChatButtons
{
	public class TaxCollectorTaxButton : ChatButton
	{
		public int CollectedTaxes(NPC npc, Player player, out int platinumTaxMoney, out int goldTaxMoney, out int silverTaxMoney, out int copperTaxMoney)
		{
			platinumTaxMoney = 0;
			goldTaxMoney = 0;
			silverTaxMoney = 0;
			copperTaxMoney = 0;
			int totalTaxMoney = player.taxMoney;
			totalTaxMoney = (int)((double)totalTaxMoney / player.currentShoppingSettings.PriceAdjustment);
			if (totalTaxMoney < 0)
				totalTaxMoney = 0;

			int storedTotalTaxMoney = totalTaxMoney;
			if (totalTaxMoney >= 1000000)
			{
				platinumTaxMoney = totalTaxMoney / 1000000;
				totalTaxMoney -= platinumTaxMoney * 1000000;
			}

			if (totalTaxMoney >= 10000)
			{
				goldTaxMoney = totalTaxMoney / 10000;
				totalTaxMoney -= goldTaxMoney * 10000;
			}

			if (totalTaxMoney >= 100)
			{
				silverTaxMoney = totalTaxMoney / 100;
				totalTaxMoney -= silverTaxMoney * 100;
			}

			if (totalTaxMoney >= 1)
				copperTaxMoney = totalTaxMoney;

			return storedTotalTaxMoney;
		}

		public override string Text(NPC npc, Player player)
		{
			string focusText = "";
			CollectedTaxes(npc, player, out int platinumTaxMoney, out int goldTaxMoney, out int silverTaxMoney, out int copperTaxMoney);
			if (platinumTaxMoney > 0)
				focusText = focusText + platinumTaxMoney + " " + Lang.inter[15].Value + " ";

			if (goldTaxMoney > 0)
				focusText = focusText + goldTaxMoney + " " + Lang.inter[16].Value + " ";

			if (silverTaxMoney > 0)
				focusText = focusText + silverTaxMoney + " " + Lang.inter[17].Value + " ";

			if (copperTaxMoney > 0)
				focusText = focusText + copperTaxMoney + " " + Lang.inter[18].Value + " ";

			focusText = Lang.inter[89].Value;
			string text2 = "";
			for (int k = 0; k < focusText.Length; k++)
			{
				text2 += " ";
			}

			return focusText + text2 + "        ";
		}

		public override double Priority => 14.0;

		public override Color? OverrideColor(NPC npc, Player player)
		{
			CollectedTaxes(npc, player, out int platinumTaxMoney, out int goldTaxMoney, out int silverTaxMoney, out int copperTaxMoney);
			float mouseTextColorFactor = (float)(int)Main.mouseTextColor / 255f;
			if (platinumTaxMoney > 0)
				return new Color((byte)(220f * mouseTextColorFactor), (byte)(220f * mouseTextColorFactor), (byte)(198f * mouseTextColorFactor), Main.mouseTextColor);
			else if (goldTaxMoney > 0)
				return new Color((byte)(224f * mouseTextColorFactor), (byte)(201f * mouseTextColorFactor), (byte)(92f * mouseTextColorFactor), Main.mouseTextColor);
			else if (silverTaxMoney > 0)
				return new Color((byte)(181f * mouseTextColorFactor), (byte)(192f * mouseTextColorFactor), (byte)(193f * mouseTextColorFactor), Main.mouseTextColor);
			else if (copperTaxMoney > 0)
				return new Color((byte)(246f * mouseTextColorFactor), (byte)(138f * mouseTextColorFactor), (byte)(96f * mouseTextColorFactor), Main.mouseTextColor);

			return null;
		}

		public override string Description(NPC npc, Player player) => npc.GivenName + " gathers money from the local townsfolk so you don't have to. Check in with him every once in a while to snag your share of the haul!";

		public override bool IsActive(NPC npc, Player player) => npc.type == NPCID.TaxCollector;

		public override void OnClick(NPC npc, Player player)
		{
			SoundEngine.PlaySound(SoundID.MenuTick);
			SoundEngine.PlaySound(SoundID.Coins);
			if (player.taxMoney > 0)
			{
				int taxMoney3 = player.taxMoney;
				taxMoney3 = (int)((double)taxMoney3 / player.currentShoppingSettings.PriceAdjustment);
				while (taxMoney3 > 0)
				{
					EntitySource_Gift source = new EntitySource_Gift(npc);
					if (taxMoney3 > 1000000)
					{
						int num21 = taxMoney3 / 1000000;
						taxMoney3 -= 1000000 * num21;
						int platCoinIndex = Item.NewItem(source, (int)player.position.X, (int)player.position.Y, player.width, player.height, 74, num21);
						if (Main.netMode == NetmodeID.MultiplayerClient)
							NetMessage.SendData(MessageID.SyncItem, -1, -1, null, platCoinIndex, 1f);

						continue;
					}

					if (taxMoney3 > 10000)
					{
						int num22 = taxMoney3 / 10000;
						taxMoney3 -= 10000 * num22;
						int goldCoinIndex = Item.NewItem(source, (int)player.position.X, (int)player.position.Y, player.width, player.height, 73, num22);
						if (Main.netMode == NetmodeID.MultiplayerClient)
							NetMessage.SendData(MessageID.SyncItem, -1, -1, null, goldCoinIndex, 1f);

						continue;
					}

					if (taxMoney3 > 100)
					{
						int num23 = taxMoney3 / 100;
						taxMoney3 -= 100 * num23;
						int silverCoinIndex = Item.NewItem(source, (int)player.position.X, (int)player.position.Y, player.width, player.height, 72, num23);
						if (Main.netMode == NetmodeID.MultiplayerClient)
							NetMessage.SendData(MessageID.SyncItem, -1, -1, null, silverCoinIndex, 1f);

						continue;
					}

					int num24 = taxMoney3;
					if (num24 < 1)
						num24 = 1;

					taxMoney3 -= num24;
					int copperCoinIndex = Item.NewItem(source, (int)player.position.X, (int)player.position.Y, player.width, player.height, 71, num24);
					if (Main.netMode == NetmodeID.MultiplayerClient)
						NetMessage.SendData(MessageID.SyncItem, -1, -1, null, copperCoinIndex, 1f);
				}

				Main.npcChatText = Lang.dialog(Main.rand.Next(380, 382));
				player.taxMoney = 0;
			}
			else
			{
				Main.npcChatText = Lang.dialog(Main.rand.Next(390, 401));
			}
		}
	}
}
