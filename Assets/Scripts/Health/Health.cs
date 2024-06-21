using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{   
    [Header("Health")]
    [SerializeField] private float startingHealth;
    [SerializeField] private TextMeshProUGUI youDiedText;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    [Header("IFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    [SerializeField] private SpriteRenderer spriteRend;


    private void Awake()    
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            StartCoroutine(Invulnerability());
        } else
        {
            if (!dead)
            {
                anim.SetTrigger("die");
                GetComponent<PlayerMovement>().enabled = false;
                dead = true;
                youDiedText.gameObject.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(1);
        }
    }

    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(9, 10, true);

        for (int i = 0; i < numberOfFlashes; i++) {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration/numberOfFlashes);
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration/numberOfFlashes );
        }

        Physics2D.IgnoreLayerCollision(9, 10, false);
    }
}
