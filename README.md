# Waves
Procedural Waves.

## Key Scripts & Components:
1. public class **BuoyancyPhysics** : MonoBehaviour
1. public class **WaveHeightMatcher** : MonoBehaviour
1. public class **WaveMeshGenerator** : MonoBehaviour
   * Properties: 
      * public Int32 ***DimensionX***  { get; }
      * public Int32 ***DimensionZ***  { get; }
   * Methods: 
      * public void ***Run***()
1. public class **Waves** : MonoBehaviour
   * Properties: 
      * public WavesSettings ***WavesSettings***  { get; }
1. public class **WavesCuller** : MonoBehaviour
### Settings
1. public class **ColorSettings** : ScriptableObject
   * Methods: 
      * public void ***ApplyTo***(Material material)
1. public class **TransparencySettings** : ScriptableObject
   * Methods: 
      * public void ***ApplyTo***(Material material)
1. public class **WavesSettings** : ScriptableObject
   * Properties: 
      * public List<Wave> ***Waves***  { get; }
      * public float ***MaximumAmplitude***  { get; }
   * Methods: 
      * public Vector3 ***GetWaveOffsetAtWorldPosition***(Vector2 worldPositionXZ)
      * public void ***ApplyTo***(Material material)
