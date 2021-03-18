using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class GenerateAnimations : ScriptableWizard
{
    public int CharacterNumber;
    public Sprite[] SpritesDown;
    public Sprite[] SpritesLeft;
    public Sprite[] SpritesRight;
    public Sprite[] SpritesUp;


    [MenuItem("CustomTools/Generate Animations")]
    static void CreateWizard()
    {
        ScriptableWizard.DisplayWizard<GenerateAnimations>("Generate all the character animations", "Generate");
    }

    private void OnWizardCreate()
    {
        AssetDatabase.CreateFolder("Assets/Art/Animations", $"Character{CharacterNumber}");

        GenerateWalk(SpritesRight, "Right");
        GenerateWalk(SpritesLeft, "Left");
        GenerateWalk(SpritesUp, "Up");
        GenerateWalk(SpritesDown, "Down");
        GenerateIdle(SpritesRight, "Right");
        GenerateIdle(SpritesLeft, "Left");
        GenerateIdle(SpritesUp, "Up");
        GenerateIdle(SpritesDown, "Down");

        GenerateController();
    }

    private void GenerateWalk(Sprite[] sprites, string Direction)
    {
        var newClip = new AnimationClip();
        newClip.frameRate = 6;

        EditorCurveBinding binding = new EditorCurveBinding();
        binding.type = typeof(SpriteRenderer);
        binding.path = "";
        binding.propertyName = "m_Sprite";

        List<ObjectReferenceKeyframe> keyFrames = new List<ObjectReferenceKeyframe>();

        int spriteCount = 0;
        for (int i = 0; i < sprites.Length; i++)
        {
            ObjectReferenceKeyframe newFrame = new ObjectReferenceKeyframe();
            newFrame.time = spriteCount / newClip.frameRate;
            spriteCount++;
            newFrame.value = sprites[i];
            keyFrames.Add(newFrame);
        }

        ObjectReferenceKeyframe lastFrame = new ObjectReferenceKeyframe();
        lastFrame.time = spriteCount / newClip.frameRate;
        lastFrame.value = sprites[1];
        keyFrames.Add(lastFrame);

        AnimationUtility.SetObjectReferenceCurve(newClip, binding, keyFrames.ToArray());

        AnimationClipSettings clipSettings = new AnimationClipSettings();
        clipSettings.loopTime = true;
        clipSettings.keepOriginalPositionY = true;
        clipSettings.startTime = 0f;
        clipSettings.stopTime = 0.6666667f;


        AnimationUtility.SetAnimationClipSettings(newClip, clipSettings);

        AssetDatabase.CreateAsset(newClip, $"Assets/Art/Animations/Character{CharacterNumber}/CharacterWalk{Direction}{CharacterNumber}.anim");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    private void GenerateIdle(Sprite[] sprites, string Direction)
    {
        var newClip = new AnimationClip();
        newClip.frameRate = 6;

        EditorCurveBinding binding = new EditorCurveBinding();
        binding.type = typeof(SpriteRenderer);
        binding.path = "";
        binding.propertyName = "m_Sprite";

        List<ObjectReferenceKeyframe> keyFrames = new List<ObjectReferenceKeyframe>();

        ObjectReferenceKeyframe lastFrame = new ObjectReferenceKeyframe();
        lastFrame.time = 0f;
        lastFrame.value = sprites[1];
        keyFrames.Add(lastFrame);

        AnimationUtility.SetObjectReferenceCurve(newClip, binding, keyFrames.ToArray());

        AnimationClipSettings clipSettings = new AnimationClipSettings();
        clipSettings.loopTime = true;
        clipSettings.keepOriginalPositionY = true;
        clipSettings.startTime = 0f;
        clipSettings.stopTime = 0.01666667f;

        AnimationUtility.SetAnimationClipSettings(newClip, clipSettings);

        AssetDatabase.CreateAsset(newClip, $"Assets/Art/Animations/Character{CharacterNumber}/CharacterIdle{Direction}{CharacterNumber}.anim");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    private void GenerateController()
    {
        AnimatorOverrideController controller = new AnimatorOverrideController(AssetDatabase.LoadAssetAtPath<RuntimeAnimatorController>("Assets/Art/Animations/Character1.controller"));
        controller.name = $"Charater{CharacterNumber}";

        AnimationClipOverrides clipOverrides = new AnimationClipOverrides(controller.overridesCount);
        controller.GetOverrides(clipOverrides);

        clipOverrides["CharacterIdleDown1"] = AssetDatabase.LoadAssetAtPath<AnimationClip>($"Assets/Art/Animations/Character{CharacterNumber}/CharacterIdleDown{CharacterNumber}.anim");
        clipOverrides["CharacterIdleLeft1"] = AssetDatabase.LoadAssetAtPath<AnimationClip>($"Assets/Art/Animations/Character{CharacterNumber}/CharacterIdleLeft{CharacterNumber}.anim");
        clipOverrides["CharacterIdleRight1"] = AssetDatabase.LoadAssetAtPath<AnimationClip>($"Assets/Art/Animations/Character{CharacterNumber}/CharacterIdleRight{CharacterNumber}.anim");
        clipOverrides["CharacterIdleUp1"] = AssetDatabase.LoadAssetAtPath<AnimationClip>($"Assets/Art/Animations/Character{CharacterNumber}/CharacterIdleUp{CharacterNumber}.anim");
        clipOverrides["CharacterWalkDown1"] = AssetDatabase.LoadAssetAtPath<AnimationClip>($"Assets/Art/Animations/Character{CharacterNumber}/CharacterWalkDown{CharacterNumber}.anim");
        clipOverrides["CharacterWalkLeft1"] = AssetDatabase.LoadAssetAtPath<AnimationClip>($"Assets/Art/Animations/Character{CharacterNumber}/CharacterWalkLeft{CharacterNumber}.anim");
        clipOverrides["CharacterWalkRight1"] = AssetDatabase.LoadAssetAtPath<AnimationClip>($"Assets/Art/Animations/Character{CharacterNumber}/CharacterWalkRight{CharacterNumber}.anim");
        clipOverrides["CharacterWalkUp1"] = AssetDatabase.LoadAssetAtPath<AnimationClip>($"Assets/Art/Animations/Character{CharacterNumber}/CharacterWalkUp{CharacterNumber}.anim");

        controller.ApplyOverrides(clipOverrides);

        AssetDatabase.CreateAsset(controller, $"Assets/Art/Animations/Character{CharacterNumber}.overrideController");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}

public class AnimationClipOverrides : List<KeyValuePair<AnimationClip, AnimationClip>>
{
    public AnimationClipOverrides(int capacity) : base(capacity) { }

    public AnimationClip this[string name]
    {
        get { return this.Find(x => x.Key.name.Equals(name)).Value; }
        set
        {
            int index = this.FindIndex(x => x.Key.name.Equals(name));
            if (index != -1)
                this[index] = new KeyValuePair<AnimationClip, AnimationClip>(this[index].Key, value);
        }
    }
}
