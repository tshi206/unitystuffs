using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour
{

	private AudioSource _audioSource;
	private Animation _animation;

	private void Start()
	{
		_audioSource = GetComponent<AudioSource>();
		_animation = GetComponent<Animation>();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire1"))
		{
			_audioSource.Play();
			_animation.Play("GunShot");
		}
	}
}
