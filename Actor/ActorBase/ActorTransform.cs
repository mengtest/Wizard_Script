﻿using UnityEngine;
using System.Collections;

public class ActorTransform : MonoBehaviour {

    CharacterController controller;
    GroundHeightUpdateProcessor heightSynchronizer;

    public bool HeightSyncable {
        get; set;
    }

    public bool IsUnderGround() {
        return this.heightSynchronizer.IsUnderGround();
    }

    protected virtual void Awake() {
        HeightSyncable = true;
        controller = this.GetComponent<CharacterController>();
        heightSynchronizer = new GroundHeightUpdateProcessor(this.transform, controller.height * 0.5f);
    }

    protected virtual void OnEnable() {
    }

    protected virtual void Start() {
    }

    protected virtual void FixedUpdate() {

    }
    protected virtual void Update() {
    }

    protected virtual void LateUpdate() {
        heightSynchronizer.MeasureHeight();
        if (HeightSyncable) {
            heightSynchronizer.UpdateHeight();
        }
    }

    protected virtual void OnDisable() {

    }

    protected virtual void OnDestroy() {

    }


    public class GroundHeightUpdateProcessor {
        Transform transform;
        float offset = 0f;

        float targetHeight = 0f;
        float refHeight = 0f;
        float smoothTime = 0.1f;

        public GroundHeightUpdateProcessor(Transform _transform, float _offset) {
            transform = _transform;
            offset = _offset;
        }

        public bool IsCloseToGround() {
            return Mathf.Abs(transform.position.y - targetHeight) < 0.001f;
        }

        public bool IsUnderGround() {
            return transform.position.y < targetHeight;
        }

        public void MeasureHeight() {
            Ray ray = new Ray(this.transform.position.AddY(30), Vector3.down);
            RaycastHit hit;
            Physics.Raycast(ray, out hit, 50, Layer.GroundMask);
            if (hit.collider != null) {
                targetHeight = hit.point.y + offset;
            }
        }

        public void UpdateHeight() {
            if (IsCloseToGround()) {
                return;
            }

            float newY = Mathf.SmoothDamp(transform.position.y, targetHeight, ref refHeight, smoothTime);
            transform.position = transform.position.SetY(newY);
        }


    }

}
