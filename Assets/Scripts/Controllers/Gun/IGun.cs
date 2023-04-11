using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public interface IGun
{
    public int AmmoCapacity { get; set; }
    public int CurrentBulletCount { get; set; }
    public void OnShoot();
    public async Task Reload() { }
}