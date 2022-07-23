using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int score;
    public int hitsToBreak;
    public Sprite hitSprite;
    public GameObject explosion;

    public void BreakBrick()
    {
        hitsToBreak --;

        GetComponent<SpriteRenderer>().sprite = hitSprite;
    }

    public void PlayParticleEffect()
    {
        GameObject newExplosion = Instantiate(explosion, this.transform.position, this.transform.rotation);
        Destroy(newExplosion, 1.2f);
    }
}
