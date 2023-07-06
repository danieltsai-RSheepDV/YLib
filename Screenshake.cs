using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Library
{
    public class Screenshake : MonoBehaviour
    {
        private IEnumerator shakeCoroutine;
        private bool shaking = false;
        private float severity;
        private Vector3 startingAngle;
        private Vector3 startingPos;

        private void Start()
        {
            startingAngle = transform.localEulerAngles;
            startingPos = transform.localPosition;
        }

        public void Pause(int milliseconds)
        {
            StartCoroutine(PauseCoroutine(milliseconds));
        }

        public void Shake(int milliseconds, float severity)
        {
            return;
            
            Abort();
            this.severity = severity;
            shakeCoroutine = ShakeCoroutine(milliseconds);
            StartCoroutine(shakeCoroutine);
        }

        public void Abort()
        {
            if(shakeCoroutine == null) return;
            StopCoroutine(shakeCoroutine);
            shaking = false;
        }

        private void Update()
        {
            if (shaking)
            {
                transform.localEulerAngles = startingAngle + new Vector3(Random.Range(-severity, severity), Random.Range(-severity, severity), 0);
                transform.localPosition = startingPos + new Vector3(Random.Range(-severity, severity), Random.Range(-severity, severity), 0);
            }
        }

        IEnumerator ShakeCoroutine(int milliseconds)
        {
            shaking = true;
            yield return new WaitForSeconds(milliseconds / 1000f);
            shaking = false;
            transform.localEulerAngles = startingAngle;
            transform.localPosition = startingPos;
        }

        IEnumerator PauseCoroutine(int milliseconds)
        {
            float savedTime = Time.realtimeSinceStartup;
            
            Time.timeScale = 0;
            while(Time.realtimeSinceStartup - savedTime < milliseconds / 1000f)
            {
                yield return 0;
            }
            Time.timeScale = 1;
        }
    }
}
