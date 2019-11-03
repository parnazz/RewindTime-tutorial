using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour
{
    public bool isRewinding = false;

    List<PointInTime> _pointsInTime;

    Rigidbody _rb;

    // Start is called before the first frame update
    void Start()
    {
        _pointsInTime = new List<PointInTime>();
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartRewinding();
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            StopRewinding();
        }
    }

    private void FixedUpdate()
    {
        if (isRewinding)
        {
            Rewind();
        }
        else
        {
            Record();
        }
    }

    private void Record()
    {
        if (_pointsInTime.Count > Mathf.Round(5f / Time.fixedDeltaTime))
        {
            _pointsInTime.RemoveAt(_pointsInTime.Count - 1);
        }

        _pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation));
    }

    private void Rewind()
    {
        if (_pointsInTime.Count > 0)
        {
            PointInTime pointInTime = _pointsInTime[0];
            transform.position = pointInTime.position;
            transform.rotation = pointInTime.rotation;
            _pointsInTime.RemoveAt(0);
        }
        else
        {
            StopRewinding();
        }
    }

    public void StartRewinding()
    {
        isRewinding = true;
        _rb.isKinematic = true;
    }

    public void StopRewinding()
    {
        isRewinding = false;
        _rb.isKinematic = false;
    }
}
