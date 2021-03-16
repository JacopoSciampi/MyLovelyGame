using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconLoader {
    private static IconLoader instance;

    public static IconLoader GetInstance() {
        if (IconLoader.instance == null) IconLoader.instance = new IconLoader();
        return IconLoader.instance;
    }

    public class LoadedIcon {
        public int id;
        public Texture2D icon;
    }
    public List<LoadedIcon> loadedIcons = new List<LoadedIcon>();


    public Texture2D GetIcon(int id) {
        LoadedIcon l = GetLoadedIcon(id);
        if (l != null) {
            return l.icon;
        } else {
            return LoadIcon(id);
        }
    }
    private LoadedIcon GetLoadedIcon(int id) {
        foreach(LoadedIcon i in loadedIcons) {
            if(i.id == id) {
                return i;
            }
        }
        return null;
    }
    private Texture2D LoadIcon(int id) {
        LoadedIcon newIcon = new LoadedIcon();
        newIcon.id = id;
        newIcon.icon = Resources.Load<Texture2D>("CustomizableCharacters/Icons/" + id);
        if (newIcon.icon != null) {
            loadedIcons.Add(newIcon);
            return newIcon.icon;
        } else {
            return GetIcon(0);
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
