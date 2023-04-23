using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public interface IReplaceable
{
    public void OnShooted(Vector3 velocity);
}