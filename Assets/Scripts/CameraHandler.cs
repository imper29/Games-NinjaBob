#pragma warning disable
using Characters;
using Level;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public static CameraHandler Instace { get; private set; }
    private Camera cam;

    [SerializeField][Range(0.001f, 1f)]
    private float sharpness = 0.05f;
    [SerializeField]
    private List<Transform> followTargets = new List<Transform>();


    private void Awake()
    {
        Instace = this;
        cam = Camera.main;

        DontDestroyOnLoad(gameObject);

        LevelData.OnLevelLoaded += LevelData_OnLevelLoaded;
        LevelData.OnLevelUnloaded += LevelData_OnLevelUnloaded;
        CharacterBase.OnCharacterSelected += CharacterBase_OnCharacterSelected;
        CharacterBase.OnCharacterDeselected += CharacterBase_OnCharacterDeselected;
    }
    private void FixedUpdate()
    {
        if (followTargets.Count != 0)
        {
            Vector3 pos = Vector3.zero;
            for (int i = 0; i < followTargets.Count; i++)
                if (followTargets[i])
                    pos += followTargets[i].position;

            pos = Vector3.Lerp(transform.position, pos / followTargets.Count, sharpness);
            pos = Vector3.Lerp(pos, cam.ScreenToWorldPoint(Input.mousePosition), sharpness / 3f);

            pos.z = transform.position.z;
            transform.position = pos;
        }
    }

    private void LevelData_OnLevelLoaded(LevelData lvl, bool reloading)
    {
        Vector3 pos = CharacterBase.ALL_CHARACTERS[0].transform.position;
        pos.z = transform.position.z;
        transform.position = pos;

        GetComponent<Camera>().enabled = FindObjectsOfType<Camera>().Length == 1;
    }
    private void LevelData_OnLevelUnloaded(LevelData lvl, bool reloading)
    {
        GetComponent<Camera>().enabled = true;
    }
    private void CharacterBase_OnCharacterSelected(CharacterBase character)
    {
        followTargets.Add(character.transform);
    }
    private void CharacterBase_OnCharacterDeselected(CharacterBase character)
    {
        followTargets.Remove(character.transform);
    }
}
