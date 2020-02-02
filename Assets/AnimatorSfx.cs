using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorSfx : MonoBehaviour
{
	public GameObject ToeFL;
	public GameObject ToeFR;
	public GameObject ToeBL;
	public GameObject ToeBR;

	AudioSource audioToeFL;
	AudioSource audioToeFR;
	AudioSource audioToeBL;
	AudioSource audioToeBR;

	private void Awake()
	{
		audioToeFL = ToeFL.GetComponent<AudioSource>();
		audioToeFR = ToeFR.GetComponent<AudioSource>();
		audioToeBL = ToeBL.GetComponent<AudioSource>();
		audioToeBR = ToeBR.GetComponent<AudioSource>();
	}

	public void SfxStep(string s)
	{
		switch (s)
		{
			case "fr":
				audioToeFL.Play();
				break;
			case "fl":
				audioToeFR.Play();
				break;
			case "bl":
				audioToeBL.Play();
				break;
			case "br":
				audioToeBR.Play();
				break;
		}
	}
}
