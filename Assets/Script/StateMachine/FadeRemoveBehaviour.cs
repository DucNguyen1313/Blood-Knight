using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeRemoveBehaviour : StateMachineBehaviour
{
    public float fadeTime = 1f;
    protected float timer = 0f;
    protected SpriteRenderer spriteRenderer;
    protected GameObject objectToRemove;
    protected Damageable damageable;
    private Color startColor;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0f;
        spriteRenderer = animator.GetComponent<SpriteRenderer>();
        startColor = spriteRenderer.color;
        objectToRemove = animator.gameObject;
        damageable = animator.gameObject.GetComponent<Damageable>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timer > fadeTime)
        {
            float bloodCollection = PlayerPrefs.GetFloat("blood_collection");
            bloodCollection += damageable.MaxHealth;
            PlayerPrefs.SetFloat("blood_collection",bloodCollection);

            Destroy(objectToRemove);
        }
        
        timer += Time.deltaTime;
        
        // newAlpha is opacity of sprite
        float newAlpha = startColor.a * (1 - (timer / fadeTime));
        spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);
    }

}
