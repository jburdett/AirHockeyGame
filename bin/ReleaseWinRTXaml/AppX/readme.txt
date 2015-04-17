1. What the application does,
The application is a virtual simulation of the arcade air hockey table.  The objective is to use your red paddle to knock the yellow 
into the AI opponent's highlighted goal area.  You must also protect your own goal from the puck.  Everytime a goal is scored, the score
increases by 1.  Once either player has reached the winning score they win the game.

The application allows for many of the game's variables to be tweaked such as: AI speed, puck speed, friction and winning scores. 
Tweaking these values can be the difference between making the game very easy or very hectic.  The tablets also have a tilt option,
allowing for the player to tilt the tablet to affect the motion of the game objects.

2. How to use it
The app only responds to touch controls although a mouse can also be used.  The human player's paddle can be moved by dragging their
finger across the screen.  The sensitivity of this control can be adjusted in the options.  The higher the sensitivity, the quicker
the paddle will move which also means the faster the player can hit the puck, at the cost of control.  Each player can only move their
puck within their half of the table.  The player can also turn on the tilt setting in the options allowing the player to "cheat" by 
effectively tilting the table to allow gravity to move the objects.  

3. How you modelled objects and entities
Models were all created using Blender using simple shapes such as cubes, cylinders and spheres.  The table is a cube streched into a 
rectangular prism, and then a smaller rectangular prism was extruded inwards.  The surface of the table is just a plane going across
the centre of the table object.  The puck was created similarily to the table except using a cylinder object instead.  The cylinder was scaled to a suitable height and
then extruded inwards to give it a little more definition.  The paddle was created using the same process as the puck except this time the extrusion was applied going outwards to create the handle
of the paddle.  A sphere was then placed on the top of the handle to give a rounded look.
Each model was then exported as Collada files and imported into the project.  Each model has its own position in the World.  The models 
are then rendered with reference to the camera that is generally centred directly above the middle of the table.

4. How you handled graphics and camera motion
The only objects that move in the app are each player's paddle, the puck and camera.  The human player's paddle moves according to touch
controls.  The app measures the distance and direction the position of the finger has moved across the screen and moves the paddle 
accordingly.  The computer's paddle is handled by the AI.  When the puck is in the human's half the AI goes into a defensive pattern
defending the goals.  When the puck enters the AI's half it first tries to move its paddle behind the puck.  It then will proceed to try 
push it into the other half.
The puck moves according to its velocity.  It's velocity is affected by collisions with the table's walls and player paddles.  It also
loses a percentage of speed at each update adding a concept of friction to the gameplay.  
The camera moves according to the accelerometer reading when the tablet is tilted.  When tilted the camera will translate along the x-y
plane according to how it has been tilted.  The camera remains pointed at the point (0,0,0) so the objects all remain along the x-y plane
at z=0 but the camera's shifted position gives the illusion this is no longer the case and instead that the world has been tipped.