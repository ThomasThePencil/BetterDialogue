using BetterDialogue.UI;
using BetterDialogue.UI.VanillaChatButtons;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BetterDialogue
{
	public class BetterDialogue : Mod
	{
		internal static BetterDialogue Instance;

		/// <summary>
		/// A list containing the numerical IDs of every single NPC, vanilla or otherwise, whose dialogue has been set up to work with Dialect rather than tModLoader's base tools.<br/>
		/// By default, contains all vanilla townsfolk and town pets, as well as the Traveling and Skeleton Merchants and the Old Man.<br/>
		/// You <b>MUST</b> add an NPC's numerical ID to this list for them to work with Dialect at all.<br/>
		/// </summary>
		public static List<int> SupportedNPCs { get; internal set; }

		/// <summary>
		/// Sets the given NPC type as a type of NPC which the player can open a standard shop at.<br/>
		/// Alongside <see cref="UnregisterShoppableNPC"/>, affects whether or not the Shop button is added to the dialogue window.<br/>
		/// </summary>
		/// <param name="npcType"></param>
		public static void RegisterShoppableNPC(int npcType)
		{
			if (!ShopButton.ShoppableNPCs.Contains(npcType))
				ShopButton.ShoppableNPCs.Add(npcType);
		}

		/// <summary>
		/// Sets the given NPC type as a type of NPC which the player can NOT open a standard shop at.<br/>
		/// Alongside <see cref="RegisterShoppableNPC"/>, affects whether or not the Shop button is added to the dialogue window.<br/>
		/// </summary>
		/// <param name="npcType"></param>
		public static void UnregisterShoppableNPC(int npcType) => ShopButton.ShoppableNPCs.Remove(npcType);

		/// <summary>
		/// Tells you which dialogue style is currently active.<br/>
		/// If the currently active style (by display name) cannot be found, switches back to Classic.<br/>
		/// </summary>
		public static DialogueStyle CurrentActiveStyle
		{
			get
			{
				foreach (DialogueStyle style in DialogueStyleLoader.DialogueStyles)
				{
					if (style.ForceActive(Main.LocalPlayer.TalkNPC, Main.LocalPlayer))
						return style;
				}

				if (DialogueStyleLoader.DialogueStyleDisplayNames.Contains(ModContent.GetInstance<BetterDialogueConfig>().DialogueStyle))
					return DialogueStyleLoader.DialogueStyles.FirstOrDefault(x => x.DisplayName == ModContent.GetInstance<BetterDialogueConfig>().DialogueStyle);
				else
				{
					ModContent.GetInstance<BetterDialogueConfig>().DialogueStyle = "Classic";
					return DialogueStyle.Classic;
				}
			}
		}

		public BetterDialogue()
		{
			Instance = this;
		}

		public override void Load()
		{
			DialogueStyleLoader.Load();
			ChatButtonLoader.Load();

			SupportedNPCs = new List<int>()
			{
				NPCID.Guide,
				NPCID.Merchant,
				NPCID.Nurse,
				NPCID.ArmsDealer,
				NPCID.Dryad,
				NPCID.OldMan,
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
				NPCID.Angler,
				NPCID.TaxCollector,
				NPCID.SkeletonMerchant,
				NPCID.DD2Bartender,
				NPCID.Golfer,
				NPCID.BestiaryGirl,
				NPCID.Princess,
				NPCID.TownCat,
				NPCID.TownDog,
				NPCID.TownBunny,
				NPCID.TownSlimeBlue,
				NPCID.TownSlimeGreen,
				NPCID.TownSlimeOld,
				NPCID.TownSlimePurple,
				NPCID.TownSlimeRainbow,
				NPCID.TownSlimeRed,
				NPCID.TownSlimeYellow,
				NPCID.TownSlimeCopper,
			};
			ShopButton.ResetShoppableNPCList();

			On_Main.GUIChatDrawInner += (orig, self) => {

				if (Main.LocalPlayer.TalkNPC is null)
					return;
				else if (!SupportedNPCs.Contains(Main.LocalPlayer.TalkNPC.type))
					orig(self);
				else
					DialogueStyleLoader.DrawActiveDialogueStyle();
			};
		}

		public override void Unload()
		{
			DialogueStyleLoader.Unload();
			ChatButtonLoader.Unload();
			SupportedNPCs = null;
			ShopButton.UnloadShoppableNPCList();
		}
	}
}