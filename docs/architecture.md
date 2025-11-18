# DeskScribe Architecture  
Version: v0.3.0  
Status: Stable  

This document describes the internal architecture of DeskScribe, a lightweight WPF desktop overlay for freehand drawing on the Windows desktop.  
It details structure, subsystems, rendering flow, file persistence, and future extensibility paths.

---

# 1. High-Level Overview

DeskScribe is composed of a single WPF overlay application designed to:

1. Render a transparent, borderless fullscreen window above the desktop.
2. Capture pointer events to draw strokes (polylines) on a canvas.
3. Support utilities such as save/load, undo, brush settings, and wallpaper integration.
4. Persist the last saved drawing path through a lightweight JSON config.

Although simple externally, the system is architected to grow into a multi-module application with stroke engines, custom persistence formats, and native graphics acceleration (Direct2D).

---

# 2. Core Components

DeskScribe consists of the following main modules:

```
DeskScribe.App/
 ├── MainWindow.xaml            # UI structure (layers, transparency)
 ├── MainWindow.xaml.cs         # Input handling, stroke logic, save/load
 ├── WallpaperInterop.cs (planned)
 ├── StrokeModels/ (future)
 └── Persistence/ (future)
```

Today, nearly all logic resides in MainWindow.xaml.cs, but the system is intentionally decoupled enough to split into modules later.

---

# 3. UI Layer Architecture

The overlay UI contains two stacked layers:

```
RootGrid (named RootGrid)
 ├── BackgroundImage (Image control)
 └── DrawCanvas (Canvas control)
```

## 3.1 Background Layer: BackgroundImage
- Type: Image
- Purpose: display the last saved PNG when loaded.
- Stretch = UniformToFill
- IsHitTestVisible = False
- Only updated when user presses Ctrl + O.

## 3.2 Drawing Layer: DrawCanvas
- Type: Canvas
- Purpose: capture and render stroke data.
- Stores strokes as Polyline objects.
- Background: #02FFFFFF (tiny alpha to maintain WPF hit-testing)
- Handles all pointer events.

---

# 4. Input Handling System

DeskScribe uses native WPF events:

## 4.1 Pointer Pipeline
- MouseDown: starts a new stroke.
- MouseMove: appends points to the stroke.
- MouseUp: ends the stroke.

## 4.2 Keyboard Shortcut Pipeline

| Shortcut | Action |
|---------|--------|
| Alt + D | Toggle draw/view mode |
| Ctrl + S | Save canvas as PNG |
| Ctrl + O | Load last saved PNG |
| Ctrl + B | Set last saved PNG as wallpaper |
| Ctrl + C | Clear strokes + background |
| Ctrl + K | Cycle brush colors |
| Ctrl + + / Ctrl + - | Adjust brush size |
| Ctrl + Z | Undo last stroke |
| Esc | Close overlay |

---

# 5. Rendering Model

## 5.1 Stroke Representation
Each stroke is a Polyline:
- StrokeThickness = _currentStrokeThickness
- Stroke = _currentBrush
- Points appended on every mouse movement.

Undo = removing the last Polyline.

## 5.2 Composite Rendering for Saving
To save the complete visible canvas:

```
RenderTargetBitmap rtb = new RenderTargetBitmap(width, height, dpi, dpi, PixelFormats.Pbgra32);
rtb.Render(RootGrid);
```

This captures:
- Background image layer
- All strokes
- Transparency

---

# 6. Persistence System

## 6.1 PNG Saving (Ctrl + S)

Saved to:
```
%USERPROFILE%/Pictures/DeskScribe/<timestamp>.png
```

## 6.2 Config Persistence

Stored at:
```
%APPDATA%/DeskScribe/config.json
```

Example:
```
{
  "lastSavedImage": "C:\path\file.png"
}
```

Used by:
- Ctrl + O
- Ctrl + B

---

# 7. Wallpaper Setting Pipeline

Windows requires BMP wallpapers.

Pipeline:
1. Read last PNG from config.
2. Convert PNG → BMP using BmpBitmapEncoder.
3. Call SystemParametersInfo with SPI_SETDESKWALLPAPER.

This ensures immediate wallpaper updates.

---

# 8. Undo System

- Each Polyline = one stroke.
- Undo removes last DrawCanvas.Children entry.
- Redo not implemented yet.

---

# 9. Transparency & Window Composition

Uses:

```
WindowStyle = None
WindowState = Maximized
AllowsTransparency = True
Background = #01FFFFFF
Topmost = True
```

Notes:
- Fully Transparent breaks hit testing in layered windows.
- Minimum alpha keeps render surface active.
- DrawCanvas must stay interactive.

---

# 10. Known Limitations

- PNG-only persistence (no vector editing)
- WPF rendering not optimal for large stroke counts
- No global hotkeys yet
- No tray mode
- Only last save tracked

---

# 11. Future Architecture (Planned)

Future modular decomposition:

```
DeskScribe/
 ├── DeskScribe.App/
 ├── DeskScribe.Core/       # Stroke engine, persistence
 ├── DeskScribe.Native/     # Direct2D accelerated renderer
 └── DeskScribe.Storage/    # PNG, vector, config formats
```

Benefits:
- Unit-testable core logic
- High-performance native rendering
- Multi-format storage
- Expandable plugin system

---

# 12. Summary

DeskScribe’s architecture is intentionally simple yet forward-compatible.  
The current codebase achieves transparent drawing, save/load, undo, and wallpaper support.  
The system is designed to scale into a modular, high-performance drawing engine in future releases.