using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerStatsChanger
{
    bool WasActivated { get; set; }
    void ChangeStats();
}
