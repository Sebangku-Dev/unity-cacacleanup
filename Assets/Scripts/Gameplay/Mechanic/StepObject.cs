using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepObject : MonoBehaviour
{
    public Hint hint;
    public bool isDestroyEffect = false;
    public int step;

    GameObject effect;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Object")
        {
            if (step == StepCollision.instance.step)
            {
                StepCollision.instance.step += step == StepCollision.instance.step ? 1 : 0;

                if (StepCollision.instance.progressBar != null)
                {
                    StepCollision.instance.progressBar.fillAmount -= .25f;
                }

                if (StepCollision.instance.effect != null)
                {
                    effect = Instantiate(StepCollision.instance.effect, StepCollision.instance.effectParent.transform);

                    if (!isDestroyEffect)
                    {
                        Invoke("DestroyEffect", .6f);
                        isDestroyEffect = true;
                    }
                }
            }

        }
    }

    void DestroyEffect()
    {
        Destroy(effect);
        isDestroyEffect = false;
    }

}
