using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;

namespace BetterDialogue.UI.VanillaChatButtons
{
	public class GuideHelpButton : ChatButton
	{
		public static string HelpText()
		{
			Player localPlayer = Main.player[Main.myPlayer];
			bool playerAteALifeCrystal = false;
			if (localPlayer.ConsumedLifeCrystals > 0)
				playerAteALifeCrystal = true;

			bool playerAteAManaCrystal = false;
			if (localPlayer.ConsumedManaCrystals > 0)
				playerAteAManaCrystal = true;

			bool playerStillHasShittyTools = true;
			bool playerHasMetalOre = false;
			bool playerHasMetalBars = false;
			bool playerFoundAFallenStar = false;
			bool playerStoleADemonEyeLens = false;
			bool playerHasEvilSummonsOrTheirMats = false;
			bool playerHasAHook = false;
			bool playerHasDessertFossils = false;
			bool playerHasHellstone = false;
			bool playerHasATempleKey = false;
			bool playerHasADungeonChestKey = false;
			for (int i = 0; i < 58; i++)
			{
				if (localPlayer.inventory[i].pick > 0 && localPlayer.inventory[i].type != ItemID.CopperPickaxe)
					playerStillHasShittyTools = false;

				if (localPlayer.inventory[i].axe > 0 && localPlayer.inventory[i].type != ItemID.CopperAxe)
					playerStillHasShittyTools = false;

				if (localPlayer.inventory[i].hammer > 0)
					playerStillHasShittyTools = false;

				if (localPlayer.inventory[i].type is ItemID.CopperOre or ItemID.TinOre
												  or ItemID.LeadOre or ItemID.IronOre
												  or ItemID.TungstenOre or ItemID.SilverOre
												  or ItemID.GoldOre or ItemID.PlatinumOre)
					playerHasMetalOre = true;

				if (localPlayer.inventory[i].type is ItemID.CopperBar or ItemID.TinBar
												  or ItemID.LeadBar or ItemID.IronBar
												  or ItemID.TungstenBar or ItemID.SilverBar
												  or ItemID.GoldBar or ItemID.PlatinumBar)
					playerHasMetalBars = true;

				if (localPlayer.inventory[i].type == ItemID.FallenStar)
					playerFoundAFallenStar = true;

				if (localPlayer.inventory[i].type == ItemID.Lens)
					playerStoleADemonEyeLens = true;

				if (localPlayer.inventory[i].type is ItemID.RottenChunk or ItemID.WormFood
												  or ItemID.Vertebrae or ItemID.BloodySpine
												  or ItemID.VilePowder or ItemID.ViciousPowder)
					playerHasEvilSummonsOrTheirMats = true;

				if (localPlayer.inventory[i].type is ItemID.GrapplingHook or ItemID.AmethystHook or ItemID.TopazHook
												  or ItemID.SapphireHook or ItemID.EmeraldHook or ItemID.RubyHook
												  or ItemID.DiamondHook or ItemID.WebSlinger or ItemID.SkeletronHand
												  or ItemID.SlimeHook or ItemID.FishHook or ItemID.IvyWhip
												  or ItemID.BatHook or ItemID.CandyCaneHook)
					playerHasAHook = true;

				if (localPlayer.inventory[i].type == ItemID.DesertFossil)
					playerHasDessertFossils = true;

				if (localPlayer.inventory[i].type == ItemID.Hellstone)
					playerHasHellstone = true;

				if (localPlayer.inventory[i].type == ItemID.TempleKey)
					playerHasATempleKey = true;

				if (localPlayer.inventory[i].type is ItemID.JungleKey or ItemID.CorruptionKey or ItemID.CrimsonKey
												  or ItemID.HallowedKey or ItemID.FrozenKey or ItemID.DungeonDesertKey)
					playerHasADungeonChestKey = true;
			}

			bool housedMerchant = false;
			bool housedNurse = false;
			bool housedSalad = false;
			bool housedArmsDealer = false;
			bool housedDemomanTF2 = false;
			bool housedMechanic = false;
			bool housedWizard = false;
			bool housedGoblinBastard = false;
			bool housedClothier = false;
			bool housedOOO_EEE_OO_A_AAA_TING_TANG_WALLAWALLABINGBANG = false;
			bool housedSteamLass = false;
			bool housedCyborg = false;
			bool housedBestGirl = false;
			bool housedThatLittleShitTriscuitForSomeReason = false;
			bool housedTaxCollector = false;
			bool housedPirate = false;
			bool housedDyeTrader = false;
			bool housedTruffle = false;
			bool housedGolfer = false;
			bool housedPainter = false;
			bool housedPartyGirl = false;
			bool housedBarkeep = false;
			bool travellingBastardNearby = false;
			bool skeletonMerchantNearby = false;
			bool housedFOXNews = false;
			int townNPCsPresent = 0;
			for (int j = 0; j < 200; j++)
			{
				if (Main.npc[j].active)
				{
					if (Main.npc[j].townNPC && Main.npc[j].type != NPCID.OldMan)
						townNPCsPresent++;

					if (Main.npc[j].type == NPCID.Merchant)
						housedMerchant = true;

					if (Main.npc[j].type == NPCID.Nurse)
						housedNurse = true;

					if (Main.npc[j].type == NPCID.ArmsDealer)
						housedArmsDealer = true;

					if (Main.npc[j].type == NPCID.Dryad)
						housedSalad = true;

					if (Main.npc[j].type == NPCID.Clothier)
						housedClothier = true;

					if (Main.npc[j].type == NPCID.Mechanic)
						housedMechanic = true;

					if (Main.npc[j].type == NPCID.Demolitionist)
						housedDemomanTF2 = true;

					if (Main.npc[j].type == NPCID.Wizard)
						housedWizard = true;

					if (Main.npc[j].type == NPCID.GoblinTinkerer)
						housedGoblinBastard = true;

					if (Main.npc[j].type == NPCID.WitchDoctor)
						housedOOO_EEE_OO_A_AAA_TING_TANG_WALLAWALLABINGBANG = true;

					if (Main.npc[j].type == NPCID.Steampunker)
						housedSteamLass = true;

					if (Main.npc[j].type == NPCID.Cyborg)
						housedCyborg = true;

					if (Main.npc[j].type == NPCID.Stylist)
						housedBestGirl = true;

					if (Main.npc[j].type == NPCID.BestiaryGirl)
						housedFOXNews = true;

					if (Main.npc[j].type == NPCID.Angler)
						housedThatLittleShitTriscuitForSomeReason = true;

					if (Main.npc[j].type == NPCID.TaxCollector)
						housedTaxCollector = true;

					if (Main.npc[j].type == NPCID.Pirate)
						housedPirate = true;

					if (Main.npc[j].type == NPCID.DyeTrader)
						housedDyeTrader = true;

					if (Main.npc[j].type == NPCID.Truffle)
						housedTruffle = true;

					if (Main.npc[j].type == NPCID.Golfer)
						housedGolfer = true;

					if (Main.npc[j].type == NPCID.Painter)
						housedPainter = true;

					if (Main.npc[j].type == NPCID.PartyGirl)
						housedPartyGirl = true;

					if (Main.npc[j].type == NPCID.DD2Bartender)
						housedBarkeep = true;

					if (Main.npc[j].type == NPCID.TravellingMerchant)
						travellingBastardNearby = true;

					if (Main.npc[j].type == NPCID.SkeletonMerchant)
						skeletonMerchantNearby = true;
				}
			}

			object obj = Lang.CreateDialogSubstitutionObject();
			while (true)
			{
				Main.helpText++;
				if (Language.Exists("GuideHelpText.Help_" + Main.helpText))
				{
					LocalizedText text = Language.GetText("GuideHelpText.Help_" + Main.helpText);
					if (text.CanFormatWith(obj))
						return text.FormatWith(obj);
				}

				if (playerStillHasShittyTools)
				{
					if (Main.helpText == 1)
						return Lang.dialog(177);

					if (Main.helpText == 2)
						return Lang.dialog(178);

					if (Main.helpText == 3)
						return Lang.dialog(179);

					if (Main.helpText == 4)
						return Lang.dialog(180);

					if (Main.helpText == 5)
						return Lang.dialog(181);

					if (Main.helpText == 6)
						return Lang.dialog(182);
				}

				if (playerStillHasShittyTools && !playerHasMetalOre && !playerHasMetalBars && Main.helpText == 11)
					return Lang.dialog(183);

				if (playerStillHasShittyTools && playerHasMetalOre && !playerHasMetalBars)
				{
					if (Main.helpText == 21)
						return Lang.dialog(184);

					if (Main.helpText == 22)
						return Lang.dialog(185);
				}

				if (playerStillHasShittyTools && playerHasMetalBars)
				{
					if (Main.helpText == 31)
						return Lang.dialog(186);

					if (Main.helpText == 32)
						return Lang.dialog(187);
				}

				if (!playerAteALifeCrystal && Main.helpText == 41)
					return Lang.dialog(188);

				if (!playerAteAManaCrystal && Main.helpText == 42)
					return Lang.dialog(189);

				if (!playerAteAManaCrystal && !playerFoundAFallenStar && Main.helpText == 43)
					return Lang.dialog(190);

				if (!housedMerchant && !housedNurse)
				{
					if (Main.helpText == 51)
						return Lang.dialog(191);

					if (Main.helpText == 52)
						return Lang.dialog(192);

					if (Main.helpText == 53)
						return Lang.dialog(193);

					if (Main.helpText == 54)
						return Lang.dialog(194);

					if (Main.helpText == 55)
						return Language.GetTextValue("GuideHelpText.Help_1065");
				}

				if (!housedMerchant && Main.helpText == 61)
					return Lang.dialog(195);

				if (!housedNurse && Main.helpText == 62)
					return Lang.dialog(196);

				if (!housedArmsDealer && Main.helpText == 63)
					return Lang.dialog(197);

				if (!housedSalad && Main.helpText == 64)
					return Lang.dialog(198);

				if (!housedMechanic && Main.helpText == 65 && NPC.downedBoss3)
					return Lang.dialog(199);

				if (!housedClothier && Main.helpText == 66 && NPC.downedBoss3)
					return Lang.dialog(200);

				if (!housedDemomanTF2 && Main.helpText == 67)
					return Lang.dialog(201);

				if (!housedGoblinBastard && NPC.downedBoss2 && Main.helpText == 68)
					return Lang.dialog(202);

				if (!housedWizard && Main.hardMode && Main.helpText == 69)
					return Lang.dialog(203);

				if (!housedOOO_EEE_OO_A_AAA_TING_TANG_WALLAWALLABINGBANG && Main.helpText == 70 && NPC.downedBoss2)
					return Language.GetTextValue("GuideHelpTextSpecific.Help_1100");

				if (!housedSteamLass && Main.helpText == 71 && Main.hardMode)
					return Language.GetTextValue("GuideHelpTextSpecific.Help_1101");

				if (!housedCyborg && Main.helpText == 72 && NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
					return Language.GetTextValue("GuideHelpTextSpecific.Help_1102");

				if (!housedBestGirl && Main.helpText == 73)
					return Language.GetTextValue("GuideHelpTextSpecific.Help_1103");

				if (!housedThatLittleShitTriscuitForSomeReason && Main.helpText == 74)
					return Language.GetTextValue("GuideHelpTextSpecific.Help_1104");

				if (!housedTaxCollector && Main.helpText == 75 && Main.hardMode)
					return Language.GetTextValue("GuideHelpTextSpecific.Help_1105");

				if (!housedPirate && Main.helpText == 76 && Main.hardMode)
					return Language.GetTextValue("GuideHelpTextSpecific.Help_1106");

				if (!housedDyeTrader && Main.helpText == 77)
					return Language.GetTextValue("GuideHelpTextSpecific.Help_1107");

				if (!housedTruffle && Main.helpText == 78 && Main.hardMode)
					return Language.GetTextValue("GuideHelpTextSpecific.Help_1108");

				if (!housedGolfer && Main.helpText == 79)
					return Language.GetTextValue("GuideHelpTextSpecific.Help_1109");

				if (!housedPainter && Main.helpText == 80 && townNPCsPresent >= 5)
					return Language.GetTextValue("GuideHelpTextSpecific.Help_1110");

				if (!housedPartyGirl && Main.helpText == 81 && townNPCsPresent >= 11)
					return Language.GetTextValue("GuideHelpTextSpecific.Help_1111");

				if (!housedBarkeep && NPC.downedBoss2 && Main.helpText == 82)
					return Language.GetTextValue("GuideHelpTextSpecific.Help_1112");

				if (!travellingBastardNearby && Main.helpText == 83 && housedMerchant)
					return Language.GetTextValueWith("GuideHelpTextSpecific.Help_1113", obj);

				if (!skeletonMerchantNearby && Main.helpText == 84 && !Main.hardMode)
					return Language.GetTextValue("GuideHelpTextSpecific.Help_1114");

				if (!housedFOXNews && Main.helpText == 85 && !Main.hardMode)
					return Language.GetTextValue("GuideHelpTextSpecific.Help_1115");

				if (playerStoleADemonEyeLens && !WorldGen.crimson && Main.helpText == 100)
					return Lang.dialog(204);

				if (playerHasEvilSummonsOrTheirMats && Main.helpText == 101)
					return Lang.dialog(WorldGen.crimson ? 403 : 205);

				if ((playerStoleADemonEyeLens || playerHasEvilSummonsOrTheirMats) && Main.helpText == 102)
					return Lang.dialog(WorldGen.crimson ? 402 : 206);

				if (playerStoleADemonEyeLens && WorldGen.crimson && Main.helpText == 103)
					return Language.GetTextValue("GuideHelpTextSpecific.Help_1159");

				if (!playerHasAHook && Main.LocalPlayer.miscEquips[4].IsAir && Main.helpText == 201 && !Main.hardMode && !NPC.downedBoss3 && !NPC.downedBoss2)
					return Lang.dialog(207);

				if (Main.helpText == 202 && !Main.hardMode && localPlayer.ConsumedLifeCrystals >= 2)
					return Language.GetTextValue("GuideHelpTextSpecific.Help_1120");

				if (Main.helpText == 203 && Main.hardMode && NPC.downedMechBossAny)
					return Language.GetTextValue("GuideHelpTextSpecific.Help_1121");

				if (Main.helpText == 204 && !NPC.downedGoblins && localPlayer.ConsumedLifeCrystals >= 5 && WorldGen.shadowOrbSmashed)
					return Language.GetTextValue("GuideHelpTextSpecific.Help_1122");

				if (Main.helpText == 205 && Main.hardMode && !NPC.downedPirates && localPlayer.ConsumedLifeCrystals >= 5)
					return Language.GetTextValue("GuideHelpTextSpecific.Help_1123");

				if (Main.helpText == 206 && Main.hardMode && NPC.downedGolemBoss && !NPC.downedMartians)
					return Language.GetTextValue("GuideHelpTextSpecific.Help_1124");

				if (Main.helpText == 207 && (NPC.downedBoss1 || NPC.downedBoss2 || NPC.downedBoss3))
					return Language.GetTextValue("GuideHelpTextSpecific.Help_1125");

				if (Main.helpText == 208 && !Main.hardMode)
					return Language.GetTextValue("GuideHelpTextSpecific.Help_1130");

				if (Main.helpText == 209 && !Main.hardMode)
					return Language.GetTextValue("GuideHelpTextSpecific.Help_1131");

				if (Main.helpText == 210 && !Main.hardMode)
					return Language.GetTextValue("GuideHelpTextSpecific.Help_1132");

				if (Main.helpText == 211 && !Main.hardMode)
					return Language.GetTextValue("GuideHelpTextSpecific.Help_1133");

				if (Main.helpText == 212 && !Main.hardMode)
					return Language.GetTextValue("GuideHelpTextSpecific.Help_1134");

				if (Main.helpText == 213 && !Main.hardMode)
					return Language.GetTextValue("GuideHelpTextSpecific.Help_1135");

				if (Main.helpText == 214 && !Main.hardMode && (playerHasMetalOre || playerHasMetalBars))
					return Language.GetTextValueWith("GuideHelpTextSpecific.Help_1136", obj);

				if (Main.helpText == 215 && Main.LocalPlayer.anglerQuestsFinished < 1)
					return Language.GetTextValue("GuideHelpTextSpecific.Help_1137");

				if (Main.helpText == 216 && !Main.hardMode)
					return Language.GetTextValue("GuideHelpTextSpecific.Help_1138");

				if (Main.helpText == 1000 && !NPC.downedBoss1 && !NPC.downedBoss2)
					return Lang.dialog(208);

				if (Main.helpText == 1001 && !NPC.downedBoss1 && !NPC.downedBoss2)
					return Lang.dialog(209);

				if (Main.helpText == 1002 && !NPC.downedBoss2)
				{
					if (WorldGen.crimson)
						return Lang.dialog(331);
					else
						return Lang.dialog(210);
				}

				if (Main.helpText == 1050 && !NPC.downedBoss1 && localPlayer.ConsumedLifeCrystals < 5)
					return Lang.dialog(211);

				if (Main.helpText == 1051 && !NPC.downedBoss1 && localPlayer.statDefense <= 10)
					return Lang.dialog(212);

				if (Main.helpText == 1052 && !NPC.downedBoss1 && localPlayer.ConsumedLifeCrystals >= 5 && localPlayer.statDefense > 10)
					return Lang.dialog(WorldGen.crimson ? 404 : 213);

				if (Main.helpText == 1053 && NPC.downedBoss1 && !NPC.downedBoss2 && localPlayer.ConsumedLifeCrystals < 10)
					return Lang.dialog(214);

				if (Main.helpText == 1054 && NPC.downedBoss1 && !NPC.downedBoss2 && !WorldGen.crimson && localPlayer.ConsumedLifeCrystals >= 10)
					return Lang.dialog(215);

				if (Main.helpText == 1055 && NPC.downedBoss1 && !NPC.downedBoss2 && !WorldGen.crimson && localPlayer.ConsumedLifeCrystals >= 10)
					return Lang.dialog(216);

				if (Main.helpText == 1056 && NPC.downedBoss1 && NPC.downedBoss2 && !NPC.downedBoss3)
					return Lang.dialog(217);

				if (Main.helpText == 1057 && NPC.downedBoss1 && NPC.downedBoss2 && NPC.downedBoss3 && !Main.hardMode && localPlayer.ConsumedLifeCrystals < Player.LifeCrystalMax)
					return Lang.dialog(218);

				if (Main.helpText == 1058 && NPC.downedBoss1 && NPC.downedBoss2 && NPC.downedBoss3 && !Main.hardMode && localPlayer.ConsumedLifeCrystals == Player.LifeCrystalMax)
					return Lang.dialog(219);

				if (Main.helpText == 1059 && NPC.downedBoss1 && NPC.downedBoss2 && NPC.downedBoss3 && !Main.hardMode && localPlayer.ConsumedLifeCrystals == Player.LifeCrystalMax)
					return Lang.dialog(220);

				if (Main.helpText == 1060 && NPC.downedBoss1 && NPC.downedBoss2 && NPC.downedBoss3 && !Main.hardMode && localPlayer.ConsumedLifeCrystals == Player.LifeCrystalMax)
					return Lang.dialog(221);

				if (Main.helpText == 1061 && Main.hardMode && !NPC.downedPlantBoss)
					return Lang.dialog(WorldGen.crimson ? 401 : 222);

				if (Main.helpText == 1062 && Main.hardMode && !NPC.downedPlantBoss)
					return Lang.dialog(223);

				if (Main.helpText == 1140 && NPC.downedBoss1 && !NPC.downedBoss2 && WorldGen.crimson && localPlayer.ConsumedLifeCrystals >= 10)
					return Language.GetTextValue("GuideHelpTextSpecific.Help_1140");

				if (Main.helpText == 1141 && NPC.downedBoss1 && !NPC.downedBoss2 && WorldGen.crimson && localPlayer.ConsumedLifeCrystals >= 10)
					return Language.GetTextValue("GuideHelpTextSpecific.Help_1141");

				if (Main.helpText == 1142 && NPC.downedBoss2 && !Main.hardMode)
					return Language.GetTextValue("GuideHelpTextSpecific.Help_1142");

				if (Main.helpText == 1143 && NPC.downedBoss2 && !NPC.downedQueenBee && localPlayer.ConsumedLifeCrystals >= 10)
					return Language.GetTextValue("GuideHelpTextSpecific.Help_1143");

				if (Main.helpText == 1144 && playerHasDessertFossils)
					return Language.GetTextValue("GuideHelpTextSpecific.Help_1144");

				if (Main.helpText == 1145 && playerHasHellstone && !Main.hardMode)
					return Language.GetTextValue("GuideHelpTextSpecific.Help_1145");

				if (Main.helpText == 1146 && Main.hardMode && localPlayer.wingsLogic == 0 && !Main.LocalPlayer.mount.Active && !NPC.downedPlantBoss)
					return Language.GetTextValue("GuideHelpTextSpecific.Help_1146");

				if (Main.helpText == 1147 && Main.hardMode && WorldGen.SavedOreTiers.Adamantite == 111 && !NPC.downedMechBossAny)
					return Language.GetTextValue("GuideHelpTextSpecific.Help_1147");

				if (Main.helpText == 1148 && Main.hardMode && WorldGen.SavedOreTiers.Adamantite == 223 && !NPC.downedMechBossAny)
					return Language.GetTextValue("GuideHelpTextSpecific.Help_1148");

				if (Main.helpText == 1149 && Main.hardMode && NPC.downedMechBossAny && localPlayer.ConsumedLifeCrystals == Player.LifeCrystalMax && localPlayer.ConsumedLifeFruit < Player.LifeFruitMax)
					return Language.GetTextValue("GuideHelpTextSpecific.Help_1149");

				if (Main.helpText == 1150 && Main.hardMode && NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && !NPC.downedPlantBoss)
					return Language.GetTextValue("GuideHelpTextSpecific.Help_1150");

				if (Main.helpText == 1151 && Main.hardMode && NPC.downedPlantBoss && !NPC.downedGolemBoss && playerHasATempleKey)
					return Language.GetTextValue("GuideHelpTextSpecific.Help_1151");

				if (Main.helpText == 1152 && Main.hardMode && NPC.downedPlantBoss && !NPC.downedGolemBoss && !playerHasATempleKey)
					return Language.GetTextValue("GuideHelpTextSpecific.Help_1152");

				if (Main.helpText == 1153 && Main.hardMode && playerHasADungeonChestKey)
					return Language.GetTextValue("GuideHelpTextSpecific.Help_1153");

				if (Main.helpText == 1154 && Main.hardMode && !NPC.downedFishron)
					return Language.GetTextValue("GuideHelpTextSpecific.Help_1154");

				if (Main.helpText == 1155 && Main.hardMode && NPC.downedGolemBoss && !NPC.downedHalloweenTree && !NPC.downedHalloweenKing)
					return Language.GetTextValue("GuideHelpTextSpecific.Help_1155");

				if (Main.helpText == 1156 && Main.hardMode && NPC.downedGolemBoss && !NPC.downedChristmasIceQueen && !NPC.downedChristmasTree && !NPC.downedChristmasSantank)
					return Language.GetTextValue("GuideHelpTextSpecific.Help_1156");

				if (Main.helpText == 1157 && Main.hardMode && NPC.downedGolemBoss && NPC.AnyNPCs(437) && !NPC.downedMoonlord)
					return Language.GetTextValue("GuideHelpTextSpecific.Help_1157");

				if (Main.helpText == 1158 && Main.hardMode && NPC.LunarApocalypseIsUp && !NPC.downedMoonlord)
					return Language.GetTextValue("GuideHelpTextSpecific.Help_1158");

				if (Main.helpText == 1159 && NPC.downedBoss1 && NPC.downedBoss2 && !NPC.downedDeerclops)
					break;

				if (Main.helpText > 1200)
					Main.helpText = 0;
			}

			return Language.GetTextValue("GuideHelpTextSpecific.Help_1160");
		}

		public override string Text(NPC npc, Player player) => Lang.inter[51].Value;

		public override string Description(NPC npc, Player player) => npc.GivenName + " has a wealth of useful information about how to progress! If you need assistance, he's your guy!";

		public override bool IsActive(NPC npc, Player player) => npc.type == NPCID.Guide;

		public override void OnClick(NPC npc, Player player)
		{
			SoundEngine.PlaySound(SoundID.MenuTick);
			Main.npcChatText = HelpText();
		}
	}
}
