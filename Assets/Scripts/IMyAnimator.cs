using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMyAnimator
{
    /// <summary>
    /// Updates the animation of the implementing class
    /// </summary>
    /// <returns>False if the animation has completed</returns>
    public bool updateAnimation();

    public void destroy();
}
