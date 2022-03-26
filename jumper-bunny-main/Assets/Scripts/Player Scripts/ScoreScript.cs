using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreScript : MonoBehaviour
{
    private Text _coinTextScore;
    private AudioSource _audioSource;

    private int _scoreCount;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _coinTextScore = GameObject.Find("Coin Text").GetComponent<Text>();
    }


    private void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Coin")
        {

            _scoreCount++;

            _coinTextScore.text = "X " + _scoreCount.ToString();

            target.gameObject.SetActive(false);

            _audioSource.Play();

        }
    }

}// class
