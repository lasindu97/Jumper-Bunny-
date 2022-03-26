using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DamagePlayer : MonoBehaviour
{
    private Text _lifeText;

    private int _lifeScoreCount;

    private bool _canDamage;

    private void Awake()
    {
        _lifeText = GameObject.Find("Life Text").GetComponent<Text>();
        _lifeScoreCount = 3;
        _lifeText.text = "X " + _lifeScoreCount;

        _canDamage = true;
    }

    private void Start()
    {
        Time.timeScale = 1;
    }

    private void Update()
    {
        if(transform.position.y < -7.0f)
        {
            StartCoroutine(RestartGame());
        }
    }

    public void DealDamage()
    {
        if (_canDamage)
        {
            _lifeScoreCount--;

            if (_lifeScoreCount >= 0)
            {
                _lifeText.text = "X " + _lifeScoreCount;
            }

            if (_lifeScoreCount == 0)
            {
                // restart the game
                Time.timeScale = 0;
                StartCoroutine(RestartGame());
            }

            _canDamage = false;

            StartCoroutine(WaitForDamage());
        }


    }// DealDamage

    private IEnumerator WaitForDamage()
    {
        yield return new WaitForSeconds(2f);
        _canDamage = true;
    }

    private IEnumerator RestartGame()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        SceneManager.LoadScene("Gameplay");
    }
}// class
