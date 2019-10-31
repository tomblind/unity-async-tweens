# Unity AsyncTweens
Tween Extension to [Unity-AsyncRoutines](https://github.com/tomblind/unity-async-routines)

## Basic Usage
```cs
using AsyncRoutines;
using AsyncTweens;

public class Foo : MonoBehaviour
{
    public RoutineManagerBehavior routineManager;

    public void Start()
    {
        routineManager.Run(Bar());
    }

    public async Routine Bar()
    {
        //Tween self to 1,1,0 over 2 seconds
        await Tween.Position.To(transform, new Vector3(1, 1, 0), 2);

        //Tween to a relative position from the current
        await Tween.Position.ToOffset(transform, new Vector3(-1, -1, 0), 2);

        //Tween from a position to the current
        await Tween.Position.From(transform, new Vector3(2, 2, 0), 2);
    }
}
```

Aside from position, there are built-in tweeners for other Transform properties (eulerAngles, localScale, etc...) and properties on other objects as well (SpriteRenderer.color, RectTransform.anchoredPosition, etc...).

## Easings
Tweeners optionally take an easing function to control the speed throughout the animation. For convenience, a number of standard curves have built-in functions. You may also write your own custom easing function. Another option is to use a Unity AnimationCurve and pass its `Evaluate` method.

```cs
public class Foo : MonoBehaviour
{
    public AnimationCurve curve;

    public async Routine Bar()
    {
        //Built-in curve function
        await Tween.Position.To(transform, new Vector3(0, 0, 0), 2, Easing.QuadInOut);

        //Custom easing function
        await Tween.Position.To(transform, new Vector3(1, 1, 0), 2, MyCustomEasing);

        //AnimationCurve
        await Tween.Position.To(transform, new Vector3(2, 2, 0), 2, curve.Evaluate);
    }

    public static float MyCustomEasing(float i)
    {
        return i * i;
    }
}
```

You can also use the `Easing` type to enable setting the function from Unity's Inspector window.
```cs
public class Foo : MonoBehaviour
{
    public Easing easing; //Allows you to select a built-in easing function or set an animation curve

    public async Routine Bar()
    {
        await Tween.Position.To(transform, new Vector3(2, 2, 0), 2, easing);
    }
}
```

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
