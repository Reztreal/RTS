# RTS Project

This is a vertical slice of a Real-Time Strategy (RTS) game developed in Unity. The project uses flow fields for pathfinding, providing efficient and dynamic movement for units across the game map.


## Demo
<iframe width="560" height="315" src="https://youtu.be/TFYM7R_-YhM" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>


## Features

- **Unit Selection**: Units can be selected individually or in groups using drag selection. When a unit is selected, a unique sound is played.
- **Unit Management**: Units have health bars and selection indicators. The health of a unit can be adjusted, and units can be selected or deselected.
- **Building Placement**: Buildings can be placed on the game map. When a building is placed, the NavMesh is updated to include the building as an obstacle.
- **Sound Management**: The game includes various ambient and character sounds. Each sound can be triggered under specific game events.
- **Event System**: The game includes an event system that allows for easy communication between game objects. The event system is used to trigger sounds and other game events.
- **Pathfinding**: A height-based flow field is implemented so the units can prefer smoother paths.



