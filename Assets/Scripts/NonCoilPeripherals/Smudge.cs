using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smudge
{
    public Smudge(GameObject Coil)
    {
        GameObject particleObj = new GameObject("DotParticleSystem");
        // particleObj.transform.position = transform.position;
        particleObj.transform.SetParent(Coil.transform);

        var particleSystem = particleObj.AddComponent<ParticleSystem>();
        var renderer = particleObj.GetComponent<ParticleSystemRenderer>();

        // --- MAIN settings ---
        var main = particleSystem.main;
        main.startSize = 0.1f;
        main.startLifetime = 9999999999f;
        main.loop = false;
        // main.duration = 1f;
        main.maxParticles = 100;
        main.simulationSpace = ParticleSystemSimulationSpace.Local;
        main.startColor = new ParticleSystem.MinMaxGradient(
            new Color32(0x76, 0xBF, 0x38, 0xFF), // #76BF38
            new Color32(0x2C, 0x5F, 0x03, 0xFF)  // #2C5F03
        );
        main.startSpeed = 0f;

        // --- EMISSION ---
        var emission = particleSystem.emission;
        emission.rateOverTime = 0;
        emission.SetBursts(new ParticleSystem.Burst[] {
            new ParticleSystem.Burst(0f, 30)
        });

        // --- SHAPE ---
        var shape = particleSystem.shape;
        shape.shapeType = ParticleSystemShapeType.Mesh;
        shape.mesh = Coil.GetComponent<MeshFilter>().sharedMesh;
        // shape.radius = 0.1f;

        // --- RENDERER ---
        // renderer.material = dotMaterial;
        renderer.renderMode = ParticleSystemRenderMode.Billboard;
        renderer.sortingOrder = 10;

        // --- Trigger it ---
        particleSystem.Play();
    }
}
