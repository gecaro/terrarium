using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [Header("Plant Points (30 max")]
    public float MaxEnergy;
    public float MatureSize;
    public float GrowthRate;
    public float SeedSpreadRadius;

    //monitoring
    [Header("Monitoring")]
    public float Energy;
    public float Size;
    public float Age;

    [Header("Sedling")]
    public GameObject SeedlingSpawn;

    //species parameters
    [Header("Species parameters")]
    public float EnergyGrowthRate = .01f;
    public float AgeRate = .001f;
    private Transform Environment;


    // Start is called before the first frame update
    void Start()
    {
        Size = 0;
        Energy = 1;
        Age = 0;
        Environment = transform.parent;
        TransformSize();
    }

    // Update is called once per frame
    void Update()
    {
        if (CanGrow) Grow();
        if (CanReproduce) Reproduce();
        if (Dead) Destroy(this);
        Age += AgeRate;
        Energy += EnergyGrowthRate; 
    }

    void TransformSize()
    {
        transform.localScale = Vector3.one * Size;
    }

    bool CanGrow
    {
        get 
        {
            return Energy > ((MaxEnergy / 2) + 1);
        }
    }

    bool CanReproduce
    {
        get
        {
            if (Size >= MatureSize && CanGrow) return true;
            else return false;
        }
    }

    bool Dead
    {
        get
        {
            return Energy < 0 || Age > MatureSize;
        }
    }

    void Grow()
    {
        if (Size < MatureSize){
            Energy = Energy / 2;
            Size += GrowthRate * Random.value;
            TransformSize();
        }
    }

    void Reproduce()
    {
        var vec = Random.insideUnitCircle * SeedSpreadRadius + new Vector2(transform.position.x, transform.position.z);
        Instantiate(SeedlingSpawn, new Vector3(vec.x, 0, vec.y), Quaternion.identity, Environment);
        Energy = Energy / 2;
    }
}
