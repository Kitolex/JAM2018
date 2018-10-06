using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect : MonoBehaviour {

    public abstract void Begin();

    public abstract void Display();

    public abstract void End();


}

public enum Effects
{
    a,
    b,
    c,
    d,
    e,
    f    
}
