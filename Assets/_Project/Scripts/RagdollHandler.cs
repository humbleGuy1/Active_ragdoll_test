using System;
using UnityEngine;
using UnityEngine.Serialization;

public class RagdollHandler : MonoBehaviour
{
    public Transform[] animatedBones;
    [FormerlySerializedAs("physicalBones")] [SerializeField] private Rigidbody[] _physicalBones;
    public float blendFactor = 0.5f; // 0 - чистая физика, 1 - чистая анимация

    private void OnValidate()
    {
        _physicalBones = GetComponentsInChildren<Rigidbody>();
    }

   private void FixedUpdate()
    {
        for (int i = 0; i < _physicalBones.Length; i++)
        {
            if (animatedBones[i] != null && _physicalBones[i] != null)
            {
                // Линейная интерполяция между физикой и анимацией
                Vector3 targetPosition = Vector3.Lerp(_physicalBones[i].position, animatedBones[i].position, 0);
                Quaternion targetRotation = Quaternion.Lerp(_physicalBones[i].rotation, animatedBones[i].rotation, 1);

                // Применяем силы для достижения цели
                _physicalBones[i].MovePosition(targetPosition);
                _physicalBones[i].MoveRotation(targetRotation);
            }
        }
    }
}