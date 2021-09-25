using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionManager : Singleton<RegionManager>
{
    public List<RegionController> regions = new List<RegionController>();
    int currentRegionId = 0;

    public RegionController currentRegion { get { return regions[currentRegionId]; } }

    public void addRegion(RegionController region)
    {
        regions.Add(region);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
