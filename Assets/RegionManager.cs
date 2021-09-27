using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RegionInfo : InfoBase
{

}
public class AllRegionInfo
{
    public List<RegionInfo> allRegion;
}
public class RegionManager : Singleton<RegionManager>
{
    public List<RegionController> regions = new List<RegionController>();
    int currentRegionId = 0;

    public RegionController currentRegion { get { return regions[currentRegionId]; } }

    public void selectNextRegion()
    {
        currentRegionId += 1;
        currentRegionId %= regions.Count;
        setCamera();
    }

    public void selectPreviousRegion()
    {
        currentRegionId -= 1;
        currentRegionId += regions.Count;
        currentRegionId %= regions.Count;
        setCamera();
    }

    public void setCamera()
    {
        clearCamera();
        currentRegion.buildCamera.gameObject.SetActive(true);
    }

    public void clearCamera()
    {

        foreach (var region in regions)
        {
            region.buildCamera.gameObject.SetActive(false);
        }
    }

    public Dictionary<string, RegionInfo> regionInfoDict = new Dictionary<string, RegionInfo>();
    // Start is called before the first frame update
    void Awake()
    {
        string text = Resources.Load<TextAsset>("json/region").text;
        var allNPCs = JsonMapper.ToObject<AllRegionInfo>(text);
        foreach (RegionInfo info in allNPCs.allRegion)
        {
            regionInfoDict[info.name] = info;
        }
    }

    public void addRegion(RegionController region)
    {
        if (region.index == 0)
        {
            currentRegionId = regions.Count;
        }
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
