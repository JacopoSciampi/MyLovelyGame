using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPGCharacters {
    public class NpcEquipment : CharacterBase {

        [Header("List of equipped items:")]
        public List<int> items = new List<int>();
        public void Start() {
            foreach (int i in items) {
                EquipItem(i);
            }
        }
        public bool updateInRealTime;
        private float updateInRealTimeTimer;
        private void Update() {
            if (updateInRealTime) {
                updateInRealTimeTimer -= 1 * Time.deltaTime;
                if (updateInRealTimeTimer <= 0) {
                    updateInRealTimeTimer = 0.5f;
                    foreach (int i in items) {
                        EquipItem(i);
                    }
                }
            }

        }
    }
}
