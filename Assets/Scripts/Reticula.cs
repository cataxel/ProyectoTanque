using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reticula : MonoBehaviour
{
	public Image h0;
	public Image h1;
	public Image h2;
	public Image v0;
	public Image v1;
	public Image v2;
	public Image v3;
	public Image v4;
	public Image v5;
	private bool ban = false;

	void Start()
    {
		ShowReticle(ban);
	}

    void Update()
    {
		if (Input.GetMouseButtonDown(1))
		{
			if (ban == false)
			{
				ban = true;
			}
			else
			{
				ban = false;
			}
			ShowReticle(ban);
		}
    }

    void ShowReticle(bool show)
    {
        h0.enabled = show;
        h1.enabled = show;
		h2.enabled = show;
		v0.enabled = show;
		v1.enabled = show;
		v2.enabled = show;
		v3.enabled = show;
		v4.enabled = show;
		v5.enabled = show;

	}
}
