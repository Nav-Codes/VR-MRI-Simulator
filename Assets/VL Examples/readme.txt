The sample scene is a simplified proof of concept only, to address some questions that were raised:

1) How can I make a knob that translates the bed platform (Tarasios)
2) How to show/hide various patient position and coils based on user menu selection

NOTE:  

Run in play mode to interact with knob in Game view.  
There is just a static camera (not VR) for testing.  To see entire scene objects look at the Scene View.

The Knob rotation code looks at mouse input for proof of concept. This should be mapped to appropriate VR controller input, and scale accordingly

The sample uses build-in GUILayout API to make buttons for testing.  This should be remapped/redone for your VR input/menu.

The sample PoC is for demonstration purposes only. 