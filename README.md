# 3D Top-Down Shooter Template

## Overview
This project is a **3D Top-Down Shooter Template**, designed to provide a foundation for creating action-packed top-down shooter games. It includes intuitive controls, robust mechanics, and modular systems to allow for easy customization and scalability.

## Features

### 1. **Twin-Stick Controller**
- Supports intuitive player movement and aiming using dual input sticks (keyboard and mouse).

### 2. **Enemy AI with Optimized Pathfinding**
- AI utilized Unity's NavMesh Agent for navigation, further optimized for improved performance.
- Behaviors include chasing, idle, and attacking the player.

### 3. **Modular Health and Damage Systems**
- Easily customizable health and damage mechanics.
- Can be applied to players, enemies, and objects.

### 4. **Modular Weapon System**
- Includes a weapon system designed for flexibility and customization.
- Add new weapons, tweak firing mechanics, and adjust attributes using **ScriptableObjects**.

### 5. **Object Pooling**
- Implements object pooling to optimize game performance by reusing objects like projectiles and enemies.

### 6. **Scriptable Objects for Data Management**
- Efficiently manage reusable data for weapons, enemies, and more using Unity’s **ScriptableObjects**.

## How to Use

### Setting Up the Project
1. Clone or download the repository.
2. Open the project in Unity (version 2021.3 LTS or higher recommended).
3. Explore the included demo scene to see all systems in action.

### Key Components
- **PlayerController**: Manages player movement and aiming.
- **EnemyAI**: Handles enemy behaviors and pathfinding.
- **Health/Damage Manager**: Applies health and damage logic to any entity.
- **Weapon**: Configures and manages weapons.
- **ObjectPool**: Oversees pooling and reuse of objects.

### Adding New Content
- **Weapons**
- Create a script that inherits from the abstract *Weapon* class and override the Fire method to define how it shoots.
- Right-click in the Project window, choose *Create* -> *ScriptableObject* -> *WeaponData*, and fill in the details like damage and fire rate.
- Attach your new weapon script to the weapon GameObject and assign the *WeaponData* ScriptableObject to it.

- **Enemies**
- Create an enemy prefab and drag it into the scene

- **Destructibles**
- Create a new script that inherits from *HealthManager* and override *OnHealthDepleted* to define what happens when health runs out.
- Attach the script on the GameObject you want to make destructible.

## Demo Scene
The demo scene includes:
- A basic level layout with test areas for testing AI behavior, weapon usage, and destructible objects.
- UI controls for understanding movement and shooting mechanics.

## Credits
Developed by: [Antazo Harvey]

---

Feel free to modify and expand this template to suit your needs. Happy game development!
