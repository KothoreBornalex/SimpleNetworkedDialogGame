using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageController : MonoBehaviour
{
    [SerializeField] private LanguageType language;

    public static LanguageController Instance { get; private set; }
    public LanguageType Language { get => language; set => language = value; }

    // Check later with titouan the need for this script, normally it should be useless
    private void Awake()
    {
        if(Instance ==null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }

    }
}
