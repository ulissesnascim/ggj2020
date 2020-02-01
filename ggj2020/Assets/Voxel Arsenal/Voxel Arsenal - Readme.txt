----------------------------------------
VOXEL ARSENAL
----------------------------------------

1. Introduction
2. Spawning effects
3. Scaling effects
4. Upgrading to LWRP
5. FAQ / Problemsolving
6. Asset Extras
7. Contact

----------------------------------------
1. INTRODUCTION
----------------------------------------

Effects can be found in the 'Voxel Arsenal/Prefabs' folder. Here they are sorted in 3 main categories: Combat, Environment and Interactive.

----------------------------------------
2. SPAWNING EFFECTS
----------------------------------------

In some cases you can just drag&drop the effect into the scene, otherwise you can spawn them via scripting.

Small example on spawning an explosion via script:

public Vector3 effectNormal;

spawnEffect = Instantiate(spawnEffect, transform.position, Quaternion.FromToRotation(Vector3.up, effectNormal)) as GameObject;

----------------------------------------
3. SCALING
----------------------------------------

To scale an effect in the scene, you can simply use the default Scaling tool (Hotkey 'R'). You can also select the effect and set the Scale in the Hierarchy.

Please remember that some parts of the effects such as Point Lights, Trail Renderers and Audio Sources may have to be manually adjusted afterwards.

----------------------------------------
4. Upgrading to LWRP / Universal
----------------------------------------

Make sure your project is correctly set up to use LWRP or Universal Pipeline.

Locate the 'Voxel Arsenal\Upgrade' folder, then open and Import the bundled 'Voxel Arsenal LWRP' unitypackage to your project. This should overwrite the Standard Shaders, custom shaders and Materials.

You can also revert to Standard materials by opening and Importing the 'Voxel Arsenal Standard Materials' unitypackage.

NOTE: This upgrade package will overwrite Demo Scenes, Materials and Prefabs folder. If you are saving edited prefabs or content in the Voxel Arsenal folder, this content may be lost when you upgrade.

----------------------------------------
5. FAQ / Problemsolving
----------------------------------------

Q: Particles appear stretched or too thin after scaling
 
A: This means that one of the effects are using a Stretched Billboard render type. Select the prefab and locate the Renderer tab at the bottom of the Particle System. If you scaled the effect up to be twice as big, you'll also need to multiply the current Length Scale by two.

--------------------

Q: The effects look grey or darker than they're supposed to

A: https://forum.unity.com/threads/epic-toon-fx.390693/#post-3279824

----------------------------------------
6. ASSET EXTRAS
----------------------------------------

In the 'Voxel Arsenal/Scripts' folder you can find some neat scripts that may further help you customize the effects.

VoxelBeamStatic - A scripted beam effect. Uses prefabs from the 'Prefabs/Combat/Beam/Setup' folder

VoxelLightFade - This lets you fade out lights which are useful for explosions

VoxelRotation - A simple script that applies constant rotation to an object

VoxelSoundSpawn - A handy script for playing sounds which destroys itself after the sound is over

----------------------------------------
7. CONTACT
----------------------------------------

Need help with anything? 

E-Mail : archanor.work@gmail.com
Website: archanor.com

Follow me on Twitter for regular updates and news

Twitter: @Archanor
