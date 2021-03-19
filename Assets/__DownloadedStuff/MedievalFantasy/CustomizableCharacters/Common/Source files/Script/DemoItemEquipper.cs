using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPGCharacters {
    public class DemoItemEquipper : MonoBehaviour {

        public CharacterBase selectedCharacter;

        // Use this for initialization
        void Start() {
            selectedCharacter.EquipItem(37);
            selectedCharacter.EquipItem(38);
            selectedCharacter.EquipItem(1);
        }
        public List<Color> skinColors = new List<Color>();
        public List<Color> hairColors = new List<Color>();
        public List<Color> eyeColors = new List<Color>();
        public Texture2D tex_white;
        public List<int> itemList = new List<int>();
        Vector2 scrollView;
        public int itemsCount = 205;
        private void OnGUI() {

            
            scrollView = GUI.BeginScrollView(new Rect(5, 5, 110, Screen.height - 10), scrollView, new Rect(0, 0, 90, 100 + itemsCount * 65));
            int counter = 1;
            for (int i = 1; i <= itemsCount; i++) {
                if (GUI.Button(new Rect(5, counter * 65, 64, 64), "")) {
                    selectedCharacter.EquipItem(i);
                }
                if (IconLoader.GetInstance().GetIcon(i) != null) {
                    GUI.DrawTexture(new Rect(5, counter * 65, 64, 64), IconLoader.GetInstance().GetIcon(i));
                    GUI.Label(new Rect(5, counter * 65, 64, 64), "" + i);
                } else {
                    Debug.Log("null " + i);
                }

                counter++;
            }
            GUI.EndScrollView();



            if (GUI.Button(new Rect(150, Screen.height - 64 - 64 - 64 - 64 - 64 - 64-100, 70, 32), "Male")) {
                selectedCharacter.ChangeGender(0);
            }
            if (GUI.Button(new Rect(150 + 70, Screen.height - 64 - 64 - 64 - 64 - 64 - 64 - 100, 70, 32), "Female")) {
                selectedCharacter.ChangeGender(1);
            }


            if (selectedCharacter.race == 0) {
                GUI.Label(new Rect(150, Screen.height - 64 - 64 - 64 - 64 - 64 - 64, 200, 25), "Race:");
            } else {
                GUI.Label(new Rect(150, Screen.height - 64 - 64 - 64 - 64 - 64 - 64-20, 200, 50), "Race:\n(Note: Races 1-3 included in separate expansion packs)");
            }
            for (int i = 0; i < 4; i++) {
                if (GUI.Button(new Rect(150 + i * 25, Screen.height - 32 - 64 - 64 - 64 - 64 - 64, 25, 25), "" + i)) {
                    selectedCharacter.ChangeRace(i);
                    selectedCharacter.ChangeHairstyle(1);
                    if (i == 2) {//goblin
                        selectedCharacter.ChangeSkinColor(skinColors[7]);
                        selectedCharacter.ChangeHairstyle(6);//bald
                        selectedCharacter.ChangeBeardstyle(6);//bald
                    } else {
                        selectedCharacter.ChangeSkinColor(skinColors[2]);
                    }
                    selectedCharacter.EquipItem(37);
                    selectedCharacter.EquipItem(38);
                    selectedCharacter.EquipItem(1);
                }
            }

            GUI.Label(new Rect(150, Screen.height - 64 - 64 - 64 - 64 - 64, 200, 25), "Eye colors:");
            for (int i = 0; i < eyeColors.Count; i++) {
                GUI.color = eyeColors[i];
                if (GUI.Button(new Rect(150 + i * 25, Screen.height - 32 - 64 - 64 - 64 - 64, 25, 25), "" + i)) {
                    selectedCharacter.ChangeEyeColor(eyeColors[i]);
                }
                GUI.color = Color.white;
            }
            GUI.Label(new Rect(150, Screen.height - 64 - 64 - 64 - 64, 200, 25), "Hair colors:");
            for (int i = 0; i < hairColors.Count; i++) {
                GUI.color = hairColors[i];
                if (GUI.Button(new Rect(150 + i * 25, Screen.height - 32 - 64 - 64 - 64, 25, 25), "" + i)) {
                    selectedCharacter.ChangeHairColor(hairColors[i]);
                }
                GUI.color = Color.white;
            }
            GUI.Label(new Rect(150, Screen.height - 64 - 64 - 64, 200, 25), "Skin colors:");
            for (int i = 0; i < skinColors.Count; i++) {
                GUI.color = skinColors[i];
                if (GUI.Button(new Rect(150 + i * 25, Screen.height - 32 - 64 - 64, 25, 25), "" + i)) {
                    selectedCharacter.ChangeSkinColor(skinColors[i]);
                }
                GUI.color = Color.white;
            }
            GUI.Label(new Rect(150, Screen.height - 64 - 64, 200, 25), "Beardstyles:");
            for (int i = 1; i <= 6; i++) {
                if (GUI.Button(new Rect(150 + i * 25 - 25, Screen.height - 32 - 64, 25, 25), "" + i)) {
                    selectedCharacter.ChangeBeardstyle(i);
                }
            }
            GUI.Label(new Rect(150, Screen.height - 64, 200, 25), "Hairstyles:");
            for (int i = 1; i <= 13; i++) {
                if (GUI.Button(new Rect(150 + i * 25 - 25, Screen.height - 32, 25, 25), "" + i)) {
                    selectedCharacter.ChangeHairstyle(i);
                }
            }

            if (selectedCharacter != null) {
                float buttonSize = Screen.height * 0.1f;
                float startY = Screen.height * 0.2f;
                DrawEquipmentSlot(selectedCharacter.GetEquipmentSlot(Item.EquipmentSlots.head), Screen.width - Screen.width * 0.20f, startY + buttonSize * 0, buttonSize);
                DrawEquipmentSlot(selectedCharacter.GetEquipmentSlot(Item.EquipmentSlots.neck), Screen.width - Screen.width * 0.20f, startY + buttonSize * 1, buttonSize);
                DrawEquipmentSlot(selectedCharacter.GetEquipmentSlot(Item.EquipmentSlots.back), Screen.width - Screen.width * 0.20f + buttonSize, startY + buttonSize * 1, buttonSize);
                DrawEquipmentSlot(selectedCharacter.GetEquipmentSlot(Item.EquipmentSlots.body), Screen.width - Screen.width * 0.20f, startY + buttonSize * 2, buttonSize);
                DrawEquipmentSlot(selectedCharacter.GetEquipmentSlot(Item.EquipmentSlots.mainhand), Screen.width - Screen.width * 0.2f - buttonSize, startY + buttonSize * 2, buttonSize);
                DrawEquipmentSlot(selectedCharacter.GetEquipmentSlot(Item.EquipmentSlots.offhand), Screen.width - Screen.width * 0.2f + buttonSize, startY + buttonSize * 2, buttonSize);

                DrawEquipmentSlot(selectedCharacter.GetEquipmentSlot(Item.EquipmentSlots.hands), Screen.width - Screen.width * 0.2f - buttonSize, startY + buttonSize * 3, buttonSize);
                DrawEquipmentSlot(selectedCharacter.GetEquipmentSlot(Item.EquipmentSlots.legs), Screen.width - Screen.width * 0.2f, startY + buttonSize * 3, buttonSize);

                DrawEquipmentSlot(selectedCharacter.GetEquipmentSlot(Item.EquipmentSlots.feet), Screen.width - Screen.width * 0.2f, startY + buttonSize * 4, buttonSize);
            }
        }
        void DrawEquipmentSlot(CharacterBase.EquipmentSlot s, float x, float y, float size) {
            if (s.inUse) {
                if (GUI.Button(new Rect(x, y, size, size), "")) {
                    selectedCharacter.UnequipSlot(s.slot);
                }
                GUI.DrawTexture(new Rect(x, y, size, size), IconLoader.GetInstance().GetIcon(s.itemId));
            } else {
                GUI.Box(new Rect(x, y, size, size), "" + s.slot.ToString());
            }
        }
    }

}