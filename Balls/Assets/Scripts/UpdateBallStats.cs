using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateBallStats : MonoBehaviour
{
    private float lifeTime = 0f;
    private Color startColor;
    private GameObject moneySystem;

    public GameSettings settings;

    private void Start()
    {
        moneySystem = GameObject.Find("MoneySystem");
        startColor = GetComponent<SpriteRenderer>().color;
    }

    private void Update()
    {
        transform.Find("Canvas").transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, -transform.localEulerAngles.z);
        if (transform.localScale.x == 1f)
        {
            transform.Find("Canvas").Find("HP").GetComponent<TextMeshProUGUI>().text = "100";
        }
        else
        {
            transform.Find("Canvas").Find("HP").GetComponent<TextMeshProUGUI>().text = (transform.localScale.x * 100f).ToString("F1");
        }
    }

    private void FixedUpdate()
    {
        lifeTime += Time.deltaTime;        
        if (lifeTime <= 20f)
        {
            GetComponent<SpriteRenderer>().color = Color.Lerp(startColor, Color.white, lifeTime / 20f);
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Color.black, (lifeTime - 20f) / 10f);
            transform.Find("Canvas").localPosition = new Vector3(Random.Range(-0.02f, 0.02f), Random.Range(-0.02f, 0.02f), 0f);
        }
        if (lifeTime >= 30f)
        {
            moneySystem.GetComponent<BallScoring>().ScoreBall(0.2f);
            Destroy(gameObject);
        }
        if (transform.localScale.x < settings.ballMinHP / 100)
        {
            transform.localScale = new Vector3(settings.ballMinHP / 101, settings.ballMinHP / 101, 0);
        }
    }    
}