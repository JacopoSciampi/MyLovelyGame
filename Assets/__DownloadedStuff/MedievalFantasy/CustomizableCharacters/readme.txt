Questions, feedback, suggestions? Want to request custom assets? Contact me:
Email:			lukebox@hailgames.net 
Discord:		Lukebox#8482

Customizable characters

- Items are loaded and equipped based on item IDs, the item prefabs can be found under each Race's folder, e.g. Dwarves\Resources\CustomizableCharacters\Race 1\Equipment
- Use the NpcCharacter prefab to set up an NPC. Change the values in the inspector to customize the NPC's look and add item IDs in the equipment list. The items will be loaded automatically when the game starts
- Use the PlayerCharacter prefab if you wish to control equipped items from your own scripts, e.g. an inventory system. Add a reference to the BaseCharacter script and call functions to equip and unequip items.
- To add your own animations, find the armature prefab in Common\Resources\CustomizableCharacters\Armatures\ and add your own Animator controller to the Animator compoenent.
If you don't know how to work with Animators, see these tutorials: https://www.youtube.com/watch?v=JeZkctmoBPw https://www.youtube.com/watch?v=b0O2CRSdiOA


PlayerCharacter Functions:
	SaveItems
		Saves the items the player is currently wearing
	LoadItems
		Equips loaded items

CharacterBase Functions:
	EquipItem(int itemId)
		Load and equip an item. If an item is already equipped in this slot, it will be automatically unequipped.

	UnequipSlot(Item.EquipmentSlots slot)
		Unequip an item in this item slot

	UnequipAll() 
		Unequips all items

	ChangeGender, ChangeBeardstyle, ChangeEyebrowstyle, ChangeHairstyle, ChangeRace (int)

	ChangeSkinColor, ChangeHairColor (Color)
		
	SetupBody()
		Load body parts, hair, beard, eyebrows. It is automatically called after equipping or unequipping an item



Race IDs (Races other than humans included in expansion packs, available on the Asset store)
0 = Human
1 = Dwarf
2 = Goblin
3 = Elf

Hairstyle IDs
1-5 = Regular hairstyles
6 = bald
7 = short cutout hair (used to show visible hair under hats)
8-13 = elvish hairstyles

Beardstyle IDs (only works for male characters)
1-5 normal beard styles
6 = no beard


Item IDs:

ID	Name

1	Cow leather boots
2	Iron platebody
3	Iron platehelm
4	Iron platelegs
5	Cow leather gloves
6	Iron kiteshield
7	Iron short sword
8	Red robe bottom
9	Red robe top
10	Wooden staff
11	Red pointy hat
12	Ruby amulet
13	Iron arming sword
14	Longbow
15	Shortbow
16	Crossbow
17	Iron dagger
18	Iron greatsword
19	Red orb staff
20	Wooden wand
21	Iron spear
22	Torch
23	Cow leather hood
24	Bear leather hood
25	Dragon leather hood
26	Cow leather body
27	Cow leather legs
28	Ruby star amulet
29	Sapphire amulet
30	Emerald amulet
31	Diamond amulet
32	Amethyst amulet
33	Diamond star amulet
34	Emeral star amulet
35	Amethyst star amulet
36	Sapphire star amulet
37	Peasant shirt
38	Peasant pants
39	Tunic
40	Curvy iron dagger
41	Curvy iron dagger (offhand)
42	Rogue mask
43	Rogue shirt
44	Rogue pants
45	Rogue shoes
46	Horned helmet
47	Chainmail shirt
48	Chainmail skirt
49	Buckler shield
50	Colored buckler shield
51	Cape (Green-yellow)
52	Purple sorcerer robe top
53	Purple sorcerer robe bottom
54	Purple sorcerer hood
55	Iron battleaxe
56	Iron halberd
57	Iron mace
58	Iron axe
59	Quiver
60	Ornamental quiver
61	Cape (White-yellow)
62	Cape (Black-red)
63	Cape (Black-green)
64	Cape (Blue-white)
65	Ornamental red orb staff
66	Templar platebody
67	Templar platehelm
68	Templar platelegs
69	Templar kiteshield
70	Ornamental blue orb staff
71	Crystal staff
72	Iron scimitar
73	Iron short sword (offhand)
74	White robe bottom
75	White robe top
76	White pointy hat
77	Blue sorcerer hood
78	Blue sorcerer robe bottom
79	Blue sorcerer robe top
80	Black sorcerer robe bottom
81	Black sorcerer robe hood
82	Black sorcerer robe top
83	Steel platelegs
84	Steel platehelm
85	Steel platebody
86	Mithril platelegs
87	Mithril platebody
88	Mithril platehelm
89	Shadow platebody
90	Shadow platehelm
91	Shadow platelegs
92	Bear leather boots
93	Dragon leather boots
94	Bear leather gloves
95	Dragon leather gloves
96	Steel kite shield
97	Mithril kite shield
98	Shadow kite shield
99	Steel short sword
100	Mithril short sword
101	Shadow short sword
102	Blue robe top
103	Blue robe bottom
104	Blue pointy hat
105	Steel arming sword
106	Mithril arming sword
107	Shadow arming sword
108	Steel dagger
109	Mithril dagger
110	Shadow dagger
111	Steel greatsword
112	Mithril greatsword
113	Shadow greatsword
114	Green orb staff
115	Blue orb staff
116	Steel spear
117	Mithril spear
118	Shadow spear
119	Bear leather legs
120	Bear leather body
121	Dragon leather legs
122	Dragon leather body
123	Curvy steel dagger
124	Curvy Mithril dagger
125	Curvy Shadow dagger
126	Curvy steel dagger (offhand)
127	Curvy Mithril dagger (offhand)
128	Curvy Shadow dagger (offhand)
129	Steel battleaxe
130	Mithril battleaxe
131	Shadow battleaxe
132	Steel halberd
133	Mithril halberd
134	Shadow halberd
135	Steel mace
136	Mithril mace
137	Shadow mace
138	Steel axe
139	Mithril axe
140	Shadow axe
141	Steel scimitar
142	Mithril scimitar
143	Shadow scimitar

New in update 1.1:
144 Shaman top
145 Shaman robe skirt
146 Shaman boots
147 Shaman mask
148 Shaman gloves
149 Shaman staff

New in update  1.2
150 Druid headgear
151 Druid shirt
152 Druid legs
153 Druid boots
154 Druid staff

New in update  1.3
155 Iron hatchet
156 Steel hatchet
157 Mithril hatchet
158 Shadow hatchet

159 Fishing rod
160 Fishing rod with reel

161 Iron hammer
162 Steel hammer
163 Mithril hammer
164 Shadow hammer

165 Iron knife
166 Steel knife
167 Mithril knife
168 Shadow knife

169 Iron pickaxe
170 Steel pickaxe
171 Mithril pickaxe
172 Shadow pickaxe

173 Iron scythe
174 Steel scythe
175 Mithril scythe
176 Shadow scythe

177 Iron shovel
178 Steel shovel
179 Mithril shovel
180 Shadow shovel

New in Update 1.4:
181-205 High fantasy weapons I

New in update 1.5:
206 Berserk body
207 Berserk legs
208 Berserk headgear
209 Berserk boots
210 Berserk cape
211 Berserk gloves

New in Update 1.6:
212-236 High fantasy weapons II

New in Update 1.7:
237 Corrupt platebody
238 Corrupt platelegs
239 Corrupt helmet
240 Corrupt gloves
241 Corrupt boots
242 Corrupt cape
243 Corrupt blade
244 Corrupt shield

New in Update 1.8:
245 Scimitar Iron
246 Scimitar Steel
247 Scimitar Mithril
248 Scimitar Shadow
249 Cutlass Iron
250 Cutlass Steel 
251 Cutlass Mithril
252 Cutlass Shadow
253 Bandana white
254 Bandana blue
255 Bandana red
256 Hook
257 Captain hat
258 Sailor shirt 1
259 Sailor shirt 2
260 Pirate pants 1
261 Pirate pants 2
262 Pirate boots