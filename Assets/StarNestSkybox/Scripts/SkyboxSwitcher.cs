using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;

public class SkyboxSwitcher : MonoBehaviour {

	Material[] mats;
	public int current = 41;


	
	

	void Start() {
		mats = Resources.LoadAll<Material>("");

		Load(41);
	}
	
	

	
	
	void Load(int i) {
		if (i < 0) { i = mats.Length + i; }
		i = i % mats.Length;
		current = i;
		Material mat = mats[i];
		RenderSettings.skybox = mat;



        float step = mat.GetFloat("_StepSize");
		



        mat.SetFloat("_CamScroll", 55 * Mathf.Sign(step));

	}

}
