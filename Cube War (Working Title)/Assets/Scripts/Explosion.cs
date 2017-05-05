using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {
    public GameObject expLight;
    public int rng;
    public float count;


    public void Awake()
    {
        StartCoroutine(timer());
        rng = Random.Range(10, 35);
        count = Time.time;
    }

    public void Update()
    {
        if (rng % 2 == 0) rng /= 2;
        else rng = rng * 3 + 1;
        expLight.GetComponent<Light>().intensity = Mathf.Lerp(expLight.GetComponent<Light>().intensity,rng*2,0.2f);
        if(Time.time - count < 0.275) expLight.GetComponent<Light>().range = Mathf.Lerp(expLight.GetComponent<Light>().range, 2.5f*expLight.GetComponent<Light>().range, (Time.time-count));
        else expLight.GetComponent<Light>().range = Mathf.Lerp(expLight.GetComponent<Light>().range, 1.5f - (Time.time - count), 0.23f);
    }

    IEnumerator timer()
    {
        yield return new WaitForSeconds(1.5f);
        GameObject.Destroy(this.gameObject);
    }
}
