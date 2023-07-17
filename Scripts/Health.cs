using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth = 100;
    public Image image;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    public void TakeDamage (float amount, Pawn source)
    {
        currentHealth = currentHealth - amount;
        Debug.Log(source.name + "did" + amount + "damage to" + gameObject.name);
            if (currentHealth <= 0)
        {
            Die(source);
            Camera.main.gameObject.GetComponent<MainCameraScript>().GameOver();
        }
            /*image.fillAmount = amount;*/
    }

    public void Heal (float amount, Pawn source)
    {
        currentHealth = currentHealth + amount;
        image.fillAmount = amount;
    }

    public void Die (Pawn source)
    {
        Destroy(gameObject);
    }
}
