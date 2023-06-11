using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace BetterDialogue.UI.DefaultDialogueStyles
{
	public class ClassicRefurbished : DialogueStyle
	{
		public override string DisplayName => "Classic Refurbished";

		public override string Description => "A slightly-touched-up variant of vanilla's dialogue style.\nAims to add even a little extra flair without losing the classic look.";

		public override Texture2D DialogueBoxTileSheet => ModContent.Request<Texture2D>("BetterDialogue/UI/DefaultDialogueStyles/ClassicRefurbished_MainBox", AssetRequestMode.ImmediateLoad).Value;
	}
}
