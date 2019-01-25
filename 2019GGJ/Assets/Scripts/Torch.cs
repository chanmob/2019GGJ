using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    public HashSet<SpriteRenderer> lightedObjects = new HashSet<SpriteRenderer>();

    private void OnTriggerStay2D(Collider2D collision)
    {
        var sp = collision.GetComponent<SpriteRenderer>();
        if(sp != null)
        {
            sp.color = new Color(1, 1, 1);
            lightedObjects.Add(sp);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
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
    }
}
