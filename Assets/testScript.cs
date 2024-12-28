using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Reflection;
using KModkit;

public class testScript : MonoBehaviour {

	public KMService Service;
	public KMGameInfo Info;

	public KMGameInfo.KMStateChangeDelegate StateChangeDelegate;

    public bool done = false;
	public string name;
    public UnityEngine.UI.Text uiText;

	void Awake() {
		
	}
	
	// Use this for initialization
	void Start () {
		Info.OnStateChange += HandleStateChange;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Tab)) {
			uiText.text = name;
		} else {
			uiText.text = "";
		}
        if(!done) {
            KMBombModule[] modules = FindObjectsOfType<KMBombModule>();
            if(modules.Length != 0) {
                done = true;
            }
            foreach(KMBombModule module in modules) {
                KMSelectable selectable = module.GetComponent<KMSelectable>();
                selectable.OnSelect += () => OnSelect(module.ModuleDisplayName);
            }
        }
	}

    private void OnSelect(string moduleDisplayName) {
		if (moduleDisplayName.ToLower ().StartsWith ("not")) {
			moduleDisplayName = moduleDisplayName.Substring (4, moduleDisplayName.Length);
		}
		name = moduleDisplayName;
    }

	private void HandleStateChange(KMGameInfo.State state) {
		if(state != KMGameInfo.State.Gameplay) {
			uiText.text = "";
			done = false;
		}
	}
}
