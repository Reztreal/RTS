# RTS Project

This is a vertical slice of a Real-Time Strategy (RTS) game developed in Unity. The project uses flow fields for pathfinding, providing efficient and dynamic movement for units across the game map.


## Demo
[![RTS Project Demo](https://img.youtube.com/vi/TFYM7R_-YhM/0.jpg)](https://www.youtube.com/watch?v=TFYM7R_-YhM)
</iframe></iframe>

#### Scriptable objects are used extensively to make the game more designer-friendly and to allow for easy customization of game elements.


## Features

- **Unit Selection**: Units can be selected individually or in groups using drag selection. When a unit is selected, a unique sound is played.
- **Unit Management**: Units have health bars and selection indicators. The health of a unit can be adjusted, and units can be selected or deselected.
- **Building Placement**: Buildings can be placed on the game map. When a building is placed, the NavMesh is updated to include the building as an obstacle.
- **Sound Management**: The game includes various ambient and character sounds. Each sound can be triggered under specific game events.
- **Event System**: The game includes an event system that allows for easy communication between game objects. The event system is used to trigger sounds and other game events.
- **Pathfinding**: A height-based flow field is implemented so the units can prefer smoother paths.
- **Skill System**: A scriptable object-based skill system is implemented. Each skill can be assigned to a unit and triggered under specific conditions.




