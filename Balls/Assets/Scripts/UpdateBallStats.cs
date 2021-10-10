using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateBallStats : MonoBehaviour
{
    private float lifeTime = 0;
    private Color startColor;

    public GameObject moneySystem;

    private void Start()
    {
        moneySystem = GameObject.Find("MoneySystem");
        startColor = GetComponent<SpriteRenderer>().color;
    }

    private void FixedUpdate()
    {
        lifeTime += Time.deltaTime;        
        if (lifeTime <= 20f)
        {
            GetComponent<SpriteRenderer>().color = Color.Lerp(startColor, Color.white, lifeTime / 20);
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Color.black, (lifeTime - 20) / 10);
            transform.Find("Canvas").localPosition = new Vector3(Random.Range(-0.02f, 0.02f), Random.Range(-0.02f, 0.02f), 0);
        }
        if (lifeTime >= 30f)
        {
            moneySystem.GetComponent<BallScoring>().ScoreBall(0.2f);
            Destroy(gameObject);
        }
        transform.Find("Canvas").transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, -transform.localEulerAngles.z);
        if (transform.localScale.x == 1)
        {
            transform.Find("Canvas").Find("HP").GetComponent<TextMeshProUGUI>().text = "100";
        }
        else
        {
            transform.Find("Canvas").Find("HP").GetComponent<TextMeshProUGUI>().text = (transform.localScale.x * 100).ToString("F1");
        }
    }
}