# HEY, YOU
are you lookin' for something a bit more interestin' out of the dialogue box?

are you bored by vanilla's "tried and true, but way too blue" dialogue box?

do you need a bit of spice in your chats with all those NPCs and such?

# WELL, IF YOU DO:

hi!

the name's Thomas, and it's my pleasure to present to you **Dialect**, a library mod for Terraria designed to make changes related to the dialogue box and all dialogue buttons significantly more accessible! Dialect is designed predominantly for use as a dependency by other mods, and isn't a whole lot on its own...instead, what it really excels with is how it changes the dialogue system to be less rigid and less mind-numbin' to do anything of substance with:
- the drawcode for the classic dialogue style and a slightly more modern variant of it have been redone
- new dialogue styles can be added by mods which use Dialect as a dependency! now you, too, can make the dialogue box look cool however you'd like! for more in-depth documentation, check out the `DialogueStyle` and `GlobalDialogueStyle` classes
- chat buttons have been redone to be much more flexible and no longer be limited in how many you can have visible! for more in-depth documentation, check out the `ChatButton` and `GlobalChatButton` classes

# WHAT YOU NEED TO KNOW (portin'/usage notes)
to make an NPC compatible with Dialect, you need to do at least one of two things:
- regardless of the NPC, you must add their type to `BetterDialogue.SupportedNPCs`
- if the NPC is one that you can shop at, you must call `BetterDialogue.RegisterShoppableNPC` with their type

additionally, the followin' tModLoader hooks **will not work** for supported NPCs. please look at `ChatButton` in particular to see how to deal with the followin' hooks, which no longer function:
- `ModNPC.SetChatButtons`
  - this ends up being superceded in functionality by the chat button system as a whole
- `GlobalNPC.PreChatButtonClicked`
  - superceded by `GlobalChatButton.PreClick`
- `(Mod/Global)NPC.OnChatButtonClicked`
  - superceded by `(Global)ChatButton.OnClick`

# AFTERWORD

this is the first entry in a possible series of library mods known as the VSC Framework, mainly designed to add tools that tModLoader doesn't provide itself for some more common use cases than one might assume libraries would be best for

if you have any questions about Dialect, please feel free to direct them to me on Discord (name there is thomasthepencil), and have a good time!

huh?
what's that?
what does VSC stand for?
well...

...Victorian Sugar Cookies. best not to think about it much, yeah?
