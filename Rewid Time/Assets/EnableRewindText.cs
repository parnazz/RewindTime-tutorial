using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableRewindText : MonoBehaviour
{
    private TimeBody _timeBody;
    [SerializeField]
    private GameObject _rewindText;

    // Start is called before the first frame update
    void Start()
    {
        _timeBody = GameObject.Find("Player").GetComponent<TimeBody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_timeBody.isRewinding)
        {
            _rewindText.SetActive(true);
        }
        else
        {
            _rewindText.SetActive(false);
        }
    }
}
