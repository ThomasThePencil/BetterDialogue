using BetterDialogue.UI.DefaultDialogueStyles;
using BetterDialogue.UI.VanillaChatButtons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace BetterDialogue.UI
{
	/// <summary>
	/// The <see cref="ModType"/> used to define and add new chat buttons.<br/>
	/// In this class, you will decide virtually everything about your chat buttons which your dialogue<br/>
	/// style doesn't decide, including what they do when clicked and when they're active.<br/>
	/// </summary>
	public abstract class ChatButton : ModType
	{
		protected sealed override void Register()
		{
			ModTypeLookup<ChatButton>.Register(this);

			ChatButtonLoader.ChatButtons.Add(this);
		}

		public static TheOneSignButton Sign { get; private set; } = new TheOneSignButton();
		public static PetButton Pet { get; private set; } = new PetButton();
		public static ShopButton Shop { get; private set; } = new ShopButton();
		public static GuideHelpButton GuideProgressHelp { get; private set; } = new GuideHelpButton();
		public static GuideCraftingButton GuideCraftHelp { get; private set; } = new GuideCraftingButton();
		public static NurseHealButton NurseHeal { get; private set; } = new NurseHealButton();
		public static AnglerQuestButton AnglerQuestBecauseTheLittleShitTriscuitWontShutUp { get; private set; } = new AnglerQuestButton();
		public static DyeTraderQuestButton DyeTraderStrangePlants { get; private set; } = new DyeTraderQuestButton();
		public static DryadWorldStatusButton DryadWorldStatus { get; private set; } = new DryadWorldStatusButton();
		public static DryadPurifyButton DryadPurifySoda { get; private set; } = new DryadPurifyButton();
		public static GoblinTinkererReforgeButton GoblinTinkererScamButton { get; private set; } = new GoblinTinkererReforgeButton();
		public static PainterSecondShopButton PainterDecorShop { get; private set; } = new PainterSecondShopButton();
		public static StylistHaircutButton StyistHaircut { get; private set; } = new StylistHaircutButton();
		public static PartyGirlMusicButton PartyGirlMusic { get; private set; } = new PartyGirlMusicButton();
		public static TaxCollectorTaxButton TaxCollectorNeedsYOUToTakeYourDamnTaxesAlready { get; private set; } = new TaxCollectorTaxButton();
		public static BartenderHelpButton DD2HelpButton { get; private set; } = new BartenderHelpButton();
		public static OldManBreakCurseButton OldManCurse { get; private set; } = new OldManBreakCurseButton();
		public static ExitButton Exit { get; private set; } = new ExitButton();

		/// <summary>
		/// The Rectangle to be used for hovering over this chat button.<br/>
		/// Can be set by dialogue styles; normally determined based on text length.<br/>
		/// </summary>
		public Rectangle HoverBox;

		/// <summary>
		/// The text your chat button is to display.<br/>
		/// <b>I need to know this, or your button shrimply will not work!</b><br/>
		/// <br/>
		/// [Required Field]<br/>
		/// </summary>
		/// <param name="npc">The NPC the given player is talking to.</param>
		/// <param name="player">The player talking to the given NPC.</param>
		public abstract string Text(NPC npc, Player player);

		/// <summary>
		/// The text your chat button is to display.<br/>
		/// Defaults to <see langword="null"/>, in which case the button will use the button text color decided by the currently active dialogue style.<br/>
		/// </summary>
		/// <param name="npc">The NPC the given player is talking to.</param>
		/// <param name="player">The player talking to the given NPC.</param>
		public virtual Color? OverrideColor(NPC npc, Player player) => null;

		/// <summary>
		/// A description of your chat button.<br/>
		/// Not used by Classic or Classic Refurbished, but can be fetched by other mods' dialogue styles.<br/>
		/// </summary>
		/// <param name="npc">The NPC the given player is talking to.</param>
		/// <param name="player">The player talking to the given NPC.</param>
		public virtual string Description(NPC npc, Player player) => null;

		public virtual bool IsActive(NPC npc, Player player) => true;

		public bool IsHovered => HoverBox.Contains(Main.MouseScreen.ToPoint());
		public bool WasHovered { get; set; }

		/// <summary>
		/// Allows checking whether or not this chat button has changed hover states in the most recent frame.
		/// </summary>
		/// <returns>
		/// If the button has not changed hover states, <see langword="null"/>.<br/>
		/// If the button was hovered over on the previous frame, but isn't hovered over as of this frame, <see langword="false"/>.<br/>
		/// If the button wasn't hovered over on the previous frame, but is hovered over as of this frame, <see langword="true"/>.<br/>
		/// </returns>
		public bool? HoverChanged
		{
			get
			{
				if (IsHovered == WasHovered)
					return null;

				return IsHovered;
			}
		}

		/// <summary>
		/// Allows you to make this button do something when it gets hovered over!
		/// </summary>
		/// <param name="npc">The NPC the given player is talking to.</param>
		/// <param name="player">The player talking to the given NPC.</param>
		public virtual void OnHover(NPC npc, Player player) { }

		/// <summary>
		/// You should make this button do something when it gets clicked on...<b>NOW!</b><br/>
		/// <br/>
		/// ...what? were you expectin' something more? something a bit less jokey?<br/>
		/// have you forgotten who MADE this API? I'm, like, 70% jokes and 30% snark<br/>
		/// <br/>
		/// ...and 5% wonderin' why you're still readin' this<br/>
		/// <br/>
		/// [Required Field]<br/>
		/// </summary>
		/// <param name="npc">The NPC the given player is talking to.</param>
		/// <param name="player">The player talking to the given NPC.</param>
		public abstract void OnClick(NPC npc, Player player);
	}
}
