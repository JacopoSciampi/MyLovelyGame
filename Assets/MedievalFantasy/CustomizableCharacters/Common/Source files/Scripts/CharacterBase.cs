using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RPGCharacters {
    public class CharacterBase : MonoBehaviour {


        public int race;
        public int gender;
        public int hairStyle;
        public int beardStyle;
        public int eyebrowStyle;

        public Color skinColor;
        public Color eyeColor;
        public Color hairColor;
        public Color mouthColor;


        public enum ItemType {
            Equipment,
            Hair,
            Beard,
            BodyParts,
            Eyebrow
        }

        [System.Serializable]
        public class EquipmentSlot {
            public Item item;
            public Item.EquipmentSlots slot;
            public Transform container;
            public GameObject instancedObject;
            public GameObject activeObject; // male or female variation
            public bool inUse;
            public int itemId;
        }


        public GameObject armature;
        public List<EquipmentSlot> equipmentSlots = new List<EquipmentSlot>();

        private List<GameObject> bodyParts = new List<GameObject>();

        [HideInInspector]
        public bool loadAllItemsMode = false; // ignore, this is just used to create the alternative character prefab that contains all items

        [System.Serializable]
        public class Armature {
            public int race;
            public SkinnedMeshRenderer referenceMesh;
            public GameObject armature;
        }
        public List<Armature> armatures = new List<Armature>();
        public Animator animator;
        [Header("AnimatorController you want to use for this character")]
        public RuntimeAnimatorController animatorController;
        private Transform equippedItemsParent; // to hold all loaded items
        private Transform bodyPartsParent; // to hold all body parts, hair etc.

        public Item EquipItem(int itemId) { // use this to equip items
            Item i = LoadItem(itemId);
            if(loadAllItemsMode==false)
                SetupBody();
            return i;
        }

        public void UnequipItem(int itemId) {
            foreach (EquipmentSlot s in equipmentSlots) {
                if (s.itemId == itemId) {
                    UnequipSlot(s.slot);
                }
            }
        }
        public void UnequipItem(Item item) {
            UnequipSlot(item.equipmentSlot);
        }
        public bool HasItemEquipped(int itemId) {
            foreach (EquipmentSlot s in equipmentSlots) {
                if (s.itemId == itemId) {
                    return true;
                }
            }
            return false;
        }


        public void UnequipSlot(Item.EquipmentSlots slot) {
            UnloadSlot(slot);
            SetupBody();
        }
        public void UnequipAll() {
            foreach (EquipmentSlot s in equipmentSlots) {
                if (s.inUse) {
                    GameObject.Destroy(s.instancedObject);
                }
                s.inUse = false;
            }
            if(loadAllItemsMode==false)
            SetupBody();
        }
        public void ChangeHairstyle(int id) {
            hairStyle = id;
            SetupBody();
        }
        public void ChangeEyebrowstyle(int id) {
            eyebrowStyle = id;
            SetupBody();
        }
        public void ChangeBeardstyle(int id) {
            beardStyle = id;
            SetupBody();
        }
        public void ChangeGender(int id) {
            gender = id;
            UnequipAll();
            SetupBody();
        }
        public void ChangeRace(int id) {
            race = id;
            beardStyle = 0;
            hairStyle= 0;
            eyebrowStyle = 0;
            UnequipAll();
            UpdateRace();
            SetupSlots();
            SetupBody();
        }
        public void ChangeSkinColor(Color c) {
            skinColor = c;
            SetupBody();
            foreach (EquipmentSlot slot in equipmentSlots) {
                if (slot.inUse) {
                    SetItemColor(slot.activeObject);
                }
            }
        }
        public void ChangeHairColor(Color c) {
            hairColor = c;
            SetupBody();
        }
        public void ChangeEyeColor(Color c) {
            eyeColor = c;
            SetupBody();
        }


        public void ClearBody() {
            foreach (GameObject g in bodyParts) {
                GameObject.Destroy(g);
            }
        }

        void UnloadSlot(Item.EquipmentSlots slot) {
            if (GetEquipmentSlot(slot).inUse) {
                GetEquipmentSlot(slot).inUse = false;
                GameObject.Destroy(GetEquipmentSlot(slot).instancedObject);
            }
        }

        public EquipmentSlot GetEquipmentSlot(Item.EquipmentSlots slot) {
            foreach (EquipmentSlot e in equipmentSlots) {
                if (e.slot == slot) {
                    return e;
                }
            }
            return null;
        }

        
        public Item LoadItem(int itemId) {
            return LoadItem(itemId, ItemType.Equipment);
        }
        public Item LoadItem(int itemId, ItemType itemType) {
            try {
                if (itemId == 0) {
                    return null;
                }
                GameObject loadedItem = null;
                object loadedObject = Resources.Load("CustomizableCharacters/Race " + race + "/" + itemType.ToString() + "/" + itemId, typeof(GameObject));
                if (loadedObject == null) {
                    Debug.LogError(gameObject.name + " Failed to load " + itemType.ToString() + " item " + itemId + ". Make sure you are using the correct item ID!");
                    return null;
                }
                loadedItem = GameObject.Instantiate((GameObject)loadedObject) as GameObject;

                Item item = loadedItem.GetComponent<Item>();
                EquipmentSlot slot = GetEquipmentSlot(item.equipmentSlot);
                GameObject itemObject = null;
                item.male.SetActive(false);
                item.female.SetActive(false);

                if (gender == 0) {
                    itemObject = item.male;
                    GameObject.Destroy(item.female); // not needed -> destroy
                } else {
                    itemObject = item.female;
                    GameObject.Destroy(item.male); // not needed -> destroy
                }
                itemObject.SetActive(true);
                loadedItem.name = 
                    (gender == 0 ? "M" : "F")+ " "+
                    (item.equipmentSlot == Item.EquipmentSlots.none ? "BodyPart" : item.equipmentSlot.ToString()) + " " 
                    + itemId; // give the gameobject a more descriptive name

                if (item.skinned) {

                    SkinnedMeshRenderer skinnedRenderer = itemObject.GetComponentInChildren<SkinnedMeshRenderer>();
                    skinnedRenderer.bones = GetReferenceMesh().bones;
                    skinnedRenderer.updateWhenOffscreen = true;
                    if(itemType == ItemType.Equipment)
                        item.transform.SetParent(equippedItemsParent);
                    else
                        item.transform.SetParent(bodyPartsParent);


                    foreach (Transform t in skinnedRenderer.GetComponentsInParent<Transform>()[1].GetComponentInChildren<Transform>()) { // destroy unnecessary gameobjects
                        if (t.GetComponent<SkinnedMeshRenderer>() == null) {
                            GameObject.Destroy(t.gameObject);
                        }
                    }

                } else {
                    item.transform.SetParent(slot.container);
                }


                loadedItem.transform.localPosition = Vector3.zero;
                loadedItem.transform.localScale = new Vector3(1, 1, 1);
                loadedItem.transform.localRotation = Quaternion.identity;

                

                if (slot != null) {
                    if (slot.inUse) { // replace current item, if you need to remove stats of the previous item, do it here (e.g. character health, damage)
                        if (loadAllItemsMode == false) {
                            GameObject.Destroy(slot.instancedObject);
                        }
                    }
                    slot.item = item;
                    slot.itemId = itemId;
                    slot.inUse = true;
                    slot.instancedObject = loadedItem;
                    slot.activeObject = itemObject.gameObject;
                }

                if (itemType == ItemType.BodyParts) {
                    bodyParts.Add(loadedItem);
                }
                GetReferenceMesh().enabled = false;
                if(loadAllItemsMode==false)
                    SetItemColor(itemObject.gameObject);
                return item;
            } catch (Exception e) {
                Debug.LogError(gameObject.name + " Failed to load " + itemType.ToString() + " item " + itemId + " (" + e.ToString() + ")");
                return null;
            }
        }

        void SetItemColor(GameObject itemObject) {
            ColorChanger colorChanger = itemObject.GetComponent<ColorChanger>();
            if (colorChanger != null) {
                foreach (ColorChanger.Changer c in colorChanger.changers) {
                    Renderer r = c.rend;
                    Material m = r.materials[c.materialIndex];
                    if (c.type == ColorChanger.Type.Skin) {
                        m.color = skinColor;
                    } else if (c.type == ColorChanger.Type.Hair) {
                        m.color = hairColor;
                    } else if (c.type == ColorChanger.Type.Mouth) {
                        m.color = mouthColor;
                    } else if (c.type == ColorChanger.Type.Eyes) {
                        m.color = eyeColor;
                    }
                }
            }
        }



        public void SetupBody() { // load body parts, hair, beard, eyebrows
            //ClearBody();

            //if (GetEquipmentSlot(Item.EquipmentSlots.head).inUse) {
            //    if (GetEquipmentSlot(Item.EquipmentSlots.head).item.showHead) {
            //        LoadItem(4, ItemType.BodyParts); // head 
            //    }
            //    if (GetEquipmentSlot(Item.EquipmentSlots.head).item.showHair) {
            //        LoadItem(hairStyle, ItemType.Hair);
            //    } else {
            //        UnloadSlot(Item.EquipmentSlots.hair);
            //    }
            //    if (GetEquipmentSlot(Item.EquipmentSlots.head).item.showEyebrow) {
            //        LoadItem(eyebrowStyle, ItemType.Eyebrow);
            //    } else {
            //        UnloadSlot(Item.EquipmentSlots.eyebrow);
            //    }
            //    if (gender == 0) {
            //        if (GetEquipmentSlot(Item.EquipmentSlots.head).item.showBeard) {
            //            LoadItem(beardStyle, ItemType.Beard);
            //        } else {
            //            UnloadSlot(Item.EquipmentSlots.beard);
            //        }
            //    } else {
            //        UnloadSlot(Item.EquipmentSlots.beard);
            //    }
            //} else {
            //    LoadItem(4, ItemType.BodyParts); // head 
            //    LoadItem(hairStyle, ItemType.Hair);
            //    LoadItem(eyebrowStyle, ItemType.Eyebrow);
            //    if (gender == 0) {
            //        LoadItem(beardStyle, ItemType.Beard);
            //    } else {
            //        UnloadSlot(Item.EquipmentSlots.beard);
            //    }
            //}


            //if (GetEquipmentSlot(Item.EquipmentSlots.body).inUse == false) {
            //    LoadItem(3, ItemType.BodyParts); // torso 
            //}
            //if (GetEquipmentSlot(Item.EquipmentSlots.hands).inUse == false) {
            //    LoadItem(5, ItemType.BodyParts); // hands
            //}
            //if (GetEquipmentSlot(Item.EquipmentSlots.legs).inUse == false) {
            //    LoadItem(1, ItemType.BodyParts); // legs
            //}
            //if (GetEquipmentSlot(Item.EquipmentSlots.feet).inUse == false) {
            //    LoadItem(2, ItemType.BodyParts); // feet
            //}
        }
        public SkinnedMeshRenderer GetReferenceMesh() {
            return armatures.SingleOrDefault(s => s.race == race).referenceMesh;
        }
        public void Awake() {
            if (equippedItemsParent == null) {
                equippedItemsParent = new GameObject("SkinnedItems").transform;
                equippedItemsParent.SetParent(transform);
            }
            if (bodyPartsParent == null) {
                bodyPartsParent = new GameObject("BodyParts").transform;
                bodyPartsParent.SetParent(transform);
            }
            SetupArmature();
            UpdateRace();
            SetupSlots();
            SetupBody();

        }
        public void SetupArmature() {
            if (armatures.Count == 0) {
                int counter = 0;
                foreach (GameObject p in Resources.LoadAll<GameObject>("CustomizableCharacters/Armatures/")) {
                    GameObject g = GameObject.Instantiate(p, transform);
                    armatures.Add(new Armature() { race = counter, armature = g, referenceMesh = g.GetComponentInChildren<SkinnedMeshRenderer>() });
                    counter++;
                    g.SetActive(false);
                }
            }

        }
        public void UpdateRace() {
            foreach (Armature a in armatures) {
                a.armature.SetActive(false);
            }
            armature = armatures.SingleOrDefault(s => s.race == race).armature;
            armature.SetActive(true);
            animator = armature.GetComponent<Animator>();
            if(animatorController!=null)
            animator.runtimeAnimatorController = animatorController;
        }
        void SetupSlots() {
            equipmentSlots.Clear();
            equipmentSlots.Add(new EquipmentSlot() { slot = Item.EquipmentSlots.body });
            equipmentSlots.Add(new EquipmentSlot() { slot = Item.EquipmentSlots.feet });
            equipmentSlots.Add(new EquipmentSlot() { slot = Item.EquipmentSlots.hair, container = armature.transform.FindDeepChild("EQ_Head") });
            equipmentSlots.Add(new EquipmentSlot() { slot = Item.EquipmentSlots.head, container = armature.transform.FindDeepChild("EQ_Head") });
            equipmentSlots.Add(new EquipmentSlot() { slot = Item.EquipmentSlots.legs });
            equipmentSlots.Add(new EquipmentSlot() { slot = Item.EquipmentSlots.mainhand, container = armature.transform.FindDeepChild("EQ_MainHand") });
            equipmentSlots.Add(new EquipmentSlot() { slot = Item.EquipmentSlots.offhand, container = armature.transform.FindDeepChild("EQ_OffHand") });
            equipmentSlots.Add(new EquipmentSlot() { slot = Item.EquipmentSlots.hands });
            equipmentSlots.Add(new EquipmentSlot() { slot = Item.EquipmentSlots.beard });
            equipmentSlots.Add(new EquipmentSlot() { slot = Item.EquipmentSlots.eyebrow });
            equipmentSlots.Add(new EquipmentSlot() { slot = Item.EquipmentSlots.neck });
            equipmentSlots.Add(new EquipmentSlot() { slot = Item.EquipmentSlots.accessory });
            equipmentSlots.Add(new EquipmentSlot() { slot = Item.EquipmentSlots.back, container = armature.transform.FindDeepChild("EQ_Back") });
        }

    }
}