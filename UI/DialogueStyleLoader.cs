using BetterDialogue.UI.DefaultDialogueStyles;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.GameContent;

namespace BetterDialogue.UI
{
	/// <summary>
	/// The class used to store existing and newly-added dialogue styles.<br/>
	/// The only thing you will need to do with this class, for the most part, is call Register.<br/>
	/// </summary>
	public static class DialogueStyleLoader
	{
		internal static List<DialogueStyle> DialogueStyles = new List<DialogueStyle>();
		public static List<string> DialogueStyleDisplayNames {
			get {
				List<string> nameList = new List<string>();
				if (DialogueStyles is null || DialogueStyles.Count <= 0)
					return null;

				foreach (DialogueStyle style in DialogueStyles)
				{
					nameList.Add(style.DisplayName);
				}
				return nameList;
			}
		}

		internal static void Load()
		{
			DialogueStyles = new List<DialogueStyle>() {
				DialogueStyle.Classic,
				DialogueStyle.ClassicRefurbished
			};
		}

		internal static void Unload()
		{
			DialogueStyles = null;
		}
	}
}
