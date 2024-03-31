using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionArrow : MonoBehaviour
{
    [SerializeField] private RectTransform[] options;
    [SerializeField] private AudioClip changeSound;
    [SerializeField] private AudioClip interactSound;
    private RectTransform rect;
    private int currentPositon;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            ChangePosition(-1);
        if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            ChangePosition(1);

        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.E))
            Interact();
    }

    private void ChangePosition(int _change)
    {
        currentPositon += _change;

        if(_change != 0)
            SoundManager.instance.PlaySound(changeSound);

        if (currentPositon < 0 )
            currentPositon = options.Length - 1;
        else if (currentPositon > options.Length - 1)
            currentPositon = 0;

        rect.position = new Vector3(rect.position.x, options[currentPositon].position.y, 0);
    }
    private void Interact()
    {
        SoundManager.instance.PlaySound(interactSound);

        options[currentPositon].GetComponent<Button>().onClick.Invoke();
    }
}
