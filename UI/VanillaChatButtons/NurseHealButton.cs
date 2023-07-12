using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Achievements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace BetterDialogue.UI.VanillaChatButtons
{
	public class NurseHealButton : ChatButton
	{
		public string CantHealReason;
		public bool CanHeal;
		public bool RemoveDebuffs;
		public int HealPrice(NPC npc, Player player, out int missingHealth, out int platinumCost, out int goldCost, out int silverCost, out int copperCost)
		{
			platinumCost = 0;
			goldCost = 0;
			silverCost = 0;
			copperCost = 0;
			int storedTotalCost = player.statLifeMax2 - player.statLife;
			missingHealth = storedTotalCost;
			for (int j = 0; j < Player.MaxBuffs; j++)
			{
				int num6 = player.buffType[j];
				if (Main.debuff[num6] && player.buffTime[j] > 60 && (num6 < 0 || !BuffID.Sets.NurseCannotRemoveDebuff[num6]))
					storedTotalCost += 100;
			}
			int totalCost = storedTotalCost;
			if (totalCost > 0 && totalCost < 1)
				totalCost = 1;

			CantHealReason = Language.GetTextValue("tModLoader.DefaultNurseCantHealChat");
			CanHeal = PlayerLoader.ModifyNurseHeal(player, npc, ref missingHealth, ref RemoveDebuffs, ref CantHealReason);
			PlayerLoader.ModifyNursePrice(player, npc, missingHealth, RemoveDebuffs, ref totalCost);

			if (totalCost < 0)
				totalCost = 0;

			storedTotalCost = totalCost;
			if (totalCost >= 1000000)
			{
				platinumCost = totalCost / 1000000;
				totalCost -= platinumCost * 1000000;
			}

			if (totalCost >= 10000)
			{
				goldCost = totalCost / 10000;
				totalCost -= goldCost * 10000;
			}

			if (totalCost >= 100)
			{
				silverCost = totalCost / 100;
				totalCost -= silverCost * 100;
			}

			if (totalCost >= 1)
				copperCost = totalCost;
			return storedTotalCost;
		}
		public override string Text(NPC npc, Player player)
		{
			string text5 = "";
			HealPrice(npc, player, out _, out int platinumCost, out int goldCost, out int silverCost, out int copperCost);
			if (platinumCost > 0)
				text5 = text5 + platinumCost + " " + Lang.inter[15].Value + " ";

			if (goldCost > 0)
				text5 = text5 + goldCost + " " + Lang.inter[16].Value + " ";

			if (silverCost > 0)
				text5 = text5 + silverCost + " " + Lang.inter[17].Value + " ";

			if (copperCost > 0)
				text5 = text5 + copperCost + " " + Lang.inter[18].Value + " ";


			if (text5 == "")
			{
				return Lang.inter[54].Value;
			}
			else
			{
				text5 = text5.Substring(0, text5.Length - 1);
				return Lang.inter[54].Value + " (" + text5 + ")";
			}
		}

		public override double Priority => 5.0;

		public override Color? OverrideColor(NPC npc, Player player)
		{
			HealPrice(npc, player, out _, out int platinumCost, out int goldCost, out int silverCost, out int copperCost);
			float mouseTextColorFactor = (float)(int)Main.mouseTextColor / 255f;
			if (platinumCost > 0)
				return new Color((byte)(220f * mouseTextColorFactor), (byte)(220f * mouseTextColorFactor), (byte)(198f * mouseTextColorFactor), Main.mouseTextColor);
			else if (goldCost > 0)
				return new Color((byte)(224f * mouseTextColorFactor), (byte)(201f * mouseTextColorFactor), (byte)(92f * mouseTextColorFactor), Main.mouseTextColor);
			else if (silverCost > 0)
				return new Color((byte)(181f * mouseTextColorFactor), (byte)(192f * mouseTextColorFactor), (byte)(193f * mouseTextColorFactor), Main.mouseTextColor);
			else if (copperCost > 0)
				return new Color((byte)(246f * mouseTextColorFactor), (byte)(138f * mouseTextColorFactor), (byte)(96f * mouseTextColorFactor), Main.mouseTextColor);

			return null;
		}

		public override string Description(NPC npc, Player player) => "Have " + npc.GivenName + " tend to your injuries, for a cost.";

		public override bool IsActive(NPC npc, Player player) => npc.type == NPCID.Nurse;

		public override void OnClick(NPC npc, Player player)
		{
			SoundEngine.PlaySound(SoundID.MenuTick);
			int num5 = HealPrice(npc, player, out int missingHealth, out int platinumCost, out int goldCost, out int silverCost, out int copperCost);
			if (num5 > 0)
			{
				if (!CanHeal)
				{
					Main.npcChatText = CantHealReason;
					return;
				}

				if (player.BuyItem(num5))
				{
					AchievementsHelper.HandleNurseService(num5);
					SoundEngine.PlaySound(SoundID.Item4);

					player.HealEffect(missingHealth, true);
					if ((double)player.statLife < (double)player.statLifeMax2 * 0.25)
						Main.npcChatText = Lang.dialog(227);
					else if ((double)player.statLife < (double)player.statLifeMax2 * 0.5)
						Main.npcChatText = Lang.dialog(228);
					else if ((double)player.statLife < (double)player.statLifeMax2 * 0.75)
						Main.npcChatText = Lang.dialog(229);
					else
						Main.npcChatText = Lang.dialog(230);

					player.statLife += missingHealth;
					if (!RemoveDebuffs)
						goto SkipDebuffRemoval;

					for (int l = 0; l < Player.MaxBuffs; l++)
					{
						int num25 = player.buffType[l];
						if (Main.debuff[num25] && player.buffTime[l] > 0 && (num25 < 0 || !BuffID.Sets.NurseCannotRemoveDebuff[num25]))
						{
							player.DelBuff(l);
							l = -1;
						}
					}

					SkipDebuffRemoval:
					PlayerLoader.PostNurseHeal(Main.LocalPlayer, Main.LocalPlayer.TalkNPC, missingHealth, RemoveDebuffs, num5);
				}
				else
				{
					int num26 = Main.rand.Next(3);
					if (num26 == 0)
						Main.npcChatText = Lang.dialog(52);

					if (num26 == 1)
						Main.npcChatText = Lang.dialog(53);

					if (num26 == 2)
						Main.npcChatText = Lang.dialog(54);
				}
			}
			else
			{
				int num27 = Main.rand.Next(3);
				if (!ChildSafety.Disabled)
					num27 = Main.rand.Next(1, 3);

				switch (num27)
				{
					case 0:
						Main.npcChatText = Lang.dialog(55);
						break;
					case 1:
						Main.npcChatText = Lang.dialog(56);
						break;
					case 2:
						Main.npcChatText = Lang.dialog(57);
						break;
				}
			}
		}
	}
}
