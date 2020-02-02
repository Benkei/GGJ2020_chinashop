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

	ParticleSystem fxToeFL;
	ParticleSystem fxToeFR;
	ParticleSystem fxToeBL;
	ParticleSystem fxToeBR;

	private void Awake()
	{
		audioToeFL = ToeFL.GetComponent<AudioSource>();
		audioToeFR = ToeFR.GetComponent<AudioSource>();
		audioToeBL = ToeBL.GetComponent<AudioSource>();
		audioToeBR = ToeBR.GetComponent<AudioSource>();

		fxToeFL = ToeFL.GetComponentInChildren<ParticleSystem>();
		fxToeFR = ToeFR.GetComponentInChildren<ParticleSystem>();
		fxToeBL = ToeBL.GetComponentInChildren<ParticleSystem>();
		fxToeBR = ToeBR.GetComponentInChildren<ParticleSystem>();
	}

	public void SfxStep(string s)
	{
		switch (s)
		{
			case "fr":
				audioToeFL.Play();
				fxToeFL.Play();
				break;
			case "fl":
				audioToeFR.Play();
				fxToeFR.Play();
				break;
			case "bl":
				audioToeBL.Play();
				fxToeBL.Play();
				break;
			case "br":
				audioToeBR.Play();
				fxToeBR.Play();
				break;
		}
	}
}
