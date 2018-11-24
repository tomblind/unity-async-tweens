# Unity AsyncTweens
Tween Extension to [Unity-AsyncRoutines](https://github.com/tomblind/unity-async-routines)

## Basic Usage
```cs
using AsyncRoutines;
using AsyncTweens;

public class Foo : MonoBehaviour
{
    public RoutineManagerBehavior routineManager;
    public AnimationCurve curve;

    public void Start()
    {
        routineManager.Run(Bar());
    }
    
    public async Routine Bar()
    {
        //Tween self to 2,2,0 over 2 seconds
        await Tween.Position.To(transform, new Vector3(1, 1, 0), 2);
        
        //Tween back using QuadInOut easing
        await Tween.Position.To(transform, new Vector3(0, 0, 0), 2, Easing.QuadInOut);
        
        //Tween to a relative position from the current
        await Tween.Position.ToOffset(transform, new Vector3(-1, -1, 0), 2);
        
        //Tween from a location back to the current
        await Tween.Position.From(transform, new Vector3(2, 2, 0), 2);
        
        //Tween using an AnimationCurve for easing
        await Tween.Position.To(transform, new Vector3(1, 1, 0), 2, curve.Evaluate);
    }
}
```

Aside from position, there are built-in tweeners for other Transform properties (eulerAngles, localScale, etc...) and properties on other objects as well (SpriteRenderer.color, RectTransform.anchoredPosition, etc...).

## Custom Tweeners
More tweeners for common Unity properties will be added over time, but you can add your own easily.
```cs
using AsyncRoutines;
using AsyncTweens;
using UnityEngine.Tilemaps;

public class Foo : MonoBehaviour
{
    public static Tweener<Color, Tilemap> tilemapColorTweener = new Tweener<Color, Tilemap>(
        (tilemap) => tilemap.color, // getter
        (tilemap, color) => tilemap.color = color //setter
    );
    
    public Tilemap tilemap;
    
    public async Routine Bar()
    {
        await tilemapColorTweener.To(tilemap, Color.black, 1);
    }
}
```
