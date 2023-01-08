using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public Slider slider;
    public BossHealth bh;

    void Start()
	{
		slider.maxValue = bh.health;
	}

	// Update is called once per frame
	void Update()
    {
		slider.value = bh.health;
    }
}
