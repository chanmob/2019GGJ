using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    public HashSet<SpriteRenderer> lightedObjects = new HashSet<SpriteRenderer>();
    public HashSet<Monster> monsterObjects = new HashSet<Monster>();

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            return;
        }

        var monster = collision.GetComponent<Monster>();
        if(monster != null)
        {
            monsterObjects.Add(monster);
            if (!monster.particle.activeInHierarchy)
            {
                monster.particle.SetActive(true);
            }
        }

        var sp = collision.GetComponent<SpriteRenderer>();
        if (sp != null)
        {
            sp.color = new Color(1, 1, 1);
            lightedObjects.Add(sp);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            return;
        }

        var monster = collision.GetComponent<Monster>();
        if (monster != null)
        {
            monsterObjects.Remove(monster);
            if (monster.particle.activeInHierarchy)
            {
                monster.particle.SetActive(false);
            }
        }


        var sp = collision.GetComponent<SpriteRenderer>();
        if (sp != null)
        {
            sp.color = new Color(0, 0, 0);
            lightedObjects.Remove(sp);
        }
    }

    private void OnDisable()
    {
        foreach(var ob in lightedObjects)
        {
            ob.color = new Color(0, 0, 0);
        }
        lightedObjects.Clear();

        foreach(var ob in monsterObjects)
        {
            ob.particle.SetActive(false);
        }
        monsterObjects.Clear();
    }
}
