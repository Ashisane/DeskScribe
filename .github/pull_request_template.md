## Feature Summary
Describe briefly what this pull request adds, improves, or fixes in DeskScribe.

Example:
> Added the first working version of the transparent overlay window with basic drawing and Alt+D toggle.

---

## Changes Made
List key updates included in this pull request.

- [ ] Implemented fullscreen transparent overlay window
- [ ] Enabled mouse-based drawing on canvas
- [ ] Added Alt + D toggle between draw and view modes
- [ ] Press Esc to close application
- [ ] Cleaned up project structure and removed temporary Whiteboard app

---

## How to Test
1. Run the project:
   ```bash
   dotnet run --project src/DeskScribe.App
   ```
2. Draw using the left mouse button.
3. Press **Alt + D** to toggle drawing mode.
4. Press **Esc** to close the app.

Expected behavior:
- The window opens fullscreen and slightly transparent.
- Drawing works when in “Drawing Mode.”
- Cursor changes to Pen or Arrow depending on mode.

---

## Screenshots / Demo (optional)
_Add screenshots or GIFs showing before/after behavior, if applicable._

---

## Notes
Include any technical notes, challenges, or ideas for next improvements.

Example:
> Next step: add clear canvas (Ctrl + C) and color picker for different pen colors.
