using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeHandler : MonoBehaviour
{
    MarketManager market;
    // Start is called before the first frame update
    void Start()
    {
        market = GetComponentInParent<MarketManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
