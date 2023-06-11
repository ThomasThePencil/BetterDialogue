# HEY, YOU
are you lookin' for something a bit more interestin' out of the dialogue box?

are you bored by vanilla's "tried and true, but way too blue" dialogue box?

do you need a bit of spice in your chats with all those NPCs and such?

# WELL, IF YOU DO:

hi!

the name's Thomas, and it's my pleasure to present to you **Dialect**, a library mod for Terraria designed to make changes related to the dialogue box and all dialogue buttons significantly more accessible! Dialect is designed predominantly for use as a dependency by other mods, and isn't a whole lot on its own...instead, what it really excels with is how it changes the dialogue system to be less rigid and less mind-numbin' to do anything of substance with:
- the drawcode for the classic dialogue style and a slightly more modern variant of it have been redone
- new dialogue styles can be added by mods which use Dialect as a dependency! now you, too, can make the dialogue box look cool however you'd like! for more in-depth documentation, check out the `DialogueStyle` class
- chat buttons have been redone to be much more flexible and no longer be limited in how many you can have! for more in-depth documentation, check out the `ChatButton` and `GlobalChatButton` classes

# WHAT YOU NEED TO KNOW (portin'/usage notes)
the followin' tModLoader hooks will not work while Dialect is active. please look at `ChatButton` in particular to see how to deal with the followin' hooks, which no longer function:
- `ModNPC.SetChatButtons`
  - this ends up being superceded in functionality by the chat button system as a whole
- `GlobalNPC.PreChatButtonClicked`
  - superceded by `GlobalChatButton.PreClick`
- `(Mod/Global)NPC.OnChatButtonClicked`
  - superceded by `(Global)ChatButton.OnClick`

# AFTERWORD

this is the first entry in a possible series of library mods known as the VSC Framework, mainly designed to add tools that tModLoader doesn't provide itself

huh?
what's that?
what does VSC stand for?
well...

...Victorian Sugar Cookies. best not to think about it much, yeah?
