# Cyberpunk2077-hack-helper

This program allows to quickly find a full solution for "Breach Protocol" puzzle in Cyberpunk 2077, or to state that there is no full solution.

## Usage

1. Run Cyberpunk 2077 game and Cyberpunk2077HackHelper.Overlay.exe (in any sequence)
2. Enter "Breach Protocol" minigame
3. Press F6 to show overlay
4. The shortest solution (if exists) will be displayed in the overlay.
5. Enter the displayed sequence.
6. Press F6 again to close the overlay

To close the app completely, press F8

https://user-images.githubusercontent.com/21694533/144754138-892b8fde-d932-4d61-b8e6-81496bb9f1d8.mp4

## How it works

The program:

1. Takes a shot of "Breach Protocol" screen
2. Checks the predefined layouts, which of them fits the screenshot
3. Grabs the screenshot using the applicable layout, finding the code matrix and daemon sequences
4. Finds possible combinations of all sequences
5. Attempts to build a path in the code matrix for each found sequence
6. Draws the shortest path in to the overlay window
